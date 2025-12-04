using Minis;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles midi keyboard using Minis plugin. Detects notes played on midi keyboard, maps it to frequencies and appplies it to an oscillator. Based on Minis example code.
/// </summary>
public class MidiMultivoiceController : MonoBehaviour
{
    [SerializeField]
    private float detune=0.01f;

    [SerializeField]
    private Vector2Int range;

    [SerializeField]
    private List<Oscillator> oscillators;

    private List<int> currentNotes;
    private int notesPressed;

    private void Awake()
    {
        currentNotes = new List<int>();
        foreach (var oscillator in oscillators)
        {
            currentNotes.Add(-1);
        }
    }

    private void Start()
    {
        InputSystem.onDeviceChange += (device, change) =>
        {
            if (change != InputDeviceChange.Added) return;

            MidiDevice midiDevice = device as MidiDevice;
            if (midiDevice == null) return;

            midiDevice.onWillNoteOn += (note, velocity) =>
            {
                // Note that you can't use note.velocity because the state
                // hasn't been updated yet (as this is "will" event). The note
                // object is only useful to specify the target note (note
                // number, channel number, device name, etc.) Use the velocity
                // argument as an input note velocity.
                Debug.Log(string.Format(
                    "Note On #{0} ({1}) vel:{2:0.00} ch:{3} dev:'{4}'",
                    note.noteNumber,
                    note.shortDisplayName,
                    velocity,
                    (note.device as MidiDevice)?.channel,
                    note.device.description.product
                ));

                if (note.noteNumber > range.x && note.noteNumber < range.y)
                {
                    for (int i = notesPressed; i < oscillators.Count; i++)
                    {
                        oscillators[i].frequency = CalculateFrequency(note.noteNumber) * (1 + i * detune);
                        oscillators[i].amp = 0.8f;
                        currentNotes[i] = note.noteNumber;
                    }
                    notesPressed++;
                }
            };

            midiDevice.onWillNoteOff += (note) =>
            {
                Debug.Log(string.Format(
                    "Note Off #{0} ({1}) ch:{2} dev:'{3}'",
                    note.noteNumber,
                    note.shortDisplayName,
                    (note.device as MidiDevice)?.channel,
                    note.device.description.product
                ));

                if (note.noteNumber > range.x && note.noteNumber < range.y)
                {
                    for (int i = 0; i < oscillators.Count; i++)
                    {
                        if (note.noteNumber == currentNotes[i])
                        {
                            if (notesPressed > 1 && i > 1)
                            {
                                oscillators[i].frequency = oscillators[i - 1].frequency *(1+i*detune);
                                oscillators[i].amp = 0.8f;
                                currentNotes[i] = currentNotes[i - 1];
                            }
                            else
                            {
                                oscillators[i].frequency = 1;
                                oscillators[i].amp = 0;
                                currentNotes[i] = -1;
                            }
                        }
                    }
                    notesPressed--;
                }
            };
        };
    }

    private float CalculateFrequency(float noteNumber)
    {
        float root = Mathf.Pow(2, 1 / 12f);
        float firstNoteFrequency = 440f / Mathf.Pow(2, 6);
        return firstNoteFrequency * Mathf.Pow(root, noteNumber);
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Composite oscillator spawns desired number of oscillator instances and alter it properties slightly (detune, change panorama position) to create more spatial sound.
/// </summary>
public class CompositeOscillator : MonoBehaviour
{
    public float frequency = 220f;
    [Range(0, 1)]
    public float amp = 0.5f;
    [Range(-1, 1)]
    public float pan = 0f;
  
    public bool isPlaying = true;

    [SerializeField]
    public int instances = 1;
    [SerializeField]
    public float frequencySpread;
    [SerializeField]
    public float panSpread;
    [SerializeField]
    public float volumeSpread;
    [SerializeField]
    private float sustain;
    [SerializeField]
    private Oscillator oscillatorPrefab;

    private List<Oscillator> oscillatorInstances = new List<Oscillator>();
    private float currentAmp;

    private void Update()
    {
        Recalculate();
    }

    private void Recalculate()
    {
        if (isPlaying)
        {
            currentAmp = amp;
        }
        else
        {
            currentAmp *= sustain;
        }

        for (int i = 0; i < instances || i < oscillatorInstances.Count; i++)
        {
            if (i < instances)
            {
                if (i >= oscillatorInstances.Count)
                {
                    Oscillator newOscillator = Instantiate(oscillatorPrefab, transform);
                    oscillatorInstances.Add(newOscillator);
                }
                if (i < oscillatorInstances.Count)
                {
                    oscillatorInstances[i].gameObject.SetActive(true);
                    oscillatorInstances[i].amp = currentAmp * Mathf.Pow(volumeSpread, IndexToParamSymmetric(i));
                    oscillatorInstances[i].frequency = frequency + frequencySpread * IndexToParam(i);
                    oscillatorInstances[i].pan = pan + panSpread * IndexToParam(i);
                }
            }
            else
            {
                if (i < oscillatorInstances.Count)
                {
                    oscillatorInstances[i].gameObject.SetActive(false);
                }
            }
        }
    }

    private float IndexToParam(int i)
    {
        float ceil = Mathf.Ceil((float)i / 2);
        if (ceil == (float)i / 2)
        {
            return ceil;
        }
        else
        {
            return -ceil;
        }
    }

    private float IndexToParamSymmetric(int i)
    {
        return Mathf.Ceil((float)i / 2);
    }
}
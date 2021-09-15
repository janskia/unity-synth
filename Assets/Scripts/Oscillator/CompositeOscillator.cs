using System;
using System.Collections.Generic;
using UnityEngine;

public class CompositeOscillator : MonoBehaviour
{
    public float frequency = 220f;
    public float amp = 0.5F;
    public float pan = 0f;

    [SerializeField]
    public int instances = 1;
    [SerializeField]
    public float frequencySpread;
    [SerializeField]
    public float panSpread;
    [SerializeField]
    public float volumeSpread;
    [SerializeField]
    private Oscillator oscillatorPrefab;

    private List<Oscillator> oscillatorInstances = new List<Oscillator>();

    private void Update()
    {
        Recalculate();
    }

    private void Recalculate()
    {
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
                    oscillatorInstances[i].amp = amp * Mathf.Pow(volumeSpread, IndexToParamSymmetric(i));
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
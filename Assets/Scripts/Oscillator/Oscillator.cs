using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Oscillator : MonoBehaviour
{
    public float frequency = 220f;
    [Range(0, 1)]
    public float amp = 0.5f;
    [Range(-1, 1)]
    public float pan = 0f;

    private float phase = 0.0f;
    private double sampleRate = 0.0f;
    private bool running = false;

    [SerializeField]
    private Sampler sampler;

    void Start()
    {
        sampleRate = AudioSettings.outputSampleRate;
        sampler = GetComponent<Sampler>();
        running = true;
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        if (!running)
            return;

        double sample = AudioSettings.dspTime * sampleRate;
        int dataLength = data.Length / channels;

        for (int n = 0; n < dataLength; n++)
        {
            float x = amp * EvaluateWaveshape(phase);

            for (int i = 0; i < channels; i++)
            {
                float panAmp = 1f;
                if (i == 0)
                {
                    panAmp = Mathf.Clamp01((-pan + 1) / 2);
                }
                else if (i == 1)
                {
                    panAmp = Mathf.Clamp01((pan + 1) / 2);
                }
                data[n * channels + i] += x * panAmp;
            }

            phase += (float)(frequency / sampleRate * 2 * Mathf.PI);
            if (phase > 2 * Mathf.PI)
            {
                phase -= 2 * Mathf.PI;
            }
        }
    }

    private float EvaluateWaveshape(float phase)
    {
        return sampler.Evaluate(phase / 2 / Mathf.PI);
    }
}
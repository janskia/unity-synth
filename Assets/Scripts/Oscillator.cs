using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Oscillator : MonoBehaviour
{
    public float frequency = 220f;
    public float amp = 0.5F;

    private float phase = 0.0F;
    private double sampleRate = 0.0F;
    private bool running = false;

    [SerializeField]
    private Sampler sampler;

    void Start()
    {
        sampleRate = AudioSettings.outputSampleRate;
        sampler = GetComponent<Sampler>();
        running = true;
    }

    void OnAudioFilterRead(float[] data, int channels)
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
                data[n * channels + i] += x;
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
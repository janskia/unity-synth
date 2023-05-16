using System;
using UnityEngine;

/// <summary>
/// Simple saturation audio effect. Takes input sample and maps it's value to an output using curve. X on the curve is input value and Y output value. Curve should have values from (-1,-1) to (1,1).
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class Saturation : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve curve;
    [SerializeField]
    [Range(0, 1)]
    private float amount = 1f;

    private float[] samples = new float[256];
    private bool running;

    private void Start()
    {
        running = true;
    }

    protected void Update()
    {
        for (int i = 0; i < samples.Length; i++)
        {
            float val = curve.Evaluate((float)i / samples.Length * 2 - 1);
            samples[i] = Mathf.Clamp(val, -1f, 1f);
        }
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        if (!running)
            return;

        int dataLength = data.Length / channels;

        for (int n = 0; n < dataLength; n++)
        {
            for (int i = 0; i < channels; i++)
            {
                Evaluate((data[n * channels + i] + 1) / 2, samples.Length, out int sampleIndex, out float rest);
                float saturatedValue = Mathf.Lerp(samples[sampleIndex], samples[sampleIndex + 1], rest);
                data[n * channels + i] = Mathf.Lerp(data[n * channels + i], saturatedValue, amount);
            }
        }
    }

    private void Evaluate(float value, int bufferLength, out int sampleIndex, out float rest)
    {
        float scaledPhase = value * bufferLength % bufferLength;
        sampleIndex = Mathf.FloorToInt(scaledPhase);
        rest = scaledPhase - sampleIndex;
    }
}

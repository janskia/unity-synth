using System;
using UnityEngine;

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

    protected void Update()
    {
        for (int i = 0; i < samples.Length; i++)
        {
            float val = curve.Evaluate((float)i / samples.Length * 2 - 1);
            samples[i] = Mathf.Clamp(val, -1f, 1f);
        }
    }

    private void Start()
    {
        running = true;
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
                AudioHelper.Evaluate((data[n * channels + i] + 1) / 2, samples.Length, out int sampleIndex, out float rest);
                float saturatedValue = Mathf.Lerp(samples[sampleIndex], samples[sampleIndex + 1], rest);
                data[n * channels + i] = Mathf.Lerp(data[n * channels + i], saturatedValue, amount);
            }
        }
    }
}
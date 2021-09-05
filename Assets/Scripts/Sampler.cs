using UnityEngine;

public class Sampler : MonoBehaviour
{
    public int samplesArraySize = 256;
    public AnimationCurve curve;

    private float[] samples;

    private void Awake()
    {
        samples = new float[samplesArraySize];
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < samples.Length; i++)
        {
            //  samples[i] = Mathf.Sin((float)i / samples.Length * 2 * Mathf.PI);
            samples[i] = curve.Evaluate((float)i / samples.Length);
        }
    }

    public float EvaluateWaveshape(float phase)
    {
        float scaledPhase = phase / 2 / Mathf.PI * samples.Length % samples.Length;
        int sampleIndex = Mathf.FloorToInt(scaledPhase);
        float rest = scaledPhase - sampleIndex;
        return samples[sampleIndex] * (1 - rest) + samples[(sampleIndex + 1) % samples.Length] * rest;
    }
}
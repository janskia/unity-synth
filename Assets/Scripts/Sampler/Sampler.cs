using UnityEngine;

/// <summary>
/// Sampler class provides an <see cref="Evaluate"/> method for reading waveform value at given phase. How waveform is generated depends on its implementation.
/// </summary>
public abstract class Sampler : MonoBehaviour
{
    public int samplesArraySize = 256;

    protected float[] samples;

    private void Awake()
    {
        samples = new float[samplesArraySize];
    }
    
    /// <param name="phase">Value in range (0:1)</param>
    /// <returns>Value in range (-1:1)</returns>
    public float Evaluate(float phase)
    {
        float scaledPhase = phase * samples.Length % samples.Length;
        int sampleIndex = Mathf.FloorToInt(scaledPhase);
        float rest = scaledPhase - sampleIndex;
        return samples[sampleIndex] * (1 - rest) + samples[(sampleIndex + 1) % samples.Length] * rest;
    }
}
using UnityEngine;

/// <summary>
/// Wraps 4 different samplers and blend waveforms between them. It's possible to randomize blend values to create wobbly sound.
/// </summary>
public class CompositeSampler : Sampler
{
    public WavePreview wavePreview;

    public Sampler sampler1;
    public Sampler sampler2;
    public Sampler sampler3;
    public Sampler sampler4;

    public bool randomize;
    public float randomizationSpeed = 0.05f;

    [Range(0, 1)]
    public float blend1;
    [Range(0, 1)]
    public float blend2;

    private void Update()
    {
        if (randomize)
        {
            blend1 = Mathf.Clamp01(blend1 + Random.Range(-randomizationSpeed, randomizationSpeed));
            blend2 = Mathf.Clamp01(blend2 + Random.Range(-randomizationSpeed, randomizationSpeed));
        }
        for (int i = 0; i < samples.Length; i++)
        {
            float val = (sampler1.Evaluate((float)i / samples.Length) * blend1 + sampler2.Evaluate((float)i / samples.Length) * (1 - blend1)
                      + sampler3.Evaluate((float)i / samples.Length) * blend2 + sampler4.Evaluate((float)i / samples.Length) * (1 - blend2))
                      / 2;
            samples[i] = Mathf.Clamp(val, -1f, 1f);
        }
        wavePreview.ShowValues(samples);
    }
}
using UnityEngine;

public class CurveSampler : MonoBehaviour
{
    public WavePreview wavePreview;
    public int samplesArraySize = 256;
    public AnimationCurve curve1;
    public AnimationCurve curve2;
    public AnimationCurve curve3;
    public AnimationCurve curve4;
    public bool randomize;
    public float randomizationSpeed=0.05f;

    [Range(0, 1)]
    public float blend1;
    [Range(0, 1)]
    public float blend2;

    private float[] samples;

    private void Awake()
    {
        samples = new float[samplesArraySize];
    }

    private void FixedUpdate()
    {
        //blend1 = Mathf.Clamp01(Input.mousePosition.x / 1000f);
        //blend2 = Mathf.Clamp01(Input.mousePosition.y / 1000f);
        if (randomize)
        {
            blend1 = Mathf.Clamp01(blend1 + Random.Range(-randomizationSpeed, randomizationSpeed));
            blend2 = Mathf.Clamp01(blend2 + Random.Range(-randomizationSpeed, randomizationSpeed));
        }
        for (int i = 0; i < samples.Length; i++)
        {
            float val = (curve1.Evaluate((float)i / samples.Length) * blend1 + curve2.Evaluate((float)i / samples.Length) * (1 - blend1)
                      + curve3.Evaluate((float)i / samples.Length) * blend2 + curve4.Evaluate((float)i / samples.Length) * (1 - blend2))/2;
            samples[i] = Mathf.Clamp(val, -1f, 1f);
        }
        wavePreview.ShowValues(samples);
    }

    public float EvaluateWaveshape(float phase)
    {
        float scaledPhase = phase / 2 / Mathf.PI * samples.Length % samples.Length;
        int sampleIndex = Mathf.FloorToInt(scaledPhase);
        float rest = scaledPhase - sampleIndex;
        return samples[sampleIndex] * (1 - rest) + samples[(sampleIndex + 1) % samples.Length] * rest;
    }
}
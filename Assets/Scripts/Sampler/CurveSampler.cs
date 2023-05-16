using UnityEngine;

/// <summary>
/// Generates waveform stored as animation curve. Easy editable from the editor.
/// </summary>
public class CurveSampler : Sampler
{
    public AnimationCurve curve;

    protected void Start()
    {
        for (int i = 0; i < samples.Length; i++)
        {
            float val = curve.Evaluate((float)i / samples.Length);
            samples[i] = Mathf.Clamp(val, -1f, 1f);
        }
    }
}
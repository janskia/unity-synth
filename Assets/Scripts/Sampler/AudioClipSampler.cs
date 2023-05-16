using UnityEngine;

/// <summary>
///  Generates waveform stored as an audio clip. Waveform should be prepared beforehand in external audio editor.
/// </summary>
public class AudioClipSampler : Sampler
{
    public AudioClip clip;

    protected void Start()
    {
        float[] data = new float[clip.samples * clip.channels];
        clip.GetData(data, 0);
        for (int i = 0; i < samples.Length; i++)
        {
            float pos = (float)i / samples.Length * clip.samples;

            float val = Mathf.Lerp(data[(int)pos * clip.channels], data[((int)pos + 1) * clip.channels], pos - Mathf.Floor(pos));
            samples[i] = Mathf.Clamp(val, -1f, 1f);
        }
    }
}
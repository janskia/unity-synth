using UnityEngine;

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
using UnityEngine;

/// <summary>
/// Generates waveform sampled from texture;
/// </summary>
public class TextureSampler : Sampler
{
    public RenderTexture renderTexture;
    public Texture2D texture;
    public WavePreview preview;
    public float r;

    protected void Start()
    {
        texture = new Texture2D(512, 512);
    }

    private void Update()
    {
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0, 0, 512, 512), 0, 0);
        texture.Apply();
        for (int i = 0; i < samples.Length; i++)
        {
            var val = texture.GetPixelBilinear(0.5f + Mathf.Cos((float)i / samples.Length * 2 * Mathf.PI) * r, 0.5f + Mathf.Sin((float)i / samples.Length * 2 * Mathf.PI) * r).r;
            samples[i] = Mathf.Clamp(val, -1f, 1f);
        }
        preview.ShowValues(samples);
    }
}
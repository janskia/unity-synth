using UnityEngine;

/// <summary>
/// Generates waveform sampled from texture;
/// </summary>
public class LineTextureSampler : Sampler
{
    public RenderTexture renderTexture;
    public Texture2D texture;
    public WavePreview preview;
    public float scaleX = 1;
    public float offsetX;
    public float offsetY;

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
            var val = texture.GetPixelBilinear((float)i / samples.Length * scaleX - scaleX / 2 + offsetX + 0.5f, 0.5f + offsetY).r;
            samples[i] = Mathf.Clamp(val, -1f, 1f);
        }
        preview.ShowValues(samples);
    }
}

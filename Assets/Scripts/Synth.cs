using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Synth : MonoBehaviour
{
    public float frequency = 220f;
    public float amp = 0.5F;

    private float phase = 0.0F;
    private double sampleRate = 0.0F;
    private bool running = false;

    void Start()
    {
        sampleRate = AudioSettings.outputSampleRate;
        running = true;
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        if (!running)
            return;

        double sample = AudioSettings.dspTime * sampleRate;
        int dataLen = data.Length / channels;

        for (int n = 0; n < dataLen; n++)
        {
            float x = amp * Mathf.Sin(phase);

            for (int i = 0; i < channels; i++)
            {
                data[n * channels + i] += x;
            }

            phase += (float)(frequency/sampleRate * 2 * Mathf.PI);
        }
    }
}
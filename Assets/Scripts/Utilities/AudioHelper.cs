using UnityEngine;

public static class AudioHelper
{
    public static void Evaluate(float x, int length, out int sampleIndex, out float rest)
    {
        float scaledPhase = x * length % length;
        sampleIndex = Mathf.FloorToInt(scaledPhase);
        rest = scaledPhase - sampleIndex;
    }
}
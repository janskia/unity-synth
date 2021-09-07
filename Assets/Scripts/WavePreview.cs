using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePreview : MonoBehaviour
{
    [SerializeField]
    private float width = 1f;
    [SerializeField]
    private float height = 1f;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        ShowValues(new float[]
        {
            0,1,0.2f,-0.5f
        });
    }

    public void ShowValues(float[] values)
    {
        float sampleWidth = width / values.Length;
        float sampleHeight = height;
        lineRenderer.positionCount = values.Length;
        for (int i = 0; i < values.Length; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(sampleWidth * i, sampleHeight * values[i], 0));
        }
    }
}

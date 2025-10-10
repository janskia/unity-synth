using UnityEngine;

public class RadialSamplingPreview : MonoBehaviour
{
    [SerializeField]
    private int positionCount = 128;
    [SerializeField]
    private Camera renderTextureCamera;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void Set(float r, float scaleX, float scaleY, float offsetX, float offsetY)
    {
        lineRenderer.positionCount = positionCount;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            float x = renderTextureCamera.transform.position.x + renderTextureCamera.transform.lossyScale.x * renderTextureCamera.orthographicSize * 2 * (r * Mathf.Sin((float)i / (lineRenderer.positionCount - 1) * 2 * Mathf.PI) * scaleX + offsetX);
            float y = renderTextureCamera.transform.position.y + renderTextureCamera.transform.lossyScale.y * renderTextureCamera.orthographicSize * 2 * (r * Mathf.Cos((float)i / (lineRenderer.positionCount - 1) * 2 * Mathf.PI) * scaleY + offsetY);
            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
        }
    }
}

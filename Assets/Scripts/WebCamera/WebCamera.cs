using UnityEngine;

public class WebCamera : MonoBehaviour
{

    private void Start()
    {
        InitializeCamera();
    }

    private void InitializeCamera()
    {
        WebCamTexture webcamTexture = new WebCamTexture();
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = webcamTexture;
        webcamTexture.Play();
        Debug.Log(webcamTexture.deviceName);
    }
}

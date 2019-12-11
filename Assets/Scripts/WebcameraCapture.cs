using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebcameraCapture : MonoBehaviour
{
    public int Width = 1920;
    public int Height = 1080;
    public int FPS = 30;
    public int Device = 0;

    // Start is called before the first frame update
    void Start()
    {
        var devices = WebCamTexture.devices;
        if (devices.Length == 0)
        {
            Debug.LogWarning("No camera devices found");
            return;
        }

        if (devices.Length < Device)
        {
            Debug.LogError($"Cannot attach device: {Device}");
        }

        var cameraDevice = devices[Device];
        Debug.Log($"Use device: {cameraDevice.name}");

        var webcamTexture = new WebCamTexture(cameraDevice.name);
        GetComponent<Renderer>().material.mainTexture = webcamTexture;

        webcamTexture.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

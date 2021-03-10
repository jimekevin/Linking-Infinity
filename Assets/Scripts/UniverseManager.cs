using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniverseManager : MonoBehaviour
{
    public Camera[] portalCameras;
    public Material[] portalCameraMats;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < portalCameras.Length; ++i)
        {
            var portalCamera = portalCameras[i];
            var portalCameraMat = portalCameraMats[i];

            if (portalCamera.targetTexture != null)
            {
                portalCamera.targetTexture.Release();
            }
            portalCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 128);
            portalCameraMat.mainTexture = portalCamera.targetTexture;
        }
    }

    void Update()
    {
        // We have to apply camera mat again after start for some reason
        for (int i = 0; i < portalCameras.Length; ++i)
        {
            var portalCamera = portalCameras[i];
            var portalCameraMat = portalCameraMats[i];
            if (portalCameraMat.mainTexture == null)
            {
                portalCameraMat.mainTexture = portalCamera.targetTexture;
            }
        }
    }
}

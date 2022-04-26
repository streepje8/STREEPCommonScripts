// ----------------------------------------------------------------------------
// Texture to camera
//
// Author: streep
// Date:   27/03/2022
// ----------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class TextureToCamera : MonoBehaviour
{
    public RenderTexture normalCamera;
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //Graphics.Blit(source, normalCamera);
        Graphics.Blit(normalCamera, destination);
    }
}

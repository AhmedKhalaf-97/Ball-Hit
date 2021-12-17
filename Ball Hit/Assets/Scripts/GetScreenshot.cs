using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach this script to a Camera
//Also attach a GameObject that has a Renderer (e.g. a cube) in the Display field

public class GetScreenshot : MonoBehaviour
{
    public Renderer m_Display;

    bool grab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            grab = true;
        }
    }

    private void OnPostRender()
    {
        if (grab)
        {
            Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
            texture.Apply();

            m_Display.material.mainTexture = texture;

            grab = false;
        }
    }
}

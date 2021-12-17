using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadPixels : MonoBehaviour
{
    bool grab;

    public Renderer m_Display;

    public int blackPixelCount;
    public int brightPixelCount;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
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

            GetTextureColor(texture);

            grab = false;
        }
    }

    void GetTextureColor(Texture texture)
    {
        Texture2D texture2D = texture as Texture2D;

        Color32[] colors = texture2D.GetPixels32();

        //print(colors.Length);
        blackPixelCount = 0;
        brightPixelCount = 0;
        for (int i = 0; i < colors.Length; i++)
        {
            if (colors[i].r < 50 && colors[i].g < 50 && colors[i].b < 50)
            {
                blackPixelCount++;
            }
            else
            {
                brightPixelCount++;
            }
            //print(colors[i]);
        }

        print("Black Pixel Percentage is " + (blackPixelCount * 100) / colors.Length + " %");
        print("Bright Pixel Percentage is " + (brightPixelCount * 100) / colors.Length + " %");
    }
}

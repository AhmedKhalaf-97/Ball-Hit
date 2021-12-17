using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBlackToWhite : MonoBehaviour
{

    public Material mat;
    public Texture2D texture2D;
    public Color colors;

    public int blackPixelCount;
    public int otherPixelCount;

    public Renderer rend;
    public Material mat1;
    public Material mat2;
    public float duration = 2f;

    int x;
    int y;

    void Start()
    {
        texture2D = (Texture2D)mat.mainTexture;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
        }
            ChangeMaterial();
    }

    void ChangeMaterial()
    {
        float lerp = Mathf.PingPong(Time.time, duration) / duration;
        rend.material.Lerp(mat1, mat2, lerp);
    }

    void ChangeColor()
    {
        for (int y = 0; y < texture2D.height; y++)
        {
            for (int x = 0; x < texture2D.width; x++)
            {
                if(texture2D.GetPixel(x,y) == Color.black)
                {
                    texture2D.SetPixel(x, y, Color.white);
                }
                else
                {
                    texture2D.SetPixel(x, y, Color.red);
                }
            }
        }

        texture2D.Apply();

        //blackPixelCount = 0;
        //otherPixelCount = 0;

        //colors = texture2D.GetPixels();

        //for (int i = 0; i < colors.Length; i++)
        //{
        //    x++;
        //    y++;

        //    if(texture2D.GetPixel(x,y) == Color.black)
        //    {
        //        blackPixelCount++;
        //        texture2D.SetPixel(x, y, Color.white);
        //    }
        //    else
        //    {
        //        otherPixelCount++;
        //    }
        //}
    }
}

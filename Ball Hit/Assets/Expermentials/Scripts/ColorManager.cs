using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [Header("Color Stuff")]
    public Material[] materials;
    public Color[] colors;
    public float speedFactor;
    public float timeToChange = 30f;

    Color _color;
    int colorIndex = 0;

    void Start()
    {
        InvokeRepeating("ChangeColorOverTime", timeToChange, timeToChange);
    }

    void ChangeColorOverTime()
    {
        colorIndex = Random.Range(0, colors.Length);

        foreach(Material _material in materials)
        {
            ChangeColorOfMaterial(_material);
        }
    }

    void ChangeColorOfMaterial(Material _material)
    {
        StartCoroutine(ChangeColorR(_material));
        StartCoroutine(ChangeColorG(_material));
        StartCoroutine(ChangeColorB(_material));
    }

    IEnumerator ChangeColorR(Material _material)
    {
        if (_material.color.r < colors[colorIndex].r)
        {
            while (_material.color.r <= colors[colorIndex].r)
            {
                _color.r += Time.deltaTime * speedFactor;
                _material.color = _color;
                yield return null;
            }
            _color.r = colors[colorIndex].r;
            _material.color = _color;
        }
        else
        {
            while (_material.color.r >= colors[colorIndex].r)
            {
                _color.r -= Time.deltaTime * speedFactor;
                _material.color = _color;
                yield return null;
            }
            _color.r = colors[colorIndex].r;
            _material.color = _color;
        }

    }

    IEnumerator ChangeColorG(Material _material)
    {
        if (_material.color.g < colors[colorIndex].g)
        {
            while (_material.color.g <= colors[colorIndex].g)
            {
                _color.g += Time.deltaTime * speedFactor;
                _material.color = _color;
                yield return null;
            }
            _color.g = colors[colorIndex].g;
            _material.color = _color;
        }
        else
        {
            while (_material.color.g >= colors[colorIndex].g)
            {
                _color.g -= Time.deltaTime * speedFactor;
                _material.color = _color;
                yield return null;
            }
            _color.g = colors[colorIndex].g;
            _material.color = _color;
        }

    }

    IEnumerator ChangeColorB(Material _material)
    {
        if (_material.color.b < colors[colorIndex].b)
        {
            while (_material.color.b <= colors[colorIndex].b)
            {
                _color.b += Time.deltaTime * speedFactor;
                _material.color = _color;
                yield return null;
            }
            _color.b = colors[colorIndex].b;
            _material.color = _color;
        }
        else
        {
            while (_material.color.b >= colors[colorIndex].b)
            {
                _color.b -= Time.deltaTime * speedFactor;
                _material.color = _color;
                yield return null;
            }
            _color.b = colors[colorIndex].b;
            _material.color = _color;
        }

    }
}
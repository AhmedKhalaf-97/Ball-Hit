using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Numbers : MonoBehaviour
{
    public float lerpSpeed;
    public float duration = 3f;

    public Slider slider;

    public int minValue;
    public int maxValue;
    public int wantedValue;
    public int currentValue;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ChangeSliderValue(minValue, maxValue, wantedValue, currentValue));
        }
    }

    IEnumerator ChangeSliderValue(int minValue, int maxValue, int wantedValue, int current)
    {
        slider.minValue = minValue;
        slider.maxValue = maxValue;
        slider.value = currentValue;

        while (true)
        {
            lerpSpeed += Time.deltaTime / duration;
            slider.value = Mathf.Lerp(currentValue, wantedValue, lerpSpeed);

            if(wantedValue == slider.value)
            {
                lerpSpeed = 0;
                yield break;
            }
            yield return null;
        }
    }
}

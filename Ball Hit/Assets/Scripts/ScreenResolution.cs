using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;

public class ScreenResolution : MonoBehaviour
{

    public Text _text;
    public Text sliderValueText;
    public Text dpiText;

    public Slider _slider;

    public PostProcessingBehaviour ppb;
    public BloomEffect bloom;

    public Text effectText;

    public Dropdown dropdown;


    void OnEnable()
    {
        if (DataSaveManager.IsDataExist("Resolution"))
        {
            QualitySettings.resolutionScalingFixedDPIFactor = DataSaveManager.LoadFloat("Resolution");
        }
    }

    void Start()
    {
        _slider.value = QualitySettings.resolutionScalingFixedDPIFactor;

        dpiText.text = GetDensityDPI().ToString();
    }

    void Update()
    {
        _text.text = Screen.currentResolution.ToString();
        sliderValueText.text = QualitySettings.resolutionScalingFixedDPIFactor.ToString();
    }

    public void SetScreenResolution(int resolution)
    {
        //int width = 640;
        //int height = 480;

        //if(resolution == 0)
        //{
        //    width = 640;
        //    height = 480;
        //}

        //if (resolution == 1)
        //{
        //    width = 800;
        //    height = 600;
        //}

        //if (resolution == 2)
        //{
        //    width = 1024;
        //    height = 768;
        //}

        //if (resolution == 3)
        //{
        //    width = 1280;
        //    height = 720;
        //}

        //if (resolution == 4)
        //{
        //    width = 1366;
        //    height = 768;
        //}

        //Screen.SetResolution(width, height, true);
    }

    public void SetFixedDPI()
    {
        QualitySettings.resolutionScalingFixedDPIFactor = _slider.value;
    }

    public void SaveResolution()
    {
        DataSaveManager.SaveFloat("Resolution", QualitySettings.resolutionScalingFixedDPIFactor);
    }

    public void BloomEffect()
    {
        if (!ppb.enabled && !bloom.enabled)
        {
            bloom.enabled = true;
            effectText.text = "bloom";
            return;
        }

        if (ppb.enabled && !bloom.enabled)
        {
            ppb.enabled = false;
            bloom.enabled = true;
            effectText.text = "bloom";
        }
        else
        {
            bloom.enabled = false;
            ppb.enabled = true;
            effectText.text = "ppb";
        }
    }

    public void DisableEffects()
    {
        bloom.enabled = false;
        ppb.enabled = false;
        effectText.text = "";
    }

    public void ChangeMSAAOnDropdownValueChange()
    {
        if (dropdown.value == 3)
        {
            QualitySettings.antiAliasing = 8;
            return;
        }

        QualitySettings.antiAliasing = dropdown.value * 2;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void FullScreenOnAndOff()
    {
        StartCoroutine(FullScreenCoroutine());
    }

    IEnumerator FullScreenCoroutine()
    {
        Screen.fullScreen = false;
        yield return new WaitForSeconds(0.1f);
        Screen.fullScreen = true;
    }

    int GetDensityDPI()
    {
        if (!Application.isMobilePlatform)
        {
            return 0;
        }
        AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = activityClass.GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaObject metrics = new AndroidJavaObject("android.util.DisplayMetrics");
        activity.Call<AndroidJavaObject>("getWindowManager").Call<AndroidJavaObject>("getDefaultDisplay").Call("getMetrics", metrics);

        return (metrics.Get<int>("densityDpi"));
    }

}

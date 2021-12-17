using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DPIInfoPanel : MonoBehaviour
{
    public Text resolutionScalingFixedDPIFactorText;
    public Text densityText;
    public Text densityDpiText;
    public Text heightPixelsText;
    public Text widthPixelsText;
    public Text scaledDensityText;
    public Text xdpiText;
    public Text ydpiText;

    float resolutionScalingFixedDPIFactor;
    float density;
    int densityDpi;
    int heightPixels;
    int widthPixels;
    float scaledDensity;
    float xdpi;
    float ydpi;


    void OnEnable()
    {
        LoadAllData();
    }

    void LoadAllData()
    {
        resolutionScalingFixedDPIFactor = QualitySettings.resolutionScalingFixedDPIFactor;
        density = GetDisplayMetricsFieldFloat("density");
        densityDpi = GetDisplayMetricsFieldInt("densityDpi");
        heightPixels = GetDisplayMetricsFieldInt("heightPixels");
        widthPixels = GetDisplayMetricsFieldInt("widthPixels");
        scaledDensity = GetDisplayMetricsFieldFloat("scaledDensity");
        xdpi = GetDisplayMetricsFieldFloat("xdpi");
        ydpi = GetDisplayMetricsFieldFloat("ydpi");

        UpdateDataUI();
    }

    void UpdateDataUI()
    {
        resolutionScalingFixedDPIFactorText.text = resolutionScalingFixedDPIFactor.ToString();
        densityText.text = density.ToString();
        densityDpiText.text = densityDpi.ToString();
        heightPixelsText.text = heightPixels.ToString();
        widthPixelsText.text = widthPixels.ToString();
        scaledDensityText.text = scaledDensity.ToString();
        xdpiText.text = xdpi.ToString();
        ydpiText.text = ydpi.ToString();
    }

    int GetDisplayMetricsFieldInt(string fieldName)
    {
        if (!Application.isMobilePlatform)
        {
            return 0;
        }
        AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = activityClass.GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaObject metrics = new AndroidJavaObject("android.util.DisplayMetrics");
        activity.Call<AndroidJavaObject>("getWindowManager").Call<AndroidJavaObject>("getDefaultDisplay").Call("getMetrics", metrics);

        return (metrics.Get<int>(fieldName));
    }

    float GetDisplayMetricsFieldFloat(string fieldName)
    {
        if (!Application.isMobilePlatform)
        {
            return 0;
        }
        AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = activityClass.GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaObject metrics = new AndroidJavaObject("android.util.DisplayMetrics");
        activity.Call<AndroidJavaObject>("getWindowManager").Call<AndroidJavaObject>("getDefaultDisplay").Call("getMetrics", metrics);

        return (metrics.Get<float>(fieldName));
    }
}

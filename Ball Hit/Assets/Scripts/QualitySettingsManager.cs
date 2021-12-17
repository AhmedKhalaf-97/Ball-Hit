using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PostProcessing;

public class QualitySettingsManager : MonoBehaviour
{
    public SceneSwitcher sceneSwitcher;
    public Dropdown qualitySettingDropdown;
    public Toggle bloomEffectToggle;

    public PostProcessingBehaviour[] bloomEffects;

    int densityDPI;

    void Awake()
    {

    }

    void OnDisable()
    {
        SaveQualitySettings();
    }

    void Start()
    {
        densityDPI = GetDensityDPI();

        if (DataSaveManager.IsDataExist("QS")) //stand for QualitySetting.
        {
            qualitySettingDropdown.value = DataSaveManager.LoadInt("QS");
            SetQualitySetting();
        }

        if (DataSaveManager.IsDataExist("BFC")) //stand for BloomEffectCondition.
        {
            bloomEffectToggle.isOn = DataSaveManager.LoadBoolean("BFC");
        }
    }

    public void SetQualitySetting()
    {
        if (qualitySettingDropdown.value == 0)
        {
            QualitySettings.resolutionScalingFixedDPIFactor = ((float)densityDPI / 1000) * 0.5f;
            ApplyNewScreenDPI();
        }

        if (qualitySettingDropdown.value == 1)
        {
            QualitySettings.resolutionScalingFixedDPIFactor = ((float)densityDPI / 1000) * 0.75f;
            ApplyNewScreenDPI();
        }

        if (qualitySettingDropdown.value == 2)
        {
            QualitySettings.resolutionScalingFixedDPIFactor = ((float)densityDPI / 1000);
            ApplyNewScreenDPI();
        }
    }

    void ApplyNewScreenDPI()
    {
        StartCoroutine(NewScreenDPICoroutine());
    }

    IEnumerator NewScreenDPICoroutine()
    {
        Screen.fullScreen = false;
        yield return new WaitForSeconds(0.01f);
        Screen.fullScreen = true;
    }

    public void BloomEffectToggle(bool condition)
    {
        foreach (PostProcessingBehaviour bloom in bloomEffects)
        {
            if (sceneSwitcher.modeToggle.isOn)
            {
                condition = false;
            }
            bloom.enabled = condition;
        }
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

    public void SaveQualitySettings()
    {
        DataSaveManager.SaveInt("QS", qualitySettingDropdown.value);
        DataSaveManager.SaveBoolean("BFC", bloomEffectToggle.isOn);
    }

    public void ResetToDefault()
    {
        qualitySettingDropdown.value = 2;
        bloomEffectToggle.isOn = false;
    }
}

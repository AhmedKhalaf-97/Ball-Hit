using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataSave : MonoBehaviour
{
    public MainMenu mainMenu;
    public Player player;
    public QualitySettingsManager qualitySettingsManager;


    void OnEnable()
    {
        Application.targetFrameRate = 1000;

        CheckIfShouldRechargeBall();
        LoadHeartCount();
        LoadSlowMotionCount();
        LoadCollectedCoin();
        LoadGameMode();
        LoadAccelerometerSensitivityValue();
        LoadBallsCount();
    }

    void OnDisable()
    {
        CheckIfShouldSaveRechargeBallTime();
        SaveHeartCount();
        SaveSlowMotionCount();
        SaveCollectedCoin();
        SaveGameMode();
        SaveAccelerometerSensitivityValue();
        SaveBallsCount();
    }

    void OnApplicationPause()
    {
        CheckIfShouldSaveRechargeBallTime();
        SaveHeartCount();
        SaveSlowMotionCount();
        SaveCollectedCoin();
        SaveGameMode();
        SaveAccelerometerSensitivityValue();
        SaveBallsCount();
    }

    public void ResetToDefault()
    {
        player.sensitivitySlider.value = 2.5f;
        player.OnValueChangeAccelerometerSensitivity();

        qualitySettingsManager.ResetToDefault();

        //DataSaveManager.DeleteAllData();

        //mainMenu.scoreLabel.text = 0.ToString();

        //player.heartCount = 100;
        //player.slowMotionCount = 20;
        //player.collectedCoin = 0;
        //mainMenu.dropdown.value = 1;
        //mainMenu.shoot.ballsCount = 10;

        //player.UpdateHeartCountUI();
        //player.UpdateSlowMotionCountUI();
        //player.UpdateCoinUI();
        //mainMenu.DropdownValueChanged();
        //mainMenu.shoot.UpdateBallCountUI();
    }

    void CheckIfShouldRechargeBall()
    {
        if (DataSaveManager.LoadFloat("BRT") > 0.5f) //stand for BallRechargerTime.
        {
            float remainTime = DataSaveManager.LoadFloat("BRT");

            mainMenu.RechargeBallsInOnEnable(remainTime);
        }
    }

    void CheckIfShouldSaveRechargeBallTime()
    {
        DataSaveManager.DeleteData("BRT");
        if (mainMenu.remainRechargeTime > 0.5f)
        {
            DataSaveManager.SaveFloat("BRT", mainMenu.remainRechargeTime);
        }
    }

    void SaveHeartCount()
    {
        DataSaveManager.SaveInt("HC", player.heartCount); //stand for HeartCount.
    }

    void LoadHeartCount()
    {
        if (DataSaveManager.IsDataExist("HC"))
        {
        player.heartCount = DataSaveManager.LoadInt("HC");
        }
    }

    void SaveSlowMotionCount()
    {
        DataSaveManager.SaveInt("SMC", player.slowMotionCount); //stand for SlowMotionCount.
    }

    void LoadSlowMotionCount()
    {
        if (DataSaveManager.IsDataExist("SMC"))
        {
            player.slowMotionCount = DataSaveManager.LoadInt("SMC");
        }
    }

    void SaveCollectedCoin()
    {
        DataSaveManager.SaveInt("CC", player.collectedCoin); //stand for CollectedCoin.
    }

    void LoadCollectedCoin()
    {
        if (DataSaveManager.IsDataExist("CC"))
        {
            player.collectedCoin = DataSaveManager.LoadInt("CC");
        }
    }

    void SaveGameMode()
    {
        DataSaveManager.SaveInt("GM", mainMenu.mode); //stand for GameMode.
    }

    void LoadGameMode()
    {
        if (DataSaveManager.IsDataExist("GM"))
        {
            mainMenu.dropdown.value = DataSaveManager.LoadInt("GM");
            mainMenu.DropdownValueChanged();
        }
    }

    void SaveAccelerometerSensitivityValue()
    {
        DataSaveManager.SaveFloat("SV", player.accelerometerSensitivity); //stand for SensitivityValue.
    }

    void LoadAccelerometerSensitivityValue()
    {
        if (DataSaveManager.IsDataExist("SV"))
        {
            player.sensitivitySlider.value = DataSaveManager.LoadFloat("SV");
            player.OnValueChangeAccelerometerSensitivity();
        }
    }

    void SaveBallsCount()
    {
        DataSaveManager.SaveInt("BC", mainMenu.shoot.ballsCount); //stand for BallsCount.
    }

    void LoadBallsCount()
    {
        if (DataSaveManager.IsDataExist("BC"))
        {
            mainMenu.shoot.ballsCount = DataSaveManager.LoadInt("BC");
        }
    }
}

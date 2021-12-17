using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public GameObject mainMenuPanel;

    [Header("Settings Panel")]
    public GameObject settingPanel;

    [Header("ExitGame Panel")]
    public GameObject exitGamePanel;

    [Header("PauseMenu Panel")]
    public GameObject pauseMenuPanel;
    public GameObject hud;
    public Player player;

    [Header("Purchase Panel")]
    public GameObject purchasePanel;

    [Header("BallAndRoad Panel")]
    public GameObject ballAndRoadPanel;
    public GameObject threeD_UICamera;
    public GameObject ballCollectionPreviewUI;
    public GameObject roadCollectionPreviewUI;

    [Header("Store Panel")]
    public GameObject storePanel;


    void Update()
    {
        OnBackButton();
    }

    void OnBackButton()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Close Settings Panel
            if (settingPanel.activeInHierarchy)
            {
                settingPanel.SetActive(false);
                mainMenuPanel.SetActive(true);
                return;
            }

            //Close Purchase Panel
            if (purchasePanel.activeInHierarchy)
            {
                purchasePanel.SetActive(false);
                mainMenuPanel.SetActive(true);
                return;
            }

            //Close BallAndRoad Panel
            if (ballAndRoadPanel.activeInHierarchy)
            {
                if (!ballCollectionPreviewUI.activeInHierarchy && !roadCollectionPreviewUI.activeInHierarchy)
                {
                    ballAndRoadPanel.SetActive(false);
                    mainMenuPanel.SetActive(true);
                }
                else if(ballCollectionPreviewUI.activeInHierarchy)
                {
                    threeD_UICamera.SetActive(false);
                    ballCollectionPreviewUI.SetActive(false);
                }
                else if (roadCollectionPreviewUI)
                {
                    roadCollectionPreviewUI.SetActive(false);
                }
                return;
            }

            //Close Store Panel
            if (storePanel.activeInHierarchy)
            {
                storePanel.SetActive(false);
                mainMenuPanel.SetActive(true);
                return;
            }

            //Open ExitGame Panel
            if (mainMenuPanel.activeInHierarchy)
            {
                exitGamePanel.SetActive(true);
                mainMenuPanel.SetActive(false);
                return;
            }

            //Close ExitGame Panel
            if (exitGamePanel.activeInHierarchy)
            {
                exitGamePanel.SetActive(false);
                mainMenuPanel.SetActive(true);
                return;
            }

            //Open Pause Menu Panel
            if (hud.activeInHierarchy && !pauseMenuPanel.activeInHierarchy && !player.isSlowMotionRunning 
                && !player.isRevivingRunning)
            {
                pauseMenuPanel.SetActive(true);
                PauseGame(true);
                return;
            }

            //Close Pause Menu panel
            if (pauseMenuPanel.activeInHierarchy)
            {
                pauseMenuPanel.SetActive(false);
                PauseGame(false);
                return;
            }
        }
    }

    public void PauseGame(bool condition)
    {
        if (condition)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

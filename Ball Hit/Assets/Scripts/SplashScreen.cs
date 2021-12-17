using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public float logoTimer = 2f;

    void Awake()
    {
        Application.targetFrameRate = 120;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        Invoke("StartGameScene", logoTimer);
    }

    void StartGameScene()
    {
        SceneManager.LoadScene(1);
    }
}

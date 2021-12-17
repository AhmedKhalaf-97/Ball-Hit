using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    public Player player;
    public ChallengeMode challengeMode;
    public Text scoreLabel;

    public GameObject hudGO;

    public UnityAds unityAds;
    public int gameSessions;

    [Header("Audio Control")]
    public Image audioToggleImage;
    public Sprite speakerOnSprite;
    public Sprite speakerOffSprite;
    bool isAudioOn = true;

    List<AudioSource> audioSources = new List<AudioSource>();

    [Header("Animation Stuff")]
    public float timeToStartGame = 1f;
    public Animator mainMenuPanelAnimator;

    [Header("Level Manager")]
    public int currentLevelNum;
    public int requiredScore;
    public int latestScore;
    public int cumulativeScore;
    public Text latestScoreText;
    public Text currentLevelNumText;
    public Slider levelSlider;

    [Header("Game Mode")]
    public int mode;
    public Dropdown dropdown;

    [Header("Ball Recharge")]
    public int ballRechargeCount;
    public float rechrargeTime;
    public int requiredCoins = 5;
    public Shoot shoot;
    public GameObject rechargeBallsPanel;
    public Button rechargeBallButton;
    public Text rechargeBallTimerText;
    public Text requiredCoinsText;
    public Image timerCircleImage;
    public Button playButton;

    public float remainRechargeTime;

    void OnEnable()
    {
        requiredCoinsText.text = requiredCoins.ToString();
    }

    void Awake()
    {
        Application.targetFrameRate = 1000;

        GetAllAudioSources();

        SetHighScore(0f);

        SetLevelNumberAndCumulativeScore();

        player.LoadShieldCount();

        LoadAudioStatus();

        StartCoroutine(LoadTheGame());
    }


    void Start()
    {
        player.StartBackgroundScene();
        hudGO.SetActive(false);
        shoot.gameObject.SetActive(false);
    }


    public void DropdownValueChanged()
    {
        mode = dropdown.value;
    }

    public void InvokeStartGame()
    {
        mainMenuPanelAnimator.SetTrigger("CloseMainMenu");
        Invoke("StartGame", timeToStartGame);
    }

    public void StartGame()
    {
        mode = dropdown.value;

        player.StartGame(mode);
        gameObject.SetActive(false);

        hudGO.SetActive(true);
        shoot.gameObject.SetActive(true);

        player.ResetShields();

        SceneSwitcher.isObstacleSmashed = false;
        challengeMode.StartChallenge();
    }

    public void EndGame(float distanceTraveled)
    {
        SetHighScore(distanceTraveled);
        SetLatestScore(distanceTraveled);
        gameObject.SetActive(true);

        hudGO.SetActive(false);
        shoot.gameObject.SetActive(false);
        player.StartBackgroundScene();

        challengeMode.StopAnyRunningChallenge();

        if (!unityAds.isAdsDisabled)
        {
            ShowAdIfPossible();
        }
    }

    public void EndGameButton()
    {
        float distanceTraveled = player.distanceTraveled;

        SetHighScore(distanceTraveled);
        SetLatestScore(distanceTraveled);
        gameObject.SetActive(true);

        hudGO.SetActive(false);
        shoot.gameObject.SetActive(false);
        player.StartBackgroundScene();

        challengeMode.StopAnyRunningChallenge();

        if (!unityAds.isAdsDisabled)
        {
            ShowAdIfPossible();
        }
    }


    void SetHighScore(float distanceTraveled)
    {
        int score = ((int)(distanceTraveled * 10f));

        if (score > DataSaveManager.LoadInt("HS")) //stand for HighScore.
        {
            DataSaveManager.SaveInt("HS", score);
            scoreLabel.text = string.Format("{0:0,0}", score);
        }
        else
        {
            scoreLabel.text = string.Format("{0:0,0}", DataSaveManager.LoadInt("HS"));
        }
    }

    void SetLatestScore(float distanceTraveled)
    {
        latestScore = ((int)(distanceTraveled * 10f));

        latestScoreText.text = string.Format("{0:0,0}", latestScore);

        SetCumulativeScore();
    }

    void SetCumulativeScore()
    {
        cumulativeScore += (latestScore / 3);

        if (cumulativeScore >= requiredScore)
        {
            cumulativeScore -= requiredScore;
            ShiftToNextLevel();
        }

        DataSaveManager.SaveInt("CS", cumulativeScore); //stand for CumulativeScore.

        UpdateLevelInfoUI();
    }

    void SetLevelNumberAndCumulativeScore()
    {
        if (DataSaveManager.IsDataExist("LN"))
        {
            currentLevelNum = DataSaveManager.LoadInt("LN"); //stand for LevelNumber.
        }
        else
        {
            currentLevelNum = 1;
            DataSaveManager.SaveInt("LN", currentLevelNum);
        }

        requiredScore = currentLevelNum * 1000;

        cumulativeScore = DataSaveManager.LoadInt("CS");

        UpdateLevelInfoUI();
    }

    void ShiftToNextLevel()
    {
        currentLevelNum++;
        requiredScore = currentLevelNum * 1000;
        DataSaveManager.SaveInt("LN", currentLevelNum);

        UpdateLevelInfoUI();
    }

    void UpdateLevelInfoUI()
    {
        currentLevelNumText.text = currentLevelNum.ToString();

        Invoke("LevelSlider", 0.1f);
    }

    void LevelSlider()
    {
        UpdateLevelSlider(0, requiredScore, cumulativeScore, 0);
    }

    void UpdateLevelSlider(float minValue, float maxValue, float wantedValue, float currentValue)
    {
        StartCoroutine(UpdateLevelSliderCoroutine(minValue, maxValue, wantedValue, currentValue));
    }

    float duration = 2f;
    float lerpSpeed = 0;
    IEnumerator UpdateLevelSliderCoroutine(float minValue, float maxValue, float wantedValue, float currentValue)
    {
        lerpSpeed = 0;

        levelSlider.minValue = minValue;
        levelSlider.maxValue = maxValue;
        levelSlider.value = currentValue;

        while (true)
        {
            lerpSpeed += Time.deltaTime / duration;
            levelSlider.value = Mathf.Lerp(currentValue, wantedValue, lerpSpeed);

            if (wantedValue == levelSlider.value)
            {
                lerpSpeed = 0;
                yield break;
            }
            yield return null;
        }
    }

    public void OutOfBalls()
    {
        DestroyBalls();
        gameObject.SetActive(true);
        requiredCoinsText.text = requiredCoins.ToString();
        rechargeBallsPanel.SetActive(true);
        StartCoroutine(RechargeBallTimer(rechrargeTime));
    }

    IEnumerator RechargeBallTimer(float _timerCountdown)
    {
        yield return new WaitForSeconds(0.05f); //to give enough time for gamedatasave to supply player.collectedcoin

        playButton.interactable = false;

        player.ResetSpeedsIfSlowMotionInterrupted();

        if (requiredCoins > player.collectedCoin)
        {
            rechargeBallButton.interactable = false;
        }

        float endTime = Time.time + _timerCountdown;
        while (Time.time < endTime && _timerCountdown > 0.1)
        {
            _timerCountdown -= Time.unscaledDeltaTime;
            rechargeBallTimerText.text = string.Format("{0:00.00}", _timerCountdown);
            remainRechargeTime = _timerCountdown;
            timerCircleImage.fillAmount = remainRechargeTime / rechrargeTime;
            yield return null;
        }

        rechargeBallTimerText.text = string.Format("{0:00.00}", 0f);

        StartCoroutine(Countdown(0.7f));
    }

    IEnumerator Countdown(float _countdown)
    {
        yield return new WaitForSeconds(_countdown);
        RechargeBall(false);
        rechargeBallButton.interactable = true;
        //shoot.gameObject.SetActive(true);
    }

    public void RechargeBall(bool callFromButton)
    {
        if (callFromButton)
        {
            if (requiredCoins > player.collectedCoin)
            {
                return;
            }
            else
            {
                player.collectedCoin -= requiredCoins;
                player.UpdateCoinUI();
            }
        }
        StopAllCoroutines();
        rechargeBallsPanel.SetActive(false);
        shoot.ballsCount += ballRechargeCount;
        shoot.UpdateBallCountUI();
        remainRechargeTime = 0f;
        playButton.interactable = true;
    }

    public void UnlimitedBallsFunction()
    {
        StopAllCoroutines();
        rechargeBallsPanel.SetActive(false);
        remainRechargeTime = 0f;
        playButton.interactable = true;
    }

    public void RechargeBallsInOnEnable(float _rechrargeTime)
    {
        rechargeBallsPanel.SetActive(true);
        StartCoroutine(RechargeBallTimer(_rechrargeTime));
    }

    void DestroyBalls()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

        foreach (GameObject ball in balls)
        {
            Destroy(ball);
        }
    }

    void LoadAudioStatus()
    {
        if (DataSaveManager.IsDataExist("IAO"))
        {
            isAudioOn = DataSaveManager.LoadBoolean("IAO");
            ApplyAudioControlStatus();
        }
    }

    public void ToggleAudioControll()
    {
        isAudioOn = !isAudioOn;

        ApplyAudioControlStatus();

        DataSaveManager.SaveBoolean("IAO", isAudioOn);
    }

    void ApplyAudioControlStatus()
    {
        if (isAudioOn)
        {
            audioToggleImage.sprite = speakerOnSprite;
        }
        else
        {
            audioToggleImage.sprite = speakerOffSprite;
        }

        foreach (AudioSource ac in audioSources)
        {
            ac.enabled = isAudioOn;
        }
    }

    void GetAllAudioSources()
    {
        audioSources.Clear();

        foreach (AudioSource _audioSource in Resources.FindObjectsOfTypeAll(typeof(AudioSource)) as AudioSource[])
        {
            audioSources.Add(_audioSource);
        }
    }

    IEnumerator LoadTheGame()
    {
        yield return new WaitForSeconds(0.5f);
        //Start Game.
        mode = dropdown.value;
        player.StartGame(mode);

        yield return new WaitForSeconds(0.5f);
        //End Game.
        player.StartBackgroundScene();
    }

    void ShowAdIfPossible()
    {
        gameSessions++;

        if(gameSessions >= 3)
        {
            unityAds.ShowVideoAd();
        }
    }

    public void OpenPrivacyPolicy()
    {
        Application.OpenURL("https://bantergames.blogspot.com/p/blog-page_19.html");
    }
}
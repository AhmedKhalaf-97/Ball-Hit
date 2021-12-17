using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public bool pipeWithItems = true;
    public PipeSystem pipeSystem;

    public float startVelocity;
    public float rotationVelocity;

    public MainMenu mainMenu;
    public HUD hud;

    public float[] accelerations;

    [Header("Audio Stuff")]
    public AudioSource bricksDestructionAudioSource;
    public AudioSource glassBreakingAudioSource;
    public AudioSource hitAudioSource;
    public AudioSource gemsAudioSource;
    public AudioSource outsideAudioSource;
    public AudioClip smSFXIn;
    public AudioClip smSFXOut;
    public AudioClip shieldSFXIn;
    public AudioClip shieldSFXOut;
    public AudioClip deathSFX;
    public AudioClip reviveSFX;

    [Header("Accelerometer Sensitivity")]
    public float accelerometerSensitivity = 2f;
    public Slider sensitivitySlider;

    [Header("BulletTime")]
    public float speed;
    public float rotationSpeed;
    public float timeLength;
    public int slowMotionCount;
    public bool isSlowMotionRunning;
    public Text[] slowMotionCountText;
    public Slider slowMotionSlider;
    public Button slowMotionButton;
    public Button pauseMenuButton;
    public Image flashEffectImage;
    public float flashSpeedFactor;

    [Header("Revival & Lives")]
    public bool isRevivingRunning;
    public float deathCountdown = 2f;
    public int livesCount;
    public int livesLeft;
    public float timerCountdown;
    public Text livesCountText;
    public GameObject hurtEffectPanel;
    public GameObject revivalPanel;
    public Button revivalButton;
    public Text timerText;
    public Image timerCircleImage;
    public float obstaclesRadius;
    public LayerMask obstacleLayer;
    public int heartCount;
    public Text[] heartCountText;

    [Header("Coin System")]
    public int collectedCoin;
    public Text[] coinText;
    public Animator coinTextAnimator;

    [Header("Shields")]
    public int shieldCount = 10;
    public float shieldTimer = 30f;
    public Image shieldTimerCircleImage;
    public Button shieldButton;
    public Text shieldCountText;
    public Text[] otherShieldCountText;
    public Avatar avatar;
    Coroutine shieldCoroutine;
    float lerpSpeed;
    float currentShieldTimer;


    Animator scoreTextAnimator;

    private Pipe currentPipe;

    private float acceleration, velocity;
    [HideInInspector]
    public float distanceTraveled;
    private float deltaToRotation;
    private float systemRotation;
    private float worldRotation, avatarRotation;

    private Transform world, rotater;

    GameObject ballShooter;

    float _speed;
    float _rotationSpeed;

    public void StartGame(int accelerationMode)
    {
        pipeWithItems = true;

        ResetAfterDeath();
        distanceTraveled = 0f;
        avatarRotation = 0f;
        systemRotation = 0f;
        worldRotation = 0f;
        acceleration = accelerations[accelerationMode];
        velocity = startVelocity;
        currentPipe = pipeSystem.SetupFirstPipe();
        SetupCurrentPipe();
        gameObject.SetActive(true);
        hud.SetValues(distanceTraveled, velocity);
    }

    public void StartBackgroundScene()
    {
        pipeWithItems = false;

        ResetSlowMotion();
        ResetAfterDeath();
        distanceTraveled = 0f;
        avatarRotation = 0f;
        systemRotation = 0f;
        worldRotation = 0f;
        acceleration = 0;
        velocity = 1f;
        currentPipe = pipeSystem.SetupFirstPipe();
        SetupCurrentPipe();
        gameObject.SetActive(true);
        hurtEffectPanel.SetActive(false);
    }

    void OnEnable()
    {
        ballShooter = Camera.main.transform.GetChild(0).gameObject;

        hurtEffectPanel.SetActive(false);
    }

    private void Awake()
    {
        world = pipeSystem.transform.parent;
        rotater = transform.GetChild(0);
        gameObject.SetActive(false);
        UpdateHeartCountUI();
        UpdateSlowMotionCountUI();
        UpdateCoinUI();

        scoreTextAnimator = hud.distanceLabel.transform.GetComponent<Animator>();
    }

    private void Update()
    {
        velocity += acceleration * Time.deltaTime;
        float delta = velocity * Time.deltaTime;
        distanceTraveled += delta;
        systemRotation += delta * deltaToRotation;

        if (systemRotation >= currentPipe.CurveAngle)
        {
            delta = (systemRotation - currentPipe.CurveAngle) / deltaToRotation;
            currentPipe = pipeSystem.SetupNextPipe();
            SetupCurrentPipe();
            systemRotation = delta * deltaToRotation;
        }

        pipeSystem.transform.localRotation = Quaternion.Euler(0f, 0f, systemRotation);

        if (!mainMenu.gameObject.activeInHierarchy)
        {
            UpdateAvatarRotation();
        }

        hud.SetValues(distanceTraveled, velocity);

        CheckForBulletTimeEffect();
        CheckIfshouldStartBulletTime();
    }


    private void UpdateAvatarRotation()
    {
        float rotationInput = 0f;
        //if (Application.isMobilePlatform)
        //{
        //    if (Input.touchCount == 1)
        //    {
        //        if (Input.GetTouch(0).position.x < Screen.width * 0.5f)
        //        {
        //            rotationInput = -1f;
        //        }
        //        else
        //        {
        //            rotationInput = 1f;
        //        }
        //    }
        //}
        //else
        //{
        //    rotationInput = Input.GetAxis("Horizontal");
        //}

        if (Application.isMobilePlatform)
        {
            rotationInput = Input.acceleration.x * accelerometerSensitivity;
        }
        else
        {
            rotationInput = Input.GetAxis("Horizontal");
        }

        avatarRotation += rotationVelocity * Time.deltaTime * rotationInput;
        if (avatarRotation < 0f)
        {
            avatarRotation += 360f;
        }
        else if (avatarRotation >= 360f)
        {
            avatarRotation -= 360f;
        }
        rotater.localRotation = Quaternion.Euler(avatarRotation, 0f, 0f);
    }

    public void OnValueChangeAccelerometerSensitivity()
    {
        accelerometerSensitivity = sensitivitySlider.value;
    }

    private void SetupCurrentPipe()
    {
        deltaToRotation = 360f / (2f * Mathf.PI * currentPipe.CurveRadius);
        worldRotation += currentPipe.RelativeRotation;
        if (worldRotation < 0f)
        {
            worldRotation += 360f;
        }
        else if (worldRotation >= 360f)
        {
            worldRotation -= 360f;
        }
        world.localRotation = Quaternion.Euler(worldRotation, 0f, 0f);
    }

    public void PlayerHit()
    {
        livesLeft--;
        livesCountText.text = livesLeft.ToString();
        if (livesLeft > 0)
        {
            StartCoroutine(TurnOnHurtEffect());
        }
        else
        {
            Time.timeScale = 0f;
            revivalPanel.SetActive(true);
            StartCoroutine(Revival(timerCountdown));
        }

        hitAudioSource.Play();
    }

    IEnumerator TurnOnHurtEffect()
    {
        hurtEffectPanel.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        hurtEffectPanel.SetActive(false);
    }

    IEnumerator Revival(float _timerCountdown)
    {
        isRevivingRunning = true;
        pauseMenuButton.interactable = false;
        slowMotionButton.interactable = false;

        BallShooterState(false);

        if (heartCount > 0)
        {
            revivalButton.onClick.AddListener(Revive);
        }
        else
        {
            revivalButton.interactable = false;
            revivalButton.image.color = Color.gray;
        }

        float endTime = Time.time + _timerCountdown;
        while (Time.time < endTime && _timerCountdown > 0.1)
        {
            _timerCountdown -= Time.unscaledDeltaTime;
            timerText.text = string.Format("{0:00.00}", _timerCountdown);
            timerCircleImage.fillAmount = _timerCountdown / timerCountdown;
            yield return null;
        }
        timerText.text = string.Format("{0:00.00}", 0f);

        StartCoroutine(StartDeathCoutdown(deathCountdown));
    }

    IEnumerator StartDeathCoutdown(float _deathCountdown)
    {
        outsideAudioSource.clip = deathSFX;
        outsideAudioSource.Play();

        revivalButton.onClick.RemoveAllListeners();
        float endTime = Time.time + _deathCountdown;

        while (Time.time < endTime && _deathCountdown > 0)
        {
            _deathCountdown -= Time.unscaledDeltaTime;

            yield return null;
        }

        Time.timeScale = 1f;
        Die();
    }

    void Die()
    {
        mainMenu.EndGame(distanceTraveled);
        BallShooterState(false);
        ResetSlowMotion();
        ResetSpeedsIfSlowMotionInterrupted();
        isRevivingRunning = false;
        if (slowMotionCount > 0)
        {
            slowMotionButton.interactable = true;
        }
        StopAllCoroutines();
        velocity = 1;
        hurtEffectPanel.SetActive(false);
    }

    void Revive()
    {
        ResetShields();
        StopAllCoroutines();
        StartCoroutine(ReviveCoroutine(0.5f));
        hurtEffectPanel.SetActive(false);
        ResetSlowMotion();
        ResetSpeedsIfSlowMotionInterrupted();
        if (slowMotionCount > 0)
        {
            slowMotionButton.interactable = true;
        }

        outsideAudioSource.clip = reviveSFX;
        outsideAudioSource.Play();
    }

    IEnumerator ReviveCoroutine(float countDown)
    {
        heartCount--;
        revivalButton.onClick.RemoveAllListeners();
        UpdateHeartCountUI();

        float endTime = Time.time + countDown;

        while (Time.time < endTime && countDown > 0)
        {
            countDown -= Time.unscaledDeltaTime;

            yield return null;
        }

        Time.timeScale = 1f;
        livesLeft = 1;
        revivalPanel.SetActive(false);
        livesCountText.text = livesLeft.ToString();
        BallShooterState(true);
        DestroyNearObstacles();
        isRevivingRunning = false;
    }

    public void UpdateHeartCountUI()
    {
        if (heartCount < 0)
        {
            heartCount = 0;
        }

        foreach (Text _text in heartCountText)
        {
            _text.text = string.Format("{0:0,0}", heartCount);
        }
    }

    void ResetAfterDeath()
    {
        livesLeft = livesCount;
        revivalPanel.SetActive(false);
        livesCountText.text = livesLeft.ToString();
        if (slowMotionCount > 0)
        {
            slowMotionButton.interactable = true;
        }
        if (heartCount > 0)
        {
            revivalButton.interactable = true;
            revivalButton.image.color = Color.white;
        }
    }

    public void BallShooterState(bool setState)
    {
        ballShooter.SetActive(setState);
    }

    bool isApplied;
    Color buttonColor;
    void CheckIfshouldStartBulletTime()
    {
        if (slowMotionCount <= 0 && !isApplied)
        {
            buttonColor = slowMotionButton.image.color;
            slowMotionButton.interactable = false;
            slowMotionButton.image.color = Color.white;
            isApplied = true;
            return;
        }

        if (slowMotionCount > 0 && isApplied)
        {
            slowMotionButton.interactable = true;
            slowMotionButton.image.color = buttonColor;
            isApplied = false;
            return;
        }
    }

    void CheckForBulletTimeEffect()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isSlowMotionRunning && slowMotionCount > 0)
        {
            StartBulletTime();
        }
    }

    public void StartBulletTime()
    {
        if (!isSlowMotionRunning && slowMotionCount > 0)
        {
            pauseMenuButton.interactable = false;
            isSlowMotionRunning = true;
            slowMotionCount--;
            UpdateSlowMotionCountUI();
            slowMotionSlider.gameObject.SetActive(true);
            StartCoroutine(StartFlashEffect());
            StartCoroutine(BulletTimeEffect(timeLength));

            outsideAudioSource.clip = smSFXIn;
            outsideAudioSource.Play();
        }
    }

    IEnumerator BulletTimeEffect(float _timeLength)
    {
        float _velocity = velocity;
        float _rotationVelocity = rotationVelocity;

        _speed = _velocity;
        _rotationSpeed = _rotationVelocity;

        velocity = speed;
        rotationVelocity = rotationSpeed;

        float endTime = Time.time + _timeLength;

        while (Time.time < endTime && _timeLength > 0)
        {
            _timeLength -= Time.unscaledDeltaTime;
            slowMotionSlider.value = _timeLength / timeLength;
            yield return null;
        }

        while (velocity < _velocity)
        {
            velocity += Time.deltaTime * _velocity;
            yield return null;
        }
        velocity = _velocity;
        rotationVelocity = _rotationVelocity;
        isSlowMotionRunning = false;
        slowMotionSlider.gameObject.SetActive(false);
        pauseMenuButton.interactable = true;

        outsideAudioSource.clip = smSFXOut;
        outsideAudioSource.Play();
    }

    Color imageColor;
    IEnumerator StartFlashEffect()
    {
        imageColor = flashEffectImage.color;

        imageColor.a = 0;
        flashEffectImage.color = imageColor;

        while (imageColor.a < 0.5f)
        {

            imageColor.a += Time.deltaTime * flashSpeedFactor;
            flashEffectImage.color = imageColor;
            yield return null;
        }


        imageColor.a = 0.5f;
        flashEffectImage.color = imageColor;

        while (imageColor.a > 0)
        {

            imageColor.a -= Time.deltaTime * flashSpeedFactor;
            flashEffectImage.color = imageColor;
            yield return null;
        }

        imageColor.a = 0;
        flashEffectImage.color = imageColor;
    }

    public void UpdateSlowMotionCountUI()
    {
        foreach (Text _text in slowMotionCountText)
        {
            _text.text = string.Format("{0:0,0}", slowMotionCount);
        }
    }

    void ResetSlowMotion()
    {
        //isSlowMotionRunning = false;
        slowMotionSlider.gameObject.SetActive(false);
        pauseMenuButton.interactable = true;
    }

    public void ResetSpeedsIfSlowMotionInterrupted()
    {
        if (isSlowMotionRunning)
        {
            velocity = _speed;
            rotationVelocity = _rotationSpeed;
            isSlowMotionRunning = false;
            pauseMenuButton.interactable = true;
        }

    }


    void DestroyNearObstacles()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, obstaclesRadius, obstacleLayer);

        foreach (Collider col in colliders)
        {
            Destroy(col.gameObject);
        }
    }

    public void OutOfBalls()
    {
        Die();
        mainMenu.OutOfBalls();
        BallShooterState(false);
    }

    public void CollectCoin()
    {
        collectedCoin++;
        UpdateCoinUI();
        CoinTextAnim();

        gemsAudioSource.Play();
    }

    public void UpdateCoinUI()
    {
        foreach (Text _text in coinText)
        {
            _text.text = string.Format("{0:0,0}", collectedCoin);
        }
    }

    void CoinTextAnim()
    {
        coinTextAnimator.SetTrigger("TextAnim");
    }

    public void ScoreTextAnim()
    {
        scoreTextAnimator.SetTrigger("TextAnim");
    }

    public void UpdateAllInfoUI()
    {
        mainMenu.shoot.UpdateBallCountUI();
        SaveShieldCount();
        UpdateCoinUI();
        UpdateHeartCountUI();
        UpdateSlowMotionCountUI();
        UpdateShieldsUI();
    }

    public void StartShields()
    {
        if (shieldCount > 0)
        {
            shieldCount--;
            
            UpdateShieldsUI();

            shieldButton.interactable = false;

            shieldCoroutine = StartCoroutine(StartShieldsCoroutine());

            outsideAudioSource.clip = shieldSFXIn;
            outsideAudioSource.Play();
        }
    }

    IEnumerator StartShieldsCoroutine()
    {
        avatar.isShieldRunning = true;

        shieldTimerCircleImage.gameObject.SetActive(true);

        lerpSpeed = 0;
        currentShieldTimer = shieldTimer;

        shieldTimerCircleImage.fillAmount = (currentShieldTimer / shieldTimer);

        while (true)
        {
            lerpSpeed += Time.deltaTime / currentShieldTimer;
            shieldTimerCircleImage.fillAmount = Mathf.Lerp((currentShieldTimer / shieldTimer), 0, lerpSpeed);

            if (0 == shieldTimerCircleImage.fillAmount)
            {
                lerpSpeed = 0;
                shieldTimerCircleImage.gameObject.SetActive(false);

                avatar.isShieldRunning = false;

                if (shieldCount > 0)
                {
                    shieldButton.interactable = true;
                }

                outsideAudioSource.clip = shieldSFXOut;
                outsideAudioSource.Play();

                yield break;
            }
            yield return null;
        }
    }

    public void ResetShields() //After Reload or Start.
    {
        if (shieldCoroutine != null)
        {
            StopCoroutine(shieldCoroutine);
        }

        shieldTimerCircleImage.gameObject.SetActive(false);

        avatar.isShieldRunning = false;

        UpdateShieldsUI();
    }

    void UpdateShieldsUI()
    {
        shieldCountText.text = shieldCount.ToString();

        Color color = shieldCountText.color;

        if (shieldCount == 0)
        {
            shieldButton.interactable = false;

            color.a = 0.5f;
            shieldCountText.color = color;
        }

        if (shieldCount > 0)
        {
            shieldButton.interactable = true;

            color.a = 1f;
            shieldCountText.color = color;
        }

        foreach (Text _text in otherShieldCountText)
        {
            _text.text = shieldCount.ToString();
        }
    }

    public void LoadShieldCount() //Called In MainMenu Awake.
    {
        if (DataSaveManager.IsDataExist("SC"))
        {
            shieldCount = DataSaveManager.LoadInt("SC");
        }

        UpdateShieldsUI();
    }

    public void SaveShieldCount()
    {
        DataSaveManager.SaveInt("SC", shieldCount);
    }

    public void PlayBricksDestructionSFX()
    {
        bricksDestructionAudioSource.Play();
    }

    public void PlayGlassBreakingSFX()
    {
        glassBreakingAudioSource.Play();
    }
}
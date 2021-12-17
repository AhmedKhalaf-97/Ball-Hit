using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Shoot : MonoBehaviour
{
    public Player player;
    public MainMenu mainMenu;
    public PanelSwitcherUIManager panelSwitcherUIManager;
    public AdsRewardsAndPurchasingPanel adsRewardsAndPurchasingPanel;

    public GameObject ballPrefab;
    public float propulsionForce;
    private Transform myTransform;

    public Transform ballPurchaserPanel;

    Vector3 worldPosition;
    Vector3 shootDir;

    Camera _camera;

    public bool unlimitedBalls;
    public int ballsCount;

     float nextShoot;
    public float shootRate;

    public Text[] ballsCountText;

    public Transform scorePanel;

    [Header("Audio Stuff")]
    public AudioSource shootAudioSource;

    [Header("Buttons Not To Be Clicked When Shooting")]
    public GameObject[] buttons;

    [SerializeField]
    public static Shoot _shoot;


    void Awake()
    {
        if (DataSaveManager.IsDataExist("UB"))
        {
            unlimitedBalls = DataSaveManager.LoadBoolean("UB");

            if (unlimitedBalls)
            {
                //update UI;
                foreach (Text countText in ballsCountText)
                {
                    countText.text = "∞".ToString();
                }

                SetBallPurchaserPanelInteractableToFalse();

                panelSwitcherUIManager.CheckIfUnlimitedBalls();
            }
        }

        _camera = Camera.main;
        myTransform = transform;
        UpdateBallCountUI();
        _shoot = this;
    }

    void Update()
    {
        if (!unlimitedBalls)
        {
            if (ballsCount <= 0)
            {
                ballsCount = 0;
                foreach (Text countText in ballsCountText)
                {
                    countText.text = 0.ToString();
                }

                player.OutOfBalls();
                return;
            }
        }
        
        foreach (GameObject gameObject in buttons)
        {
            if (EventSystem.current.currentSelectedGameObject == gameObject)
            {
                return;
            }
        }

        if (Application.isMobilePlatform)
        {
            if (Input.touchCount > 0 && Time.unscaledTime > nextShoot && Time.timeScale == 1)
            {
                nextShoot = Time.unscaledTime + shootRate;
                SpawnBall();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.unscaledTime > nextShoot && Time.timeScale == 1)
            {
                nextShoot = Time.unscaledTime + shootRate;
                SpawnBall();
            }
        }
    }

    void SpawnBall()
    {
        if (Application.isMobilePlatform)
        {
            var touchInput = Input.GetTouch(0).position;
            Vector3 position = new Vector3(_camera.scaledPixelWidth - touchInput.x, _camera.scaledPixelHeight - touchInput.y, _camera.nearClipPlane);
            Vector3 shootPosition = new Vector3(touchInput.x, touchInput.y, _camera.nearClipPlane);

            worldPosition = _camera.ScreenToWorldPoint(position);

            Ray camRay = Camera.main.ScreenPointToRay(shootPosition);
            RaycastHit pipeHit;

            if (Physics.Raycast(camRay, out pipeHit, 200f))
            {
                shootDir = pipeHit.point - myTransform.position;
            }
        }
        else
        {
            var mouseInput = Input.mousePosition;
            Vector3 position = new Vector3(_camera.scaledPixelWidth - mouseInput.x, _camera.scaledPixelHeight - mouseInput.y, _camera.nearClipPlane);
            Vector3 shootPosition = new Vector3(mouseInput.x, mouseInput.y, _camera.nearClipPlane);

            worldPosition = _camera.ScreenToWorldPoint(position);

            Ray camRay = Camera.main.ScreenPointToRay(shootPosition);
            RaycastHit pipeHit;

            if (Physics.Raycast(camRay, out pipeHit, 200f))
            {
                shootDir = pipeHit.point - myTransform.position;
            }
        }

        GameObject go = (GameObject)Instantiate(ballPrefab, worldPosition, Quaternion.identity);
        go.GetComponent<Rigidbody>().AddForce(shootDir * propulsionForce, ForceMode.Acceleration);

        shootAudioSource.Play();
    }

    public void UpdateBallCountUI()
    {
        if (unlimitedBalls)
        {
            return;
        }

        foreach(Text countText in ballsCountText)
        {
            countText.text = string.Format("{0:0,0}", ballsCount);
        }
    }

    public void PurchasedUnlimitedBalls()
    {
        DataSaveManager.SaveBoolean("UB", true);

        unlimitedBalls = true;
        ballsCount = 0;
        mainMenu.UnlimitedBallsFunction();

        //update UI;
        foreach (Text countText in ballsCountText)
        {
            countText.text = "∞".ToString();
        }

        SetBallPurchaserPanelInteractableToFalse();

        panelSwitcherUIManager.CheckIfUnlimitedBalls();

        adsRewardsAndPurchasingPanel.ShowPurchasingCompletedMessage("Unlimited Balls");
    }

    void SetBallPurchaserPanelInteractableToFalse()
    {
        for (int i = 0; i < ballPurchaserPanel.childCount; i++)
        {
            ballPurchaserPanel.GetChild(i).GetComponent<Button>().interactable = false;
        }
    }
}

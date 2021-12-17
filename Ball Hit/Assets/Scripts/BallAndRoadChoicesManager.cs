using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallAndRoadChoicesManager : MonoBehaviour
{
    public Player player;
    public Shoot shoot;
    public SceneSwitcher sceneSwitcher;
    public GameObject noEnoughGemsPanel;

    public GameObject threeD_UICamera;
    public GameObject ballCollectionPreview;

    public GameObject previewPanel;
    public Text previewPanelPriceText;

    public GameObject previewPanelForRoad;
    public RawImage previewPanelImageRoad;
    public Text previewPanelRoadPriceText;

    [Header("Audio Stuff")]
    public AudioSource audioSource;
    public AudioClip buttonSelectSound;
    public AudioClip buyItemSound;

    [Header("Ball Stuff")]
    public Transform ballsButtonsContent;
    public int ballPrice;
    public Sprite[] ballButtonImages;
    public GameObject[] ballsItemsPrefabs;
    public List<ButtonProperties> buttonPropertiesForBalls = new List<ButtonProperties>();

    [Header("Road Stuff")]
    public Transform roadButtonsContent;
    public int roadPrice;
    public Texture[] roadButtonsImages;
    public Texture[] roadNightModeButtonImages;
    public List<ButtonPropertiesRoad> buttonPropertiesForRoads = new List<ButtonPropertiesRoad>();

    int selectedButtonIndex;
    int selectedButtonRoadIndex;

    [System.Serializable]
    public class ButtonProperties
    {
        public Button myButton;
        public int price;
        public Sprite buttonImage;
        public GameObject itemPrefab;
        public bool isPurchased = false;
        public bool isSelected = false;
    }

    [System.Serializable]
    public class ButtonPropertiesRoad
    {
        public Button myButton;
        public int price;
        public Texture buttonImage;
        public Texture nightModeButtonImage;
        public int roadIndex;
        public bool isPurchased = false;
        public bool isSelected = false;
    }

    void OnEnable()
    {
        UpdateButtonProperties();
    }

   public void UpdateButtonProperties()
    {
        //for ball.
        for (int i = 0; i < buttonPropertiesForBalls.Count; i++)
        {
            var buttonProp = buttonPropertiesForBalls[i];

            buttonProp.myButton = ballsButtonsContent.GetChild(i).GetComponent<Button>();
            buttonProp.buttonImage = ballButtonImages[i];
            buttonProp.itemPrefab = ballsItemsPrefabs[i];
            buttonProp.price = ballPrice;

            buttonProp.myButton.transform.GetChild(0).GetComponent<Image>().sprite = buttonProp.buttonImage;
            buttonProp.myButton.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text = buttonProp.price.ToString();
        }

        //for roads.
        for (int i = 0; i < buttonPropertiesForRoads.Count; i++)
        {
            var buttonProp = buttonPropertiesForRoads[i];

            buttonProp.myButton = roadButtonsContent.GetChild(i).GetComponent<Button>();

            buttonProp.price = roadPrice;

            buttonProp.buttonImage = roadButtonsImages[i];
            buttonProp.nightModeButtonImage = roadNightModeButtonImages[i];

            buttonProp.roadIndex = i;

            if (sceneSwitcher.isNightMode)
            {
                buttonProp.myButton.transform.GetChild(0).GetComponent<RawImage>().texture = buttonProp.nightModeButtonImage;
            }
            else
            {
                buttonProp.myButton.transform.GetChild(0).GetComponent<RawImage>().texture = buttonProp.buttonImage;
            }
            buttonProp.myButton.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text = buttonProp.price.ToString();
        }

        LoadPurchasedBallItem();
        LoadPurchasedRoadItem();
    }

    public void ButtonClicked(int i)
    {
        var buttonProp = buttonPropertiesForBalls[i];

        if (buttonProp.isPurchased)
        {
            if (buttonProp.isSelected)
            {
                return;
            }
            else
            {
                SwitchBetweenPurchasedItems(i);
                PlayButtonSelectSound();
                return;
            }
        }

        selectedButtonIndex = i; 

        for (int x = 0; x < ballCollectionPreview.transform.childCount; x++)
        {
            ballCollectionPreview.transform.GetChild(x).gameObject.SetActive(false);
        }

        previewPanelPriceText.text = buttonProp.price.ToString();
        ballCollectionPreview.transform.GetChild(i).gameObject.SetActive(true);

        previewPanel.SetActive(true);
        threeD_UICamera.SetActive(true);

        PlayButtonSelectSound();
    }

    public void BuyItem(int i)
    {
        i = selectedButtonIndex;
        var buttonProp = buttonPropertiesForBalls[i];

        if(player.collectedCoin >= buttonProp.price)
        {
            player.collectedCoin -= buttonProp.price;
            player.UpdateCoinUI();

            buttonProp.isPurchased = true;
            SwitchBetweenPurchasedItems(i);

            previewPanel.SetActive(false);
            threeD_UICamera.SetActive(false);

            SavePurchasedBallItem();

            PlayBuyItemSound();
        }
        else
        {
            DoNotHaveEnoughGems();
        }
    }

    void SwitchBetweenPurchasedItems(int currentSelectedItem)
    {
        for (int i = 0; i < buttonPropertiesForBalls.Count; i++)
        {
            var buttonProp = buttonPropertiesForBalls[i];

            if (i == currentSelectedItem)
            {
                buttonProp.isSelected = true;
                buttonProp.myButton.transform.GetChild(1).GetChild(0).gameObject.SetActive(false); //Disable Price Text.
                buttonProp.myButton.transform.GetChild(1).GetChild(1).gameObject.SetActive(true); //Enable True Mark.
                shoot.ballPrefab = buttonProp.itemPrefab;
                DataSaveManager.SaveInt("LSBI", i); //shortcut for last selected ball item.
            }
            else
            {
                buttonProp.isSelected = false;
                buttonProp.myButton.transform.GetChild(1).GetChild(1).gameObject.SetActive(false); //True Mark.
            }
        }
    }

    void SavePurchasedBallItem()
    {
        for (int i = 0; i < buttonPropertiesForBalls.Count; i++)
        {
            var buttonProp = buttonPropertiesForBalls[i];

            if (buttonProp.isPurchased && !DataSaveManager.IsDataExist("PBI" + i)) //shortcut for purchased ball item.
            {
                DataSaveManager.SaveBoolean("PBI" + i, buttonProp.isPurchased);
            }
        }
    }

    void LoadPurchasedBallItem()
    {
        for (int i = 0; i < buttonPropertiesForBalls.Count; i++)
        {
            var buttonProp = buttonPropertiesForBalls[i];

            if (DataSaveManager.IsDataExist("PBI" + i))
            {
                buttonProp.isPurchased = DataSaveManager.LoadBoolean("PBI" + i);

                if (buttonProp.isPurchased)
                {
                    buttonProp.myButton.transform.GetChild(1).GetChild(0).gameObject.SetActive(false); //Disable Price Text.
                }
            }
            else
            {
                if (buttonProp.isPurchased && buttonProp.isSelected)
                {
                    SwitchBetweenPurchasedItems(i);
                }
            }

            if (DataSaveManager.IsDataExist("LSBI"))
            {
                SwitchBetweenPurchasedItems(DataSaveManager.LoadInt("LSBI"));
            }
        }
    }

    /*********************************************************************************************************/
    public void ButtonClickedRoad(int i)
    {
        var buttonProp = buttonPropertiesForRoads[i];

        if (buttonProp.isPurchased)
        {
            if (buttonProp.isSelected)
            {
                return;
            }
            else
            {
                SwitchBetweenPurchasedRoadItems(i);
                PlayButtonSelectSound();
                return;
            }
        }

        selectedButtonRoadIndex = i;

        if (sceneSwitcher.isNightMode)
        {
            previewPanelImageRoad.texture = buttonProp.nightModeButtonImage;
        }
        else
        {
            previewPanelImageRoad.texture = buttonProp.buttonImage;
        }

        previewPanelRoadPriceText.text = buttonProp.price.ToString();

        previewPanelForRoad.SetActive(true);

        PlayButtonSelectSound();
    }

    public void BuyRoadItem(int i)
    {
        i = selectedButtonRoadIndex;
        var buttonProp = buttonPropertiesForRoads[i];

        if (player.collectedCoin >= buttonProp.price)
        {
            player.collectedCoin -= buttonProp.price;
            player.UpdateCoinUI();

            buttonProp.isPurchased = true;
            SwitchBetweenPurchasedRoadItems(i);

            previewPanelForRoad.SetActive(false);

            SavePurchasedRoadItem();

            PlayBuyItemSound();
        }
        else
        {
            DoNotHaveEnoughGems();
        }
    }

    void SwitchBetweenPurchasedRoadItems(int currentSelectedItem)
    {
        for (int i = 0; i < buttonPropertiesForRoads.Count; i++)
        {
            var buttonProp = buttonPropertiesForRoads[i];

            if (i == currentSelectedItem)
            {
                buttonProp.isSelected = true;
                buttonProp.myButton.transform.GetChild(1).GetChild(0).gameObject.SetActive(false); //Disable Price Text.
                buttonProp.myButton.transform.GetChild(1).GetChild(1).gameObject.SetActive(true); //Enable True Mark.
                sceneSwitcher.ApplyTheChanges(buttonProp.roadIndex);
                DataSaveManager.SaveInt("LSRI", i); //shortcut for last selected Road item.
            }
            else
            {
                buttonProp.isSelected = false;
                buttonProp.myButton.transform.GetChild(1).GetChild(1).gameObject.SetActive(false); //True Mark.
            }
        }
    }

    void SavePurchasedRoadItem()
    {
        for (int i = 0; i < buttonPropertiesForRoads.Count; i++)
        {
            var buttonProp = buttonPropertiesForRoads[i];

            if (buttonProp.isPurchased && !DataSaveManager.IsDataExist("PRI" + i)) //shortcut for purchased road item.
            {
                DataSaveManager.SaveBoolean("PRI" + i, buttonProp.isPurchased);
            }
        }
    }

    void LoadPurchasedRoadItem()
    {
        for (int i = 0; i < buttonPropertiesForRoads.Count; i++)
        {
            var buttonProp = buttonPropertiesForRoads[i];

            if (DataSaveManager.IsDataExist("PRI" + i))
            {
                buttonProp.isPurchased = DataSaveManager.LoadBoolean("PRI" + i);

                if (buttonProp.isPurchased)
                {
                    buttonProp.myButton.transform.GetChild(1).GetChild(0).gameObject.SetActive(false); //Disable Price Text.
                }
            }
            else
            {
                if (buttonProp.isPurchased && buttonProp.isSelected)
                {
                    SwitchBetweenPurchasedRoadItems(i);
                }
            }

            if (DataSaveManager.IsDataExist("LSRI"))
            {
                SwitchBetweenPurchasedRoadItems(DataSaveManager.LoadInt("LSRI"));
            }
        }
    }

    void DoNotHaveEnoughGems()
    {
        StartCoroutine(NoEnoughGemsPanel());
    }

    IEnumerator NoEnoughGemsPanel()
    {
        threeD_UICamera.SetActive(false);
        noEnoughGemsPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        if (previewPanel.activeInHierarchy)
        {
            threeD_UICamera.SetActive(true);
        }
        noEnoughGemsPanel.SetActive(false);
    }

    void PlayButtonSelectSound()
    {
        audioSource.clip = buttonSelectSound;
        audioSource.Play();
    }

    void PlayBuyItemSound()
    {
        audioSource.clip = buyItemSound;
        audioSource.Play();
    }
}

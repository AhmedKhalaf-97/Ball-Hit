using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;

public class StoreManager : MonoBehaviour
{
    public Player player;

    Purchaser purchaser;
    AdsRewardsAndPurchasingPanel adsRewardsAndPurchasingPanel;

    public List<StoreButtonProperties> storeButtonProperties = new List<StoreButtonProperties>();

    [System.Serializable]
    public class StoreButtonProperties
    {
        public Button myButton;
        public int purchasedGemCount;
        public Sprite buttonImage;
        public float price;
        public string productID;
    }

    void Start()
    {
        purchaser = GetComponent<Purchaser>();
        adsRewardsAndPurchasingPanel = GetComponent<AdsRewardsAndPurchasingPanel>();

        UpdateStoreButtonPropertiesUI();

        InvokeRepeating("UpdatePricesFromStoreController", 5f, 5f);
    }

    void UpdateStoreButtonPropertiesUI()
    {
        for (int i = 0; i < storeButtonProperties.Count; i++)
        {
            var buttonProp = storeButtonProperties[i];
            buttonProp.myButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = string.Format("{0:0,0}", buttonProp.purchasedGemCount);
            buttonProp.myButton.transform.GetChild(1).GetComponent<Image>().sprite = buttonProp.buttonImage;
            //buttonProp.myButton.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = buttonProp.price.ToString() + " USD";
        }
    }

    public void BuyItem(int i)
    {
        var buttonProp = storeButtonProperties[i];

        purchaser.BuyConsumable(buttonProp.productID);
    }

    public void ItemPurchased(string productID)
    {
        for (int i = 0; i < storeButtonProperties.Count; i++)
        {
            var buttonProp = storeButtonProperties[i];

            if (buttonProp.productID == productID)
            {
                player.collectedCoin += buttonProp.purchasedGemCount;

                adsRewardsAndPurchasingPanel.ShowPurchasingCompletedMessage(string.Format("{0:0,0}", buttonProp.purchasedGemCount) + " Gems");
            }
        }
    }

    void UpdatePricesFromStoreController()
    {
        for (int i = 0; i < storeButtonProperties.Count; i++)
        {
            var buttonProp = storeButtonProperties[i];

            if (purchaser.IsInitialized())
            {
                buttonProp.myButton.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = purchaser.m_StoreController.products.WithID(buttonProp.productID).metadata.localizedPrice
                + " " + purchaser.m_StoreController.products.WithID(buttonProp.productID).metadata.isoCurrencyCode;
            }
        }

        if(purchaser.m_StoreController.products.WithID(storeButtonProperties[0].productID).metadata.localizedPrice != 0
            && purchaser.m_StoreController.products.WithID(storeButtonProperties[(storeButtonProperties.Count - 1)].productID).metadata.localizedPrice != 0)
        {
            CancelInvoke();
        }
    }
}
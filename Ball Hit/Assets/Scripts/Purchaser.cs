using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;


public class Purchaser : MonoBehaviour, IStoreListener
{
    public IStoreController m_StoreController;          
    private static IExtensionProvider m_StoreExtensionProvider;

    public static string[] kProductsIDsConsumable =
        { "com.bantergames.ballhit.25kgems",
        "com.bantergames.ballhit.55kgems",
        "com.bantergames.ballhit.80kgems",
        "com.bantergames.ballhit.150kgems"};

    public static string kProductIDNonConsumable = "com.bantergames.ballhit.unlimitedballs";

    StoreManager storeManager;
    UnityAds unityAds;

    void Start()
    {
        storeManager = GetComponent<StoreManager>();
        unityAds = GetComponent<UnityAds>();

        if (m_StoreController == null)
        {
            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {
        if (IsInitialized())
        {
            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        foreach(string productID in kProductsIDsConsumable)
        {
            builder.AddProduct(productID, ProductType.Consumable);
        }

        builder.AddProduct(kProductIDNonConsumable, ProductType.NonConsumable);

        UnityPurchasing.Initialize(this, builder);
    }


    public bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }


    public void BuyConsumable(string productID)
    {
        BuyProductID(productID);
    }


    public void BuyNonConsumable()
    {
        BuyProductID(kProductIDNonConsumable);
    }


    void BuyProductID(string productId)
    {
        if (!NetworkAvailability.instance.IsConnected())
        {
            return;
        }

        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);

            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }


    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");

        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        for (int i = 0; i < kProductsIDsConsumable.Length; i++)
        {
            if (String.Equals(args.purchasedProduct.definition.id, kProductsIDsConsumable[i], StringComparison.Ordinal))
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                storeManager.ItemPurchased(kProductsIDsConsumable[i]);
            }
        }

        if (String.Equals(args.purchasedProduct.definition.id, kProductIDNonConsumable, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            Shoot._shoot.PurchasedUnlimitedBalls();
            if (!unityAds.isAdsDisabled)
            {
                unityAds.isAdsDisabled = true;
                DataSaveManager.SaveBoolean("NA", true);
            }
        }

        return PurchaseProcessingResult.Complete;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}
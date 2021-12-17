using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateInfoUI : MonoBehaviour
{
    public Player player;
    public PurchaseManager purchaseManager;

    void OnEnable()
    {
        purchaseManager.OnDisablePurchacePanel();

        player.UpdateAllInfoUI();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdsRewardsAndPurchasingPanel : MonoBehaviour
{
    public Player player;

    public int gemsRewardCount = 100;
    public int keysRewardCount = 1;

    public GameObject parentPanel;
    public GameObject purchasingCompletedPanel;
    public GameObject adsRewardPanel;
    public Text purchasingCompletedMessageText;
    public Text rewardedGemsText;
    public Text rewardedKeysText;

    public void ApplyTheRewards()
    {
        player.collectedCoin += gemsRewardCount;
        player.heartCount += keysRewardCount;

        player.UpdateAllInfoUI();

        UpdateRewardsUI();

        purchasingCompletedPanel.SetActive(false);
        adsRewardPanel.SetActive(true);
        parentPanel.SetActive(true);
    }

    void UpdateRewardsUI()
    {
        rewardedGemsText.text = gemsRewardCount.ToString();
        rewardedKeysText.text = keysRewardCount.ToString();
    }

    public void ShowPurchasingCompletedMessage(string message)
    {
        purchasingCompletedMessageText.text = "Congratulations! You Have Purchased " + message;

        adsRewardPanel.SetActive(false);
        purchasingCompletedPanel.SetActive(true);
        parentPanel.SetActive(true);
    }
}

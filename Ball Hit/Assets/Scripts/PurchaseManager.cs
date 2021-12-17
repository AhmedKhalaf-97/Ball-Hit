using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class PurchaseManager : MonoBehaviour
{
    public Player player;
    public MainMenu mainMenu;
    Shoot shoot;

    public GameObject purchasePanel;
    public GameObject mainMenuPanel;
    public GameObject noEnoughGemsPanel;

    public Text[] unlimitedBallsPriceTexts;
    Purchaser purchaser;

    [Header("Audio Stuff")]
    public AudioSource audioSource;

    [Header("Gems Info")]
    public Animator gemsTextAnimator;
    public Text gemsInfoText;

    [Header("Ball Purchase Info")]
    public Animator ballTextAnimator;
    public Text ballInfoText;
    public List<BallPurchaseButtons> ballPurchaseButtonsInfo = new List<BallPurchaseButtons>();

    [Header("Key Purchase Info")]
    public Animator keyTextAnimator;
    public Text keyInfoText;
    public List<KeyPurchaseButtons> keyPurchaseButtonsInfo = new List<KeyPurchaseButtons>();

    [Header("SlowMotion Purchase Info")]
    public Animator slowMotionTextAnimator;
    public Text slowMotionInfoText;
    public List<SlowMotionPurchaseButtons> slowMotionPurchaseButtonsInfo = new List<SlowMotionPurchaseButtons>();

    [Header("Shields Purchase Info")]
    public Animator shieldsTextAnimator;
    public Text shieldsInfoText;
    public List<ShieldsPurchaseButtons> shieldsPurchaseButtonsInfo = new List<ShieldsPurchaseButtons>();

    [System.Serializable]
    public class BallPurchaseButtons
    {
        public int purchasedBallCount;
        public int requiredGems;

        public Text purchsedBallCountText;
        public Text ballRequiredGemsText;
    }

    [System.Serializable]
    public class KeyPurchaseButtons
    {
        public int purchasedKeyCount;
        public int requiredGems;


        public Text purchsedKeyCountText;
        public Text keyRequiredGemsText;
    }

    [System.Serializable]
    public class SlowMotionPurchaseButtons
    {
        public int purchasedSlowMotionCount;
        public int requiredGems;

        public Text purchsedSlowMotionCountText;
        public Text slowMotionRequiredGemsText;
    }

    [System.Serializable]
    public class ShieldsPurchaseButtons
    {
        public int purchasedShieldsCount;
        public int requiredGems;

        public Text purchsedShieldsCountText;
        public Text shieldsRequiredGemsText;
    }

    void OnEnable()
    {
        purchaser = GetComponent<Purchaser>();

        shoot = mainMenu.shoot;

        UpdatePurchaseButtonsUI();
    }

    void Start()
    {
        InvokeRepeating("UpdateUnlimitedBallsTextFromStoreController", 5f, 5f);
    }

    void UpdatePurchaseButtonsUI()
    {
        for (int i = 0; i < ballPurchaseButtonsInfo.Count; i++)
        {
            var ballInfo = ballPurchaseButtonsInfo[i];
            ballInfo.purchsedBallCountText.text = ballInfo.purchasedBallCount.ToString();
            ballInfo.ballRequiredGemsText.text = string.Format("{0:0,0}", ballInfo.requiredGems);
        }

        for (int i = 0; i < keyPurchaseButtonsInfo.Count; i++)
        {
            var keyInfo = keyPurchaseButtonsInfo[i];
            keyInfo.purchsedKeyCountText.text = keyInfo.purchasedKeyCount.ToString();
            keyInfo.keyRequiredGemsText.text = string.Format("{0:0,0}", keyInfo.requiredGems);
        }

        for (int i = 0; i < slowMotionPurchaseButtonsInfo.Count; i++)
        {
            var slowMotionInfo = slowMotionPurchaseButtonsInfo[i];
            slowMotionInfo.purchsedSlowMotionCountText.text = slowMotionInfo.purchasedSlowMotionCount.ToString();
            slowMotionInfo.slowMotionRequiredGemsText.text = string.Format("{0:0,0}", slowMotionInfo.requiredGems);
        }

        for (int i = 0; i < shieldsPurchaseButtonsInfo.Count; i++)
        {
            var shieldsInfo = shieldsPurchaseButtonsInfo[i];
            shieldsInfo.purchsedShieldsCountText.text = shieldsInfo.purchasedShieldsCount.ToString();
            shieldsInfo.shieldsRequiredGemsText.text = string.Format("{0:0,0}", shieldsInfo.requiredGems);
        }
    }

    public void PurchaseBall(int buttonIndex)
    {
        var ballInfo = ballPurchaseButtonsInfo[buttonIndex];

        if (player.collectedCoin >= ballInfo.requiredGems)
        {
            player.collectedCoin -= ballInfo.requiredGems;
            StartCoroutine(TextAnimation(gemsTextAnimator, gemsInfoText, player.collectedCoin, 200));

            shoot.ballsCount += ballInfo.purchasedBallCount;
            StartCoroutine(TextAnimation(ballTextAnimator, ballInfoText, shoot.ballsCount, 10));

            PlayAudioWhenPurchase();
        }
        else
        {
            DoNotHaveEnoughGems();
        }
    }

    public void PurchaseKey(int buttonIndex)
    {
        var KeyInfo = keyPurchaseButtonsInfo[buttonIndex];

        if (player.collectedCoin >= KeyInfo.requiredGems)
        {
            player.collectedCoin -= KeyInfo.requiredGems;
            StartCoroutine(TextAnimation(gemsTextAnimator, gemsInfoText, player.collectedCoin, 200));

            player.heartCount += KeyInfo.purchasedKeyCount;
            StartCoroutine(TextAnimation(keyTextAnimator, keyInfoText, player.heartCount, 10));

            PlayAudioWhenPurchase();
        }
        else
        {
            DoNotHaveEnoughGems();
        }
    }

    public void PurchaseSlowMotion(int buttonIndex)
    {
        var slowMotionInfo = slowMotionPurchaseButtonsInfo[buttonIndex];

        if (player.collectedCoin >= slowMotionInfo.requiredGems)
        {
            player.collectedCoin -= slowMotionInfo.requiredGems;
            StartCoroutine(TextAnimation(gemsTextAnimator, gemsInfoText, player.collectedCoin, 200));

            player.slowMotionCount += slowMotionInfo.purchasedSlowMotionCount;
            StartCoroutine(TextAnimation(slowMotionTextAnimator, slowMotionInfoText, player.slowMotionCount, 10));

            PlayAudioWhenPurchase();
        }
        else
        {
            DoNotHaveEnoughGems();
        }
    }

    public void PurchaseShields(int buttonIndex)
    {
        var shieldsInfo = shieldsPurchaseButtonsInfo[buttonIndex];

        if (player.collectedCoin >= shieldsInfo.requiredGems)
        {
            player.collectedCoin -= shieldsInfo.requiredGems;
            StartCoroutine(TextAnimation(gemsTextAnimator, gemsInfoText, player.collectedCoin, 200));

            player.shieldCount += shieldsInfo.purchasedShieldsCount;
            player.SaveShieldCount();
            StartCoroutine(TextAnimation(shieldsTextAnimator, shieldsInfoText, player.shieldCount, 10));

            PlayAudioWhenPurchase();
        }
        else
        {
            DoNotHaveEnoughGems();
        }
    }

    float duration = 2f;
    float lerpSpeed = 0;
    IEnumerator TextAnimation(Animator textAnimator, Text textToAnimate, int countToReach, int speedInt)
    {
        lerpSpeed = 0;
        int currnetCount = int.Parse(Regex.Replace(textToAnimate.text, @"[^\d]", ""));

        if(countToReach != currnetCount)
        {
            if (!textAnimator.enabled)
            {
                textAnimator.enabled = true;
            }

            while (currnetCount != countToReach)
            {
                lerpSpeed += Time.deltaTime / duration;
                currnetCount = (int)Mathf.Lerp(currnetCount, countToReach, lerpSpeed);
                textToAnimate.text = string.Format("{0:0,0}", currnetCount);

                if (currnetCount == countToReach)
                {
                    lerpSpeed = 0;

                    textAnimator.enabled = false;
                    textToAnimate.transform.localScale = Vector3.one;
                    yield break;
                }
                yield return null;
            }
        }
    }

    public void OnDisablePurchacePanel()
    {
        StopAllCoroutines();

        gemsTextAnimator.enabled = false;
        ballTextAnimator.enabled = false;
        keyTextAnimator.enabled = false;
        slowMotionTextAnimator.enabled = false;
        shieldsTextAnimator.enabled = false;

        gemsInfoText.transform.localScale = Vector3.one;
        ballInfoText.transform.localScale = Vector3.one;
        keyInfoText.transform.localScale = Vector3.one;
        slowMotionInfoText.transform.localScale = Vector3.one;
        shieldsInfoText.transform.localScale = Vector3.one;
    }

    void DoNotHaveEnoughGems()
    {
        StartCoroutine(NoEnoughGemsPanel());
    }

    IEnumerator NoEnoughGemsPanel()
    {
        noEnoughGemsPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        noEnoughGemsPanel.SetActive(false);
    }

    void PlayAudioWhenPurchase()
    {
        audioSource.Play();
    }

    void UpdateUnlimitedBallsTextFromStoreController()
    {
        if(purchaser.IsInitialized())
        {
            if(purchaser.m_StoreController.products.WithID("com.bantergames.ballhit.unlimitedballs").metadata.localizedPrice != 0)
            {
                foreach(Text childText in unlimitedBallsPriceTexts)
                {
                    childText.text = purchaser.m_StoreController.products.WithID("com.bantergames.ballhit.unlimitedballs").metadata.localizedPrice
                    + " " + purchaser.m_StoreController.products.WithID("com.bantergames.ballhit.unlimitedballs").metadata.isoCurrencyCode;
                }

                CancelInvoke();
            }
        }
    }
}

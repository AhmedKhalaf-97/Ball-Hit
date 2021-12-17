using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class ChallengeMode : MonoBehaviour
{
    [Header("Challenge Setting Variables")]
    public int wantedChallengesNumber = 22;
    List<int> randomIndexList = new List<int>();

    [Header("Skip Panel Stuff")]
    public int skipPrice = 20000;
    public Text skipPriceText;
    public GameObject noEnoughGemsPanel;

    [Header("Scripts")]
    public Player player;
    public MainMenu mainMenu;

    public static ChallengeMode _challengeMode;

    [Header("MainMenu UI")]
    public bool isRewardsShowen = true;
    public GameObject rewardsAndNextChallengePanel;
    public Text challengeNumberText;
    public Text challengeInfoText;
    public Slider challengeProgressSlider;
    public Text currentProgressNumText;
    public Text maxProgressNumText;

    public Transform completedChallengePanel;
    public Transform nextChallengePanel;
    public Transform rewardsContent;

    public Text allCompletedChallengesText;
    public Text allCompletedChallengesInNextPanelText;

    [Header("HUD UI")]
    public Text timerText;
    public Text smashedObstaclesCountText;

    public GameObject completedChallengeNotificationPanel;


    [System.Serializable]
    public class Challenge
    {
        [Space(10f)]
        [Multiline(2)]
        public string challengeName;
        [Space(10f)]
        [Header("Mode 1_Score ## in 1 Run")]
        public bool isMode1;
        public Mode1Variables mode1Variables;
        [Space(10f)]

        [Header("Mode 2_Score ## in ## a row")]
        public bool isMode2;
        public Mode2Variables mode2Variables;
        [Space(10f)]

        [Header("Mode 3_Score ## in total")]
        public bool isMode3;
        public Mode3Variables mode3Variables;
        [Space(10f)]

        [Header("Mode 4_Score ## in ## run")]
        public bool isMode4;
        public Mode4Variables mode4Variables;
        [Space(10f)]

        [Header("Mode 5_Score between ## and ## in 1 Run")]
        public bool isMode5;
        public Mode5Variables mode5Variables;
        [Space(10f)]

        [Header("Mode 6_Score between ## and ## in ## a row")]
        public bool isMode6;
        public Mode6Variables mode6Variables;
        [Space(10f)]

        [Header("Mode 7_Score between ## and ## in ## run")]
        public bool isMode7;
        public Mode7Variables mode7Variables;
        [Space(10f)]

        [Header("Mode 8_Score ## without smashing any obstacle")]
        public bool isMode8;
        public Mode8Variables mode8Variables;
        [Space(10f)]

        [Header("Mode 9_Score ## without hit by any obstacle")]
        public bool isMode9;
        public Mode9Variables mode9Variables;
        [Space(10f)]

        [Header("Mode 10_Score ## without hit by or smashing any obstacle")]
        public bool isMode10;
        public Mode10Variables mode10Variables;
        [Space(10f)]

        [Header("Mode 11_Collect ## gems in one run")]
        public bool isMode11;
        public Mode11Variables mode11Variables;
        [Space(10f)]

        [Header("Mode 12_Collect ## gems ## in a row")]
        public bool isMode12;
        public Mode12Variables mode12Variables;
        [Space(10f)]

        [Header("Mode 13_Collect ## gems in total")]
        public bool isMode13;
        public Mode13Variables mode13Variables;
        [Space(10f)]

        [Header("Mode 14_Collect ## gems in ## run")]
        public bool isMode14;
        public Mode14Variables mode14Variables;
        [Space(10f)]

        [Header("Mode 15_Collect gems between ## and ## in one run")]
        public bool isMode15;
        public Mode15Variables mode15Variables;
        [Space(10f)]

        [Header("Mode 16_Collect gems between ## and ## in a row")]
        public bool isMode16;
        public Mode16Variables mode16Variables;
        [Space(10f)]

        [Header("Mode 17_Collect gems between ## and ## in ## run")]
        public bool isMode17;
        public Mode17Variables mode17Variables;
        [Space(10f)]

        [Header("Mode 18_Collect ## gems without smashing any obstacle")]
        public bool isMode18;
        public Mode18Variables mode18Variables;
        [Space(10f)]

        [Header("Mode 19_Smash ## obstacles in ## time in one run")]
        public bool isMode19;
        public Mode19Variables mode19Variables;
        [Space(10f)]

        [Header("Mode 20_Smash ## obstacles in ## time in ## a row")]
        public bool isMode20;
        public Mode20Variables mode20Variables;
        [Space(10f)]

        [Header("Mode 21_Smash ## obstacles in ## time in ## run")]
        public bool isMode21;
        public Mode21Variables mode21Variables;
        [Space(10f)]

        [Header("Mode 22_Don't smash any obstacles for ## time")]
        public bool isMode22;
        public Mode22Variables mode22Variables;
    }

    [System.Serializable]
    public class Mode1Variables
    {
        public float requiredScore;
        public int rewardGems;
        public int rewardKeys;
        public int rewardSM;
    }

    [System.Serializable]
    public class Mode2Variables
    {
        public float requiredScore;
        public int rowTimes;
        public int rewardGems;
        public int rewardKeys;
        public int rewardSM;
    }

    [System.Serializable]
    public class Mode3Variables
    {
        public float requiredScore;
        public int rewardGems;
        public int rewardKeys;
        public int rewardSM;
    }

    [System.Serializable]
    public class Mode4Variables
    {
        public float requiredScore;
        public int runTimes;
        public int rewardGems;
        public int rewardKeys;
        public int rewardSM;
    }

    [System.Serializable]
    public class Mode5Variables
    {
        public float minRequiredScore;
        public float maxRequiredScore;
        public int rewardGems;
        public int rewardKeys;
        public int rewardSM;
    }

    [System.Serializable]
    public class Mode6Variables
    {
        public float minRequiredScore;
        public float maxRequiredScore;
        public int rowTimes;
        public int rewardGems;
        public int rewardKeys;
        public int rewardSM;
    }

    [System.Serializable]
    public class Mode7Variables
    {
        public float minRequiredScore;
        public float maxRequiredScore;
        public int runTimes;
        public int rewardGems;
        public int rewardKeys;
        public int rewardSM;
    }

    [System.Serializable]
    public class Mode8Variables
    {
        public float requiredScore;
        public int rewardGems;
        public int rewardKeys;
        public int rewardSM;
    }

    [System.Serializable]
    public class Mode9Variables
    {
        public float requiredScore;
        public int rewardGems;
        public int rewardKeys;
        public int rewardSM;
    }

    [System.Serializable]
    public class Mode10Variables
    {
        public float requiredScore;
        public int rewardGems;
        public int rewardKeys;
        public int rewardSM;
    }

    [System.Serializable]
    public class Mode11Variables
    {
        public int requiredGemsCount;
        public int rewardGems;
        public int rewardKeys;
        public int rewardSM;
    }

    [System.Serializable]
    public class Mode12Variables
    {
        public int requiredGemsCount;
        public int rowTimes;
        public int rewardGems;
        public int rewardKeys;
        public int rewardSM;
    }

    [System.Serializable]
    public class Mode13Variables
    {
        public int requiredGemsCount;
        public int rewardGems;
        public int rewardKeys;
        public int rewardSM;
    }

    [System.Serializable]
    public class Mode14Variables
    {
        public float requiredGemsCount;
        public int runTimes;
        public int rewardGems;
        public int rewardKeys;
        public int rewardSM;
    }

    [System.Serializable]
    public class Mode15Variables
    {
        public int minRequiredGemsCount;
        public int maxRequiredGemsCount;
        public int rewardGems;
        public int rewardKeys;
        public int rewardSM;
    }

    [System.Serializable]
    public class Mode16Variables
    {
        public int minRequiredGemsCount;
        public int maxRequiredGemsCount;
        public int rowTimes;
        public int rewardGems;
        public int rewardKeys;
        public int rewardSM;
    }

    [System.Serializable]
    public class Mode17Variables
    {
        public int minRequiredGemsCount;
        public int maxRequiredGemsCount;
        public int runTimes;
        public int rewardGems;
        public int rewardKeys;
        public int rewardSM;
    }

    [System.Serializable]
    public class Mode18Variables
    {
        public float requiredGemsCount;
        public int rewardGems;
        public int rewardKeys;
        public int rewardSM;
    }

    [System.Serializable]
    public class Mode19Variables
    {
        public float requiredSmashedObstacles;
        public float timerCountdown;
        public int rewardGems;
        public int rewardKeys;
        public int rewardSM;
    }

    [System.Serializable]
    public class Mode20Variables
    {
        public float requiredSmashedObstacles;
        public float timerCountdown;
        public int rowTimes;
        public int rewardGems;
        public int rewardKeys;
        public int rewardSM;
    }

    [System.Serializable]
    public class Mode21Variables
    {
        public float requiredSmashedObstacles;
        public float timerCountdown;
        public int runTimes;
        public int rewardGems;
        public int rewardKeys;
        public int rewardSM;
    }

    [System.Serializable]
    public class Mode22Variables
    {
        public float timerCountdown;
        public int rewardGems;
        public int rewardKeys;
        public int rewardSM;
    }

    [Range(1, 100)]
    public int challengeIndex;

    public float timeToStart = 2f;

    public bool isChallengeStart;

    public int challengePartIndex = 0;
    public float challengeCumulativeScore;

    public int latestCollectedCoin;
    public float challengeCumulativeGemsCount;

    public int rewardedGems;
    public int rewardedKeys;
    public int rewardedSM;

    [Header("")]
    public List<Challenge> challengeList = new List<Challenge>();

    void OnEnable()
    {
        SetInitialReferences();

        challengeIndex = DataSaveManager.LoadInt("CI");

        if(challengeIndex == 0)
        {
            challengeIndex = 1;
        }

        UpdateChalengeUIOnEnable();
        UpdateSkipPanelUI();
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    void Start()
    {

    }

    void SetInitialReferences()
    {
        _challengeMode = this;
    }

    IEnumerator Mode1(float requiredScore)
    {
        yield return new WaitForSeconds(timeToStart);

        while (true)
        {
            if ((player.distanceTraveled * 10f) >= requiredScore)
            {
                ChallengeCompleted();
                yield break;
            }

            yield return null;
        }
    }

    IEnumerator Mode2(float requiredScore, int rowTimes)
    {
        yield return new WaitForSeconds(timeToStart);

        while (true)
        {
            if ((player.distanceTraveled * 10f) >= requiredScore)
            {
                ChallengePartiallyCompleted(rowTimes);
                yield break;
            }

            yield return null;
        }
    }

    IEnumerator Mode3(float requiredScore)
    {
        yield return new WaitForSeconds(timeToStart);

        while (true)
        {
        if ((player.distanceTraveled * 10f) >= requiredScore)
            {
                ChallengeCompleted();
                challengeCumulativeScore = 0;
                yield break;
            }

            yield return null;
        }
    }

    IEnumerator Mode4(float requiredScore, int runTimes)
    {
        yield return new WaitForSeconds(timeToStart);

        while (true)
        {
            if ((player.distanceTraveled * 10f) >= requiredScore)
            {
                ChallengePartiallyCompleted(runTimes);
                yield break;
            }

            yield return null;
        }
    }

    IEnumerator Mode5(float minRequiredScore, float maxRequiredScore)
    {
        yield return new WaitForSeconds(timeToStart);

        //while (true)
        //{
        //    if ((player.distanceTraveled * 10f) >= requiredScore)
        //    {
        //        ChallengeCompleted();
        //        yield break;
        //    }

        //    yield return null;
        //}
    }

    IEnumerator Mode6(float minRequiredScore, float maxRequiredScore, int rowTimes)
    {
        yield return new WaitForSeconds(timeToStart);

        //while (true)
        //{
        //    if ((player.distanceTraveled * 10f) >= requiredScore)
        //    {
        //        ChallengePartiallyCompleted(rowTimes);
        //        yield break;
        //    }

        //    yield return null;
        //}
    }

    IEnumerator Mode7(float minRequiredScore, float maxRequiredScore, int runTimes)
    {
        yield return new WaitForSeconds(timeToStart);

        //while (true)
        //{
        //    if ((player.distanceTraveled * 10f) >= requiredScore)
        //    {
        //        ChallengePartiallyCompleted(runTimes);
        //        yield break;
        //    }

        //    yield return null;
        //}
    }

    IEnumerator Mode8(float requiredScore)
    {
        yield return new WaitForSeconds(timeToStart);

        while (true)
        {
            if ((player.distanceTraveled * 10f) >= requiredScore && !SceneSwitcher.isObstacleSmashed)
            {
                ChallengeCompleted();
                yield break;
            }

            yield return null;
        }
    }

    IEnumerator Mode9(float requiredScore)
    {
        yield return new WaitForSeconds(timeToStart);

        while (true)
        {
            if ((player.distanceTraveled * 10f) >= requiredScore && player.livesCount == player.livesLeft)
            {
                ChallengeCompleted();
                yield break;
            }

            yield return null;
        }
    }

    IEnumerator Mode10(float requiredScore)
    {
        yield return new WaitForSeconds(timeToStart);

        while (true)
        {
            if ((player.distanceTraveled * 10f) >= requiredScore && player.livesCount == player.livesLeft && !SceneSwitcher.isObstacleSmashed)
            {
                ChallengeCompleted();
                yield break;
            }

            yield return null;
        }
    }

    IEnumerator Mode11(float requiredGemsCount)
    {
        int gemsInStart = player.collectedCoin;

        yield return new WaitForSeconds(timeToStart);

        while (true)
        {
            latestCollectedCoin = (player.collectedCoin - gemsInStart);
            if (latestCollectedCoin >= requiredGemsCount)
            {
                ChallengeCompleted();
                yield break;
            }

            yield return null;
        }
    }

    IEnumerator Mode12(float requiredGemsCount, int rowTimes)
    {
        int gemsInStart = player.collectedCoin;

        yield return new WaitForSeconds(timeToStart);

        while (true)
        {
            latestCollectedCoin = (player.collectedCoin - gemsInStart);
            if (latestCollectedCoin >= requiredGemsCount)
            {
                ChallengePartiallyCompleted(rowTimes);
                yield break;
            }

            yield return null;
        }
    }

    IEnumerator Mode13(float requiredGemsCount)
    {
        latestCollectedCoin = 0;
        int gemsInStart = player.collectedCoin;

        yield return new WaitForSeconds(timeToStart);

        while (true)
        {
            latestCollectedCoin = (player.collectedCoin - gemsInStart);
            if (latestCollectedCoin >= requiredGemsCount)
            {
                ChallengeCompleted();
                challengeCumulativeScore = 0;
                yield break;
            }

            yield return null;
        }
    }

    IEnumerator Mode14(float requiredGemsCount, int runTimes)
    {
        int gemsInStart = player.collectedCoin;

        yield return new WaitForSeconds(timeToStart);

        while (true)
        {
            latestCollectedCoin = (player.collectedCoin - gemsInStart);
            if (latestCollectedCoin >= requiredGemsCount)
            {
                ChallengePartiallyCompleted(runTimes);
                yield break;
            }

            yield return null;
        }
    }

    IEnumerator Mode15(float minRequiredGemsCount, float maxRequiredGemsCount)
    {
        int gemsInStart = player.collectedCoin;

        yield return new WaitForSeconds(timeToStart);

        while (true)
        {
            latestCollectedCoin = (player.collectedCoin - gemsInStart);
            //if (latestCollectedCoin >= requiredGemsCount)
            //{
            //    ChallengeCompleted();
            //    yield break;
            //}

            yield return null;
        }
    }

    IEnumerator Mode16(float minRequiredGemsCount, float maxRequiredGemsCount, int rowTimes)
    {
        int gemsInStart = player.collectedCoin;

        yield return new WaitForSeconds(timeToStart);

        while (true)
        {
            latestCollectedCoin = (player.collectedCoin - gemsInStart);
            //    if (latestCollectedCoin >= requiredGemsCount)
            //    {
            //        ChallengePartiallyCompleted(rowTimes);
            //        yield break;
            //    }

            yield return null;
        }
    }

    IEnumerator Mode17(float minRequiredGemsCount, float maxRequiredGemsCount, int runTimes)
    {
        latestCollectedCoin = 0;
        int gemsInStart = player.collectedCoin;

        yield return new WaitForSeconds(timeToStart);

        while (true)
        {
            latestCollectedCoin = (player.collectedCoin - gemsInStart);
            //    if (latestCollectedCoin >= requiredGemsCount)
            //    {
            //        ChallengePartiallyCompleted(runTimes);
            //        yield break;
            //    }

            yield return null;
        }
    }

    IEnumerator Mode18(float requiredGemsCount)
    {
        int gemsInStart = player.collectedCoin;

        yield return new WaitForSeconds(timeToStart);

        while (true)
        {
            latestCollectedCoin = (player.collectedCoin - gemsInStart);
            if (latestCollectedCoin >= requiredGemsCount && !SceneSwitcher.isObstacleSmashed)
            {
                ChallengeCompleted();
                yield break;
            }

            yield return null;
        }
    }

    IEnumerator Mode19(float requiredSmashedObstacles, float timerCountdown)
    {
        bool isRunning = true;

        SceneSwitcher.smashedObstaclesCount = 0;
        SceneSwitcher.sceneSwitcher.UpdateSmashedObstaclesUI();
        smashedObstaclesCountText.gameObject.SetActive(true);
        timerText.text = string.Format("{0:00.00}", 0f);
        timerText.gameObject.SetActive(true);

        yield return new WaitForSeconds(timeToStart);

        float endTime = Time.time + timerCountdown;

        while (Time.time < endTime && timerCountdown > 0.1)
        {
            timerCountdown -= Time.deltaTime;
            timerText.text = string.Format("{0:00.00}", timerCountdown);

            if (SceneSwitcher.smashedObstaclesCount >= requiredSmashedObstacles)
            {
                ChallengeCompleted();

                isRunning = false;
                if (!isRunning)
                {
                    SceneSwitcher.smashedObstaclesCount = 0;
                    SceneSwitcher.sceneSwitcher.UpdateSmashedObstaclesUI();
                    timerText.text = string.Format("{0:00.00}", 0f);
                    yield return new WaitForSeconds(1.5f);
                    timerText.gameObject.SetActive(false);
                    smashedObstaclesCountText.gameObject.SetActive(false);
                }
                yield break;
            }

            yield return null;
        }
        SceneSwitcher.smashedObstaclesCount = 0;
        SceneSwitcher.sceneSwitcher.UpdateSmashedObstaclesUI();
        timerText.text = string.Format("{0:00.00}", 0f);
        yield return new WaitForSeconds(1.5f);
        timerText.gameObject.SetActive(false);
        smashedObstaclesCountText.gameObject.SetActive(false);
    }

    IEnumerator Mode20(float requiredSmashedObstacles, float timerCountdown, int rowTimes)
    {
        bool isRunning = true;

        SceneSwitcher.smashedObstaclesCount = 0;
        SceneSwitcher.sceneSwitcher.UpdateSmashedObstaclesUI();
        smashedObstaclesCountText.gameObject.SetActive(true);
        timerText.text = string.Format("{0:00.00}", 0f);
        timerText.gameObject.SetActive(true);

        yield return new WaitForSeconds(timeToStart);

        float endTime = Time.time + timerCountdown;

        while (Time.time < endTime && timerCountdown > 0.1)
        {
            timerCountdown -= Time.deltaTime;
            timerText.text = string.Format("{0:00.00}", timerCountdown);

            if (SceneSwitcher.smashedObstaclesCount >= requiredSmashedObstacles)
            {
                ChallengePartiallyCompleted(rowTimes);

                isRunning = false;
                if (!isRunning)
                {
                    //SceneSwitcher.smashedObstaclesCount = 0;
                    SceneSwitcher.sceneSwitcher.UpdateSmashedObstaclesUI();
                    timerText.text = string.Format("{0:00.00}", 0f);
                    yield return new WaitForSeconds(1.5f);
                    timerText.gameObject.SetActive(false);
                    smashedObstaclesCountText.gameObject.SetActive(false);
                }
                yield break;
            }

            yield return null;
        }
        //SceneSwitcher.smashedObstaclesCount = 0;
        SceneSwitcher.sceneSwitcher.UpdateSmashedObstaclesUI();
        timerText.text = string.Format("{0:00.00}", 0f);
        yield return new WaitForSeconds(1.5f);
        timerText.gameObject.SetActive(false);
        smashedObstaclesCountText.gameObject.SetActive(false);
    }

    IEnumerator Mode21(float requiredSmashedObstacles, float timerCountdown, int runTimes)
    {
        bool isRunning = true;

        SceneSwitcher.smashedObstaclesCount = 0;
        SceneSwitcher.sceneSwitcher.UpdateSmashedObstaclesUI();
        smashedObstaclesCountText.gameObject.SetActive(true);
        timerText.text = string.Format("{0:00.00}", 0f);
        timerText.gameObject.SetActive(true);

        yield return new WaitForSeconds(timeToStart);

        float endTime = Time.time + timerCountdown;

        while (Time.time < endTime && timerCountdown > 0.1)
        {
            timerCountdown -= Time.deltaTime;
            timerText.text = string.Format("{0:00.00}", timerCountdown);

            if (SceneSwitcher.smashedObstaclesCount >= requiredSmashedObstacles)
            {
                ChallengePartiallyCompleted(runTimes);

                isRunning = false;
                if (!isRunning)
                {
                    //SceneSwitcher.smashedObstaclesCount = 0;
                    SceneSwitcher.sceneSwitcher.UpdateSmashedObstaclesUI();
                    timerText.text = string.Format("{0:00.00}", 0f);
                    yield return new WaitForSeconds(1.5f);
                    timerText.gameObject.SetActive(false);
                    smashedObstaclesCountText.gameObject.SetActive(false);
                }
                yield break;
            }

            yield return null;
        }
        //SceneSwitcher.smashedObstaclesCount = 0;
        SceneSwitcher.sceneSwitcher.UpdateSmashedObstaclesUI();
        timerText.text = string.Format("{0:00.00}", 0f);
        yield return new WaitForSeconds(1.5f);
        timerText.gameObject.SetActive(false);
        smashedObstaclesCountText.gameObject.SetActive(false);
    }

    IEnumerator Mode22(float timerCountdown)
    {
        bool isRunning = true;

        //SceneSwitcher.smashedObstaclesCount = 0;
        //SceneSwitcher.sceneSwitcher.UpdateSmashedObstaclesUI();
        //smashedObstaclesCountText.gameObject.SetActive(true);

        timerText.text = string.Format("{0:00.00}", 0f);
        timerText.gameObject.SetActive(true);

        yield return new WaitForSeconds(timeToStart);

        float endTime = Time.time + timerCountdown;

        while (Time.time < endTime && timerCountdown > 0.1)
        {
            timerCountdown -= Time.deltaTime;
            timerText.text = string.Format("{0:00.00}", timerCountdown);

            if (SceneSwitcher.isObstacleSmashed)
            {
                ChallengeNotCompleted();

                isRunning = false;
                if (!isRunning)
                {
                    //SceneSwitcher.smashedObstaclesCount = 0;
                    //SceneSwitcher.sceneSwitcher.UpdateSmashedObstaclesUI();

                    timerText.text = string.Format("{0:00.00}", 0f);
                    yield return new WaitForSeconds(1.5f);
                    timerText.gameObject.SetActive(false);

                    //smashedObstaclesCountText.gameObject.SetActive(false);
                }
                yield break;
            }

            yield return null;
        }

        if (!SceneSwitcher.isObstacleSmashed)
        {
            ChallengeCompleted();
        }

        //SceneSwitcher.smashedObstaclesCount = 0;
        //SceneSwitcher.sceneSwitcher.UpdateSmashedObstaclesUI();

        timerText.text = string.Format("{0:00.00}", 0f);
        yield return new WaitForSeconds(1.5f);
        timerText.gameObject.SetActive(false);

        //smashedObstaclesCountText.gameObject.SetActive(false);
    }

    void ChallengePartiallyCompleted(int runTimes)
    {
        challengePartIndex++;

        Debug.Log("Challenge " + challengeIndex + " Partially Completed With Part Number " + challengePartIndex);

        if(challengePartIndex >= runTimes)
        {
            challengePartIndex = 0;
            ChallengeCompleted();
        }
    }

    void ChallengeCompleted()
    {
        if (challengeIndex - 1 >= challengeList.Count)
        {
            return;
        }

        var _challengeIndex = challengeList[challengeIndex - 1];

        var _mode1Variables = _challengeIndex.mode1Variables;
        var _mode2Variables = _challengeIndex.mode2Variables;
        var _mode3Variables = _challengeIndex.mode3Variables;
        var _mode4Variables = _challengeIndex.mode4Variables;
        var _mode5Variables = _challengeIndex.mode5Variables;
        var _mode6Variables = _challengeIndex.mode6Variables;
        var _mode7Variables = _challengeIndex.mode7Variables;
        var _mode8Variables = _challengeIndex.mode8Variables;
        var _mode9Variables = _challengeIndex.mode9Variables;
        var _mode10Variables = _challengeIndex.mode10Variables;
        var _mode11Variables = _challengeIndex.mode11Variables;
        var _mode12Variables = _challengeIndex.mode12Variables;
        var _mode13Variables = _challengeIndex.mode13Variables;
        var _mode14Variables = _challengeIndex.mode14Variables;
        var _mode15Variables = _challengeIndex.mode15Variables;
        var _mode16Variables = _challengeIndex.mode16Variables;
        var _mode17Variables = _challengeIndex.mode17Variables;
        var _mode18Variables = _challengeIndex.mode18Variables;
        var _mode19Variables = _challengeIndex.mode19Variables;
        var _mode20Variables = _challengeIndex.mode20Variables;
        var _mode21Variables = _challengeIndex.mode21Variables;
        var _mode22Variables = _challengeIndex.mode22Variables;

        if (_challengeIndex.isMode1)
        {
            player.collectedCoin += _mode1Variables.rewardGems;
            player.heartCount += _mode1Variables.rewardKeys;
            player.slowMotionCount += _mode1Variables.rewardSM;

            rewardedGems = _mode1Variables.rewardGems;
            rewardedKeys = _mode1Variables.rewardKeys;
            rewardedSM = _mode1Variables.rewardSM;
        }

        if (_challengeIndex.isMode2)
        {
            player.collectedCoin += _mode2Variables.rewardGems;
            player.heartCount += _mode2Variables.rewardKeys;
            player.slowMotionCount += _mode2Variables.rewardSM;

            rewardedGems = _mode2Variables.rewardGems;
            rewardedKeys = _mode2Variables.rewardKeys;
            rewardedSM = _mode2Variables.rewardSM;
        }

        if (_challengeIndex.isMode3)
        {
            player.collectedCoin += _mode3Variables.rewardGems;
            player.heartCount += _mode3Variables.rewardKeys;
            player.slowMotionCount += _mode3Variables.rewardSM;

            rewardedGems = _mode3Variables.rewardGems;
            rewardedKeys = _mode3Variables.rewardKeys;
            rewardedSM = _mode3Variables.rewardSM;
        }

        if (_challengeIndex.isMode4)
        {
            player.collectedCoin += _mode4Variables.rewardGems;
            player.heartCount += _mode4Variables.rewardKeys;
            player.slowMotionCount += _mode4Variables.rewardSM;

            rewardedGems = _mode4Variables.rewardGems;
            rewardedKeys = _mode4Variables.rewardKeys;
            rewardedSM = _mode4Variables.rewardSM;
        }

        if (_challengeIndex.isMode5)
        {
            player.collectedCoin += _mode5Variables.rewardGems;
            player.heartCount += _mode5Variables.rewardKeys;
            player.slowMotionCount += _mode5Variables.rewardSM;

            rewardedGems = _mode5Variables.rewardGems;
            rewardedKeys = _mode5Variables.rewardKeys;
            rewardedSM = _mode5Variables.rewardSM;
        }

        if (_challengeIndex.isMode6)
        {
            player.collectedCoin += _mode6Variables.rewardGems;
            player.heartCount += _mode6Variables.rewardKeys;
            player.slowMotionCount += _mode6Variables.rewardSM;

            rewardedGems = _mode6Variables.rewardGems;
            rewardedKeys = _mode6Variables.rewardKeys;
            rewardedSM = _mode6Variables.rewardSM;
        }

        if (_challengeIndex.isMode7)
        {
            player.collectedCoin += _mode7Variables.rewardGems;
            player.heartCount += _mode7Variables.rewardKeys;
            player.slowMotionCount += _mode7Variables.rewardSM;

            rewardedGems = _mode7Variables.rewardGems;
            rewardedKeys = _mode7Variables.rewardKeys;
            rewardedSM = _mode7Variables.rewardSM;
        }

        if (_challengeIndex.isMode8)
        {
            player.collectedCoin += _mode8Variables.rewardGems;
            player.heartCount += _mode8Variables.rewardKeys;
            player.slowMotionCount += _mode8Variables.rewardSM;

            rewardedGems = _mode8Variables.rewardGems;
            rewardedKeys = _mode8Variables.rewardKeys;
            rewardedSM = _mode8Variables.rewardSM;
        }

        if (_challengeIndex.isMode9)
        {
            player.collectedCoin += _mode9Variables.rewardGems;
            player.heartCount += _mode9Variables.rewardKeys;
            player.slowMotionCount += _mode9Variables.rewardSM;

            rewardedGems = _mode9Variables.rewardGems;
            rewardedKeys = _mode9Variables.rewardKeys;
            rewardedSM = _mode9Variables.rewardSM;
        }

        if (_challengeIndex.isMode10)
        {
            player.collectedCoin += _mode10Variables.rewardGems;
            player.heartCount += _mode10Variables.rewardKeys;
            player.slowMotionCount += _mode10Variables.rewardSM;

            rewardedGems = _mode10Variables.rewardGems;
            rewardedKeys = _mode10Variables.rewardKeys;
            rewardedSM = _mode10Variables.rewardSM;
        }

        if (_challengeIndex.isMode11)
        {
            player.collectedCoin += _mode11Variables.rewardGems;
            player.heartCount += _mode11Variables.rewardKeys;
            player.slowMotionCount += _mode11Variables.rewardSM;

            rewardedGems = _mode11Variables.rewardGems;
            rewardedKeys = _mode11Variables.rewardKeys;
            rewardedSM = _mode11Variables.rewardSM;
        }

        if (_challengeIndex.isMode12)
        {
            player.collectedCoin += _mode12Variables.rewardGems;
            player.heartCount += _mode12Variables.rewardKeys;
            player.slowMotionCount += _mode12Variables.rewardSM;

            rewardedGems = _mode12Variables.rewardGems;
            rewardedKeys = _mode12Variables.rewardKeys;
            rewardedSM = _mode12Variables.rewardSM;
        }

        if (_challengeIndex.isMode13)
        {
            player.collectedCoin += _mode13Variables.rewardGems;
            player.heartCount += _mode13Variables.rewardKeys;
            player.slowMotionCount += _mode13Variables.rewardSM;

            rewardedGems = _mode13Variables.rewardGems;
            rewardedKeys = _mode13Variables.rewardKeys;
            rewardedSM = _mode13Variables.rewardSM;
        }

        if (_challengeIndex.isMode14)
        {
            player.collectedCoin += _mode14Variables.rewardGems;
            player.heartCount += _mode14Variables.rewardKeys;
            player.slowMotionCount += _mode14Variables.rewardSM;

            rewardedGems = _mode14Variables.rewardGems;
            rewardedKeys = _mode14Variables.rewardKeys;
            rewardedSM = _mode14Variables.rewardSM;
        }

        if (_challengeIndex.isMode15)
        {
            player.collectedCoin += _mode15Variables.rewardGems;
            player.heartCount += _mode15Variables.rewardKeys;
            player.slowMotionCount += _mode15Variables.rewardSM;

            rewardedGems = _mode15Variables.rewardGems;
            rewardedKeys = _mode15Variables.rewardKeys;
            rewardedSM = _mode15Variables.rewardSM;
        }

        if (_challengeIndex.isMode16)
        {
            player.collectedCoin += _mode16Variables.rewardGems;
            player.heartCount += _mode16Variables.rewardKeys;
            player.slowMotionCount += _mode16Variables.rewardSM;

            rewardedGems = _mode16Variables.rewardGems;
            rewardedKeys = _mode16Variables.rewardKeys;
            rewardedSM = _mode16Variables.rewardSM;
        }

        if (_challengeIndex.isMode17)
        {
            player.collectedCoin += _mode17Variables.rewardGems;
            player.heartCount += _mode17Variables.rewardKeys;
            player.slowMotionCount += _mode17Variables.rewardSM;

            rewardedGems = _mode17Variables.rewardGems;
            rewardedKeys = _mode17Variables.rewardKeys;
            rewardedSM = _mode17Variables.rewardSM;
        }

        if (_challengeIndex.isMode18)
        {
            player.collectedCoin += _mode18Variables.rewardGems;
            player.heartCount += _mode18Variables.rewardKeys;
            player.slowMotionCount += _mode18Variables.rewardSM;

            rewardedGems = _mode18Variables.rewardGems;
            rewardedKeys = _mode18Variables.rewardKeys;
            rewardedSM = _mode18Variables.rewardSM;
        }

        if (_challengeIndex.isMode19)
        {
            player.collectedCoin += _mode19Variables.rewardGems;
            player.heartCount += _mode19Variables.rewardKeys;
            player.slowMotionCount += _mode19Variables.rewardSM;

            rewardedGems = _mode19Variables.rewardGems;
            rewardedKeys = _mode19Variables.rewardKeys;
            rewardedSM = _mode19Variables.rewardSM;
        }

        if (_challengeIndex.isMode20)
        {
            player.collectedCoin += _mode20Variables.rewardGems;
            player.heartCount += _mode20Variables.rewardKeys;
            player.slowMotionCount += _mode20Variables.rewardSM;

            rewardedGems = _mode20Variables.rewardGems;
            rewardedKeys = _mode20Variables.rewardKeys;
            rewardedSM = _mode20Variables.rewardSM;
        }

        if (_challengeIndex.isMode21)
        {
            player.collectedCoin += _mode21Variables.rewardGems;
            player.heartCount += _mode21Variables.rewardKeys;
            player.slowMotionCount += _mode21Variables.rewardSM;

            rewardedGems = _mode21Variables.rewardGems;
            rewardedKeys = _mode21Variables.rewardKeys;
            rewardedSM = _mode21Variables.rewardSM;
        }

        if (_challengeIndex.isMode22)
        {
            player.collectedCoin += _mode22Variables.rewardGems;
            player.heartCount += _mode22Variables.rewardKeys;
            player.slowMotionCount += _mode22Variables.rewardSM;

            rewardedGems = _mode22Variables.rewardGems;
            rewardedKeys = _mode22Variables.rewardKeys;
            rewardedSM = _mode22Variables.rewardSM;
        }


        Debug.Log("Challenge " + challengeIndex + " Completed");
        ChallengeCompletedNotification();

        isRewardsShowen = false;
        isChallengeStart = false;

        challengeIndex++;

        Invoke("UpdateChalengeUIOnEnable", 0.1f);

        DataSaveManager.SaveInt("CI", challengeIndex);
    }

    void CheckIfChallengeNotCompleted()
    {
        if (challengeIndex -1 >= challengeList.Count)
        {
            return;
        }

        var _challengeIndex = challengeList[challengeIndex - 1];

        var _mode1Variables = _challengeIndex.mode1Variables;
        var _mode2Variables = _challengeIndex.mode2Variables;
        var _mode3Variables = _challengeIndex.mode3Variables;
        var _mode4Variables = _challengeIndex.mode4Variables;
        var _mode5Variables = _challengeIndex.mode5Variables;
        var _mode6Variables = _challengeIndex.mode6Variables;
        var _mode7Variables = _challengeIndex.mode7Variables;
        var _mode8Variables = _challengeIndex.mode8Variables;
        var _mode9Variables = _challengeIndex.mode9Variables;
        var _mode10Variables = _challengeIndex.mode10Variables;
        var _mode11Variables = _challengeIndex.mode11Variables;
        var _mode12Variables = _challengeIndex.mode12Variables;
        var _mode13Variables = _challengeIndex.mode13Variables;
        var _mode14Variables = _challengeIndex.mode14Variables;
        var _mode15Variables = _challengeIndex.mode15Variables;
        var _mode16Variables = _challengeIndex.mode16Variables;
        var _mode17Variables = _challengeIndex.mode17Variables;
        var _mode18Variables = _challengeIndex.mode18Variables;
        var _mode19Variables = _challengeIndex.mode19Variables;
        var _mode20Variables = _challengeIndex.mode20Variables;
        var _mode21Variables = _challengeIndex.mode21Variables;
        var _mode22Variables = _challengeIndex.mode22Variables;

        if (isChallengeStart)
        {
            if (_challengeIndex.isMode1)
            {
                if (mainMenu.latestScore < _mode1Variables.requiredScore)
                {
                    ChallengeNotCompleted();

                    UpdateChallengeProgressSlider(0, _mode1Variables.requiredScore, 0, mainMenu.latestScore);
                }
            }

            if (_challengeIndex.isMode2)
            {
                if (challengePartIndex > 0)
                {
                    if (mainMenu.latestScore < _mode2Variables.requiredScore)
                    {
                        UpdateChallengeProgressSlider(0, _mode2Variables.rowTimes, 0, challengePartIndex);

                        challengePartIndex = 0;
                        ChallengeNotCompleted();
                    }
                    else
                    {
                        UpdateChallengeProgressSlider(0, _mode2Variables.rowTimes, challengePartIndex, (challengePartIndex - 1));
                    }
                }
            }

            if (_challengeIndex.isMode3)
            {
                challengeCumulativeScore += mainMenu.latestScore;

                if (challengeCumulativeScore >= _mode3Variables.requiredScore)
                {
                    ChallengeCompleted();
                    challengeCumulativeScore = 0;
                }
                else
                {
                    ChallengeNotCompleted();
                    UpdateChallengeProgressSlider(0, _mode3Variables.requiredScore, challengeCumulativeScore, 0);
                }
            }

            if (_challengeIndex.isMode4)
            {
                if (challengePartIndex > 0)
                {
                    if (mainMenu.latestScore < _mode4Variables.requiredScore)
                    {
                        ChallengeNotCompleted();
                    }
                    else
                    {
                        UpdateChallengeProgressSlider(0, _mode4Variables.runTimes, challengePartIndex, (challengePartIndex - 1));
                    }
                }
            }

            if (_challengeIndex.isMode5)
            {
                if (_mode5Variables.minRequiredScore <= mainMenu.latestScore && mainMenu.latestScore <= _mode5Variables.maxRequiredScore)
                {
                    ChallengeCompleted();
                }
                else
                {
                    ChallengeNotCompleted();

                    UpdateChallengeProgressSlider(0, _mode5Variables.maxRequiredScore, 0, mainMenu.latestScore);
                }
            }

            if (_challengeIndex.isMode6)
            {
                if (_mode6Variables.minRequiredScore <= mainMenu.latestScore && mainMenu.latestScore <= _mode6Variables.maxRequiredScore)
                {
                    ChallengePartiallyCompleted(_mode6Variables.rowTimes);

                    UpdateChallengeProgressSlider(0, _mode6Variables.rowTimes, challengePartIndex, (challengePartIndex - 1));
                }
                else
                {
                    if (challengePartIndex > 0)
                    {
                        UpdateChallengeProgressSlider(0, _mode6Variables.rowTimes, 0, challengePartIndex);

                        challengePartIndex = 0;
                        ChallengeNotCompleted();
                    }
                }
            }

            if (_challengeIndex.isMode7)
            {
                if (_mode7Variables.minRequiredScore <= mainMenu.latestScore && mainMenu.latestScore <= _mode7Variables.maxRequiredScore)
                {
                    ChallengePartiallyCompleted(_mode7Variables.runTimes);

                    UpdateChallengeProgressSlider(0, _mode7Variables.runTimes, challengePartIndex, (challengePartIndex - 1));
                }
                else
                {
                    if (challengePartIndex > 0)
                    {
                        ChallengeNotCompleted();
                    }
                }

            }

            if (_challengeIndex.isMode8)
            {
                if (mainMenu.latestScore < _mode8Variables.requiredScore || SceneSwitcher.isObstacleSmashed)
                {
                    ChallengeNotCompleted();

                    UpdateChallengeProgressSlider(0, _mode8Variables.requiredScore, 0, mainMenu.latestScore);
                }
            }

            if (_challengeIndex.isMode9)
            {
                if (mainMenu.latestScore < _mode9Variables.requiredScore || player.livesLeft != player.livesCount)
                {
                    ChallengeNotCompleted();

                    UpdateChallengeProgressSlider(0, _mode9Variables.requiredScore, 0, mainMenu.latestScore);
                }
            }

            if (_challengeIndex.isMode10)
            {
                if (mainMenu.latestScore < _mode9Variables.requiredScore || player.livesLeft != player.livesCount || SceneSwitcher.isObstacleSmashed)
                {
                    ChallengeNotCompleted();

                    UpdateChallengeProgressSlider(0, _mode10Variables.requiredScore, 0, mainMenu.latestScore);
                }
            }

            if (_challengeIndex.isMode11)
            {
                if (latestCollectedCoin < _mode11Variables.requiredGemsCount)
                {
                    ChallengeNotCompleted();

                    UpdateChallengeProgressSlider(0, _mode11Variables.requiredGemsCount, 0, latestCollectedCoin);
                }
            }

            if (_challengeIndex.isMode12)
            {
                if (challengePartIndex > 0)
                {
                    if (latestCollectedCoin < _mode12Variables.requiredGemsCount)
                    {
                        UpdateChallengeProgressSlider(0, _mode12Variables.rowTimes, 0, challengePartIndex);

                        challengePartIndex = 0;
                        ChallengeNotCompleted();
                    }
                    else
                    {
                        UpdateChallengeProgressSlider(0, _mode12Variables.rowTimes, challengePartIndex, (challengePartIndex - 1));
                    }
                }
            }

            if (_challengeIndex.isMode13)
            {
                challengeCumulativeGemsCount += latestCollectedCoin;

                if (challengeCumulativeGemsCount >= _mode13Variables.requiredGemsCount)
                {
                    ChallengeCompleted();
                    challengeCumulativeGemsCount = 0;
                }
                else
                {
                    ChallengeNotCompleted();
                    UpdateChallengeProgressSlider(0, _mode13Variables.requiredGemsCount, challengeCumulativeGemsCount, 0);
                }
            }

            if (_challengeIndex.isMode14)
            {
                if (challengePartIndex > 0)
                {
                    if (latestCollectedCoin < _mode14Variables.requiredGemsCount)
                    {
                        ChallengeNotCompleted();
                    }
                    else
                    {
                        UpdateChallengeProgressSlider(0, _mode14Variables.runTimes, challengePartIndex, (challengePartIndex - 1));
                    }
                }
            }

            if (_challengeIndex.isMode15)
            {
                if (_mode15Variables.minRequiredGemsCount <= latestCollectedCoin && latestCollectedCoin <= _mode15Variables.maxRequiredGemsCount)
                {
                    ChallengeCompleted();
                }
                else
                {
                    ChallengeNotCompleted();

                    UpdateChallengeProgressSlider(0, _mode15Variables.maxRequiredGemsCount, 0, latestCollectedCoin);
                }
            }

            if (_challengeIndex.isMode16)
            {
                if (_mode16Variables.minRequiredGemsCount <= latestCollectedCoin && latestCollectedCoin <= _mode16Variables.maxRequiredGemsCount)
                {
                    ChallengePartiallyCompleted(_mode16Variables.rowTimes);

                    UpdateChallengeProgressSlider(0, _mode16Variables.rowTimes, challengePartIndex, (challengePartIndex - 1));
                }
                else
                {
                    if (challengePartIndex > 0)
                    {
                        UpdateChallengeProgressSlider(0, _mode16Variables.rowTimes, 0, challengePartIndex);

                        challengePartIndex = 0;
                        ChallengeNotCompleted();
                    }
                }
            }

            if (_challengeIndex.isMode17)
            {
                if (_mode17Variables.minRequiredGemsCount <= latestCollectedCoin && latestCollectedCoin <= _mode17Variables.maxRequiredGemsCount)
                {
                    ChallengePartiallyCompleted(_mode17Variables.runTimes);

                    UpdateChallengeProgressSlider(0, _mode17Variables.runTimes, challengePartIndex, (challengePartIndex - 1));
                }
                else
                {
                    if (challengePartIndex > 0)
                    {
                        ChallengeNotCompleted();
                    }
                }

            }

            if (_challengeIndex.isMode18)
            {
                if (latestCollectedCoin < _mode18Variables.requiredGemsCount || SceneSwitcher.isObstacleSmashed)
                {
                    ChallengeNotCompleted();

                    UpdateChallengeProgressSlider(0, _mode18Variables.requiredGemsCount, 0, latestCollectedCoin);
                }
            }

            if (_challengeIndex.isMode19)
            {
                if (SceneSwitcher.smashedObstaclesCount < _mode19Variables.requiredSmashedObstacles)
                {
                    UpdateChallengeProgressSlider(0, _mode19Variables.requiredSmashedObstacles, 0, SceneSwitcher.smashedObstaclesCount);

                    SceneSwitcher.smashedObstaclesCount = 0;
                    SceneSwitcher.sceneSwitcher.UpdateSmashedObstaclesUI();
                    smashedObstaclesCountText.gameObject.SetActive(false);
                    timerText.text = string.Format("{0:00.00}", 0f);
                    timerText.gameObject.SetActive(false);
                    ChallengeNotCompleted();
                }
            }

            if (_challengeIndex.isMode20)
            {
                if (challengePartIndex > 0)
                {
                    if (SceneSwitcher.smashedObstaclesCount < _mode20Variables.requiredSmashedObstacles)
                    {
                        SceneSwitcher.smashedObstaclesCount = 0;
                        SceneSwitcher.sceneSwitcher.UpdateSmashedObstaclesUI();
                        smashedObstaclesCountText.gameObject.SetActive(false);
                        timerText.text = string.Format("{0:00.00}", 0f);
                        timerText.gameObject.SetActive(false);

                        UpdateChallengeProgressSlider(0, _mode20Variables.rowTimes, 0, challengePartIndex);

                        challengePartIndex = 0;
                        ChallengeNotCompleted();
                    }
                    else
                    {
                        UpdateChallengeProgressSlider(0, _mode20Variables.rowTimes, challengePartIndex, (challengePartIndex - 1));
                    }
                }
            }

            if (_challengeIndex.isMode21)
            {
                if (challengePartIndex > 0)
                {
                    if (SceneSwitcher.smashedObstaclesCount < _mode21Variables.requiredSmashedObstacles)
                    {
                        SceneSwitcher.smashedObstaclesCount = 0;
                        SceneSwitcher.sceneSwitcher.UpdateSmashedObstaclesUI();
                        smashedObstaclesCountText.gameObject.SetActive(false);
                        timerText.text = string.Format("{0:00.00}", 0f);
                        timerText.gameObject.SetActive(false);

                        ChallengeNotCompleted();
                    }
                    else
                    {
                        UpdateChallengeProgressSlider(0, _mode21Variables.runTimes, challengePartIndex, (challengePartIndex - 1));
                    }
                }
            }

            if (_challengeIndex.isMode22)
            {
                //SceneSwitcher.smashedObstaclesCount = 0;
                //SceneSwitcher.sceneSwitcher.UpdateSmashedObstaclesUI();
                //smashedObstaclesCountText.gameObject.SetActive(false);

                timerText.text = string.Format("{0:00.00}", 0f);
                timerText.gameObject.SetActive(false);
            }
        }
    }

    void ChallengeNotCompleted()
    {
        Debug.Log("Challenge " + challengeIndex + " Not Completed");
    }

    public void StartChallenge()
    {
        if (challengeIndex - 1 >= challengeList.Count)
        {
            Debug.Log("There Is No Data In Challenge Number " + challengeIndex);
            return;
        }

        var _challengeIndex = challengeList[challengeIndex - 1];

        var _mode1Variables = _challengeIndex.mode1Variables;
        var _mode2Variables = _challengeIndex.mode2Variables;
        var _mode3Variables = _challengeIndex.mode3Variables;
        var _mode4Variables = _challengeIndex.mode4Variables;
        var _mode5Variables = _challengeIndex.mode5Variables;
        var _mode6Variables = _challengeIndex.mode6Variables;
        var _mode7Variables = _challengeIndex.mode7Variables;
        var _mode8Variables = _challengeIndex.mode8Variables;
        var _mode9Variables = _challengeIndex.mode9Variables;
        var _mode10Variables = _challengeIndex.mode10Variables;
        var _mode11Variables = _challengeIndex.mode11Variables;
        var _mode12Variables = _challengeIndex.mode12Variables;
        var _mode13Variables = _challengeIndex.mode13Variables;
        var _mode14Variables = _challengeIndex.mode14Variables;
        var _mode15Variables = _challengeIndex.mode15Variables;
        var _mode16Variables = _challengeIndex.mode16Variables;
        var _mode17Variables = _challengeIndex.mode17Variables;
        var _mode18Variables = _challengeIndex.mode18Variables;
        var _mode19Variables = _challengeIndex.mode19Variables;
        var _mode20Variables = _challengeIndex.mode20Variables;
        var _mode21Variables = _challengeIndex.mode21Variables;
        var _mode22Variables = _challengeIndex.mode22Variables;

        if (_challengeIndex.isMode1)
        {
            StartCoroutine(Mode1(_mode1Variables.requiredScore));
        }

        if (_challengeIndex.isMode2)
        {
            StartCoroutine(Mode2(_mode2Variables.requiredScore, _mode2Variables.rowTimes));
        }

        if (_challengeIndex.isMode3)
        {
            StartCoroutine(Mode3(_mode3Variables.requiredScore));
        }

        if (_challengeIndex.isMode4)
        {
            StartCoroutine(Mode4(_mode4Variables.requiredScore, _mode4Variables.runTimes));
        }

        if (_challengeIndex.isMode5)
        {
            StartCoroutine(Mode5(_mode5Variables.minRequiredScore, _mode5Variables.maxRequiredScore));
        }

        if (_challengeIndex.isMode6)
        {
            StartCoroutine(Mode6(_mode6Variables.minRequiredScore, _mode6Variables.maxRequiredScore, _mode6Variables.rowTimes));
        }

        if (_challengeIndex.isMode7)
        {
            StartCoroutine(Mode7(_mode7Variables.minRequiredScore, _mode7Variables.maxRequiredScore, _mode7Variables.runTimes));
        }

        if (_challengeIndex.isMode8)
        {
            StartCoroutine(Mode8(_mode8Variables.requiredScore));
        }

        if (_challengeIndex.isMode9)
        {
            StartCoroutine(Mode9(_mode9Variables.requiredScore));
        }

        if (_challengeIndex.isMode10)
        {
            StartCoroutine(Mode10(_mode10Variables.requiredScore));
        }

        if (_challengeIndex.isMode11)
        {
            StartCoroutine(Mode11(_mode11Variables.requiredGemsCount));
        }

        if (_challengeIndex.isMode12)
        {
            StartCoroutine(Mode12(_mode12Variables.requiredGemsCount, _mode12Variables.rowTimes));
        }

        if (_challengeIndex.isMode13)
        {
            StartCoroutine(Mode13(_mode13Variables.requiredGemsCount));
        }

        if (_challengeIndex.isMode14)
        {
            StartCoroutine(Mode14(_mode14Variables.requiredGemsCount, _mode14Variables.runTimes));
        }

        if (_challengeIndex.isMode15)
        {
            StartCoroutine(Mode15(_mode15Variables.minRequiredGemsCount, _mode15Variables.maxRequiredGemsCount));
        }

        if (_challengeIndex.isMode16)
        {
            StartCoroutine(Mode16(_mode16Variables.minRequiredGemsCount, _mode16Variables.maxRequiredGemsCount, _mode16Variables.rowTimes));
        }

        if (_challengeIndex.isMode17)
        {
            StartCoroutine(Mode17(_mode17Variables.minRequiredGemsCount, _mode17Variables.maxRequiredGemsCount, _mode17Variables.runTimes));
        }

        if (_challengeIndex.isMode18)
        {
            StartCoroutine(Mode18(_mode18Variables.requiredGemsCount));
        }


        if (_challengeIndex.isMode19)
        {
            StartCoroutine(Mode19(_mode19Variables.requiredSmashedObstacles, _mode19Variables.timerCountdown));
        }

        if (_challengeIndex.isMode20)
        {
            StartCoroutine(Mode20(_mode20Variables.requiredSmashedObstacles, _mode20Variables.timerCountdown, _mode20Variables.rowTimes));
        }

        if (_challengeIndex.isMode21)
        {
            StartCoroutine(Mode21(_mode21Variables.requiredSmashedObstacles, _mode21Variables.timerCountdown, _mode21Variables.runTimes));
        }

        if (_challengeIndex.isMode22)
        {
            StartCoroutine(Mode22( _mode22Variables.timerCountdown));
        }

        isChallengeStart = true;
    }

    public void StopAnyRunningChallenge()
    {
        //isChallengeStart = false;

        StopAllCoroutines();

        CheckIfChallengeNotCompleted();

        CheckIfRewardsAndNextChallengeUIIsShowen();
    }

    public bool IsSmashingChallenge() //Pipes don't generate thick glass obstacles for challenge that require no smashing.
    {
        if (challengeIndex - 1 >= challengeList.Count)
        {
            return false;
        }

        var currentChallengeMode = ChallengeMode._challengeMode.challengeList[ChallengeMode._challengeMode.challengeIndex - 1];

        if (currentChallengeMode.isMode8 || currentChallengeMode.isMode10 || currentChallengeMode.isMode18 || currentChallengeMode.isMode22)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsObstacleSmashingChallenge() //For counting smashed obstacles.
    {
        if (challengeIndex - 1 >= challengeList.Count)
        {
            return false;
        }

        var currentChallengeMode = ChallengeMode._challengeMode.challengeList[ChallengeMode._challengeMode.challengeIndex - 1];

        if (timerText.enabled)
        {
            if (currentChallengeMode.isMode19 || currentChallengeMode.isMode20 || currentChallengeMode.isMode21)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    void CheckIfRewardsAndNextChallengeUIIsShowen()
    {
        if (!isRewardsShowen)
        {
            if (challengeIndex - 1 >= challengeList.Count)
            {
                //Complete Challenge Panel.
                completedChallengePanel.GetChild(0).GetComponent<Text>().text = "Challenge " + (challengeIndex - 1) + " Completed";
                completedChallengePanel.GetChild(1).GetComponent<Text>().text = challengeList[challengeIndex - 2].challengeName;

                //Next Challenge Panel.
                nextChallengePanel.GetChild(0).gameObject.SetActive(false);
                nextChallengePanel.GetChild(1).gameObject.SetActive(false);
                nextChallengePanel.GetChild(2).gameObject.SetActive(false);
                nextChallengePanel.GetChild(3).gameObject.SetActive(true);

                //Rewards Content Panel.
                if (rewardedGems == 0)
                {
                    rewardsContent.GetChild(0).gameObject.SetActive(false);
                }
                else
                {
                    rewardsContent.GetChild(0).gameObject.SetActive(true);

                    rewardsContent.GetChild(0).GetChild(0).GetComponent<Text>().text = rewardedGems.ToString();
                }

                if (rewardedKeys == 0)
                {
                    rewardsContent.GetChild(1).gameObject.SetActive(false);
                }
                else
                {
                    rewardsContent.GetChild(1).gameObject.SetActive(true);

                    rewardsContent.GetChild(1).GetChild(0).GetComponent<Text>().text = rewardedKeys.ToString();
                }

                if (rewardedSM == 0)
                {
                    rewardsContent.GetChild(2).gameObject.SetActive(false);
                }
                else
                {
                    rewardsContent.GetChild(2).gameObject.SetActive(true);

                    rewardsContent.GetChild(2).GetChild(0).GetComponent<Text>().text = rewardedSM.ToString();
                }

                rewardsAndNextChallengePanel.SetActive(true);
                isRewardsShowen = true;

                return;
            }

            //Complete Challenge Panel.
            completedChallengePanel.GetChild(0).GetComponent<Text>().text = "Challenge " + (challengeIndex - 1) + " Completed";
            completedChallengePanel.GetChild(1).GetComponent<Text>().text = challengeList[challengeIndex - 2].challengeName;

            //Next Challenge Panel.
            nextChallengePanel.GetChild(0).GetComponent<Text>().text = "Next Challenge " + challengeIndex;
            nextChallengePanel.GetChild(1).GetComponent<Text>().text = challengeList[challengeIndex - 1].challengeName;

            //Rewards Content Panel.
            if(rewardedGems == 0)
            {
                rewardsContent.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                rewardsContent.GetChild(0).gameObject.SetActive(true);

                rewardsContent.GetChild(0).GetChild(0).GetComponent<Text>().text = rewardedGems.ToString();
            }

            if (rewardedKeys == 0)
            {
                rewardsContent.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                rewardsContent.GetChild(1).gameObject.SetActive(true);

                rewardsContent.GetChild(1).GetChild(0).GetComponent<Text>().text = rewardedKeys.ToString();
            }

            if (rewardedSM == 0)
            {
                rewardsContent.GetChild(2).gameObject.SetActive(false);
            }
            else
            {
                rewardsContent.GetChild(2).gameObject.SetActive(true);

                rewardsContent.GetChild(2).GetChild(0).GetComponent<Text>().text = rewardedSM.ToString();
            }

            rewardsAndNextChallengePanel.SetActive(true);
            isRewardsShowen = true;
        }
    }

    void UpdateChalengeUIOnEnable()
    {
        UpdateChallengeUI();

        if (challengeIndex - 1 >= challengeList.Count)
        {
            return;
        }

        var _challengeIndex = challengeList[challengeIndex - 1];

        var _mode1Variables = _challengeIndex.mode1Variables;
        var _mode2Variables = _challengeIndex.mode2Variables;
        var _mode3Variables = _challengeIndex.mode3Variables;
        var _mode4Variables = _challengeIndex.mode4Variables;
        var _mode5Variables = _challengeIndex.mode5Variables;
        var _mode6Variables = _challengeIndex.mode6Variables;
        var _mode7Variables = _challengeIndex.mode7Variables;
        var _mode8Variables = _challengeIndex.mode8Variables;
        var _mode9Variables = _challengeIndex.mode9Variables;
        var _mode10Variables = _challengeIndex.mode10Variables;
        var _mode11Variables = _challengeIndex.mode11Variables;
        var _mode12Variables = _challengeIndex.mode12Variables;
        var _mode13Variables = _challengeIndex.mode13Variables;
        var _mode14Variables = _challengeIndex.mode14Variables;
        var _mode15Variables = _challengeIndex.mode15Variables;
        var _mode16Variables = _challengeIndex.mode16Variables;
        var _mode17Variables = _challengeIndex.mode17Variables;
        var _mode18Variables = _challengeIndex.mode18Variables;
        var _mode19Variables = _challengeIndex.mode19Variables;
        var _mode20Variables = _challengeIndex.mode20Variables;
        var _mode21Variables = _challengeIndex.mode21Variables;
        var _mode22Variables = _challengeIndex.mode22Variables;

        if (_challengeIndex.isMode1)
        {
            UpdateChallengeProgressSlider(0, _mode1Variables.requiredScore, 0, 0);
        }

        if (_challengeIndex.isMode2)
        {
            UpdateChallengeProgressSlider(0, _mode2Variables.rowTimes, 0, 0);
        }

        if (_challengeIndex.isMode3)
        {
            UpdateChallengeProgressSlider(0, _mode3Variables.requiredScore, 0, 0);
        }

        if (_challengeIndex.isMode4)
        {
            UpdateChallengeProgressSlider(0, _mode4Variables.runTimes, 0, 0);
        }

        if (_challengeIndex.isMode5)
        {
            UpdateChallengeProgressSlider(0, _mode5Variables.maxRequiredScore, 0, 0);
        }

        if (_challengeIndex.isMode6)
        {
            UpdateChallengeProgressSlider(0, _mode6Variables.rowTimes, 0, 0);
        }

        if (_challengeIndex.isMode7)
        {
            UpdateChallengeProgressSlider(0, _mode7Variables.runTimes, 0, 0);
        }

        if (_challengeIndex.isMode8)
        {
            UpdateChallengeProgressSlider(0, _mode8Variables.requiredScore, 0, 0);
        }

        if (_challengeIndex.isMode9)
        {
            UpdateChallengeProgressSlider(0, _mode9Variables.requiredScore, 0, 0);
        }

        if (_challengeIndex.isMode10)
        {
            UpdateChallengeProgressSlider(0, _mode10Variables.requiredScore, 0, 0);
        }

        if (_challengeIndex.isMode11)
        {
            UpdateChallengeProgressSlider(0, _mode11Variables.requiredGemsCount, 0, 0);
        }

        if (_challengeIndex.isMode12)
        {
            UpdateChallengeProgressSlider(0, _mode12Variables.rowTimes, 0, 0);
        }

        if (_challengeIndex.isMode13)
        {
            UpdateChallengeProgressSlider(0, _mode13Variables.requiredGemsCount, 0, 0);
        }

        if (_challengeIndex.isMode14)
        {
            UpdateChallengeProgressSlider(0, _mode14Variables.runTimes, 0, 0);
        }

        if (_challengeIndex.isMode15)
        {
            UpdateChallengeProgressSlider(0, _mode15Variables.maxRequiredGemsCount, 0, 0);
        }

        if (_challengeIndex.isMode16)
        {
            UpdateChallengeProgressSlider(0, _mode16Variables.rowTimes, 0, 0);
        }

        if (_challengeIndex.isMode17)
        {
            UpdateChallengeProgressSlider(0, _mode17Variables.runTimes, 0, 0);
        }

        if (_challengeIndex.isMode18)
        {
            UpdateChallengeProgressSlider(0, _mode18Variables.requiredGemsCount, 0, 0);
        }

        if (_challengeIndex.isMode19)
        {
            UpdateChallengeProgressSlider(0, _mode19Variables.requiredSmashedObstacles, 0, 0);
        }

        if (_challengeIndex.isMode20)
        {
            UpdateChallengeProgressSlider(0, _mode20Variables.rowTimes, 0, 0);
        }

        if (_challengeIndex.isMode21)
        {
            UpdateChallengeProgressSlider(0, _mode21Variables.runTimes, 0, 0);
        }

        if (_challengeIndex.isMode22)
        {
            UpdateChallengeProgressSlider(0, _mode22Variables.timerCountdown, 0, 0);
        }
    }

    void UpdateChallengeUI()
    {
        if (challengeIndex - 1 >= challengeList.Count)
        {
            challengeNumberText.gameObject.SetActive(false);
            challengeInfoText.gameObject.SetActive(false);
            challengeProgressSlider.gameObject.SetActive(false);

            allCompletedChallengesText.gameObject.SetActive(true);

            return;
        }

        challengeNumberText.text = "Challenge " + challengeIndex;
        challengeInfoText.text = challengeList[challengeIndex - 1].challengeName;
    }

    void UpdateChallengeProgressSlider(float minValue, float maxValue, float wantedValue, float currentValue)
    {
        StartCoroutine(UpdateChallengeProgressSliderCoroutine(minValue, maxValue, wantedValue, currentValue));
    }

    float duration = 1f;
    float lerpSpeed = 0;
    IEnumerator UpdateChallengeProgressSliderCoroutine(float minValue, float maxValue, float wantedValue, float currentValue)
    {
        lerpSpeed = 0;

        challengeProgressSlider.minValue = minValue;
        challengeProgressSlider.maxValue = maxValue;
        challengeProgressSlider.value = currentValue;

        currentProgressNumText.text = wantedValue.ToString();
        maxProgressNumText.text = maxValue.ToString();

        while (true)
        {
            lerpSpeed += Time.deltaTime / duration;
            challengeProgressSlider.value = Mathf.Lerp(currentValue, wantedValue, lerpSpeed);

            if (wantedValue == challengeProgressSlider.value)
            {
                lerpSpeed = 0;
                yield break;
            }
            yield return null;
        }
    }

    void ChallengeCompletedNotification()
    {
        completedChallengeNotificationPanel.transform.GetChild(0).GetComponent<Text>().text = "Challenge " + challengeIndex + " Completed";

        completedChallengeNotificationPanel.SetActive(true);
    }

    public void SkipChallenge()
    {
        if(player.collectedCoin >= skipPrice)
        {
            player.collectedCoin -= skipPrice;

            challengePartIndex = 0;
            challengeCumulativeScore = 0;
            latestCollectedCoin = 0;
            challengeCumulativeGemsCount = 0;

            ChallengeCompleted();
            CheckIfRewardsAndNextChallengeUIIsShowen();

            player.UpdateAllInfoUI();
        }
        else
        {
            DoNotHaveEnoughGems();
        }
    }

    void UpdateSkipPanelUI()
    {
        skipPriceText.text = "Skip For " + skipPrice;
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

    /*
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SetChallengeVariables();
            }
        }

        void OpenChallengeSlotsWithRandomModes()
        {
            for (int i = 0; i < wantedChallengesNumber; i++)
            {
                randomIndexList.Add(new int());

                randomIndexList[i] = i;
            }

            int challengeLevelCount = challengeList.Count;
            int neededChallengeLevelsCount = (challengeList.Count + wantedChallengesNumber);

            for (int i = challengeLevelCount; i < neededChallengeLevelsCount; i++)
            {
                challengeList.Add(new Challenge());
                var challengeLevel = challengeList[i];

                int randomIndex = Random.Range(0, randomIndexList.Count);

                int randomListIndex = randomIndexList[randomIndex];
                randomIndexList.RemoveAt(randomIndex);
                print("Challenge " + i + " Is " + randomListIndex);


                switch (randomListIndex)
                {
                    case 0:
                        challengeLevel.isMode1 = true;
                        break;
                    case 1:
                        challengeLevel.isMode2 = true;
                        break;
                    case 2:
                        challengeLevel.isMode3 = true;
                        break;
                    case 3:
                        challengeLevel.isMode4 = true;
                        break;
                    case 4:
                        challengeLevel.isMode5 = true;
                        break;
                    case 5:
                        challengeLevel.isMode6 = true;
                        break;
                    case 6:
                        challengeLevel.isMode7 = true;
                        break;
                    case 7:
                        challengeLevel.isMode8 = true;
                        break;
                    case 8:
                        challengeLevel.isMode9 = true;
                        break;
                    case 9:
                        challengeLevel.isMode10 = true;
                        break;
                    case 10:
                        challengeLevel.isMode11 = true;
                        break;
                    case 11:
                        challengeLevel.isMode12 = true;
                        break;
                    case 12:
                        challengeLevel.isMode13 = true;
                        break;
                    case 13:
                        challengeLevel.isMode14 = true;
                        break;
                    case 14:
                        challengeLevel.isMode15 = true;
                        break;
                    case 15:
                        challengeLevel.isMode16 = true;
                        break;
                    case 16:
                        challengeLevel.isMode17 = true;
                        break;
                    case 17:
                        challengeLevel.isMode18 = true;
                        break;
                    case 18:
                        challengeLevel.isMode19 = true;
                        break;
                    case 19:
                        challengeLevel.isMode20 = true;
                        break;
                    case 20:
                        challengeLevel.isMode21 = true;
                        break;
                    case 21:
                        challengeLevel.isMode22 = true;
                        break;
                }
            }
        }

        void SetChallengeVariables()
        {
            for (int i = 0; i < challengeList.Count; i++)
            {
                var challengeLevel = challengeList[i];

                if(challengeLevel.isMode1 == true)
                {
                    challengeLevel.mode1Variables.requiredScore = GetRandomScoreBasedOnLevel(i);

                    challengeLevel.mode1Variables.rewardGems = GetGemsRewardBasedOnLevel(i);
                    challengeLevel.mode1Variables.rewardKeys = GetKeysRewardBasedOnLevel(i);
                    challengeLevel.mode1Variables.rewardSM = GetSMRewardBasedOnLevel(i);
                }

                if (challengeLevel.isMode2 == true)
                {
                    challengeLevel.mode2Variables.requiredScore = GetRandomScoreBasedOnLevel(i);
                    challengeLevel.mode2Variables.rowTimes = GetRowOrRunTimes();

                    challengeLevel.mode2Variables.rewardGems = GetGemsRewardBasedOnLevel(i);
                    challengeLevel.mode2Variables.rewardKeys = GetKeysRewardBasedOnLevel(i);
                    challengeLevel.mode2Variables.rewardSM = GetSMRewardBasedOnLevel(i);
                }

                if (challengeLevel.isMode3 == true)
                {
                    challengeLevel.mode3Variables.requiredScore = GetRandomScoreBasedOnLevel(i);

                    challengeLevel.mode3Variables.rewardGems = GetGemsRewardBasedOnLevel(i);
                    challengeLevel.mode3Variables.rewardKeys = GetKeysRewardBasedOnLevel(i);
                    challengeLevel.mode3Variables.rewardSM = GetSMRewardBasedOnLevel(i);
                }

                if (challengeLevel.isMode4 == true)
                {
                    challengeLevel.mode4Variables.requiredScore = GetRandomScoreBasedOnLevel(i);
                    challengeLevel.mode4Variables.runTimes = GetRowOrRunTimes();

                    challengeLevel.mode4Variables.rewardGems = GetGemsRewardBasedOnLevel(i);
                    challengeLevel.mode4Variables.rewardKeys = GetKeysRewardBasedOnLevel(i);
                    challengeLevel.mode4Variables.rewardSM = GetSMRewardBasedOnLevel(i);
                }

                if (challengeLevel.isMode5 == true)
                {
                    challengeLevel.mode5Variables.minRequiredScore = GetRandomScoreBasedOnLevel(i);
                    challengeLevel.mode5Variables.maxRequiredScore = (challengeLevel.mode5Variables.minRequiredScore + 100);

                    challengeLevel.mode5Variables.rewardGems = GetGemsRewardBasedOnLevel(i);
                    challengeLevel.mode5Variables.rewardKeys = GetKeysRewardBasedOnLevel(i);
                    challengeLevel.mode5Variables.rewardSM = GetSMRewardBasedOnLevel(i);
                }

                if (challengeLevel.isMode6 == true)
                {
                    challengeLevel.mode6Variables.minRequiredScore = GetRandomScoreBasedOnLevel(i);
                    challengeLevel.mode6Variables.maxRequiredScore = (challengeLevel.mode6Variables.minRequiredScore + 100);
                    challengeLevel.mode6Variables.rowTimes = GetRowOrRunTimes();

                    challengeLevel.mode6Variables.rewardGems = GetGemsRewardBasedOnLevel(i);
                    challengeLevel.mode6Variables.rewardKeys = GetKeysRewardBasedOnLevel(i);
                    challengeLevel.mode6Variables.rewardSM = GetSMRewardBasedOnLevel(i);
                }

                if (challengeLevel.isMode7 == true)
                {
                    challengeLevel.mode7Variables.minRequiredScore = GetRandomScoreBasedOnLevel(i);
                    challengeLevel.mode7Variables.maxRequiredScore = (challengeLevel.mode7Variables.minRequiredScore + 100);
                    challengeLevel.mode7Variables.runTimes = GetRowOrRunTimes();

                    challengeLevel.mode7Variables.rewardGems = GetGemsRewardBasedOnLevel(i);
                    challengeLevel.mode7Variables.rewardKeys = GetKeysRewardBasedOnLevel(i);
                    challengeLevel.mode7Variables.rewardSM = GetSMRewardBasedOnLevel(i);
                }

                if (challengeLevel.isMode8 == true)
                {
                    challengeLevel.mode8Variables.requiredScore = GetRandomScoreBasedOnLevel(i);

                    challengeLevel.mode8Variables.rewardGems = GetGemsRewardBasedOnLevel(i);
                    challengeLevel.mode8Variables.rewardKeys = GetKeysRewardBasedOnLevel(i);
                    challengeLevel.mode8Variables.rewardSM = GetSMRewardBasedOnLevel(i);
                }

                if (challengeLevel.isMode9 == true)
                {
                    challengeLevel.mode9Variables.requiredScore = GetRandomScoreBasedOnLevel(i);

                    challengeLevel.mode9Variables.rewardGems = GetGemsRewardBasedOnLevel(i);
                    challengeLevel.mode9Variables.rewardKeys = GetKeysRewardBasedOnLevel(i);
                    challengeLevel.mode9Variables.rewardSM = GetSMRewardBasedOnLevel(i);
                }

                if (challengeLevel.isMode10 == true)
                {
                    challengeLevel.mode10Variables.requiredScore = GetRandomScoreBasedOnLevel(i);

                    challengeLevel.mode10Variables.rewardGems = GetGemsRewardBasedOnLevel(i);
                    challengeLevel.mode10Variables.rewardKeys = GetKeysRewardBasedOnLevel(i);
                    challengeLevel.mode10Variables.rewardSM = GetSMRewardBasedOnLevel(i);
                }

                if (challengeLevel.isMode11 == true)
                {
                    challengeLevel.mode11Variables.requiredGemsCount = GetRandomGemsBasedOnLevel(i);

                    challengeLevel.mode11Variables.rewardGems = GetGemsRewardBasedOnLevel(i);
                    challengeLevel.mode11Variables.rewardKeys = GetKeysRewardBasedOnLevel(i);
                    challengeLevel.mode11Variables.rewardSM = GetSMRewardBasedOnLevel(i);
                }

                if (challengeLevel.isMode12 == true)
                {
                    challengeLevel.mode12Variables.requiredGemsCount = GetRandomGemsBasedOnLevel(i);
                    challengeLevel.mode12Variables.rowTimes = GetRowOrRunTimes();

                    challengeLevel.mode12Variables.rewardGems = GetGemsRewardBasedOnLevel(i);
                    challengeLevel.mode12Variables.rewardKeys = GetKeysRewardBasedOnLevel(i);
                    challengeLevel.mode12Variables.rewardSM = GetSMRewardBasedOnLevel(i);
                }

                if (challengeLevel.isMode13 == true)
                {
                    challengeLevel.mode13Variables.requiredGemsCount = GetRandomGemsBasedOnLevel(i);

                    challengeLevel.mode13Variables.rewardGems = GetGemsRewardBasedOnLevel(i);
                    challengeLevel.mode13Variables.rewardKeys = GetKeysRewardBasedOnLevel(i);
                    challengeLevel.mode13Variables.rewardSM = GetSMRewardBasedOnLevel(i);
                }

                if (challengeLevel.isMode14 == true)
                {
                    challengeLevel.mode14Variables.requiredGemsCount = GetRandomGemsBasedOnLevel(i);
                    challengeLevel.mode14Variables.runTimes = GetRowOrRunTimes();

                    challengeLevel.mode14Variables.rewardGems = GetGemsRewardBasedOnLevel(i);
                    challengeLevel.mode14Variables.rewardKeys = GetKeysRewardBasedOnLevel(i);
                    challengeLevel.mode14Variables.rewardSM = GetSMRewardBasedOnLevel(i);
                }

                if (challengeLevel.isMode15 == true)
                {
                    challengeLevel.mode15Variables.minRequiredGemsCount = GetRandomGemsBasedOnLevel(i);
                    challengeLevel.mode15Variables.maxRequiredGemsCount = (challengeLevel.mode15Variables.minRequiredGemsCount + 10);

                    challengeLevel.mode15Variables.rewardGems = GetGemsRewardBasedOnLevel(i);
                    challengeLevel.mode15Variables.rewardKeys = GetKeysRewardBasedOnLevel(i);
                    challengeLevel.mode15Variables.rewardSM = GetSMRewardBasedOnLevel(i);
                }

                if (challengeLevel.isMode16 == true)
                {
                    challengeLevel.mode16Variables.minRequiredGemsCount = GetRandomGemsBasedOnLevel(i);
                    challengeLevel.mode16Variables.maxRequiredGemsCount = (challengeLevel.mode16Variables.minRequiredGemsCount + 10);
                    challengeLevel.mode16Variables.rowTimes = GetRowOrRunTimes();

                    challengeLevel.mode16Variables.rewardGems = GetGemsRewardBasedOnLevel(i);
                    challengeLevel.mode16Variables.rewardKeys = GetKeysRewardBasedOnLevel(i);
                    challengeLevel.mode16Variables.rewardSM = GetSMRewardBasedOnLevel(i);
                }

                if (challengeLevel.isMode17 == true)
                {
                    challengeLevel.mode17Variables.minRequiredGemsCount = GetRandomGemsBasedOnLevel(i);
                    challengeLevel.mode17Variables.maxRequiredGemsCount = (challengeLevel.mode17Variables.minRequiredGemsCount + 10);
                    challengeLevel.mode17Variables.runTimes = GetRowOrRunTimes();

                    challengeLevel.mode17Variables.rewardGems = GetGemsRewardBasedOnLevel(i);
                    challengeLevel.mode17Variables.rewardKeys = GetKeysRewardBasedOnLevel(i);
                    challengeLevel.mode17Variables.rewardSM = GetSMRewardBasedOnLevel(i);
                }

                if (challengeLevel.isMode18 == true)
                {
                    challengeLevel.mode18Variables.requiredGemsCount = GetRandomGemsBasedOnLevel(i);

                    challengeLevel.mode17Variables.rewardGems = GetGemsRewardBasedOnLevel(i);
                    challengeLevel.mode17Variables.rewardKeys = GetKeysRewardBasedOnLevel(i);
                    challengeLevel.mode17Variables.rewardSM = GetSMRewardBasedOnLevel(i);
                }

                if (challengeLevel.isMode19 == true)
                {
                    challengeLevel.mode19Variables.requiredSmashedObstacles = GetRandomObstaclesNumberBasedOnLevel(i);
                    challengeLevel.mode19Variables.timerCountdown = GetTimerBasedOnLevel(i);

                    challengeLevel.mode19Variables.rewardGems = GetGemsRewardBasedOnLevel(i);
                    challengeLevel.mode19Variables.rewardKeys = GetKeysRewardBasedOnLevel(i);
                    challengeLevel.mode19Variables.rewardSM = GetSMRewardBasedOnLevel(i);
                }

                if (challengeLevel.isMode20 == true)
                {
                    challengeLevel.mode20Variables.requiredSmashedObstacles = GetRandomObstaclesNumberBasedOnLevel(i);
                    challengeLevel.mode20Variables.timerCountdown = GetTimerBasedOnLevel(i);
                    challengeLevel.mode20Variables.rowTimes = GetRowOrRunTimes();

                    challengeLevel.mode20Variables.rewardGems = GetGemsRewardBasedOnLevel(i);
                    challengeLevel.mode20Variables.rewardKeys = GetKeysRewardBasedOnLevel(i);
                    challengeLevel.mode20Variables.rewardSM = GetSMRewardBasedOnLevel(i);
                }

                if (challengeLevel.isMode21 == true)
                {
                    challengeLevel.mode21Variables.requiredSmashedObstacles = GetRandomObstaclesNumberBasedOnLevel(i);
                    challengeLevel.mode21Variables.timerCountdown = GetTimerBasedOnLevel(i);
                    challengeLevel.mode21Variables.runTimes = GetRowOrRunTimes();

                    challengeLevel.mode21Variables.rewardGems = GetGemsRewardBasedOnLevel(i);
                    challengeLevel.mode21Variables.rewardKeys = GetKeysRewardBasedOnLevel(i);
                    challengeLevel.mode21Variables.rewardSM = GetSMRewardBasedOnLevel(i);
                }

                if (challengeLevel.isMode22 == true)
                {
                    challengeLevel.mode22Variables.timerCountdown = GetTimerBasedOnLevel(i);

                    challengeLevel.mode22Variables.rewardGems = GetGemsRewardBasedOnLevel(i);
                    challengeLevel.mode22Variables.rewardKeys = GetKeysRewardBasedOnLevel(i);
                    challengeLevel.mode22Variables.rewardSM = GetSMRewardBasedOnLevel(i);
                }

                SetChallengeName(i);
            }
        }

        void SetChallengeName(int i)
        {
            var challengeLevel = challengeList[i];
            string challengeName = "";

            if (challengeLevel.isMode1 == true)
            {
                challengeName = "Score " + challengeLevel.mode1Variables.requiredScore + " In 1 Run";
            }

            if (challengeLevel.isMode2 == true)
            {
                challengeName = "Score " + challengeLevel.mode2Variables.requiredScore + " In x" + challengeLevel.mode2Variables.rowTimes + " A Row";
            }

            if (challengeLevel.isMode3 == true)
            {
                challengeName = "Score " + challengeLevel.mode3Variables.requiredScore + " In Total";
            }

            if (challengeLevel.isMode4 == true)
            {
                challengeName = "Score " + challengeLevel.mode4Variables.requiredScore + " In x" + challengeLevel.mode4Variables.runTimes + " Run";
            }

            if (challengeLevel.isMode5 == true)
            {
                challengeName = "Score Between " + challengeLevel.mode5Variables.minRequiredScore + " And " + challengeLevel.mode5Variables.maxRequiredScore + " In 1 Run";
            }

            if (challengeLevel.isMode6 == true)
            {
                challengeName = "Score Between " + challengeLevel.mode6Variables.minRequiredScore + " And " + challengeLevel.mode6Variables.maxRequiredScore + " In x" + challengeLevel.mode6Variables.rowTimes + " A Row";
            }

            if (challengeLevel.isMode7 == true)
            {
                challengeName = "Score Between " + challengeLevel.mode7Variables.minRequiredScore + " And " + challengeLevel.mode7Variables.maxRequiredScore + " In x" + challengeLevel.mode7Variables.runTimes + " Run";
            }

            if (challengeLevel.isMode8 == true)
            {
                challengeName = "Score " + challengeLevel.mode8Variables.requiredScore + " Without Smashing Any Obstacle";
            }

            if (challengeLevel.isMode9 == true)
            {
                challengeName = "Score " + challengeLevel.mode9Variables.requiredScore + " Without Hit By Any Obstacle";
            }

            if (challengeLevel.isMode10 == true)
            {
                challengeName = "Score " + challengeLevel.mode10Variables.requiredScore + " Without Hit By Or Smashing Any Obstacle";
            }

            if (challengeLevel.isMode11 == true)
            {
                challengeName = "Collect " + challengeLevel.mode11Variables.requiredGemsCount + " Gems In 1 Run";
            }

            if (challengeLevel.isMode12 == true)
            {
                challengeName = "Collect " + challengeLevel.mode12Variables.requiredGemsCount + " Gems In x" + challengeLevel.mode12Variables.rowTimes + " A Row";
            }

            if (challengeLevel.isMode13 == true)
            {
                challengeName = "Collect " + challengeLevel.mode13Variables.requiredGemsCount + " Gems In Total";
            }

            if (challengeLevel.isMode14 == true)
            {
                challengeName = "Collect " + challengeLevel.mode14Variables.requiredGemsCount + " Gems In x" + challengeLevel.mode14Variables.runTimes + " Run";
            }

            if (challengeLevel.isMode15 == true)
            {
                challengeName = "Collect Between " + challengeLevel.mode15Variables.minRequiredGemsCount + " And " + challengeLevel.mode15Variables.minRequiredGemsCount + " Gems In 1 Run";
            }

            if (challengeLevel.isMode16 == true)
            {
                challengeName = "Collect Between " + challengeLevel.mode16Variables.minRequiredGemsCount + " And " + challengeLevel.mode16Variables.maxRequiredGemsCount + " Gems In x" + challengeLevel.mode16Variables.rowTimes + " A Row";
            }

            if (challengeLevel.isMode17 == true)
            {
                challengeName = "Collect Between " + challengeLevel.mode17Variables.minRequiredGemsCount + " And " + challengeLevel.mode17Variables.maxRequiredGemsCount + " Gems In x" + challengeLevel.mode17Variables.runTimes + " Run";
            }

            if (challengeLevel.isMode18 == true)
            {
                challengeName = "Collect " + challengeLevel.mode18Variables.requiredGemsCount + " Without Smashing Any Obstacle";
            }

            if (challengeLevel.isMode19 == true)
            {
                challengeName = "Smash " + challengeLevel.mode19Variables.requiredSmashedObstacles + " Obstacles In " + challengeLevel.mode19Variables.timerCountdown + "s In 1 Run";
            }

            if (challengeLevel.isMode20 == true)
            {
                challengeName = "Smash " + challengeLevel.mode20Variables.requiredSmashedObstacles + " Obstacles In " + challengeLevel.mode20Variables.timerCountdown + "s In x" + challengeLevel.mode20Variables.rowTimes + " A Row";
            }

            if (challengeLevel.isMode21 == true)
            {
                challengeName = "Smash " + challengeLevel.mode21Variables.requiredSmashedObstacles + " Obstacles In " + challengeLevel.mode21Variables.timerCountdown + "s In x" + challengeLevel.mode21Variables.runTimes + " Run";
            }

            if (challengeLevel.isMode22 == true)
            {
                challengeName = "Don't Smash Any Obstacles For " + challengeLevel.mode22Variables.timerCountdown + "s";
            }

            challengeLevel.challengeName = challengeName;
        }

        int GetRandomScoreBasedOnLevel(int i)
        {
            int score = 0;

            if(0 <= i && i <= 21)
            {
                score = Random.Range(900, 1101);
            }

            if (22 <= i && i <= 43)
            {
                score = Random.Range(1900, 2101);
            }

            if (44 <= i && i <= 65)
            {
                score = Random.Range(2900, 3101);
            }

            if (66 <= i && i <= 87)
            {
                score = Random.Range(3900, 4101);
            }

            if (88 <= i && i <= 99)
            {
                score = Random.Range(4400, 4601);
            }

            return score;
        }

        int GetRandomGemsBasedOnLevel(int i)
        {
            int gems = 0;

            if (0 <= i && i <= 21)
            {
                gems = Random.Range(10, 30);
            }

            if (22 <= i && i <= 43)
            {
                gems = Random.Range(30, 50);
            }

            if (44 <= i && i <= 65)
            {
                gems = Random.Range(40, 60);
            }

            if (66 <= i && i <= 87)
            {
                gems = Random.Range(50, 70);
            }

            if (88 <= i && i <= 99)
            {
                gems = Random.Range(55, 75);
            }

            return gems;
        }

        int GetRandomObstaclesNumberBasedOnLevel(int i)
        {
            int obstacles = 0;

            if (0 <= i && i <= 21)
            {
                obstacles = Random.Range(45, 55);
            }

            if (22 <= i && i <= 43)
            {
                obstacles = Random.Range(95, 105);
            }

            if (44 <= i && i <= 65)
            {
                obstacles = Random.Range(195, 205);
            }

            if (66 <= i && i <= 87)
            {
                obstacles = Random.Range(245, 255);
            }

            if (88 <= i && i <= 99)
            {
                obstacles = Random.Range(295, 305);
            }

            return obstacles;
        }

        int GetTimerBasedOnLevel(int i)
        {
            int timer = 0;

            if (0 <= i && i <= 21)
            {
                timer = Random.Range(30, 35);
            }

            if (22 <= i && i <= 43)
            {
                timer = Random.Range(60, 65);
            }

            if (44 <= i && i <= 65)
            {
                timer = Random.Range(90, 95);
            }

            if (66 <= i && i <= 87)
            {
                timer = Random.Range(100, 110);
            }

            if (88 <= i && i <= 99)
            {
                timer = Random.Range(120, 130);
            }

            return timer;
        }

        int GetRowOrRunTimes()
        {
            return Random.Range(2, 4);
        }

        int GetGemsRewardBasedOnLevel(int i)
        {
            int gems = 0;

            if (0 <= i && i <= 9)
            {
                gems = 100;
            }

            if (10 <= i && i <= 19)
            {
                gems = 200;
            }

            if (20 <= i && i <= 29)
            {
                gems = 300;
            }

            if (30 <= i && i <= 39)
            {
                gems = 400;
            }

            if (40 <= i && i <= 49)
            {
                gems = 500;
            }

            if (50 <= i && i <= 59)
            {
                gems = 600;
            }

            if (60 <= i && i <= 69)
            {
                gems = 700;
            }

            if (70 <= i && i <= 79)
            {
                gems = 800;
            }

            if (80 <= i && i <= 89)
            {
                gems = 900;
            }

            if (90 <= i && i <= 99)
            {
                gems = 1000;
            }

            return gems;
        }

        int GetKeysRewardBasedOnLevel(int i)
        {
            int keys = 0;

            if (0 <= i && i <= 9)
            {
                keys = 1;
            }

            if (10 <= i && i <= 19)
            {
                keys = 2;
            }

            if (20 <= i && i <= 29)
            {
                keys = 3;
            }

            if (30 <= i && i <= 39)
            {
                keys = 4;
            }

            if (40 <= i && i <= 49)
            {
                keys = 5;
            }

            if (50 <= i && i <= 59)
            {
                keys = 6;
            }

            if (60 <= i && i <= 69)
            {
                keys = 7;
            }

            if (70 <= i && i <= 79)
            {
                keys = 8;
            }

            if (80 <= i && i <= 89)
            {
                keys = 9;
            }

            if (90 <= i && i <= 99)
            {
                keys = 10;
            }

            return keys;
        }

        int GetSMRewardBasedOnLevel(int i)
        {
            int sm = 0;

            if (0 <= i && i <= 9)
            {
                sm = 1;
            }

            if (10 <= i && i <= 19)
            {
                sm = 2;
            }

            if (20 <= i && i <= 29)
            {
                sm = 3;
            }

            if (30 <= i && i <= 39)
            {
                sm = 4;
            }

            if (40 <= i && i <= 49)
            {
                sm = 5;
            }

            if (50 <= i && i <= 59)
            {
                sm = 6;
            }

            if (60 <= i && i <= 69)
            {
                sm = 7;
            }

            if (70 <= i && i <= 79)
            {
                sm = 8;
            }

            if (80 <= i && i <= 89)
            {
                sm = 9;
            }

            if (90 <= i && i <= 99)
            {
                sm = 10;
            }

            return sm;
        }
        */
}

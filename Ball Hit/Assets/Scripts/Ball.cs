using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    Player player;
    Shoot shoot;
    bool isCollide;

    public int allowedHitTimes;
    int hitTimes;

    public Text floatingScoreObject;
    Transform scorePanel;

    public List<ScoreInfo> scoreInfo = new List<ScoreInfo>();

    [System.Serializable]
    public class ScoreInfo
    {
        public string objetName;
        public int scoreValue;
    }

    void OnEnable()
    {
        shoot = Camera.main.transform.GetChild(0).GetComponent<Shoot>();
        player = Camera.main.transform.root.GetComponent<Player>();
        scorePanel = shoot.scorePanel;

        RandomRotation();
    }


    void OnTriggerEnter(Collider other)
    {
        if(hitTimes == allowedHitTimes)
        {
            DestroyAndCount();
            return;
        }

        hitTimes++;

        if (other.CompareTag("Obstacles"))
        {
            SceneSwitcher.isObstacleSmashed = true;
            if (ChallengeMode._challengeMode.IsObstacleSmashingChallenge())
            {
                SceneSwitcher.smashedObstaclesCount++;
                SceneSwitcher.sceneSwitcher.UpdateSmashedObstaclesUI();
            }

            isCollide = true;
            other.GetComponent<Obstacles>().DestroyObstacle();
            player.PlayBricksDestructionSFX();

            //for floating score text.
            var cam = Camera.main;
            Vector2 textPosition = cam.WorldToViewportPoint(other.transform.position);
            Text _text = (Text)Instantiate(floatingScoreObject, scorePanel.transform, false);
            RectTransform rt = _text.GetComponent<RectTransform>();
            rt.anchorMax = textPosition;
            rt.anchorMin = textPosition;
            for (int i = 0; i < scoreInfo.Count; i++)
            {
                if (other.name == scoreInfo[i].objetName)
                {
                    _text.text = scoreInfo[i].scoreValue.ToString() + "+";
                    int scoreToBeAdded = scoreInfo[i].scoreValue / 10;
                    player.distanceTraveled += scoreToBeAdded;
                    player.ScoreTextAnim();
                }
            }
        }

        if (other.CompareTag("GlassShards"))
        {
            if(other.GetComponent<GlassShards>() != null)
            {
                other.GetComponent<GlassShards>().BreakGlassShards();
                player.PlayGlassBreakingSFX();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pipe"))
        {
            DestroyAndCount();
        }
    }

    void DestroyAndCount()
    {
        if (!shoot.unlimitedBalls)
        {
            if (!isCollide)
            {
                shoot.ballsCount--;
                shoot.UpdateBallCountUI();
            }
        }

        Destroy(gameObject);
    }

    void RandomRotation()
    {
        float x = Random.Range(0, 180);
        float y = Random.Range(0, 180);
        float z = Random.Range(0, 180);
        float w = 0;

        transform.localRotation = new Quaternion(x, y, z, w);
    }
}

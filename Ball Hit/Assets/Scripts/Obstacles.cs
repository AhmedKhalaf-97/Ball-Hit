using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{

    public bool isThickGlass;
    Transform fracturedObstacle;
    Transform glassShards;

    void Awake()
    {
        if (!isThickGlass)
        {
            fracturedObstacle = transform.GetChild(0);
        }
        else
        {
            glassShards = transform.parent.GetChild(1);
        }
    }

    public void DestroyObstacle()
    {
        if (!isThickGlass)
        {
            fracturedObstacle.parent = null;
            fracturedObstacle.gameObject.SetActive(true);
            Destroy(gameObject);
        }
        else
        {
            glassShards.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}

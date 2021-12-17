using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFracturedObstacles : MonoBehaviour {
    void OnEnable()
    {
        Invoke("Destroy", 3f);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}

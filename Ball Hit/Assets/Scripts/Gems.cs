using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gems : MonoBehaviour
{
    public float speed = 10f;
    Transform myTransform;

    void Awake()
    {
        myTransform = transform;
    }

    void Update()
    {
        myTransform.Rotate(Vector3.right, Time.deltaTime * speed);
    }
}

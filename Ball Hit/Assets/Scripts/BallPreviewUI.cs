using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPreviewUI : MonoBehaviour
{
    public float speed = 100f;

    void Update()
    {
        transform.Rotate(Vector3.down, speed * Time.deltaTime);
    }
}

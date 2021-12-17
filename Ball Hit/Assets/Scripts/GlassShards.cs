using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassShards : MonoBehaviour
{
    Rigidbody myRigidbody;

    void OnEnable()
    {

    }

    public void BreakGlassShards()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.isKinematic = false;
        transform.tag = "Untagged";
        transform.gameObject.layer = LayerMask.NameToLayer("ObstacleShards");
        Invoke("Destroy", 3f);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}

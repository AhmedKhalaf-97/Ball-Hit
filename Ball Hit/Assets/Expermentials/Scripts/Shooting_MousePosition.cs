using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting_MousePosition : MonoBehaviour
{
    public GameObject goPrefab;
    public float propulsionForce;

    Camera _camera;

    Vector3 worldPosition;
    Vector3 shootDir;

    void Awake()
    {
        _camera = Camera.main;
    }

    void Start()
    {

    }


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ShootRay();
        }
    }

    void ShootRay()
    {
        var mouseInput = Input.mousePosition;
        Vector3 position = new Vector3(_camera.scaledPixelWidth-mouseInput.x, _camera.scaledPixelHeight-mouseInput.y, _camera.nearClipPlane);
        Vector3 shootPosition = new Vector3(mouseInput.x, mouseInput.y, _camera.nearClipPlane);

        worldPosition = _camera.ScreenToWorldPoint(position);

        Ray camRay = Camera.main.ScreenPointToRay(shootPosition);
        RaycastHit pipeHit;

        if (Physics.Raycast(camRay, out pipeHit, 200f))
        {
            shootDir = pipeHit.point - transform.position;
        }

        GameObject go = (GameObject)Instantiate(goPrefab, worldPosition, Quaternion.identity);
        go.GetComponent<Rigidbody>().AddRelativeForce(shootDir * propulsionForce, ForceMode.Acceleration);
        Destroy(go, 10f);
    }
}

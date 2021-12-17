using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkAvailability : MonoBehaviour
{
    public GameObject networkNotAvailablePanel;

    bool isConnected;

    public static NetworkAvailability instance;

    void Start()
    {
        instance = this;

        StartCoroutine(CheckInternetConnection());
    }

    public bool IsConnected()
    {
        StartCoroutine(CheckInternetConnection());

        if (!isConnected)
        {
            networkNotAvailablePanel.SetActive(true);
        }

        return isConnected;
    }

    IEnumerator CheckInternetConnection()
    {
        UnityWebRequest webRequest = new UnityWebRequest("http://google.com");
        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError)
        {
            isConnected = false;
        }
        else
        {
            isConnected = true;
        }
    }
}

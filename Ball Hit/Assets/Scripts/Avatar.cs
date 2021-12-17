using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    public bool isShieldRunning = false;
    private Player player;

    private void Awake()
    {
        player = transform.root.GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!isShieldRunning)
        {
            if (collider.CompareTag("Obstacles") || collider.CompareTag("GlassShards"))
            {
                player.PlayerHit();
            }
        }

        if (collider.CompareTag("Coins"))
        {
            player.CollectCoin();
        }
    }
}
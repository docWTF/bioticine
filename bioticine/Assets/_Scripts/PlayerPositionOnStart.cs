using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionOnStart : MonoBehaviour
{
    public GameObject player;

    void Awake()
    {
        player = PlayerStats.Instance.player;
        
        if (player != null)
        {
            player.transform.position = transform.position;
            PlayerStats.Instance.isSceneStart = false;
        }
    }

}

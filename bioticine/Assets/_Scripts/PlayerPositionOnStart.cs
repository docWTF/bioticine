using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionOnStart : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        player = PlayerStats.Instance.player;
        player.transform.position = transform.position;
    }

}

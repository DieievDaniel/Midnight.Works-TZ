using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameSystem : MonoBehaviour
{
    private GameObject player;
    private Transform spawn;
    void Start()
    {
        PhotonNetwork.Instantiate(player.name, spawn.position, Quaternion.identity);
    }

    
}

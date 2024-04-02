using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class IsMine : MonoBehaviour
{
    [SerializeField] private CarController carController;
    [SerializeField] private PhotonView photonView;
    void Start()
    {
        
        if (!photonView.IsMine)
        {
            carController.enabled = false;
            
        }
    }

    
}

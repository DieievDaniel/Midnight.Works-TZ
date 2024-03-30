using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class FhotonPlayerList : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textPlayerCounts;

    public void SetInfo(RoomInfo info)
    {
        textName.text = info.Name;
        textPlayerCounts.text = info.PlayerCount + "/" + info.MaxPlayers;
    }
}

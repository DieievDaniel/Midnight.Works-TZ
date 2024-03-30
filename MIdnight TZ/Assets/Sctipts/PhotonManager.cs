using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using TMPro;
using System.Collections.Generic;
using UnityEditorInternal.VersionControl;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField roomName;
    [SerializeField] FhotonPlayerList playerList;
    [SerializeField] Transform content;
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void CreateRoomButton()
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(roomName.text, roomOptions, TypedLobby.Default);
        PhotonNetwork.LoadLevel("GameScene");
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("Создана комната " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            FhotonPlayerList list = Instantiate(playerList, content);
            if (list != null)
            {
                list.SetInfo(info);
            }

        }
    }

}

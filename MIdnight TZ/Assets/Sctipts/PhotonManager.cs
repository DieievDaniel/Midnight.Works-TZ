using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using TMPro;
using System.Collections.Generic;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField roomName;
   // [SerializeField] FhotonPlayerList playerList;
    [SerializeField] Transform content;
    [SerializeField] private SceneLoad sceneLoad;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        // �������� ����� JoinLobby() ��� �������� ����������� � ������-�������
        PhotonNetwork.JoinLobby();
    }

    public void CreateRoomButton()
    {
        if (!PhotonNetwork.IsConnectedAndReady)
        {
            return;
        }
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(roomName.text, roomOptions, TypedLobby.Default);
        PhotonNetwork.LoadLevel("GameScene");
    }

    public void JoinRoomButton(string roomName)
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinRoom(roomName);
        }
        else
        {
            Debug.LogError("Not connected to lobby. Wait for OnJoinedLobby or OnConnectedToMaster callback.");
        }
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("������� ������� " + PhotonNetwork.CurrentRoom.Name);
        // ������� � ������� ����� ����� �������� �������
        sceneLoad.LoadNextSceneWithCar("EvoX");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("�� ������� �������������� � �������: " + message);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("�������������� � ������� " + PhotonNetwork.CurrentRoom.Name);
        // ������� � ������� ����� ����� ������������� � �������
        sceneLoad.LoadNextSceneWithCar("EvoX");
    }

   

    // ���������� ��� ������ �� ����������
    private void OnApplicationQuit()
    {
        // ���������, ���������� �� �� � Photon �������
        if (PhotonNetwork.IsConnected)
        {
            // �������� ������� ������� ����� �����������
            if (PhotonNetwork.InRoom)
            {
                PhotonNetwork.LeaveRoom();
            }

            // ����������� �� Photon �������
            PhotonNetwork.Disconnect();
        }
    }
}

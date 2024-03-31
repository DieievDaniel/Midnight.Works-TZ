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
        // Вызываем метод JoinLobby() при успешном подключении к мастер-серверу
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
        Debug.Log("Создана комната " + PhotonNetwork.CurrentRoom.Name);
        // Переход к третьей сцене после создания комнаты
        sceneLoad.LoadNextSceneWithCar("EvoX");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Не удалось присоединиться к комнате: " + message);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Присоединились к комнате " + PhotonNetwork.CurrentRoom.Name);
        // Переход к третьей сцене после присоединения к комнате
        sceneLoad.LoadNextSceneWithCar("EvoX");
    }

   

    // Вызывается при выходе из приложения
    private void OnApplicationQuit()
    {
        // Проверяем, подключены ли мы к Photon серверу
        if (PhotonNetwork.IsConnected)
        {
            // Покидаем текущую комнату перед отключением
            if (PhotonNetwork.InRoom)
            {
                PhotonNetwork.LeaveRoom();
            }

            // Отключаемся от Photon сервера
            PhotonNetwork.Disconnect();
        }
    }
}

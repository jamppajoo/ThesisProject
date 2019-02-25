using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace MultiPlayer
{
    public class MP_Launcher : MonoBehaviourPunCallbacks
    {

        [SerializeField]
        private GameObject controlPanel;
        private byte maxPlayersPerRoom = 4;

        private bool isConnecting;

        string gameVersion = "1";

        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        public void Connect()
        {

            isConnecting = true;

            controlPanel.SetActive(false);

            if(PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.GameVersion = gameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }

        }

        public override void OnConnectedToMaster()
        {
            if(isConnecting)
            {
                PhotonNetwork.JoinRandomRoom();
            }
        }
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        }
        public override void OnDisconnected(DisconnectCause cause)
        {
            isConnecting = false;
            controlPanel.SetActive(true);
        }
        public override void OnJoinedRoom()
        {
            if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                PhotonNetwork.LoadLevel(1);
            }
        }
    }
}

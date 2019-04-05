using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace MultiPlayer
{
    public class MP_Launcher : MonoBehaviourPunCallbacks
    {
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

            if (PhotonNetwork.IsConnected)
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
            if (isConnecting)
            {
                PhotonNetwork.JoinRandomRoom();
                Debug.Log("Joined Room: " + PhotonNetwork.CurrentRoom);
                Debug.Log("Other players count: " + PhotonNetwork.PlayerListOthers.Length);

            }
        }
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("Made room");
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom, PublishUserId = true });
        }
        public override void OnDisconnected(DisconnectCause cause)
        {
            isConnecting = false;
            Debug.Log("Disconnected from Room " + PhotonNetwork.CurrentRoom);
        }
    }
}

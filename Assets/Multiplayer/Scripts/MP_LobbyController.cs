using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace MultiPlayer
{
    /// <summary>
    /// Get every player and add it to local list
    /// Get other players ID 0-3 to which position they are
    /// Find available position
    /// Reserve that position by sending it's int ID
    /// 
    /// </summary>
    public class MP_LobbyController : MonoBehaviourPunCallbacks, IPunObservable
    {
        public int playerCount = 0;
        public List<MP_PlayerJoinedLobbyController> playerControllers;

        public List<RoomPlayer> joinedPlayers = new List<RoomPlayer>();

        public class RoomPlayer
        {
            public Player PlayerInstance;
            public int ID;
            public bool onTheGame;
        }

        public override void OnJoinedRoom()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.Log("Lobby Controller OnJoinedRoom Master client");
                //RoomPlayer player = new RoomPlayer() { PlayerInstance = PhotonNetwork.LocalPlayer, ID = playerCount, onTheGame = true };
                //joinedPlayers.Add(player);
                //playerCount++;
                //CheckPlayers();
            }

        }
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.Log("Lobby Controller OnPlayerEnteredRoom");
            if (PhotonNetwork.IsMasterClient)
            {
                //RoomPlayer player = new RoomPlayer() { PlayerInstance = PhotonNetwork.LocalPlayer, ID = playerCount, onTheGame = true };
                //joinedPlayers.Add(player);
                //playerCount++;
                //CheckPlayers();
            }
        }
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            if (PhotonNetwork.IsMasterClient)
            {

                //foreach (RoomPlayer item in joinedPlayers)
                //{
                //    if(item.PlayerInstance == otherPlayer)
                //    {
                //        item.onTheGame = false;
                //        joinedPlayers.Remove(item);
                //    }
                //}
                //playerCount--;
                //CheckPlayers();
            }
        }
        public override void OnLeftRoom()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                //    foreach (RoomPlayer item in joinedPlayers)
                //    {
                //        if (item.PlayerInstance == PhotonNetwork.LocalPlayer)
                //        {
                //            item.onTheGame = false;
                //            joinedPlayers.Remove(item);
                //        }
                //    }
                //    playerCount--;
                //    CheckPlayers();
            }
            PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerListOthers[0]);
            Debug.Log("Master client changed to: " + PhotonNetwork.PlayerListOthers[0]);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                //if (PhotonNetwork.IsMasterClient)
                //    stream.SendNext(playerCount);
            }
            else
            {
                //playerCount = (int)stream.ReceiveNext();
                //CheckPlayers();
            }
        }
        private void CheckPlayers()
        {
            //    foreach (RoomPlayer item in joinedPlayers)
            //    {
            //        if(item.onTheGame)
            //        {
            //            // Show players choosing menu 
            //        }
            //        else
            //        {

            //            // dont show players choosing menu 
            //        }
            //    }
            //}
        }
}

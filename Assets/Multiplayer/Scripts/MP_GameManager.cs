using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

namespace MultiPlayer
{
    public class MP_GameManager : MonoBehaviourPunCallbacks
    {
        static public MP_GameManager Instance;

        private GameObject playerPrefabInstance;

        [SerializeField]
        private GameObject playerPrefab;

        private void Start()
        {
            Instance = this;

            PhotonNetwork.SendRate = 32;
            PhotonNetwork.SerializationRate = 24;

            if (!PhotonNetwork.IsConnected)
            {
                SceneManager.LoadScene(0);
                return;
            }

            if (playerPrefab == null)
            {
                Debug.LogError("Missing playerPrefab instance");
                return;
            }

            if (MP_PlayerManager.LocalPlayerInstance == null)
            {
                playerPrefabInstance = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(-5, 0, 0), Quaternion.identity, 0);
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                QuitApplication();
            }
        }
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                LoadArena();
            }
        }
        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }
        public void QuitApplication()
        {
            Application.Quit();
        }

        private void LoadArena()
        {
            PhotonNetwork.LoadLevel(1);
        }
    }
}

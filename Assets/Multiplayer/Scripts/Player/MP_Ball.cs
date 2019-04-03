using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace MultiPlayer
{
    public class MP_Ball : MonoBehaviourPunCallbacks
    {
        public string playerID = "P1";

        [HideInInspector]
        public LayerMask myLayerMask;

        private void Awake()
        {
            myLayerMask = gameObject.layer;
        }
        private void Start()
        {
            Camera.main.GetComponent<MP_CameraMovement>().AddTarget(gameObject.transform);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debug.Log("PlayerLeftRoom1");
            if (photonView.IsMine)
            {
                Debug.Log("PlayerLeftRoom2");

            }
        }
    }
}

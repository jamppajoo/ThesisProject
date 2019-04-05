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

        private MP_CameraMovement camera;

        private void Awake()
        {
            myLayerMask = gameObject.layer;
            camera = Camera.main.GetComponent<MP_CameraMovement>();


        }
        private void Start()
        {
            camera.AddTarget(gameObject.transform);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            if (photonView.IsMine)
            {
                camera.RemoveTarget(gameObject.transform);

            }
        }
    }
}

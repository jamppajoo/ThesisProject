using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace MultiPlayer
{
    [RequireComponent(typeof(MP_BallMovement))]
    public class MP_PlayerManager : MonoBehaviourPunCallbacks
    {

        public static GameObject LocalPlayerInstance;
        private MP_BallMovement ballMovement;

        private void Awake()
        {
            ballMovement = gameObject.GetComponent<MP_BallMovement>();
            if(photonView.IsMine)
            {
                LocalPlayerInstance = gameObject;
            }
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if(photonView.IsMine)
            {
                ballMovement.ProcessInputs();
            }

        }

    }
}

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
        private MP_BallChangeColor ballChangeColor;
        private MP_BallSpeedBoost ballSpeedBoost;

        private void Awake()
        {
            ballMovement = gameObject.GetComponent<MP_BallMovement>();

            if (gameObject.HasComponent<MP_BallChangeColor>())
                ballChangeColor = GetComponent<MP_BallChangeColor>();

            if (gameObject.HasComponent<MP_BallSpeedBoost>())
            {
                ballSpeedBoost = GetComponent<MP_BallSpeedBoost>();
            }

            if (photonView.IsMine)
            {
                LocalPlayerInstance = gameObject;
            }
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (photonView.IsMine)
            {
                ballMovement.ProcessInputs();
                if (ballChangeColor != null)
                    ballChangeColor.ProcessInputs();
                if (ballSpeedBoost != null)
                    ballSpeedBoost.ProcessInputs();
            }

        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MultiPlayer
{
    public class MP_BallSpeedBoost : MonoBehaviour
    {
        public float maxSpeedBoostAmount;
        private string playerID;


        private PlayerUIManager playerUIManager;

        private Image fillBar;

        private MP_Ball ball;
        private MP_BallMovement ballMovement;
        [SerializeField]
        private float speedBoostAmount = 1;
        public float speedBoostAddAmount = 1;

        private void Awake()
        {
            playerID = GetComponent<MP_Ball>().playerID;
            playerUIManager = GameObject.Find(playerID + "UIArea").GetComponent<PlayerUIManager>();

            ball = GetComponent<MP_Ball>();
            ballMovement = GetComponent<MP_BallMovement>();
        }
        void Start()
        {
            playerUIManager.SetSpeedBoostMaxValue(maxSpeedBoostAmount);
        }


        public void SpeedBoostActivated(bool activated)
        {

            if (speedBoostAmount > 0 && activated)
            {
                speedBoostAmount -= Time.deltaTime;
                ballMovement.currentMaxVelocity = ballMovement.boostedVelocity;
                ballMovement.torgueMultiplier = ballMovement.boostingTorgueMultiplier;
            }
            else
            {
                ballMovement.currentMaxVelocity = ballMovement.normalVelocity;
                ballMovement.torgueMultiplier = ballMovement.normalTorgueMultiplier;
            }

            playerUIManager.SetSpeedBoostValue(speedBoostAmount);

        }

        public void changeBarColor(Color color)
        {
            playerUIManager.SetSpeedBoostColor(color);
        }

        public void AddSpeedBoost()
        {
            if ((speedBoostAmount + speedBoostAddAmount) < maxSpeedBoostAmount)
            {
                speedBoostAmount += speedBoostAddAmount;
            }
            else
                speedBoostAmount = maxSpeedBoostAmount;
        }
    }
}

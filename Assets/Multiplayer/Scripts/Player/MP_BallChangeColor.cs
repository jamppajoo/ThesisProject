using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Photon.Pun;

namespace MultiPlayer
{
    [RequireComponent(typeof(MP_Ball))]
    public class MP_BallChangeColor : MonoBehaviourPunCallbacks, IPunObservable
    {
        private LevelManager levelManager;

        private PlayerUIManager playerUIManager;

        private string playerID;

        private Material playerMaterial;
        private bool jump;

        private float deadZoneValue = 1f;

        private MP_BallSpeedBoost ballSpeedBoost;

        private MP_Ball ball;

        private Collider2D triggerCollider;

        private bool playerInsideObject = false;

        private string networkColorName = "";
        private void Awake()
        {
            levelManager = FindObjectOfType<LevelManager>();
            playerID = GetComponent<MP_Ball>().playerID;

            ballSpeedBoost = GetComponent<MP_BallSpeedBoost>();
            ball = GetComponent<MP_Ball>();
            triggerCollider = gameObject.GetComponentInChildren<CircleCollider2D>();

            playerUIManager = GameObject.Find(playerID + "UIArea").GetComponent<PlayerUIManager>();
        }

        void Start()
        {
            playerMaterial = gameObject.GetComponent<Renderer>().material;
        }


        void Update()
        {

            //For testing purposes
            if (Input.GetKeyDown(KeyCode.U))
            {
                RedChoosed();
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                GreenChoosed();
            }
            else if (Input.GetKeyDown(KeyCode.O))
            {
                YellowChoosed();
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                BlueChoosed();
            }
        }

        public void ProcessInputs()
        {
            float horPos = CrossPlatformInputManager.GetAxis("MP_Horizontal2");
            float verPos = CrossPlatformInputManager.GetAxis("MP_Vertical2");
            ChangeBallColor(verPos, horPos);

        }
        public void ChangeBallColor(float verPos, float horPos)
        {
            horPos *= 40;
            verPos *= 40;
            if (!playerInsideObject)
            {
                if (Mathf.Abs(horPos) > deadZoneValue || Mathf.Abs(verPos) > deadZoneValue)
                {
                    playerUIManager.DisplayColorSelector(true);

                    if (verPos > horPos && verPos > horPos * -1)
                    {
                        RedChoosed();
                    }
                    else if (verPos < horPos && verPos < horPos * -1)
                    {
                        YellowChoosed();
                    }
                    else if (horPos > verPos && horPos > verPos * -1)
                    {
                        GreenChoosed();
                    }
                    else if (horPos < verPos && horPos < verPos * -1)
                    {
                        BlueChoosed();
                    }

                }
                else
                    playerUIManager.DisplayColorSelector(false);
            }
            else
                playerUIManager.DisplayColorSelector(false);

            playerUIManager.MoveColorSelector(new Vector3(horPos * -1, verPos, 0));
        }

        public void TogglePlayerInsideObject(bool isInside)
        {
            playerInsideObject = isInside;
        }

        private void DefaultChoosed()
        {
            playerMaterial.color = Color.white;
            ballSpeedBoost.changeBarColor(Color.white);
            gameObject.layer = LayerMask.NameToLayer("Default");
            ball.myLayerMask = gameObject.layer;
            networkColorName = "Default";
        }

        private void RedChoosed()
        {
            playerMaterial.color = levelManager.colorRed;
            ballSpeedBoost.changeBarColor(levelManager.colorRed);
            gameObject.layer = LayerMask.NameToLayer("RedObject");
            ball.myLayerMask = gameObject.layer;
            networkColorName = "Red";
        }
        private void GreenChoosed()
        {
            playerMaterial.color = levelManager.colorGreen;
            ballSpeedBoost.changeBarColor(levelManager.colorGreen);
            gameObject.layer = LayerMask.NameToLayer("GreenObject");
            ball.myLayerMask = gameObject.layer;
            networkColorName = "Green";
        }
        private void YellowChoosed()
        {
            playerMaterial.color = levelManager.colorYellow;
            ballSpeedBoost.changeBarColor(levelManager.colorYellow);
            gameObject.layer = LayerMask.NameToLayer("YellowObject");
            ball.myLayerMask = gameObject.layer;
            networkColorName = "Yellow";
        }
        private void BlueChoosed()
        {
            playerMaterial.color = levelManager.colorBlue;
            ballSpeedBoost.changeBarColor(levelManager.colorBlue);
            gameObject.layer = LayerMask.NameToLayer("BlueObject");
            ball.myLayerMask = gameObject.layer;
            networkColorName = "Blue";
        }
        private void NetworkChangeColor(string color)
        {
            switch (color)
            {
                case "Red":
                    RedChoosed();
                    break;
                case "Green":
                    GreenChoosed();
                    break;
                case "Yellow":
                    YellowChoosed();
                    break;
                case "Blue":
                    BlueChoosed();
                    break;
                default:
                    DefaultChoosed();
                    break;
            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(networkColorName);
            }
            else
            {
                networkColorName = (string)stream.ReceiveNext();
                NetworkChangeColor(networkColorName);
            }
        }
    }
}

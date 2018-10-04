using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Ball
{
    public class BallUserControl : MonoBehaviour
    {
        private string playerID;
        private Ball ball; // Reference to the ball controller.

        private Vector3 move;
        // the world-relative desired move direction, calculated from the camForward and user input.

        private Transform cam; // A reference to the main camera in the scenes transform
        private Vector3 camForward; // The current forward direction of the camera
        private bool jump; // whether the jump button is currently pressed



        private void Awake()
        {
            ball = GetComponent<Ball>();
        }
        private void Start()
        {
            playerID = ball.playerID;
            cam = Camera.main.transform;
        }

        private void Update()
        {
            // Get the axis and jump input.

            float h = Input.GetAxis("Horizontal_" + playerID);
            jump = Input.GetButton("Jump_" + playerID);

            // calculate move direction
            if (cam != null)
            {
                // calculate camera relative direction to move:
                camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
                move = (h * cam.right).normalized;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                move = (h * Vector3.right).normalized;
            }
        }


        private void FixedUpdate()
        {
            // Call the Move function of the ball controller
            ball.Move(move, jump);
            jump = false;
        }
    }
}

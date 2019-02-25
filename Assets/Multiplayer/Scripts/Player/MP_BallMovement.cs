using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiPlayer
{
    public class MP_BallMovement : MonoBehaviour
    {
        [SerializeField]
        private float movePower = 5;
        [SerializeField]
        private float airControl = 2;

        public float currentMaxVelocity = 12;
        public float normalVelocity = 12;
        public float boostedVelocity = 15;

        public float torgueMultiplier = 1;
        public float normalTorgueMultiplier = 1;
        public float boostingTorgueMultiplier = 2;

        [SerializeField] private float jumpPower = 2;
        public float jumpTime = 0.2f;

        [HideInInspector]
        public LayerMask myLayerMask;


        private const float groundRayLength = .6f;
        private Rigidbody2D ballRB;

        private bool canJump = false;
        private float jumpTimer = 0;

        RaycastHit2D groundHit;

        private bool jump;
        private Transform cam;

        private Vector3 camForward;

        private Vector3 movement;

        private void Awake()
        {
            cam = Camera.main.transform;
            ballRB = gameObject.GetComponent<Rigidbody2D>();
        }
        public void ProcessInputs()
        {
            float h = Input.GetAxis("MP_Horizontal");
            jump = Input.GetButton("MP_Jump");

            if (cam != null)
            {
                camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
                movement = (h * cam.right).normalized;
            }
            else
            {
                movement = (h * Vector3.right).normalized;
            }
            Move(movement, jump);
        }


        public void Move(Vector2 moveDirection, bool jump)
        {
            Vector2 moveForce = Vector2.right * moveDirection.x * movePower * torgueMultiplier;
            Vector2 airForce = Vector2.right * moveDirection.x * airControl * torgueMultiplier;

            groundHit = Physics2D.Raycast(transform.position, -Vector2.up, groundRayLength);


            if (ballRB.velocity.x < currentMaxVelocity && moveForce.x > 0 || ballRB.velocity.x > -currentMaxVelocity && moveForce.x < 0)
            {
                if (groundHit.collider != null)
                {
                    ballRB.AddForce(moveForce);
                }
                else
                {
                    ballRB.AddForce(airForce);
                }
            }
            else
            {
                ballRB.AddForce(-ballRB.velocity);
            }

            Jump(jump);

        }

        private void Jump(bool jump)
        {
            bool grounded = false;
            if (groundHit.collider != null && groundHit.collider.gameObject.layer != myLayerMask)
                grounded = true;
            else
                grounded = false;

            if (grounded)
                canJump = true;
            if (canJump && jump && jumpTimer < jumpTime)
            {
                jumpTimer += Time.fixedDeltaTime;
                ballRB.AddForce(Vector2.up * jumpPower);
            }
            else
            {
                jumpTimer = 0;
                canJump = false;
            }

        }
    }
}

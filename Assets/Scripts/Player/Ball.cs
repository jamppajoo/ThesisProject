using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Ball
{
    [RequireComponent(typeof(BallUserControl))]
    public class Ball : MonoBehaviour
    {
        public string playerID;

        [SerializeField] private float m_MovePower = 5; // The force added to the ball to move it.
        public float m_MaxAngularVelocity = 25; // The maximum velocity the ball can rotate at.
        public float m_NormalAngularVelocity = 25; // The maximum velocity the ball can rotate at.
        public float m_BoostedAngularVelocity = 25; // The maximum velocity the ball can rotate at.

        [SerializeField] private float m_JumpPower = 2; // The force added to the ball when it jumps.

        private const float k_GroundRayLength = 1f; // The length of the ray to check if the ball is grounded.
        private Rigidbody2D m_Rigidbody;


        private void Start()
        {
            m_Rigidbody = GetComponent<Rigidbody2D>();
        }
        private void Awake()
        {
            Camera.main.GetComponent<MultipleCameraTarget>().targets.Add(gameObject.transform);
        }


        public void Move(Vector2 moveDirection, bool jump)
        {
            float torgue = moveDirection.x * -1 * m_MovePower;
            
            if ((m_Rigidbody.angularVelocity < m_MaxAngularVelocity && torgue > 0) 
                || (m_Rigidbody.angularVelocity > -m_MaxAngularVelocity && torgue < 0))
                    m_Rigidbody.AddTorque(torgue);
            

            if (Physics2D.Raycast(transform.position, -Vector2.up, k_GroundRayLength) && jump )
            {
                m_Rigidbody.AddForce(Vector2.up * m_JumpPower, ForceMode2D.Impulse);
            }
        }

    }
}

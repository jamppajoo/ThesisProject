using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BallUserControl))]
public class Ball : MonoBehaviour
{
    /// <summary>
    /// Ball jumping on same color platforms
    ///
    /// </summary>



    public string playerID;

    [SerializeField] private float movePower = 5;
    [SerializeField] private float airControl = 2;

    public float currentMaxVelocity = 12;
    public float normalVelocity = 12;
    public float boostedVelocity = 15;

    public float torgueMultiplier = 1;
    public float normalTorgueMultiplier = 1;
    public float boostingTorgueMultiplier = 2;

    [SerializeField] private float jumpPower = 2; // The force added to the ball when it jumps.

    private const float groundRayLength = 1f; // The length of the ray to check if the ball is grounded.
    private Rigidbody2D ballRB;
    public LayerMask myLayerMask;

    private void Awake()
    {
        ballRB = GetComponent<Rigidbody2D>();
        Camera.main.GetComponent<MultipleCameraTarget>().targets.Add(gameObject.transform);

        myLayerMask = gameObject.layer;
    }


    public void Move(Vector2 moveDirection, bool jump)
    {

        Vector2 moveForce = Vector2.right * moveDirection.x * movePower * torgueMultiplier;
        Vector2 airForce = Vector2.right * moveDirection.x * airControl * torgueMultiplier;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, groundRayLength);

        if (ballRB.velocity.x < currentMaxVelocity && moveForce.x > 0 || ballRB.velocity.x > -currentMaxVelocity && moveForce.x < 0)
        {
            if (hit.collider != null)
            {
                ballRB.AddForce(moveForce);
            }
            else
                ballRB.AddForce(airForce);
        }
        else
            ballRB.AddForce(-ballRB.velocity);
        

        if (hit.collider != null && jump)
        {
            if (hit.collider.gameObject.layer != myLayerMask)
                ballRB.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

}


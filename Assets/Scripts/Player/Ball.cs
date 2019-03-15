using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles player movement according to BallUserControl input
/// </summary>

[RequireComponent(typeof(BallUserControl))]
public class Ball : MonoBehaviour
{

    public string playerID;

    [SerializeField] private float movePower = 5;
    [SerializeField] private float airControl = 2;

    public float currentMaxVelocity = 12;
    public float normalVelocity = 12;
    public float boostedVelocity = 15;

    public float torgueMultiplier = 1;
    public float normalTorgueMultiplier = 1;
    public float boostingTorgueMultiplier = 2;

    [SerializeField] private float jumpPower = 2;

    [HideInInspector]
    public LayerMask myLayerMask;

    public float jumpTime = 0.2f;
    public float jumpDampening = 0.2f;

    private const float groundRayLength = .6f;
    private Rigidbody2D ballRB;

    private bool canJump = false;
    private float jumpTimer = 0;

    private bool jumping = false;

    RaycastHit2D groundHit;

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
            ballRB.AddForce(new Vector2(-ballRB.velocity.x, 0));

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

            if (grounded && !jumping)
            {
                ballRB.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                jumping = true;
            }
        }
        else
        {
            jumpTimer = 0;
            canJump = false;
            jumping = false;
        }
        if (!jump && ballRB.velocity.y > 0)
        {
            ballRB.AddForce(new Vector2(0, -ballRB.velocity.y / jumpDampening));
        }

    }
}


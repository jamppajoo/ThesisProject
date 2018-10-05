using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class BallUserControl : MonoBehaviour
{
    private string playerID;
    private Ball ball; 

    private Vector3 move;

    private Transform cam;
    private Vector3 camForward; 
    private bool jump; 



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
        
        if (cam != null)
        {
            camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
            move = (h * cam.right).normalized;
        }
        else
        {
            move = (h * Vector3.right).normalized;
        }
    }

    private void FixedUpdate()
    {
        jump = Input.GetButton("Jump_" + playerID);
        ball.Move(move, jump);
        print("ASD: " + jump);
        jump = false;
    }
}


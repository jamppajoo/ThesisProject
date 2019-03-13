using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Ball))]
public class BallUserControl : MonoBehaviour
{
    private string playerID;
    private Ball ball;
    private BallSpeedBoost ballSpeedBoost;
    private BallChangeColor ballChangeColor;

    private Vector3 move;

    private Transform cam;
    private Vector3 camForward;
    private bool jump;

    private bool speedBoost;

    private float rightStickVerticalPos, rightStickHorizontalPos;

    private void Awake()
    {
        ball = GetComponent<Ball>();

        if (gameObject.HasComponent<BallSpeedBoost>())
            ballSpeedBoost = GetComponent<BallSpeedBoost>();

        if (gameObject.HasComponent<BallChangeColor>())
            ballChangeColor = GetComponent<BallChangeColor>();
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

        speedBoost = Input.GetButton("Boost_" + playerID);

        rightStickVerticalPos = CrossPlatformInputManager.GetAxis("Vertical2_" + playerID);
        rightStickHorizontalPos = CrossPlatformInputManager.GetAxis("Horizontal2_" + playerID);

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
        ball.Move(move, jump);
        ballSpeedBoost.SpeedBoostActivated(speedBoost);
        ballChangeColor.ChangeBallColor(rightStickVerticalPos, rightStickHorizontalPos);
    }
}


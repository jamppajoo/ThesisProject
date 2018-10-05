using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Ball))]
[RequireComponent(typeof(BallSpeedBoost))]
public class BallChangeColor : MonoBehaviour
{
    private LevelManager levelManager;

    private PlayerUIManager playerUIManager;

    private string playerID;

    private Material playerMaterial;
    private bool jump;

    private float deadZoneValue = 1f;

    private BallSpeedBoost ballSpeedBoost;

    private Ball ball;

    private Collider2D triggerCollider;

    private bool playerInsideObject = false;
    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        playerID = GetComponent<Ball>().playerID;

        ballSpeedBoost = GetComponent<BallSpeedBoost>();
        ball = GetComponent<Ball>();
        triggerCollider = gameObject.GetComponentInChildren<CircleCollider2D>();

        playerUIManager = GameObject.Find(playerID + "UIArea").GetComponent<PlayerUIManager>();
    }

    void Start()
    {
        playerMaterial = gameObject.GetComponent<Renderer>().material;
    }


    void Update()
    {

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



        float rightStickVerticalPosition = CrossPlatformInputManager.GetAxis("Vertical2_" + playerID);
        float rightStickHorizontalPosition = CrossPlatformInputManager.GetAxis("Horizontal2_" + playerID);

        rightStickHorizontalPosition *= 40;
        rightStickVerticalPosition *= 40;
        if (!playerInsideObject)
        {
            if (Mathf.Abs(rightStickHorizontalPosition) > deadZoneValue || Mathf.Abs(rightStickVerticalPosition) > deadZoneValue)
            {
                playerUIManager.DisplayColorSelector(true);

                if (rightStickVerticalPosition > rightStickHorizontalPosition && rightStickVerticalPosition > rightStickHorizontalPosition * -1)
                {
                    RedChoosed();
                }
                else if (rightStickVerticalPosition < rightStickHorizontalPosition && rightStickVerticalPosition < rightStickHorizontalPosition * -1)
                {
                    YellowChoosed();
                }
                else if (rightStickHorizontalPosition > rightStickVerticalPosition && rightStickHorizontalPosition > rightStickVerticalPosition * -1)
                {
                    GreenChoosed();
                }
                else if (rightStickHorizontalPosition < rightStickVerticalPosition && rightStickHorizontalPosition < rightStickVerticalPosition * -1)
                {
                    BlueChoosed();
                }

            }
            else
                playerUIManager.DisplayColorSelector(false);
        }
        else
            playerUIManager.DisplayColorSelector(false);

        playerUIManager.MoveColorSelector(new Vector3(rightStickHorizontalPosition * -1, rightStickVerticalPosition, 0));


    }

    public void TogglePlayerInsideObject(bool isInside)
    {
        playerInsideObject = isInside;
    }

    void RedChoosed()
    {
        playerMaterial.color = levelManager.colorRed;
        ballSpeedBoost.changeBarColor(levelManager.colorRed);
        gameObject.layer = LayerMask.NameToLayer("RedObject");
        ball.myLayerMask = gameObject.layer;
    }
    void GreenChoosed()
    {
        playerMaterial.color = levelManager.colorGreen;
        ballSpeedBoost.changeBarColor(levelManager.colorGreen);
        gameObject.layer = LayerMask.NameToLayer("GreenObject");
        ball.myLayerMask = gameObject.layer;
    }
    void YellowChoosed()
    {
        playerMaterial.color = levelManager.colorYellow;
        ballSpeedBoost.changeBarColor(levelManager.colorYellow);
        gameObject.layer = LayerMask.NameToLayer("YellowObject");
        ball.myLayerMask = gameObject.layer;
    }
    void BlueChoosed()
    {
        playerMaterial.color = levelManager.colorBlue;
        ballSpeedBoost.changeBarColor(levelManager.colorBlue);
        gameObject.layer = LayerMask.NameToLayer("BlueObject");
        ball.myLayerMask = gameObject.layer;
    }
}

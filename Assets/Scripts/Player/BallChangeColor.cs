using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


/// <summary>
/// Handles changing ball color and layer according to right joystick input
/// </summary>

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
    public void ChangeBallColor(float horPos, float verPos)
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

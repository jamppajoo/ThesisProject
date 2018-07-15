using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Vehicles.Ball;

public class BallChangeColor : MonoBehaviour
{

    private LevelManager levelManager;

    private string playerID;

    private Material playerMaterial;
    private bool jump;
    private Vector3 rightStickOriginalPosition;

    private float deadZoneValue = 1f;

    private BallSpeedBoost ballSpeedBoost;

    private GameObject rightStickPlace, rightStickArea;
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        playerID = GetComponent<Ball>().playerID;

        ballSpeedBoost = GetComponent<BallSpeedBoost>();

        playerMaterial = gameObject.GetComponent<Renderer>().material;
        rightStickPlace = GameObject.Find("RightStickPlace" + playerID);
        rightStickArea = GameObject.Find("RightStickArea" + playerID);
        rightStickOriginalPosition = rightStickPlace.transform.localPosition;

        rightStickArea.SetActive(true);
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

        if (Mathf.Abs(rightStickHorizontalPosition) > deadZoneValue || Mathf.Abs(rightStickVerticalPosition) > deadZoneValue)
        {
            rightStickArea.SetActive(true);

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
                BlueChoosed();
            }
            else if (rightStickHorizontalPosition < rightStickVerticalPosition && rightStickHorizontalPosition < rightStickVerticalPosition * -1)
            {
                GreenChoosed();
            }
            
        }
        else
            rightStickArea.SetActive(false);

        rightStickPlace.transform.localPosition = new Vector3(rightStickOriginalPosition.x + rightStickHorizontalPosition * -1, rightStickOriginalPosition.y + rightStickVerticalPosition, rightStickOriginalPosition.z);


    }


    void RedChoosed()
    {
        playerMaterial.color = levelManager.colorRed;
        ballSpeedBoost.changeBarColor(levelManager.colorRed);
        gameObject.layer = LayerMask.NameToLayer("RedObject");
    }
    void GreenChoosed()
    {
        playerMaterial.color = levelManager.colorGreen;
        ballSpeedBoost.changeBarColor(levelManager.colorGreen);
        gameObject.layer = LayerMask.NameToLayer("GreenObject");
    }
    void YellowChoosed()
    {
        playerMaterial.color = levelManager.colorYellow;
        ballSpeedBoost.changeBarColor(levelManager.colorYellow);
        gameObject.layer = LayerMask.NameToLayer("YellowObject");
    }
    void BlueChoosed()
    {
        playerMaterial.color = levelManager.colorBlue;
        ballSpeedBoost.changeBarColor(levelManager.colorBlue);
        gameObject.layer = LayerMask.NameToLayer("BlueObject");
    }
}

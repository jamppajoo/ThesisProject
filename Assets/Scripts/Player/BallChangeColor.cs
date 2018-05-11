using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Vehicles.Ball;

public class BallChangeColor : MonoBehaviour
{

    private string playerID;

    private Material playerMaterial;
    private bool jump;
    private Vector3 rightStickOriginalPosition;

    private float deadZoneValue = 1f;

    private BallSpeedBoost ballSpeedBoost;

    private GameObject rightStickPlace, rightStickArea;
    void Start()
    {
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
            redChoosed();
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            greenChoosed();
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            yellowChoosed();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            pinkChoosed();
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
                redChoosed();
            }
            else if (rightStickVerticalPosition < rightStickHorizontalPosition && rightStickVerticalPosition < rightStickHorizontalPosition * -1)
            {
                yellowChoosed();
            }
            else if (rightStickHorizontalPosition > rightStickVerticalPosition && rightStickHorizontalPosition > rightStickVerticalPosition * -1)
            {
                pinkChoosed();
            }
            else if (rightStickHorizontalPosition < rightStickVerticalPosition && rightStickHorizontalPosition < rightStickVerticalPosition * -1)
            {
                greenChoosed();
            }





        }
        else
            rightStickArea.SetActive(false);
        rightStickPlace.transform.localPosition = new Vector3(rightStickOriginalPosition.x + rightStickHorizontalPosition * -1, rightStickOriginalPosition.y + rightStickVerticalPosition, rightStickOriginalPosition.z);


    }


    void redChoosed()
    {
        playerMaterial.color = Color.red;
        ballSpeedBoost.changeBarColor(Color.red);
        gameObject.layer = LayerMask.NameToLayer("RedObject");
    }
    void greenChoosed()
    {
        playerMaterial.color = Color.green;
        ballSpeedBoost.changeBarColor(Color.green);
        gameObject.layer = LayerMask.NameToLayer("GreenObject");
    }
    void yellowChoosed()
    {
        playerMaterial.color = Color.yellow;
        ballSpeedBoost.changeBarColor(Color.yellow);
        gameObject.layer = LayerMask.NameToLayer("YellowObject");
    }
    void pinkChoosed()
    {
        playerMaterial.color = new Color(255, 0, 216);
        ballSpeedBoost.changeBarColor(new Color(255, 0, 216));
        gameObject.layer = LayerMask.NameToLayer("PinkObject");
    }
}

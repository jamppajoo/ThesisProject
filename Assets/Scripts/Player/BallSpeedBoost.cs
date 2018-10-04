using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Ball;

public class BallSpeedBoost : MonoBehaviour {

    public float maxSpeedBoostAmount;
    private string playerID;

    private bool speedBoostActivated;

    private PlayerUIManager playerUIManager;

    private Image fillBar;

    private Ball ball;

    private float speedBoostAmount = 1;

    private void Awake()
    {
        playerID = GetComponent<Ball>().playerID;
        playerUIManager = GameObject.Find(playerID + "UIArea").GetComponent<PlayerUIManager>();

        ball = GetComponent<Ball>();
    }
    void Start () {
        playerUIManager.SetSpeedBoostMaxValue(maxSpeedBoostAmount);
	}
	
	void Update () {

        speedBoostActivated = CrossPlatformInputManager.GetButton("Boost_" + playerID);

        if(speedBoostAmount > 0 && speedBoostActivated)
        {
            speedBoostAmount -= Time.deltaTime;
            ball.m_MaxAngularVelocity = ball.m_BoostedAngularVelocity;
        }
        else
            ball.m_MaxAngularVelocity = ball.m_NormalAngularVelocity;

        playerUIManager.SetSpeedBoostValue(speedBoostAmount);

    }

    public void changeBarColor(Color color)
    {
        playerUIManager.SetSpeedBoostColor(color);
    }
    

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "SpeedBoost")
        {
            if (speedBoostAmount + 1.5f < maxSpeedBoostAmount)
            {
                speedBoostAmount += 1.5f;
            }
            else
                speedBoostAmount = maxSpeedBoostAmount;

            Destroy(collision.gameObject);
        }
    }



}

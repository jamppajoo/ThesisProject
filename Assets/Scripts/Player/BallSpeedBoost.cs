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
    private Image fillBar;
    private Slider speedBoostSlider;

    private Ball ball;

    private float speedBoostAmount = 1;
    // Use this for initialization
    void Start () {

        playerID = GetComponent<Ball>().playerID;

        fillBar = GameObject.Find("Fill" + playerID).GetComponent<Image>();
        speedBoostSlider = GameObject.Find("Slider" + playerID).GetComponent<Slider>();
        speedBoostSlider.maxValue = maxSpeedBoostAmount;
        ball = GetComponent<Ball>();
	}
	
	// Update is called once per frame
	void Update () {

        speedBoostActivated = CrossPlatformInputManager.GetButton("Boost_" + playerID);

        if(speedBoostAmount > 0 && speedBoostActivated)
        {
            speedBoostAmount -= Time.deltaTime;
            ball.m_MaxAngularVelocity = ball.m_BoostedAngularVelocity;
        }
        else
            ball.m_MaxAngularVelocity = ball.m_NormalAngularVelocity;
        ball.changeAngularVelocity();


        speedBoostSlider.value = speedBoostAmount;

    }

    public void changeBarColor(Color color)
    {
        fillBar.color = color;
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

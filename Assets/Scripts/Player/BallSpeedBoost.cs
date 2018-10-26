using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class BallSpeedBoost : MonoBehaviour {

    public float maxSpeedBoostAmount;
    private string playerID;

    private bool speedBoostActivated;

    private PlayerUIManager playerUIManager;

    private Image fillBar;

    private Ball ball;
    [SerializeField]
    private float speedBoostAmount = 1;
    public float speedBoostAddAmount = 1;

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

        speedBoostActivated = Input.GetButton("Boost_" + playerID);

        if(speedBoostAmount > 0 && speedBoostActivated)
        {
            speedBoostAmount -= Time.deltaTime;
            ball.currentMaxVelocity = ball.boostedVelocity;
            ball.torgueMultiplier = ball.boostingTorgueMultiplier;
        }
        else
        {
            ball.currentMaxVelocity = ball.normalVelocity;
            ball.torgueMultiplier = ball.normalTorgueMultiplier;
        }

        playerUIManager.SetSpeedBoostValue(speedBoostAmount);

    }

    public void changeBarColor(Color color)
    {
        playerUIManager.SetSpeedBoostColor(color);
    }

    public void AddSpeedBoost()
    {
        if ((speedBoostAmount + speedBoostAddAmount) < maxSpeedBoostAmount)
        {
            speedBoostAmount += speedBoostAddAmount;
        }
        else
            speedBoostAmount = maxSpeedBoostAmount;
    }
}

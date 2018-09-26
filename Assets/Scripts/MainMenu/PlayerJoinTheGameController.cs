using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerJoinTheGameController : MonoBehaviour
{

    private string myPlayerID;
    private RectTransform playerJoinedGameImage;
    private RectTransform joinTheGameText;

    private bool playerHasJoinedGame = false;

    void Start()
    {
        myPlayerID = gameObject.transform.name.Substring(0, 2);
        joinTheGameText = gameObject.transform.GetChild(0).Find("JoinTheGameText").GetComponent<RectTransform>();
        playerJoinedGameImage = gameObject.transform.GetChild(0).Find("PlayerChoosing").GetComponent<RectTransform>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (!playerHasJoinedGame)
                JoinTheGame();
            else
                LeaveTheGame();
        }

    }

    private void JoinTheGame()
    {
        joinTheGameText.position = playerJoinedGameImage.position;
        playerJoinedGameImage.localPosition = Vector3.zero;
        playerHasJoinedGame = true;
    }
    private void LeaveTheGame()
    {
        playerJoinedGameImage.position = joinTheGameText.position;
        joinTheGameText.localPosition = Vector3.zero;
        playerHasJoinedGame = false;
    }
}

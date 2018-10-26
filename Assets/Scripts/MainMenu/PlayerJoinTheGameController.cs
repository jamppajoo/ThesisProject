using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerJoinTheGameController : MonoBehaviour
{
    [HideInInspector]
    public bool playerCanModify = false;

    private string myPlayerID;
    private string myPlayerName;

    private Sprite myPlayerSprite;
    private RectTransform playerJoinedGameImage;
    private RectTransform joinTheGameText;

    private Vector3 playerJoinedGameImageOriginalPosition;
    private Vector3 joinTheGameTextOriginalPosition;

    private bool playerHasJoinedGame = false;

    private void Awake()
    {
        myPlayerID = gameObject.transform.name.Substring(0, 2);
        joinTheGameText = gameObject.transform.GetChild(0).Find("JoinTheGameText").GetComponent<RectTransform>();
        playerJoinedGameImage = gameObject.transform.GetChild(0).Find("PlayerChoosing").GetComponent<RectTransform>();
        gameObject.GetComponentInChildren<PlayerChangeInfo>().SetMyPlayerID(myPlayerID);
    }

    private void Start()
    {
        playerJoinedGameImageOriginalPosition = playerJoinedGameImage.transform.localPosition;
        joinTheGameTextOriginalPosition = joinTheGameText.transform.localPosition;
    }

    void Update()
    {
        if (playerCanModify)
        {
            if (Input.GetButton("Jump_" + myPlayerID))
            {
                if (!playerHasJoinedGame)
                    JoinTheGame();
                else
                    LockSelection();
            }
            if (Input.GetButton("B_" + myPlayerID))
                LeaveTheGame();
        }
    }

    private void JoinTheGame()
    {
        joinTheGameText.localPosition = playerJoinedGameImage.localPosition;
        playerJoinedGameImage.localPosition = Vector3.zero;
        playerHasJoinedGame = true;
    }
    private void LeaveTheGame()
    {
        playerJoinedGameImage.transform.localPosition = playerJoinedGameImageOriginalPosition;
        joinTheGameText.transform.localPosition = joinTheGameTextOriginalPosition;
        playerHasJoinedGame = false;
        //Delete from all the lists if added
    }
    private void LockSelection()
    {
        //Add to gamemanager
    }
    public void SetMyPlayerInfo(Sprite playerImage, string name)
    {
        myPlayerSprite = playerImage;
        myPlayerName = name;
    }
}

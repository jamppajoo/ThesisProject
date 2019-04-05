using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP_PlayerJoinedLobbyController : MonoBehaviour
{
    [HideInInspector]
    public bool playerCanModify = false;

    private string myPlayerID;
    private string myPlayerName;

    private Sprite myPlayerSprite;
    private RectTransform playerJoinedGameImage;

    private Vector3 playerJoinedGameImageOriginalPosition;

    private bool playerHasJoinedGame = false;

    private void Awake()
    {
        myPlayerID = gameObject.transform.name.Substring(0, 2);
        playerJoinedGameImage = gameObject.transform.GetChild(0).Find("PlayerChoosing").GetComponent<RectTransform>();
        gameObject.GetComponentInChildren<PlayerChangeInfo>().SetMyPlayerID(myPlayerID);
    }

    private void Start()
    {
        playerJoinedGameImageOriginalPosition = playerJoinedGameImage.transform.localPosition;
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

    public void JoinTheGame()
    {
        playerJoinedGameImage.localPosition = Vector3.zero;
        playerHasJoinedGame = true;
    }
    private void LeaveTheGame()
    {
        playerJoinedGameImage.transform.localPosition = playerJoinedGameImageOriginalPosition;
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

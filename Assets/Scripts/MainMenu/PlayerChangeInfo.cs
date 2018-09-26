using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChangeInfo : MonoBehaviour
{

    private string myPlayerID;
    private string myCurrentPlayerName;

    private Sprite myCurrentPlayerSprite;
    private Image myPlayerImageSlot;

    private Text myPlayerNameSlot;

    private bool pictureChanged = false;

    private List<Sprite> playerImages;

    private int myCurrentImageIndex = 0;

    void Start()
    {
        myPlayerImageSlot = gameObject.GetComponent<Image>();
        myPlayerNameSlot = gameObject.transform.parent.GetComponentInChildren<Text>();

        myCurrentPlayerSprite = myPlayerImageSlot.sprite;
        myCurrentPlayerName = myPlayerNameSlot.text;

        playerImages = FindObjectOfType<MainMenuManager>().playerImages;

        myPlayerImageSlot.sprite = playerImages[myCurrentImageIndex];

    }

    void Update()
    {

        float horizontalAxis = Input.GetAxis("Horizontal_" + myPlayerID);
        print("ASD: " + horizontalAxis);
        if (horizontalAxis < 0.5f && horizontalAxis > -0.5f)
            pictureChanged = false;

        if (horizontalAxis > 0.5f)
        {
            if (!pictureChanged)
                ScrollPictures(true);
            pictureChanged = true;
        }
        if (horizontalAxis < -0.5f)
        {
            if (!pictureChanged)
                ScrollPictures(false);
            pictureChanged = true;
        }

    }

    void ScrollPictures(bool toRight)
    {
        if (toRight)
        {
            if (myCurrentImageIndex + 1 < playerImages.Count)
                myCurrentImageIndex++;
            else
            {
                myCurrentImageIndex = 0;
            }
        }

        else
        {
            if (myCurrentImageIndex - 1 >= 0)
                myCurrentImageIndex--;
            else
            {
                myCurrentImageIndex = playerImages.Count -1;
            }
        }



        myPlayerImageSlot.sprite = playerImages[myCurrentImageIndex];
    }

    public void SetMyPlayerID(string playerID)
    {
        myPlayerID = playerID;
    }

    private void SetMyImage()
    {
        gameObject.GetComponentInParent<PlayerJoinTheGameController>().SetMyPlayerInfo(myCurrentPlayerSprite, myCurrentPlayerName);
    }
}

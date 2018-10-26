using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChangeInfo : MonoBehaviour
{
    [HideInInspector]
    public bool playerCanModify = false;

    private string myPlayerID;
    private string myCurrentPlayerName;

    private Sprite myCurrentPlayerSprite;
    private Image myPlayerImageSlot;

    private Text myPlayerNameSlot;


    private List<Sprite> playerImages;
    private List<string> playerNames;

    private int myCurrentImageIndex = 0;
    private int myCurrentNameIndex = 0;

    private Image imageSlotLeftArrow;
    private Image imageSlotRightArrow;

    private Image nameSlotLeftArrow;
    private Image nameSlotRightArrow;

    private bool pictureChanged = false;
    private bool nameChanged = false;
    private bool scrollingPictures = true;
    private bool canChangeFocus = false;

    private void Awake()
    {
        myPlayerImageSlot = gameObject.GetComponent<Image>();
        myPlayerNameSlot = gameObject.transform.parent.GetComponentInChildren<Text>();
        playerImages = FindObjectOfType<MainMenuManager>().playerImages;
        playerNames = FindObjectOfType<MainMenuManager>().playerNames;

        imageSlotRightArrow = gameObject.transform.parent.Find("RightArrowImage").GetComponent<Image>();
        imageSlotLeftArrow = gameObject.transform.parent.Find("LeftArrowImage").GetComponent<Image>();

        nameSlotRightArrow = myPlayerNameSlot.transform.Find("ChangeNameRightArrow").GetComponent<Image>();
        nameSlotLeftArrow = myPlayerNameSlot.transform.Find("ChangeNameLeftArrow").GetComponent<Image>();
    }

    void Start()
    {

        myCurrentPlayerSprite = myPlayerImageSlot.sprite;
        myCurrentPlayerName = myPlayerNameSlot.text;

        myPlayerImageSlot.sprite = playerImages[myCurrentImageIndex];
    }

    void Update()
    {
        if (playerCanModify)
        {
            if (Mathf.Abs(Input.GetAxis("Vertical_" + myPlayerID)) > 0.5f)
            {
                if ( canChangeFocus)
                    scrollingPictures = !scrollingPictures;
                canChangeFocus = false;
            }
            else 
            {
                canChangeFocus = true;
            }

            float horizontalAxis = Input.GetAxis("Horizontal_" + myPlayerID);

            if (scrollingPictures)
            {
                nameSlotLeftArrow.color = Color.white;
                nameSlotRightArrow.color = Color.white;
                imageSlotLeftArrow.color = Color.blue;
                imageSlotRightArrow.color = Color.blue;

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
            else
            {
                nameSlotLeftArrow.color = Color.blue;
                nameSlotRightArrow.color = Color.blue;
                imageSlotLeftArrow.color = Color.white;
                imageSlotRightArrow.color = Color.white;

                if (horizontalAxis < 0.5f && horizontalAxis > -0.5f)
                    nameChanged = false;

                if (horizontalAxis > 0.5f)
                {
                    if (!nameChanged)
                        ScrollNames(true);
                    nameChanged = true;
                }
                if (horizontalAxis < -0.5f)
                {
                    if (!nameChanged)
                        ScrollNames(false);
                    nameChanged = true;
                }


            }
            
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
                myCurrentImageIndex = playerImages.Count - 1;
            }
        }

        myPlayerImageSlot.sprite = playerImages[myCurrentImageIndex];
        SetMyInfo();
    }
    void ScrollNames(bool toRight)
    {
        if (toRight)
        {
            if (myCurrentNameIndex + 1 < playerNames.Count)
                myCurrentNameIndex++;
            else
            {
                myCurrentNameIndex = 0;
            }
        }
        else
        {
            if (myCurrentNameIndex - 1 >= 0)
                myCurrentNameIndex--;
            else
            {
                myCurrentNameIndex = playerNames.Count - 1;
            }
        }

        myPlayerNameSlot.text = playerNames[myCurrentNameIndex];
        SetMyInfo();
    }

    public void SetMyPlayerID(string playerID)
    {
        myPlayerID = playerID;
    }

    private void SetMyInfo()
    {
        myCurrentPlayerSprite = myPlayerImageSlot.sprite;
        myCurrentPlayerName = myPlayerNameSlot.text;
        gameObject.GetComponentInParent<PlayerJoinTheGameController>().SetMyPlayerInfo(myCurrentPlayerSprite, myCurrentPlayerName);
    }
}

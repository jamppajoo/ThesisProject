using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPlayerIDText : MonoBehaviour
{

    private string playerID;
    private Quaternion initRotation;
    private GameObject playerIDText;

    private void Start()
    {
        playerID = gameObject.GetComponent<Ball>().playerID;
        playerIDText = gameObject.transform.GetChild(0).gameObject;

        playerIDText.GetComponent<TextMesh>().text = ReturnPlayerName();

        initRotation = gameObject.transform.rotation;
    }

    private void LateUpdate()
    {
        playerIDText.transform.rotation = initRotation;
    }

    public string ReturnPlayerName()
    {
        string name = "";
        switch (playerID)
        {
            case "P1":
                name = GameManager.sharedGM.P1Name;
                break;
            case "P2":
                name = GameManager.sharedGM.P2Name;
                break;
            case "P3":
                name = GameManager.sharedGM.P3Name;
                break;
            case "P4":
                name = GameManager.sharedGM.P4Name;
                break;
            default:
                break;
        }
        return name;
    }


}

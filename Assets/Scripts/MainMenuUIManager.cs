using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuUIManager : MonoBehaviour
{

    private GameObject mainObjects;
    private GameObject playerAmountObjects;

    private Vector3 mainObjectsOriginalPosition, playerAmountOriginalPosition;
    private Button PlayGame1, PlayGame2, PlayerAmountBackButton, twoPlayers, threePlayers, fourPlayers;

    private int whichGame = 0;
    private int playerAmountNumber = 2;

    private EventSystem eventSystem;

    private InputField p1InputField, p2InputField, p3InputField, p4InputField;

    // Use this for initialization
    void Start()
    {
        findObjects();
        mainObjectsOriginalPosition = mainObjects.transform.position;
        playerAmountOriginalPosition = playerAmountObjects.transform.position;

        addListeners();

    }

    private void findObjects()
    {
        mainObjects = GameObject.Find("MainObjects");
        playerAmountObjects = GameObject.Find("PlayerAmount");

        PlayGame1 = GameObject.Find("PlayGame1").GetComponent<Button>();
        PlayGame2 = GameObject.Find("PlayGame2").GetComponent<Button>();
        PlayerAmountBackButton = GameObject.Find("PlayerAmountBackButton").GetComponent<Button>();
        twoPlayers = GameObject.Find("2Players").GetComponent<Button>();
        threePlayers = GameObject.Find("3Players").GetComponent<Button>();
        fourPlayers = GameObject.Find("4Players").GetComponent<Button>();
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

        p1InputField = GameObject.Find("InputFieldP1").GetComponent<InputField>();
        p2InputField = GameObject.Find("InputFieldP2").GetComponent<InputField>();
        p3InputField = GameObject.Find("InputFieldP3").GetComponent<InputField>();
        p4InputField = GameObject.Find("InputFieldP4").GetComponent<InputField>();

        p1InputField.text = GameManager.sharedGM.P1Name;
        p2InputField.text = GameManager.sharedGM.P2Name;
        p3InputField.text = GameManager.sharedGM.P3Name;
        p4InputField.text = GameManager.sharedGM.P4Name;

    }

    private void addListeners()
    {
        PlayGame1.onClick.AddListener(delegate { playGame(1); });
        PlayGame2.onClick.AddListener(delegate { playGame(2); });

        twoPlayers.onClick.AddListener(delegate { choosePlayerAmount(2); });
        threePlayers.onClick.AddListener(delegate { choosePlayerAmount(3); });
        fourPlayers.onClick.AddListener(delegate { choosePlayerAmount(4); });

        PlayerAmountBackButton.onClick.AddListener(mainObjectsAppear);

        p1InputField.onEndEdit.AddListener(delegate { getInputFieldData(p1InputField, 1); });
        p2InputField.onEndEdit.AddListener(delegate { getInputFieldData(p2InputField, 2); });
        p3InputField.onEndEdit.AddListener(delegate { getInputFieldData(p3InputField, 3); });
        p4InputField.onEndEdit.AddListener(delegate { getInputFieldData(p4InputField, 4); });
    }

    private void getInputFieldData(InputField playerID, int playerNumber)
    {
        switch (playerNumber)
        {
            case 1:
                GameManager.sharedGM.P1Name = playerID.text;
                break;
            case 2:
                GameManager.sharedGM.P2Name = playerID.text;
                break;
            case 3:
                GameManager.sharedGM.P3Name = playerID.text;
                break;
            case 4:
                GameManager.sharedGM.P4Name = playerID.text;
                break;
        }
    }

    private void playGame(int game)
    {
        whichGame = game;
        mainObjectsDisappear();
    }
    private void choosePlayerAmount(int amount)
    {
        playerAmountNumber = amount;
        GameManager.sharedGM.changePlayerCount(playerAmountNumber);
        loadGame(whichGame);
    }

    private void mainObjectsDisappear()
    {
        mainObjects.transform.position = playerAmountOriginalPosition;
        playerAmountAppear();
    }
    private void mainObjectsAppear()
    {
        mainObjects.transform.position = mainObjectsOriginalPosition;
        eventSystem.SetSelectedGameObject(PlayGame1.gameObject);
        playerAmountDisappear();
    }
    private void playerAmountDisappear()
    {
        playerAmountObjects.transform.position = playerAmountOriginalPosition;
    }
    private void playerAmountAppear()
    {
        playerAmountObjects.transform.position = mainObjectsOriginalPosition;
        eventSystem.SetSelectedGameObject(twoPlayers.gameObject);

    }

    private void loadGame(int gameNumber)
    {
        SceneManager.LoadScene(gameNumber);
    }

}


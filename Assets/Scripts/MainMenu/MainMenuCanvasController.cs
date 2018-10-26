using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvasController : MonoBehaviour {

    private GameObject playerSelection;
    private Button playButton;
    private Button settingsButton;

    private Vector3 playerSelectionOriginalPosition;
    private Vector3 playButtonOriginalPosition;
    private Vector3 settingsButtonOriginalPosition;

    private void Awake()
    {
        playerSelection = gameObject.transform.Find("PlayerSelection").gameObject;
        playButton = gameObject.transform.Find("PlayButton").GetComponent<Button>();
        settingsButton = gameObject.transform.Find("SettingsButton").GetComponent<Button>();
       
    }
    private void Start()
    {
        playerSelectionOriginalPosition = playerSelection.GetComponent<RectTransform>().localPosition;
        playButtonOriginalPosition = playButton.GetComponent<RectTransform>().localPosition;
        settingsButtonOriginalPosition = settingsButton.GetComponent<RectTransform>().localPosition;
        playButton.onClick.AddListener(ShowPlayerSelection);
    }

    private void ShowPlayerSelection()
    {
        playButton.GetComponent<RectTransform>().localPosition = playerSelectionOriginalPosition;
        settingsButton.GetComponent<RectTransform>().localPosition = playerSelectionOriginalPosition;

        playerSelection.GetComponent<RectTransform>().localPosition = Vector3.zero ;
        TogglePlayersCanModify(true);
    }

    private void HidePlayerSelection()
    {
        playButton.GetComponent<RectTransform>().localPosition = playButtonOriginalPosition;
        settingsButton.GetComponent<RectTransform>().localPosition = settingsButtonOriginalPosition;

        playerSelection.GetComponent<RectTransform>().localPosition = playerSelectionOriginalPosition;

        TogglePlayersCanModify(false);
    }

    private void TogglePlayersCanModify(bool canModify)
    {
        PlayerJoinTheGameController[] playerJoins;
        PlayerChangeInfo[] playerChangeInfos;

        playerJoins = FindObjectsOfType<PlayerJoinTheGameController>();
        playerChangeInfos = FindObjectsOfType<PlayerChangeInfo>();

        for (int i = 0; i < playerJoins.Length; i++)
        {
            playerJoins[i].playerCanModify = canModify;
            playerChangeInfos[i].playerCanModify = canModify;
        }
    }
}

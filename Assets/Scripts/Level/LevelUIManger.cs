using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Ball;

public class LevelUIManger : MonoBehaviour {

    private GameObject pauseMenu, winnerMenu;
    private bool pauseMenuShowing = false;
    private Button pauseMenuContinueButton, pauseMenuBackToMenuButton, pauseMenuRestartButton;
    private Button winnerMenuContinueButton, winnerMenuRestartButton;
    private GameObject p1UIElements, p2UIElements, p3UIElements, p4UIElements;
    private int playerAmount;
    private Text PlayerNameP1, PlayerNameP2, PlayerNameP3, PlayerNameP4;
    private Vector3 pauseMenuOriginalPosition, winnerMenuOriginalPosition;

    private Text winnerNameText, winningTimeText;
    
    void Start()
    {
        FindStuff();
        if (playerAmount >= 2)
        {
            p1UIElements.SetActive(true);
            p2UIElements.SetActive(true);
            if (playerAmount >= 3)
            {
                p3UIElements.SetActive(true);
                if (playerAmount >= 4)
                {
                    p4UIElements.SetActive(true);
                }
            }
        }


        pauseMenuOriginalPosition = pauseMenu.transform.position;
        winnerMenuOriginalPosition = winnerMenu.transform.position;

        pauseMenuContinueButton.onClick.AddListener(delegate { ContinueButtonPressed(); });
        pauseMenuBackToMenuButton.onClick.AddListener(BackToMenuButtonPressed);
        pauseMenuRestartButton.onClick.AddListener(RestartLevel);

        winnerMenuContinueButton.onClick.AddListener(BackToMenuButtonPressed);
        winnerMenuRestartButton.onClick.AddListener(RestartLevel);
    }
    private void FindStuff()
    {
        playerAmount = GameManager.sharedGM.playerCount;
        pauseMenu = GameObject.Find("PauseMenu");
        winnerMenu = GameObject.Find("WinnerMenu");

        pauseMenuContinueButton = GameObject.Find("PauseMenuContinueButton").GetComponent<Button>();
        pauseMenuBackToMenuButton = GameObject.Find("PauseMenuBackToMenuButton").GetComponent<Button>();
        pauseMenuRestartButton = GameObject.Find("PauseMenuResetButton").GetComponent<Button>();
        winnerMenuContinueButton = GameObject.Find("WinnerMenuContinueButton").GetComponent<Button>();
        winnerMenuRestartButton = GameObject.Find("WinnerMenuRestartButton").GetComponent<Button>();
        winnerNameText = GameObject.Find("WinnerName").GetComponent<Text>();
        winningTimeText = GameObject.Find("WinningTime").GetComponent<Text>();
        if (playerAmount >= 2)
        {
            p1UIElements = GameObject.Find("P1UIElements");
            p2UIElements = GameObject.Find("P2UIElements");
            PlayerNameP1 = GameObject.Find("PlayerNameP1").GetComponent<Text>();
            PlayerNameP2 = GameObject.Find("PlayerNameP2").GetComponent<Text>();
            PlayerNameP1.text = GameManager.sharedGM.P1Name;
            PlayerNameP2.text = GameManager.sharedGM.P2Name;
            if (playerAmount >= 3)
            {
                p3UIElements = GameObject.Find("P3UIElements");
                PlayerNameP3 = GameObject.Find("PlayerNameP3").GetComponent<Text>();
                PlayerNameP3.text = GameManager.sharedGM.P3Name;
                if (playerAmount >= 4)
                {
                    p4UIElements = GameObject.Find("P4UIElements");
                    PlayerNameP4 = GameObject.Find("PlayerNameP4").GetComponent<Text>();
                    PlayerNameP4.text = GameManager.sharedGM.P4Name;
                }
            }
        }

    }
    private void Update()
    {
        if(Input.GetKeyDown("joystick button 7") || Input.GetKeyDown("joystick 2 button 7") || Input.GetKeyDown("joystick 3 button 7") || Input.GetKeyDown("joystick 4 button 7")  || Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenuShowing)
                ShowPauseMenu();
            else
                HidePauseMenu();
        }
    }
    public void ShowWinnerMenu(GameObject winner, float winTime)
    {
        //winnerNameText.text = winnerNameText.text + winnerName;
        winnerNameText.text = winner.GetComponent<BallPlayerIDText>().ReturnPlayerName();
        winningTimeText.text = winningTimeText.text + winTime.ToString("F2");
        winnerMenu.GetComponent<RectTransform>().localPosition = Vector3.zero;
    }

    private void ShowPauseMenu()
    {
        pauseMenu.GetComponent<RectTransform>().localPosition = Vector3.zero;
        pauseMenuShowing = true;
        Time.timeScale = 0;
    }
    private void HidePauseMenu()
    {
        pauseMenu.transform.position = pauseMenuOriginalPosition;
        pauseMenuShowing = false;
        Time.timeScale = 1;
    }
    
    private void ContinueButtonPressed()
    {
        HidePauseMenu();
    }
   private void BackToMenuButtonPressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
}

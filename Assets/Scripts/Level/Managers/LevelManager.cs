using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int playerAmount = 1;

    public Color colorRed, colorGreen, colorYellow, colorBlue;

    private GameObject player1, player2, player3, player4;
    private LevelUIManger levelUIManger;

    private float timeEplased = 0;
    private bool winnerFound = false;

    private void Start()
    {
        playerAmount = GameManager.sharedGM.playerCount;
        FindPlayers(playerAmount);
        //levelUIManger = GameObject.Find("Canvas").GetComponent<LevelUIManger>();
    }
    private void Update()
    {
        if (!winnerFound)
            timeEplased += Time.deltaTime;
    }
    private void FindPlayers(int amount)
    {
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        player3 = GameObject.Find("Player3");
        player4 = GameObject.Find("Player4");

        //player1.SetActive(false);
        //player2.SetActive(false);
        //player3.SetActive(false);
        //player4.SetActive(false);

        //if (amount >= 2)
        //{
        //    player1.SetActive(true);
        //    player2.SetActive(true);
        //    if (amount >= 3)
        //    {
        //        player3.SetActive(true);
        //        if (amount >= 4)
        //        {
        //            player4.SetActive(true);
        //        }
        //    }
        //}

    }

    public void Winner(GameObject winner)
    {
        winnerFound = true;
        //levelUIManger.ShowWinnerMenu(winner.gameObject, timeEplased);
    }
    public void playerKilled(GameObject player)
    {

    }

}

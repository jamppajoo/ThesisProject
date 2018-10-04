using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    public static GameManager sharedGM = null;

    public int playerCount ;

    public bool devBuild = false;
    public string P1Name, P2Name, P3Name, P4Name;

    public void changePlayerCount(int count)
    {
        playerCount = count;
    }
}

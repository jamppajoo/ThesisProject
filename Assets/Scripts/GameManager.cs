using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager sharedGM = null;

    public int playerCount ;

    public bool devBuild = false;
    public string P1Name, P2Name, P3Name, P4Name;

    void Awake()
    {
        if (sharedGM == null)

            //if not, set instance to this
            sharedGM = this;

        //If instance already exists and it's not this:
        else if (sharedGM != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    public void changePlayerCount(int count)
    {
        playerCount = count;
    }
}

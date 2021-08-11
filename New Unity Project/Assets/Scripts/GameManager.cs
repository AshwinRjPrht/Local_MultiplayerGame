using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int maxPlayer;
    public List<PlayerController> activePlayers = new List<PlayerController>();

    public GameObject playerSpawnEffect;

    public bool canFight;

    public string[] allLevels;
    private List<string> levelOrder = new List<string>();

    [HideInInspector]
    public int lastPlayerNumber;

    public int pointsToWin;
    private List<int> roundwins = new List<int>();

    private bool gameWon;

    public string winLevel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddPlayer(PlayerController newPlayer)
    {
        if (activePlayers.Count < maxPlayer)
        {
            activePlayers.Add(newPlayer);

            Instantiate(playerSpawnEffect, newPlayer.transform.position, newPlayer.transform.rotation);
        }
        else
        {
            Destroy(newPlayer.gameObject);
        }
    }
    public void ActivatePlayers()
    {
        foreach (PlayerController player in activePlayers)
        {
            player.gameObject.SetActive(true);
            player.GetComponent<PlayerHealthController>().FillHealth();
        }
    }
    public int checkActivePlayers()
    {
        int playerAliveCount = 0;

        for (int i = 0; i < activePlayers.Count; i++)
        {
            if (activePlayers[i].gameObject.activeInHierarchy)
            {
                playerAliveCount++;
                lastPlayerNumber = i;
            }
        }

        return playerAliveCount;
    }

    public void GotoNextArena()
    {
        //SceneManager.LoadScene(allLevels[Random.Range(0, allLevels.Length)]);

        if (!gameWon) 
        {
        if (levelOrder.Count == 0)
        {
            List<string> allLevelList = new List<string>();
            allLevelList.AddRange(allLevels);

            for (int i = 0; i < allLevels.Length; i++)
            {
                int selected = Random.Range(0, allLevelList.Count);

                levelOrder.Add(allLevelList[selected]);
                allLevelList.RemoveAt(selected);
            }
        }

        string levelToLoad = levelOrder[0];
        levelOrder.RemoveAt(0);

        foreach (PlayerController player in activePlayers)
        {
            player.gameObject.SetActive(true);
            player.GetComponent<PlayerHealthController>().FillHealth();
        }

        SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            foreach (PlayerController player in activePlayers)
            {
                player.gameObject.SetActive(false);
                player.GetComponent<PlayerHealthController>().FillHealth();
            }

            SceneManager.LoadScene(winLevel);
        }
}

    public void StartFirstRound()
    {
        roundwins.Clear();
        foreach(PlayerController player in activePlayers)
        {
            roundwins.Add(0);
        }
        gameWon = false;

        GotoNextArena();
    }

    public void AddRoundWin()
    {
        if(checkActivePlayers() == 1)
        {
            roundwins[lastPlayerNumber]++;

            if(roundwins[lastPlayerNumber] >= pointsToWin)
            {
                gameWon = true;
            }
        }
    }
}

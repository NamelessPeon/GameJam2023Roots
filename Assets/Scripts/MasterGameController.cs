using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public enum gameDifficulty { Easy, Medium, Hard }

public class MasterGameController : MonoBehaviour
{
    public int gamesPlayed = 0;
    public List<string> setList = new List<string>();
    private List<string> gameTypes = new List<string>() {"RootingAround", "Spudacus", "SwingingVine", "Angry Potato"};
    public gameDifficulty nextDifficulty;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        Random.InitState((int)DateTime.Now.Ticks);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("GameButton");

        int i = 0;
        foreach (GameObject button in buttons)
        {
            i++;
            if (i <= gamesPlayed)
                button.SetActive(true);
        }

        // Populate setList with each game, three times for three difficulties
        for (i = 0; i < 3; i++)
        {
            foreach (string game in gameTypes)
            {
                setList.Add(game);
            }
        }

        // Shuffle setList
        var temp = new List<string>(setList);
        setList.Clear();
        for (i = temp.Count(); i > 0; i--)
        {
            int k = Random.Range(0, temp.Count() - 1);
            setList.Add(temp[k]);
            temp.RemoveAt(k);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextGame()
    {
        string nextGame = setList[gamesPlayed];
        int timesPlayed = 0;
        if (gamesPlayed > 1)
        {
            for (int i = gamesPlayed - 1; i >= 0; i--)
            {
                if (setList[i] == nextGame)
                    timesPlayed++;
            }
        }
        switch (timesPlayed)
        {
            case 1: nextDifficulty = gameDifficulty.Medium; break;
            case 2: nextDifficulty = gameDifficulty.Hard; break;
            default: nextDifficulty = gameDifficulty.Easy;  break;
        }
        SceneManager.LoadScene(nextGame);
    }
    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
        Destroy(this.gameObject);
    }
}

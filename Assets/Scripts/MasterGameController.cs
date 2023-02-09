using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum gameDifficulty { Easy, Medium, Hard }

public class MasterGameController : MonoBehaviour
{
    public int gamesPlayed = 0;
    public List<string> setList = new List<string>();
    private List<string> gameTypes = new List<string>() {"RootingAround", "Spudacus", "SwingingVine", "Angry Potato"};
    public gameDifficulty nextDifficulty;
    private GameObject[] levelScreens;
    private GameObject winScreen;
    public List<string> levelProg = new List<string>();

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        Random.InitState((int)DateTime.Now.Ticks);

        // Populate setList with each game, three times for three difficulties
        for (int i = 0; i < 3; i++)
        {
            foreach (string game in gameTypes)
            {
                setList.Add(game);
            }
        }

        // Shuffle setList
        var temp = new List<string>(setList);
        setList.Clear();
        for (int i = temp.Count(); i > 0; i--)
        {
            int k = Random.Range(0, temp.Count() - 1);
            setList.Add(temp[k]);
            temp.RemoveAt(k);
        }

        // Set button text
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("GameButton");
        int temp2 = 0;
        foreach (GameObject button in buttons)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = setList[temp2];
            temp2++;
        }

        UpdateScene();
    }

    public void UpdateScene()
    {
        levelScreens = GameObject.FindGameObjectsWithTag("Level");
        GameObject curLevel = null;
        winScreen = GameObject.Find("Victory");
        string levelName = null;
        switch (gamesPlayed)
        {
            case 0: levelName = "Level1";  break;
            case int i when i >= 1 && i < 3: 
                levelName = "Level2";  break;
            case int i when i >= 3 && i < 6:
                levelName = "Level3"; break;
            case int i when i >= 6 && i < 9: 
                levelName = "Level4"; break;
            case int i when i >= 9 && i < 12: 
                levelName = "Level5"; break;
            case 12: curLevel = winScreen; break;
            default: Debug.Log("Level Find Error"); break;
        }

        for (int i = 0; i < levelScreens.Count(); i++)
        {
            if (levelScreens[i].name == levelName)
                curLevel = levelScreens[i];
        }
        foreach (GameObject container in levelScreens)
        {
            if (container != curLevel)
                container.SetActive(false);
            else
                container.SetActive(true);
        }
        if (curLevel != winScreen)
            winScreen.SetActive(false);

        // Turn off previously selected buttons
        foreach (Transform child in curLevel.transform)
        {
            if (levelProg.Contains(child.name))
            {
               child.gameObject.SetActive(false);
            }
        }
    }

    public void LoadNextGame(GameObject button)
    {
        GameObject.Find("MenuTheme").GetComponent<AudioSource>().Stop();
        // Gets the text of the button clicked and stores it to deactivate later
        string nextGame = button.GetComponentInChildren<TextMeshProUGUI>().text;
        button.SetActive(false);
        Debug.Log("Loading " + nextGame + "...");
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
}

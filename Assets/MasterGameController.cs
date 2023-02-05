using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterGameController : MonoBehaviour
{
    private List<string> gamesPlayed = new List<string>();
    private List<string> gameTypes = new List<string>() {"RootingAround", "Spudacus", "SwingingVine"};

    private void Awake()
    {
        DontDestroyOnLoad(this);
        Random.seed = Mathf.RoundToInt(Time.time);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("GameButton");

        int i = 0;
        foreach (GameObject button in buttons)
        {
            i++;
            if (i <= gamesPlayed.Count)
                button.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseNextGame()
    {
        string nextGame = "";
        if (gamesPlayed.Count > 2)
        {
            foreach (string game in gameTypes)
            {
                if (game == gamesPlayed[gamesPlayed.Count - 2] || game == gamesPlayed[gamesPlayed.Count - 1])
                    continue;
                else
                    nextGame = game;
            }
        }
        else
            nextGame = gameTypes[Random.Range(0, gameTypes.Count - 1)];
        SceneManager.LoadScene(nextGame);
    }
}

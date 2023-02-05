using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Canvas UI;
    public TextMeshProUGUI CountDown;
    public TextMeshProUGUI Timer;
    public GameObject TutorialContainer;
    public GameObject Player;

    public enum gameDifficulty { Easy, Medium, Hard }
    public enum gameState { Tutorial, Playing, Lost, Won }
    public gameState curState = gameState.Tutorial;
    public float gameTimer;
    public float tutorialTimer = 5;
    public gameDifficulty curDifficulty = gameDifficulty.Easy;
    private float endTimer = 5;

    private AudioSource[] music;

    // Start is called before the first frame update
    void Start()
    {
        if (curDifficulty == gameDifficulty.Easy)
        {
            gameTimer = 20;
        }
        else if (curDifficulty == gameDifficulty.Medium)
        {
            gameTimer = 30;
        }
        else
        {
            gameTimer = 45;
        }
        music = GetComponents<AudioSource>();
        music[1].PlayDelayed(music[0].clip.length);
    }

    void Update()
    {
        if (tutorialTimer > 0 && curState == gameState.Tutorial)
        {
            tutorialTimer -= Time.deltaTime;
            CountDown.text = Mathf.RoundToInt(tutorialTimer).ToString();
        }
        else if (curState == gameState.Tutorial && tutorialTimer < 0)
        {
            CountDown.text = "0";
            CountDown.gameObject.SetActive(false);
            TutorialContainer.SetActive(false);
            curState = gameState.Playing;
            Player.gameObject.GetComponent<PlayerMovement>().startMoving = true;
        }
        else if (gameTimer > 0 && curState == gameState.Playing)
        {
            gameTimer -= Time.deltaTime;
            Timer.text = Mathf.RoundToInt(gameTimer).ToString();
        }
        else if (curState == gameState.Playing && gameTimer <= 0)
        {
            curState = gameState.Won;
            UI.GetComponent<Spudacus_UI>().LoadVictoryScreen();
        }
        else if ((curState == gameState.Lost || curState == gameState.Won) && endTimer > 0)
        {
            endTimer -= Time.deltaTime;
            //Player.SetActive(false);
        }
        else if ((curState == gameState.Lost || curState == gameState.Won) && endTimer <= 0)
            SceneManager.LoadScene("MainMenu");
    }

    public void GameOver()
    {
        curState = gameState.Lost;
        Timer.gameObject.SetActive(false);
        UI.GetComponent<Spudacus_UI>().LoadDeathScreen();
    }
}

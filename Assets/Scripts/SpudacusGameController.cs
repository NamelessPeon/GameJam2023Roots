using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static SpudacusGameController;

public class SpudacusGameController : MonoBehaviour
{
    public Canvas UI;
    public TextMeshProUGUI CountDown;
    public TextMeshProUGUI Timer;
    public GameObject TutorialContainer;
    public GameObject SpawnController;
    public enum gameDifficulty {Easy, Medium, Hard}
    public enum gameState {Tutorial, Playing, Lost, Won}
    public gameState curState = gameState.Tutorial;
    public float gameTimer;
    public float tutorialTimer = 5;
    public gameDifficulty curDifficulty = gameDifficulty.Easy;

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
    }

    // Update is called once per frame
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
            SpawnController.GetComponent<Spawn_Controller>().Can_Spawn = true;
            CountDown.gameObject.SetActive(false);
            TutorialContainer.SetActive(false);
            curState = gameState.Playing;
        }
        else if (gameTimer > 0 && curState == gameState.Playing)
        {
            gameTimer -= Time.deltaTime;
            Timer.text = Mathf.RoundToInt(gameTimer).ToString();
        }
        else if (curState == gameState.Playing && gameTimer <= 0)
        {
            curState = gameState.Won;
            SpawnController.GetComponent<Spawn_Controller>().Can_Spawn = false;
            UI.GetComponent<Spudacus_UI>().LoadVictoryScreen();
        }

        //Debug.Log(music[0].time);
        if (music[0].time > 4.5)
            music[1].Play();
    }

    public void GameOver()
    {
        curState = gameState.Lost;
        Timer.gameObject.SetActive(false);
        SpawnController.GetComponent<Spawn_Controller>().Can_Spawn = false;
        UI.GetComponent<Spudacus_UI>().LoadDeathScreen();
    }
}

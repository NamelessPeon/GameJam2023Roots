using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SpudacusGameController;

public class SpudacusGameController : MonoBehaviour
{
    public Canvas UI;
    public TextMeshProUGUI CountDown;
    public TextMeshProUGUI Timer;
    public GameObject TutorialContainer;
    public GameObject SpawnController;
    public GameObject Playa;
    public enum gameState {Tutorial, Playing, Lost, Won}
    public gameState curState = gameState.Tutorial;
    public float gameTimer;
    public float tutorialTimer = 5;
    public gameDifficulty curDifficulty;
    private float endTimer = 5;

    private AudioSource[] music;
    private MasterGameController MasterController;

    // Start is called before the first frame update
    void Start()
    {
        GameObject MCObj = GameObject.FindGameObjectWithTag("MC");
        if (MCObj)
        {
            MasterController = MCObj.GetComponent<MasterGameController>();
            curDifficulty = MasterController.nextDifficulty;
        }
        else
            curDifficulty = gameDifficulty.Medium;
        Spawn_Controller spawnScript = SpawnController.GetComponent<Spawn_Controller>();
        if (curDifficulty == gameDifficulty.Easy)
        {
            gameTimer = 20;
            spawnScript.Spawn_Delay = 2.5f;
        }
        else if (curDifficulty == gameDifficulty.Medium)
        {
            gameTimer = 30;
            spawnScript.Spawn_Delay = 2.0f;
        }
        else
        {
            gameTimer = 45;
            spawnScript.Spawn_Delay = 1.0f;
        }
        music = GetComponents<AudioSource>();
        music[1].PlayDelayed(music[0].clip.length);
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
        else if ((curState == gameState.Lost || curState == gameState.Won) && endTimer > 0)
        {
            endTimer -= Time.deltaTime;
            Playa.SetActive(false);
        }
        else if ((curState == gameState.Lost || curState == gameState.Won) && endTimer <= 0)
        { 
            if (MasterController && curState == gameState.Won)
            {
                MasterController.gamesPlayed++;
                SceneManager.LoadScene("TreeScene");
            }
            else
                SceneManager.LoadScene("MainMenu"); 
        }
    }

    public void GameOver()
    {
        curState = gameState.Lost;
        Timer.gameObject.SetActive(false);
        SpawnController.GetComponent<Spawn_Controller>().Can_Spawn = false;
        if (MasterController)
            Destroy(MasterController.gameObject);
        UI.GetComponent<Spudacus_UI>().LoadDeathScreen();
    }
}

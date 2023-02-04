using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Questions? Call Robert Howard/NamelessPeon on discord

public class MainMenuScript : MonoBehaviour
{
    // Main Menu Options
    public void Play()
    {
        // This section would lot a scene that would present the player with two options for what game to play. 
        Debug.Log("Player has pressed Play");
        //SceneManager.LoadScene("SampleScene");
    }

    public void Quit()
    {
        // Quits the game. 
        Application.Quit();
        Debug.Log("Player has quit the game.");
    }

    // Level Select Options
    public void RootingAround()
    {
        // Loads Rooting Around Scene
        Debug.Log("Player has pressed Rooting Around");
        SceneManager.LoadScene("RootingAround");
    }

    public void SwingingVine()
    {
        // Loads Swinging Vine
        Debug.Log("Player has pressed Swinging Vine");
        SceneManager.LoadScene("SwingingVine");
    }

    public void Spudacus()
    {
        // Loads Spudacus
        Debug.Log("Player has pressed Spudacus");
        SceneManager.LoadScene("Spudacus");
    }
}

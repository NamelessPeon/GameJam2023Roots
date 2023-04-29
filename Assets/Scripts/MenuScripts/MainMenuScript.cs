using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

// Questions? Call Robert Howard/NamelessPeon on discord

public class MainMenuScript : MonoBehaviour
{
    public AudioClip menuMusic;
    public AudioMixerGroup theMixer;
    private void Awake()
    {
        GameObject MenuTheme = GameObject.FindGameObjectWithTag("MenuMusic");
        if (MenuTheme == null)
        {
            MenuTheme = new GameObject();
            MenuTheme.tag = "MenuMusic";
            MenuTheme.name = "MenuTheme";
            MenuTheme.AddComponent<AudioSource>();
            AudioSource musicSource = MenuTheme.GetComponent<AudioSource>();
            musicSource.clip = menuMusic;
            musicSource.outputAudioMixerGroup = theMixer;
            musicSource.Play();
            musicSource.loop = true;
        }
    }

    // Main Menu Options
    public void Play()
    {
        // This section would lot a scene that would present the player with two options for what game to play. 
        Debug.Log("Player has pressed Play");
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("MenuMusic"));
        SceneManager.LoadScene("TreeScene");
    }

    public void Quit()
    {
        Destroy(GameObject.FindGameObjectWithTag("MenuMusic"));
        // Quits the game. 
        Application.Quit();
        Debug.Log("Player has quit the game.");
    }

    // Level Select Options
    public void RootingAround()
    {
        Destroy(GameObject.FindGameObjectWithTag("MenuMusic"));
        // Loads Rooting Around Scene
        Debug.Log("Player has pressed Rooting Around");
        SceneManager.LoadScene("RootingAround");
    }

    public void SwingingVine()
    {
        Destroy(GameObject.FindGameObjectWithTag("MenuMusic"));
        // Loads Swinging Vine
        Debug.Log("Player has pressed Swinging Vine");
        SceneManager.LoadScene("SwingingVine");
    }

    public void Spudacus()
    {
        Destroy(GameObject.FindGameObjectWithTag("MenuMusic"));
        // Loads Spudacus
        Debug.Log("Player has pressed Spudacus");
        SceneManager.LoadScene("Spudacus");
    }

    public void AngryPotato()
    {
        Destroy(GameObject.FindGameObjectWithTag("MenuMusic"));
        // Loads Angry Potato
        Debug.Log("Player has pressed Angry Potato");
        SceneManager.LoadScene("Angry Potato");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButton : MonoBehaviour
{
    public void NotifyMC()
    {
        GameObject MasterController = GameObject.FindGameObjectWithTag("MC");
        MasterGameController mcscript = MasterController.GetComponent<MasterGameController>();
        mcscript.levelProg.Add(this.gameObject.name);
        mcscript.LoadNextGame(this.gameObject);
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
        Destroy(GameObject.FindGameObjectWithTag("MC"));
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnMasterController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject MasterController = GameObject.FindGameObjectWithTag("MC");
        if (MasterController == null)
        {
            MasterController = new GameObject();
            MasterController.tag = "MC";
            MasterController.AddComponent<MasterGameController>();
        }
        else
        {
            GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioSource>().Play();
            MasterGameController mcscript = MasterController.GetComponent<MasterGameController>();
            GameObject[] buttons = GameObject.FindGameObjectsWithTag("GameButton");
            int i = 0;
            foreach (GameObject button in buttons)
            {
                button.GetComponentInChildren<TextMeshProUGUI>().text = mcscript.setList[i];
                i++;
            }
            mcscript.UpdateScene();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spudacus_UI : MonoBehaviour
{
    public TextMeshProUGUI CountDown;
    public Spawn_Controller SpawnController;

    float time = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        CountDown.text = time.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(time > 0)
        {
            time -= Time.deltaTime;
            CountDown.text = System.Math.Round(time, 2).ToString();
            if (time < 0)
            {
                CountDown.text = "0";
                SpawnController.Can_Spawn = true;
                CountDown.gameObject.SetActive(false);
            }
            
        }
    }
}

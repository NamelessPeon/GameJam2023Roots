using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetManager : MonoBehaviour
{
    public GameObject targetPrefab;
    Rigidbody target;
    public int numberOfTargets;
    private MasterGameController MasterController;

    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i<numberOfTargets; i++)
        {
            target = Instantiate(targetPrefab).GetComponent<Rigidbody>();
            target.position = new Vector3(Random.Range(10, 20), Random.Range(1, 6), 0);
        }

    }

    void Update()
    {
        //print(numberOfTargets);
        if (numberOfTargets == 0)
        {
            if (MasterController)
            {
                MasterController.gamesPlayed++;
                SceneManager.LoadScene("TreeScene");
            }
            else
                SceneManager.LoadScene("MainMenu");
        }
    }
}

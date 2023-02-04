using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Controller : MonoBehaviour
{
    public List<Spawner> Spawners;
    public List<GameObject> Objects;
    public GameObject Player;

    public float Spawn_Delay = 2;

    public bool Can_Spawn = false;
    float time;
    public int Num_Spawners;
    // Start is called before the first frame update
    void Start()
    {
        Num_Spawners = Spawners.Count;
        time = Spawn_Delay;
        for(int i = 0; i < Num_Spawners; i++ )
            Spawners[i].Player = Player;
    }

    // Update is called once per frame
    void Update()
    {
        if (Can_Spawn)
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                time = Spawn_Delay;
                Spawners[Random.Range(0, Num_Spawners)].Spawn_Object_At_Player(Objects[Random.Range(0, Objects.Count)]);
            }
        }

    }
}

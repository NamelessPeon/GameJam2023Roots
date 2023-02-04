using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Player;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Spawn_Object_At_Player(GameObject Object)
    {
        GameObject new_instance = GameObject.Instantiate(Object);
        new_instance.transform.position = transform.position;
        new_instance.transform.LookAt(Player.transform.position);
    }

}

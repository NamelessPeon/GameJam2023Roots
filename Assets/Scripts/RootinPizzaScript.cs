using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootinPizzaScript : MonoBehaviour
{
    public int tilesOnPizza = 0;
    public GameObject player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != player)
        {
            tilesOnPizza++;
        }
        

    } 
}

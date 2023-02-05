using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RootinPizzaScript : MonoBehaviour
{
    public int tilesOnPizza = 0;
    public List<GameObject> tilesList;
    public float amountRemoved = 0f;
    public int maxTiles = 0;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "RootinRacoon")
        {
            tilesList.Add(other.gameObject);
            tilesOnPizza++;
            maxTiles = tilesOnPizza;
        }
    }

    private void Update()
    {
        amountRemoved = (float)tilesOnPizza / (float)maxTiles;
        if (amountRemoved <= .2f)
        {
            Debug.Log("WIN");
        }

        foreach (var tile in tilesList)
        {
            if (tile == null)
            {
                tilesList.Remove(tile);
                tilesOnPizza--;
                break;
            }
        }
    }
}

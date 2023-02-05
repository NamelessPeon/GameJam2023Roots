using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile_Generation : MonoBehaviour
{
    public GameObject player;
    public GameObject vineTile;
    public GameObject groundTile;
    private float addNewTilePos;
    private bool lastTileWasGround = false;
    private float tileScale = 6.0f;

    private List<GameObject> tileList = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        addNewTilePos = 4.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        if (playerPos.x >= addNewTilePos)
        {
            bool isGroundTile = false;
            bool isHazardTile = false;
            if (UnityEngine.Random.Range(0, 2) == 0) isGroundTile = true;
            if (isGroundTile)
            {
                if (UnityEngine.Random.Range(0, 3) == 0) isHazardTile = true;
            }
            SpawnTile(isGroundTile, isHazardTile, new Vector3(addNewTilePos + (4.0f * tileScale), 0.0f, 0.0f));
            addNewTilePos += tileScale;

            //Debug.Log(tileList.Count);

            // Remove Off-Screen Tiles
            if (tileList.Count >= 10)
            {
                GameObject oldTile = tileList[0];
                tileList.RemoveAt(0);
                Destroy(oldTile);
            }
            //Destroy(tileArray[0]);
        }
    }

    void SpawnTile(bool isGroundTile, bool isHazardTile, Vector3 pos)
    {
        if (isGroundTile)
        {
            //if (lastTileWasGround)
            {
                /*GameObject oldTile = tileList[tileList.Count-1];
                Vector3 newSize = oldTile.transform.localScale;
                newSize.x *= 2;
                tileList.RemoveAt(0);
                Destroy(oldTile);
                GameObject newTile = Instantiate(groundTile, pos, Quaternion.Euler(0, 0, 0));
                newTile.transform.localScale = newSize;
                tileList.Add(newTile);*/

            }
            //else
            {
                pos.y = -1.0f;
                GameObject newTile = Instantiate(groundTile, pos, Quaternion.Euler(0, 0, 0));
                tileList.Add(newTile);
                lastTileWasGround = true;
            }


        }
        else
        {
            pos.y = 5.0f;
            GameObject newTile = Instantiate(vineTile, pos, Quaternion.Euler(0, 0, 0));
            tileList.Add(newTile);
            lastTileWasGround = false;
        }
        
    }
}

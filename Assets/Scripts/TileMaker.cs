using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMaker : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject tile;
    public int tileWidth;
    public int tileHeight;
    public int tileDepth;
    public int NumRow;
    public int NumCol;
    void Start()
    {
        for (int i = 0; i < NumCol; i++)
            for (int j = 0; j < NumRow; j++)
            {
                GameObject NewTile = GameObject.Instantiate(tile);
                NewTile.transform.localScale = new Vector3(tileWidth, tileHeight, tileDepth);
                NewTile.transform.position = new Vector3(transform.position.x + (j * tileWidth), transform.position.y + (i * tileHeight), transform.position.z);
            }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddingMovingIndicatorTile : MonoBehaviour
{
    public static GameObject[,] map = new GameObject[Constant.mapX, Constant.mapY];


    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < Constant.mapX; i++)
        {
            for (int j = 0; j < Constant.mapY; j++)
            {
                GameObject tile = (GameObject)Instantiate(Resources.Load("TileIndicator")
                    , new Vector3((float)(i * Constant.tileSize + Constant.tileOffSetX)
                    , (float)(j * Constant.tileSize + Constant.tileOffSetY), 0), Quaternion.identity);
                tile.transform.parent = gameObject.transform;
                TileIndicator tileIndicator = tile.GetComponent<TileIndicator>();
                tileIndicator.tileX = i;
                tileIndicator.tileY = j;
                map[i, j] = tile;
                
            }
        }
        ///Add cat 
        Instantiate(Resources.Load("Cat")
                   , new Vector3((float)(5 * Constant.tileSize + Constant.tileOffSetX)
                   , (float)(4 * Constant.tileSize + Constant.tileOffSetY), 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

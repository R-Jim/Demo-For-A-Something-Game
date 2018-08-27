using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMovementIndicator : MonoBehaviour
{
    //public static bool objectSelected = false;
    //private SpriteRenderer spriteRenderer;

    //private Sprite[] arrows;
    //private Sprite[] paths;

    //public static Vector3 gameObjectPos;
    //public static Vector3 lastIndicatorPos;

    //private GameObjectStats gameObjectStats;

    //// Use this for initialization
    //void Start()
    //{
    //    spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    //    arrows = Resources.LoadAll<Sprite>("Path Sprites/Arrow");
    //    paths = Resources.LoadAll<Sprite>("Path Sprites/Path");
    //    gameObjectStats = new GameObjectStats();
    //    gameObjectStats.name = "Cat";
    //    gameObjectStats.movement = 5;
    //    gameObjectStats.attackRange = 1;
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    //private void OnMouseEnter()
    //{
    //    if (objectSelected)
    //    {
    //        GameObject sourceTile = null;
    //        GameObject desTile = null;
    //        GameObject[,] map = AddingMovingIndicatorTile.map;
    //        for (int i = 0; i < map.GetLength(0); i++)
    //        {
    //            for (int j = 0; j < map.GetLength(1); j++)
    //            {
    //                if (map[i, j].transform.position == transform.position)
    //                {
    //                    desTile = map[i, j];
    //                }
    //                if (map[i, j].transform.position == gameObjectPos)
    //                {
    //                    sourceTile = map[i, j];
    //                }
    //            }
    //        }
    //        Debug.Log("src: " + sourceTile.transform.position.x + ", " + sourceTile.transform.position.y);
    //        Debug.Log("des: " + desTile.transform.position.x + ", " + desTile.transform.position.y);
    //        spriteRenderer.sprite = arrows[0];
    //    }
    //}

    //private void OnMouseExit()
    //{
    //    if (objectSelected)
    //    {
    //        //Debug.Log("Tile ready");
    //        //spriteRenderer.sprite = sprites[0];
    //        bool isHorizotor = transform.position.y - lastIndicatorPos.y == 0;
    //        if (isHorizotor)
    //        {
    //            Debug.Log("H");
    //            spriteRenderer.sprite = paths[0];
    //        }
    //        lastIndicatorPos = transform.position;
    //    }
    //}
}

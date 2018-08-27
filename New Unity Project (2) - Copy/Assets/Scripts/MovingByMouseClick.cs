using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingByMouseClick : MonoBehaviour {

    Animator animator;
    public GameObjectStats gameObjectStats { get; set; }


    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        GameObject[,] map = AddingMovingIndicatorTile.map;
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j].transform.position == transform.position)
                {
                    TileIndicator tileIndicator = map[i, j].GetComponent<TileIndicator>();
                    tileIndicator.gameObjectInMap = this;
                    //Set up stats
                    gameObjectStats = new GameObjectStats();
                    gameObjectStats.name = "Cat";
                    gameObjectStats.movement = 4;
                    gameObjectStats.attackRange = 1;
                    Debug.Log(map.GetLength(0));
                    break;
                }
            }
        }
    }

    public void ChangeState(int state)
    {
        animator.SetInteger("Stage", state);
    }
}

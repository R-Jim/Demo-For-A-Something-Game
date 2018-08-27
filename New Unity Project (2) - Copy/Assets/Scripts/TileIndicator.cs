using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileIndicator : MonoBehaviour
{

    public MovingByMouseClick gameObjectInMap { get; set; }

    public int tileX { get; set; }
    public int tileY { get; set; }
    public bool switchToMoveTile { get; set; }
    public bool isMoveAbleTile { get; set; }

    // Use this for initialization
    void Start()
    {
        switchToMoveTile = false;
        isMoveAbleTile = true;
        //Add prefab Indicator when hover to tile
        GameObject selector = (GameObject)Instantiate(Resources.Load("SelectTileIndicator")
             , gameObject.transform.position, Quaternion.identity);
        selector.transform.parent = gameObject.transform;
        selector.SetActive(false);

        //Add prefab move Indicator when hover to tile with a character object above it to show move range
        GameObject MovementIndicator = (GameObject)Instantiate(Resources.Load("MovementIndicator")
             , gameObject.transform.position, Quaternion.identity);
        MovementIndicator.transform.parent = gameObject.transform;
        MovementIndicator.SetActive(false);

        //Add prefove move path indicator when choose a path for character to move in move range
        GameObject SelectedMovingTile = (GameObject)Instantiate(Resources.Load("SelectedMovingTile")
             , gameObject.transform.position, Quaternion.identity);
        SelectedMovingTile.transform.parent = gameObject.transform;
        SelectedMovingTile.SetActive(false);
    }

    public void changeState(int tileState, bool active)
    {
        switch (tileState)
        {
            //When hover to tile
            case 1:
                gameObject.transform.GetChild(0).gameObject.SetActive(active);
                break;
            //When a tile is move able for the character
            case 2:
                gameObject.transform.GetChild(1).gameObject.SetActive(active);
                break;
            //When a tile is in a path for the character to move to destication
            case 3:
                gameObject.transform.GetChild(2).gameObject.SetActive(active);
                break;
        }
    }

    private void OnMouseEnter()
    {
        changeState(1, true);
        if (gameObjectInMap != null)
        {
            if (OnGoingProperties.selectedObject == null)
            {
                gameObjectInMap.ChangeState(1);
                showMovementTile(gameObjectInMap.gameObjectStats.movement, tileX, tileY, true);
            }
        }
        if (OnGoingProperties.selectedObject != null)
        {
            if (switchToMoveTile)
            {
                createPathToDestination(OnGoingProperties.selectedObject, gameObject, AddingMovingIndicatorTile.map);
            }
        }
    }

    public void clearPreviousPath()
    {
        ArrayList prePaths = OnGoingProperties.selectedMoveTiles;
        if (prePaths == null)
        {
            return;
        }
        foreach (GameObject tile in prePaths)
        {
            tile.GetComponent<TileIndicator>().changeState(3, false);
        }
    }

    //#1 Create path to the destination
    public void createPathToDestination(GameObject src, GameObject des, GameObject[,] map)
    {
        clearPreviousPath();
        movableTiles = null;
        TileIndicator srcTile = src.GetComponent<TileIndicator>();
        TileIndicator desTile = des.GetComponent<TileIndicator>();
        int incrementalX = (desTile.tileX - srcTile.tileX > 0) ? 1 : -1;
        int incrementalY = (desTile.tileY - srcTile.tileY > 0) ? 1 : -1;

        int moveX = Math.Abs(desTile.tileX - srcTile.tileX);
        int moveY = Math.Abs(desTile.tileY - srcTile.tileY);
        int move = moveX + moveY;

        int curX = srcTile.tileX, curY = srcTile.tileY;
        while (move > 0)
        {
            bool blockX = false;
            bool blockY = false;
            while (!blockX && moveX > 0)
            {
                GameObject nextTile = map[curX + incrementalX, curY];
                ///check if tile is movable
                if (nextTile.GetComponent<TileIndicator>().isMoveAbleTile)
                {
                    addMovableTile(nextTile);
                    curX = curX + incrementalX;
                    moveX--;
                    move--;
                }
                else
                {
                    blockX = true;
                }

            }
            while (!blockY && moveY > 0)
            {
                GameObject nextTile = map[curX, curY + incrementalY];
                ///check if tile is movable
                if (nextTile.GetComponent<TileIndicator>().isMoveAbleTile)
                {
                    addMovableTile(nextTile);
                    curY = curY + incrementalY;
                    moveY--;
                    move--;
                }
                else
                {
                    blockY = true;
                }
            }
        }
        if (movableTiles != null)
        {
            OnGoingProperties.selectedMoveTiles = movableTiles;
        }
    }
    private ArrayList movableTiles;
    private void addMovableTile(GameObject tile)
    {
        if (movableTiles == null)
        {
            movableTiles = new ArrayList();
        }
        movableTiles.Add(tile);
        tile.GetComponent<TileIndicator>().changeState(3, true);
    }
    //End #1

    private void OnMouseExit()
    {
        changeState(1, false);
        if (gameObjectInMap != null)
        {
            //Spawn tile for movement
            if (OnGoingProperties.selectedObject == null)
            {
                gameObjectInMap.ChangeState(0);
                showMovementTile(gameObjectInMap.gameObjectStats.movement, tileX, tileY, false);
            }
        }
    }

    //#2 Spawn moveable tile based on character move stat
    public void showMovementTile(int movement, int srcX, int srcY, bool show)
    {
        GameObject[,] map = AddingMovingIndicatorTile.map;
        if (movement > 0)
        {
            foreach (int[] dimension in Constant.getDimensions())
            {
                int desX = srcX + dimension[0];
                int desY = srcY + dimension[1];
                try
                {
                    GameObject moveTile = map[desX, desY];
                    moveTile.GetComponent<TileIndicator>().switchToMoveTile = show;
                    moveTile.GetComponent<TileIndicator>().changeState(2, show);
                    showMovementTile(movement - 1, desX, desY, show);
                }
                catch (IndexOutOfRangeException) { }
            }
        }
    }
    //End #2

    private void OnMouseDown()
    {
        //#3 If there is a selected object need to move to new tile
        if (OnGoingProperties.selectedObject != null)
        {
            GameObject srcTile = OnGoingProperties.selectedObject;
            TileIndicator tileIndicator = srcTile.GetComponent<TileIndicator>();
            if (switchToMoveTile)
            {
                ArrayList moveTiles = OnGoingProperties.selectedMoveTiles;
                if (moveTiles != null)
                {
                    StartCoroutine(yeahWait(srcTile, moveTiles));
                }
            }
            else
            {
                gameObject.GetComponent<TileIndicator>().gameObjectInMap = tileIndicator.gameObjectInMap;
                tileIndicator.gameObjectInMap.ChangeState(0);
            }
            showMovementTile(tileIndicator.gameObjectInMap.gameObjectStats.movement, tileIndicator.tileX, tileIndicator.tileY, false);
            clearPreviousPath();
            OnGoingProperties.selectedObject = null;
            OnGoingProperties.selectedMoveTiles = null;
            return;
        }
        //#End #3
        if (gameObjectInMap != null)
        {
            gameObjectInMap.ChangeState(2);
            OnGoingProperties.selectedObject = gameObject;
            showMovementTile(gameObjectInMap.gameObjectStats.movement, tileX, tileY, true);
        }
    }

    //#4: Animation for running to destination
    bool stillRunning = false;

    IEnumerator moveObjectToNewPosition(GameObject srcTile, GameObject desTile)
    {
        stillRunning = true;
        MovingByMouseClick srcGameObject = srcTile.GetComponent<TileIndicator>().gameObjectInMap;
        float y = desTile.transform.position.y - srcGameObject.gameObject.transform.position.y;
        float x = desTile.transform.position.x - srcGameObject.gameObject.transform.position.x;
        for (int i = 0; i < 10; i++)
        {
            srcGameObject.gameObject.transform.Translate(x / 10, y / 10, 0);
            yield return new WaitForSecondsRealtime(0.0001f);
        }
        stillRunning = false;
    }

    IEnumerator yeahWait(GameObject srcTile, ArrayList moveTiles)
    {
        foreach (GameObject tile in moveTiles)
        {
            StartCoroutine(moveObjectToNewPosition(srcTile, tile));
            yield return new WaitUntil(() => !stillRunning);
        }
        TileIndicator tileIndicator = srcTile.GetComponent<TileIndicator>();
        gameObject.GetComponent<TileIndicator>().gameObjectInMap = tileIndicator.gameObjectInMap;
        tileIndicator.gameObjectInMap.ChangeState(0);
        tileIndicator.gameObjectInMap = null;
    }
    //End #4
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GridSpace : MonoBehaviour
{
    public Button button;
    public Text btnText;
    public int pos { get; set; }

    //public string playerSide;
    public GameController gameController {
        get; set;
    }

    //public void SetGameController(GameController gameController)
    //{
    //    this.gameController = gameController;
    //}

    public void SetSpace()
    {
        btnText.text = gameController.GetPlayerSide();
        button.interactable = false;
        gameController.EndTurn(pos);
    }
}

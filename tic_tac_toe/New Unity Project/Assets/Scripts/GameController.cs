using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text[] btnList;
    private string playerSide;

    public GameObject gameOverPanel;
    public Text gameOverText;

    private const string X_SIDE = "X";
    private const string O_SIDE = "O";

    private int[,] rowsCheck = new int[3, 2];
    private int[,] colsCheck = new int[3, 2];
    private int[,] diagsCheck = new int[2, 2]{{0,0}, {0,0}};
    private int moveCount = 0;

    void SetGameControllerRefOnBtns()
    {
        int count = 0;
        foreach (Text txt in btnList)
        {
            GridSpace gridParent = txt.GetComponentInParent<GridSpace>();
            gridParent.gameController = this;
            gridParent.pos = count++;
        }
    }

    void Awake()
    {
        gameOverPanel.SetActive(false);
        if (btnList.Length == 0)
        {
            return;
        }
        playerSide = X_SIDE;
        SetGameControllerRefOnBtns();
    }

    public string GetPlayerSide()
    {
        return this.playerSide;
    }

    public void EndTurn(int pos)
    {
        bool isEnd = CheckWin(pos);
        bool isDraw = false;

        moveCount++;
        if (moveCount == 9)
        {
            isEnd = true;
            isDraw = true;
        }

        if (isEnd)
        {
            SetBoardInteractable(false);
            gameOverPanel.SetActive(true);
            
            if (isDraw)
            {
                gameOverText.text = "It's a draw!";

                Debug.Log("It's a draw!");
            } else
            {
                gameOverText.text = playerSide + " Wins!";

                Debug.Log(playerSide + "_side won!");
            }
        }

        playerSide = (playerSide == X_SIDE) ? O_SIDE : X_SIDE;

    }

    public void SetBoardInteractable(bool state)
    {
        foreach (Text txt in btnList)
        {
            Button btn = txt.GetComponentInParent<Button>();
            btn.interactable = state;
        }
    }

    public void ResetGame()
    {
        this.playerSide = "X";
        moveCount = 0;
        gameOverPanel.SetActive(false);

        this.rowsCheck = new int[3,2];
        this.colsCheck = new int[3, 2];
        this.diagsCheck = new int[2, 2];

        foreach (Text txt in btnList)
        {
            txt.text = "";
        }

        SetBoardInteractable(true);
    }

    public bool CheckWin(int pos)
    {
        int col = pos % 3;
        int row = pos / 3;
        bool isDiag = (pos % 4) == 0;
        bool isAntiDiag = pos == 4 || (pos % 2 == 0) && (pos % 4 != 0);

        Debug.Log(pos + "|" + isDiag + "|" + isAntiDiag);

        int curSideIndex = 0;

        if (playerSide == O_SIDE)
        {
            curSideIndex = 1;
        }


        // Check row win
        if (rowsCheck[row, curSideIndex] + 1 == 3)
        {
            Debug.Log("Row " + row + " matched");
            return true;
        } else
        {
            rowsCheck[row, curSideIndex] += 1;
        }

        // Check col win
        if (colsCheck[col, curSideIndex] + 1 == 3)
        {
            Debug.Log("Col " + col + " matched");
            return true;
        } else
        {
            colsCheck[col, curSideIndex] += 1;
        }

        // Check diagonal win
        if (isDiag && diagsCheck[0, curSideIndex] + 1 == 3)
        {
            Debug.Log("Diag matched");
            return true;
        }
        else if (isDiag)
        {
            diagsCheck[0, curSideIndex] += 1;
        }

        // Check antidiagonal win
        if (isAntiDiag && diagsCheck[1, curSideIndex] + 1 == 3)
        {
            Debug.Log("AntiDiag matched");
            return true;
        }
        else if (isAntiDiag)
        {
            diagsCheck[1, curSideIndex] += 1;
        }

        return false;
    }

    void GameOver()
    {
        foreach (Text txt in btnList)
        {
            txt.GetComponentInParent<Button>().interactable = false;
        }
    }
}

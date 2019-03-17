
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public enum PlayerTurn
    {
        PLAYER_1,
        PLAYER2,
        COMPUTER
    }

    public Sprite CrossSprite, NoughtSprite;
    public Cell[] Cells;
    public Text PlayerTurnDisplayText;
    public Text GameOverText;
    public GameObject PlayerModePanel;
    public GameObject GameOverPanel;
    public List<int> FreeCellIndexList = new List<int>();
    public ResultSet[] resultSets = new ResultSet[]
    {
        new ResultSet(0,1,2),
        new ResultSet(3,4,5),
        new ResultSet(6,7,8),
        new ResultSet(0,3,6),
        new ResultSet(1,4,7),
        new ResultSet(2,5,8),
        new ResultSet(0,4,8),
        new ResultSet(2,4,6),
    };


    PlayerTurn _currentPlayer = PlayerTurn.PLAYER_1;
    private Sprite _currentCellSprite;
    private Cell.SetCellValue _currentCellValue;
    private PlayerTurn _nextPlayer = PlayerTurn.PLAYER_1;

    private bool _playerMode;


    private void Start()
    {
        PlayerTurnDisplayText.text = _currentPlayer.ToString();
        PlayerModePanel.SetActive(true);

        AddCellListener();
    }

    private void AddCellListener()
    {

        for(int i = 0; i< Cells.Length; i++)
        {
            int index = i;
            Cells[i].Button.onClick.AddListener(() => SetCellValue(index));
            FreeCellIndexList.Add(index);
        }
        
    }

    private void SetCellValue(int indexValue)
    {
        
        if(_currentPlayer == PlayerTurn.PLAYER_1)
        {
            _currentCellSprite = CrossSprite;
            _currentCellValue = Cell.SetCellValue.CROSS;
          
            TurnChange(indexValue);
            
        }else if(_playerMode)
        {
            _currentCellSprite = NoughtSprite;
            _currentCellValue = Cell.SetCellValue.NOUGHT;

            TurnChange(indexValue);
        }else
        {
            
        }



    }

    private void TurnChange(int index)
    {
        Debug.Log("Cell Index Clicked : " + index);
       
        
            _nextPlayer = (_currentPlayer == PlayerTurn.PLAYER_1) ? PlayerTurn.PLAYER2 : PlayerTurn.PLAYER_1;
        


        PlayerTurnDisplayText.text = _nextPlayer.ToString();
        Cells[index].SetCellImage(_currentCellSprite, _currentCellValue);
        FreeCellIndexList.Remove(index);
        //if(!WinConditionThroughIndexCompair())
        if( !CheackWinConditionThroughArray(index))

        {
            if(FreeCellIndexList.Count == 0)
            {
                DrawGame();
                return;
            }
            _currentPlayer = _nextPlayer;

            if (!_playerMode && !(_currentPlayer == PlayerTurn.PLAYER_1))
            {
                Invoke("ComputerTurn", 1f);
            }

        }
       
    }

    private bool CheackWinConditionThroughArray(int cellIndex)
    {
        for(int i =0; i < resultSets.Length; i++ )
        {
            if(resultSets[i].IndexExits(cellIndex))
            {
                var value1 = Cells[resultSets[i].Index1].Value;
                var value2 = Cells[resultSets[i].Index2].Value;
                var value3 = Cells[resultSets[i].Index3].Value;

               if (value1== _currentCellValue && value2 == _currentCellValue && value3 == _currentCellValue  )
                {
                    Debug.Log("Win Condition " + _currentCellValue);
                    GameOver();
                    return true;
                }
            }
           
        }
        


        return false;
    }

    private void ComputerTurn()
    {

        int randomlistindex = Random.Range(0,FreeCellIndexList.Count);
        int cellindex =FreeCellIndexList[randomlistindex];
        _currentCellSprite = NoughtSprite;
        _currentCellValue = Cell.SetCellValue.NOUGHT;

        TurnChange(cellindex);

        
    }

    private void DrawGame()
    {
        GameOver();
        GameOverPanel.SetActive(true);
        GameOverText.text = "It's Tie !!";
        
    }



    private bool WinConditionThroughIndexCompair()
    {
       if(Cells[0].Value == _currentCellValue && Cells[1].Value == _currentCellValue && Cells[2].Value == _currentCellValue)
        {
            Debug.Log("Win Condition " +_currentCellValue);
            GameOver();
            return true;
            
        }else if (Cells[3].Value == _currentCellValue && Cells[4].Value == _currentCellValue && Cells[5].Value == _currentCellValue)
        {
            Debug.Log("Win Condition " + _currentCellValue);
            GameOver();
            return true;
        }
        else if (Cells[6].Value == _currentCellValue && Cells[7].Value == _currentCellValue && Cells[8].Value == _currentCellValue)
        {
            Debug.Log("Win Condition " + _currentCellValue);
            GameOver();
            return true;
        }
        else if (Cells[0].Value == _currentCellValue && Cells[3].Value == _currentCellValue && Cells[6].Value == _currentCellValue)
        {
            Debug.Log("Win Condition " + _currentCellValue);
            GameOver();
            return true;

        }
        else if(Cells[1].Value == _currentCellValue && Cells[4].Value == _currentCellValue && Cells[7].Value == _currentCellValue)
        {
            Debug.Log("Win Condition " + _currentCellValue);
            GameOver();
            return true;

        }else if (Cells[2].Value == _currentCellValue && Cells[5].Value == _currentCellValue && Cells[8].Value == _currentCellValue)
        {
            Debug.Log("Win Condition " + _currentCellValue);
            GameOver();
            return true;

        }
        else if (Cells[0].Value == _currentCellValue && Cells[4].Value == _currentCellValue && Cells[8].Value == _currentCellValue)
        {
            Debug.Log("Win Condition " + _currentCellValue);
            GameOver();
            return true;

        }
        else if (Cells[2].Value == _currentCellValue && Cells[4].Value == _currentCellValue && Cells[6].Value == _currentCellValue)
        {
            Debug.Log("Win Condition " + _currentCellValue);
            GameOver();
            return true;

        }
        


            return false;
    }

    private void GameOver()
    {
        GameOverPanel.SetActive(true);
        GameOverText.text = _currentPlayer.ToString() + " Win !!";
        for(int i =0; i < Cells.Length; i++)

        {
            Cells[i].Button.onClick.RemoveAllListeners();
        }
    }

    public void ResetGame()
    {
        
        Debug.Log("Game Reset");

        GameOverPanel.SetActive(false);
        _currentPlayer = PlayerTurn.PLAYER_1;
        for(int i =0; i < Cells.Length; i++)
        {
            Cells[i].Value = Cell.SetCellValue.NONE;
            Cells[i].Image.enabled = false;
            Cells[i].Button.enabled = true;
            
        }
        FreeCellIndexList.Clear();
        AddCellListener();
    }

    public void MainMenu()
    {
        GameOver();
        ResetGame();
        PlayerModePanel.SetActive(true);
        GameOverPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit !!");
    }

    public void PlayerMode()
    {
        _playerMode = true;
        PlayerModePanel.SetActive(false);

    }

    public void ComMode()
    {
        _playerMode = false;
        PlayerModePanel.SetActive(false);
    }
}

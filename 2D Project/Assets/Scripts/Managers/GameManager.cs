using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }
    #endregion

    void Awake()
    {
        #region Dont Destroy On Load
        var objects = GameObject.FindObjectsOfType(this.GetType());
        if (objects.Length > 1)
        {
            DestroyImmediate(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion
    }
    private void Start()
    {
        RoundStart();
    }
    int rolledNumber;
    public void RollDice()
    {
        rolledNumber = Random.Range(1, 7);
        UIManager.Instance.ChangeDice(rolledNumber);
    }

    public void MovePlayer()
    {
        if(rolledNumber != 0) //...Jeff... , check if player has rolled something before moving
        {
            UIManager.Instance.ToggleButtons(false);
            PlayerManager.Instance.CurrentPlayer.MovePosition(rolledNumber);
            //PlayerManager.Instance.CurrentPlayer.CurrentTile.Action();
            rolledNumber = 0;
            UIManager.Instance.ChangeDice(rolledNumber);
        }

    }

    public void RoundStart()
    {
        PlayerManager.Instance.PlayerInPlay();
    }

    public void MinigameEnd(bool playerWon, int positionChange = 0)
    {
        BackToBoard();
        if (playerWon)
        {
            Debug.Log("Player win!");
            PlayerManager.Instance.CurrentPlayer.AddWon();
        }
        else
        {
            Debug.Log("Player lost!");
            PlayerManager.Instance.CurrentPlayer.AddLost();
        }
        if (positionChange == 0)
        {
            RoundEnd();
            return;
        }
        PlayerManager.Instance.CurrentPlayer.MovePosition(positionChange, false);
    }
       
    public void BackToBoard()
    {
        UIManager.Instance.MainCanvas.gameObject.SetActive(true);
        BoardManager.Instance.TileMap.gameObject.SetActive(true);
        LevelManager.Instance.UnloadLastScene();
    }
    
    public void RoundEnd()
    {
        UIManager.Instance.ToggleButtons(true);
        PlayerManager.Instance.NextPlayer();
    }
}

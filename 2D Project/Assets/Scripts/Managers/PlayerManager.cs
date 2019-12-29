using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    private static PlayerManager _instance = null;
    public static PlayerManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PlayerManager>();
            }
            return _instance;
        }
    }
    #endregion

    [SerializeField] private List<Player> _players;
    [SerializeField] private int _currentPlayerNumber = 0;
    private Player _currentPlayer;

    public List<Player> Players { get => _players; set => _players = value; }
    public int CurrentPlayerNumber { get => _currentPlayerNumber; set => _currentPlayerNumber = value; }
    public Player CurrentPlayer { get => _players[_currentPlayerNumber]; }

    void Awake()
    {
        #region Dont Destroy On Load
        var objects = GameObject.FindObjectsOfType(this.GetType());
        if (objects.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion
    }

    public void NextPlayer()
    {
        _currentPlayerNumber++;
        if (_currentPlayerNumber >= _players.Count)
        {
            _currentPlayerNumber = 0;
        }
        UIManager.Instance.UpdateCurrentPlayer();
    }
}

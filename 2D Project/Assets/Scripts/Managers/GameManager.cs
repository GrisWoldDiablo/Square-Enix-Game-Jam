using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance = null;
    [SerializeField] private GameObject _player;
    private int playerPosition;

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
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion
    }

    int rolledNumber;
    public void RollDice()
    {
        rolledNumber = Random.Range(1, 7);
        UIManager.Instance.ChangeDice(rolledNumber);
    }

    public void MovePlayer()
    {
        PlayerManager.Instance.CurrentPlayer.MovePosition(rolledNumber);
        rolledNumber = 0;
        UIManager.Instance.ChangeDice(rolledNumber);
        PlayerManager.Instance.NextPlayer();
    }
}

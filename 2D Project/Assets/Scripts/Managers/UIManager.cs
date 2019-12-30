using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton
    private static UIManager _instance = null;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UIManager>();
            }
            return _instance;
        }
    }
    #endregion

    [SerializeField] private Text _dice;
    [SerializeField] private Text _currentPlayer;
    [SerializeField] private Text _playerOrder;
    [SerializeField] private Image _currentPlayerSprite;
    [SerializeField] private Canvas _mainCanvas;
    [SerializeField] private Button _rollButton;
    [SerializeField] private Button _moveButton;

    public Canvas MainCanvas { get => _mainCanvas; }

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

    private void Start()
    {
        if (PlayerManager.Instance.Players.Count > 0)
        {
            UpdateCurrentPlayer();
        }
    }

    public void ChangeDice(int value)
    {
        _dice.text = value.ToString();
    }

    public void UpdateCurrentPlayer()
    {
        _currentPlayer.text = PlayerManager.Instance.CurrentPlayer.Name;
        _currentPlayerSprite.sprite = PlayerManager.Instance.CurrentPlayer.PlayerSprite;
        _currentPlayerSprite.color = PlayerManager.Instance.CurrentPlayer.PlayerColor;
        _playerOrder.text = string.Empty;
        foreach (var Player in PlayerManager.Instance.Players)
        {
            _playerOrder.text += $"{Player.Position} : {Player.Name}, W:{Player.WonCount},L:{Player.LossCount}\n";
        }
    }

    public void ToggleButtons(bool value)
    {
        _rollButton.interactable = value;
        _moveButton.interactable = value;
    }
}

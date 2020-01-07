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

    [Header("Winner Panel")]
    [SerializeField] private GameObject _panelWinner;
    [SerializeField] private Text _winnerPlayer;
    [SerializeField] private Image _winnerPlayerSprite;



    public Canvas MainCanvas { get => _mainCanvas; }
    public object BouncesLeftUI { get; internal set; }

    void Awake()
    {
        #region Dont Destroy On Load
        var objects = GameObject.FindObjectsOfType(this.GetType());
        if (objects.Length > 1)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            ManagersList.Instance.Managers.Add(this.gameObject);
            DontDestroyOnLoad(this.gameObject);
        }
        #endregion
    }

    private void Start()
    {
        if (PlayerManager.Instance.Players.Count > 0)
        {
            UpdateCurrentPlayer();
        }

        _panelWinner.SetActive(false);
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

    public void ShowWinnerPanel()
    {
        _panelWinner.SetActive(true);
        _winnerPlayer.text = "Congratulation to " + PlayerManager.Instance.CurrentPlayer.Name;
        _winnerPlayerSprite.sprite = PlayerManager.Instance.CurrentPlayer.PlayerSprite;
        _winnerPlayerSprite.color = PlayerManager.Instance.CurrentPlayer.PlayerColor;
    }
}

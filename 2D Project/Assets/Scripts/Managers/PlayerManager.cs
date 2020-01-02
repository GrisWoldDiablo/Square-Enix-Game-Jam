using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    [Header("Start Scene")]
    [SerializeField] private GameObject _panelStart;
    [SerializeField] private GameObject _panelPlayerSetup;
    [SerializeField] private InputField _playerNameInput;
    [SerializeField] private Slider _quantitySlider;
    private int _quantityOfPlayers = 1;
    [SerializeField] private List<Toggle> _colorToggles;
    [SerializeField] private GameObject _playerPrefab;

    [SerializeField] private List<Player> _players;
    [SerializeField] private int _currentPlayerNumber = 0;
    [SerializeField] private float _scaleSpeed = 2.0f;
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
            DestroyImmediate(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
        #endregion
    }

    private void Start()
    {
        _panelPlayerSetup.SetActive(false);
    }

    public void NextPlayer()
    {
        StartCoroutine(ScalePlayer(CurrentPlayer, true));
        CurrentPlayer.GetComponent<SpriteRenderer>().sortingOrder = 1;
        _currentPlayerNumber++;
        if (_currentPlayerNumber >= _players.Count)
        {
            _currentPlayerNumber = 0;
        }
        PlayerInPlay();
        UIManager.Instance.UpdateCurrentPlayer();
    }

    public void PlayerInPlay()
    {
        CurrentPlayer.GetComponent<SpriteRenderer>().sortingOrder = 2;
        StartCoroutine(ScalePlayer(CurrentPlayer));
    }

    private IEnumerator ScalePlayer(Player player, bool shrink = false)
    {
        float lerpValue = 1.0f;
        if (shrink)
        {
            
            while (lerpValue > 0)
            {
                lerpValue -= Time.deltaTime * _scaleSpeed;
                player.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 2.0f, lerpValue);
                yield return null;
            } 
        }
        else
        {
            while (lerpValue > 0)
            {
                lerpValue -= Time.deltaTime * _scaleSpeed;
                player.transform.localScale = Vector3.Lerp(Vector3.one * 2.0f, Vector3.one, lerpValue);
                yield return null;
            } 
        }
    }

    public void ChooseQuantityOfPlayers()
    {
        _quantityOfPlayers = (int)_quantitySlider.value;
        _panelStart.SetActive(false);
        _panelPlayerSetup.SetActive(true);
    }
    
    public void SetPlayer()
    {
        
        var playerOjb = Instantiate(_playerPrefab, this.transform);
        var player = playerOjb.GetComponent<Player>();
        var toggleOn = _colorToggles.Find(x => x.isOn);

        toggleOn.isOn = false;
        toggleOn.group = null;
        toggleOn.gameObject.SetActive(false);

        player.PlayerColor = toggleOn.GetComponentInChildren<Image>().color;
        player.Name = _playerNameInput.text;
        player.name = $"Player ({_playerNameInput.text})";
        _players.Add(player);
        player.gameObject.SetActive(false);
        player.Init();
        _colorToggles.Remove(toggleOn);
        _playerNameInput.text = string.Empty;
        _quantityOfPlayers--;
        if (_quantityOfPlayers <= 0)
        {
            var async = SceneManager.LoadSceneAsync("Board Scene", LoadSceneMode.Single);
            if (async != null)
            {
                async.allowSceneActivation = true;
                _panelPlayerSetup.SetActive(false);
            }
            return;
        }
        _colorToggles[0].isOn = true;
    }
}

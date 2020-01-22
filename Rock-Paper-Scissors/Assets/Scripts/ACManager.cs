using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum PlayerNumber
{
    Player1,
    Player2,
    None
}

public class ACManager : MonoBehaviour
{
    #region Singleton
    private static ACManager _instance = null;
    public static ACManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ACManager>();
            }
            return _instance;
        }
    }
    #endregion

    private const int MIN_PLAYERS = 2;
    // TODO 
    // change scene names
    private readonly string[] SCENE_TO_LOAD = new string[] {
        "TestConnect",
        "main"
    }; // name of the scene to be loaded.

    [SerializeField] private GameObject _panelWarning;
    [SerializeField] private Text _textWarning;

    private Dictionary<int, PlayerNumber> _players;

    private AsyncOperation _async;
    private void Awake()
    {
        _players = new Dictionary<int, PlayerNumber>(); // Init the players list.
        StartCoroutine(LoadScenes()); // Load the required scenes.

        /// AirConsole's delegates.
        AirConsole.instance.onMessage += OnMessage;
        AirConsole.instance.onConnect += OnConnect;
        AirConsole.instance.onDisconnect += OnDisconnect;
    }

    /// <summary>
    /// Check if the device is part of the player's list.
    /// If it is, then it will parse it message and send it to the approriate place.
    /// </summary>
    /// <param name="device_id">incoming id of the device</param>
    /// <param name="data">incoming data</param>
    private void OnMessage(int device_id, JToken data)
    {
        Debug.Log($"Received message from {device_id}.\nMessage = {data}");
        if (_players.Count >=MIN_PLAYERS && _players.ContainsKey(device_id))
        {
            if (data["menu"] != null)
            {
                Debug.Log($"Menu button value : {data["menu"]}");
                // TODO
                // Add proper function name here.
                // MenuManager.Instance.Action(data["menu"].ToString());
            }
            else if (data["move"] != null)
            {
                Debug.Log($"Move button value : {data["move"]}");
                // TODO 
                // check if this syntax is proper.
                GameLogic.Instance.PlayerMove(_players[device_id], data["move"].ToString());
            }
            Debug.Log(_players[device_id]);
        }
    }

    /// <summary>
    /// Add device to the player list and assign them a player name until there is a minimun
    /// connected then it just discard the extra devices connecting and tell them the game is full.
    /// </summary>
    /// <param name="device_id">incoming id of the device</param>
    private void OnConnect(int device_id)
    {
        Debug.Log($"Device {device_id} has connected.");
        if (_players.Count < MIN_PLAYERS && !_players.ContainsKey(device_id))
        {
            if (!_players.ContainsValue(PlayerNumber.Player1))
            {
                _players.Add(device_id, PlayerNumber.Player1);
            }
            else
            {
                _players.Add(device_id, PlayerNumber.Player2);
            }
            if (_players.Count == MIN_PLAYERS)
            {
                _panelWarning.SetActive(false);
                AllPlayers();
            }
            UpdateTextWarning();
        }
        else
        {
            GameFull(device_id);
        }
    }

    /// <summary>
    /// Remove player who gets disconnected.
    /// </summary>
    /// <param name="device_id">incoming id of the device</param>
    private void OnDisconnect(int device_id)
    {
        Debug.Log($"Device {device_id} has disconnected.");
        if (_players.Remove(device_id))
        {
            _panelWarning.SetActive(true);
            UpdateTextWarning();
            MissingPlayer();
        }
    }

    /// <summary>
    /// Update the warning text based on the amount of players currently connected.
    /// </summary>
    private void UpdateTextWarning()
    {
        if (_players.Count == MIN_PLAYERS - 1)
        {
            _textWarning.text = "Please connect one more player.";
        }
        else
        {
            _textWarning.text = $"Please connect 2 players.";
        }
    }

    /// <summary>
    /// Scene managements
    /// Load all the scenes required for the game, one after another.
    /// </summary>
    private IEnumerator LoadScenes()
    {
        int index = 0;
        while (index < SCENE_TO_LOAD.Length)
        {
            _async = SceneManager.LoadSceneAsync(SCENE_TO_LOAD[index++], LoadSceneMode.Additive);
            if (_async != null)
            {
                _async.allowSceneActivation = true;
                while (!_async.isDone)
                {
                    yield return null;
                }
            }
        }
    }

    // Message sent to AirConsole controllers.
    /// <summary>
    /// Send a message to all AirConsole controllers telling them they are in the menu.
    /// </summary>
    public void InMenu()
    {
        AirConsole.instance.Broadcast("inmenu");
    }

    /// <summary>
    /// Send a message to all AirConsole controllers telling them they are in the game.
    /// </summary>
    public void InGame()
    {
        AirConsole.instance.Broadcast("ingame");
    }

    /// <summary>
    /// Send a message to all AirConsole controllers telling them there is missing players.
    /// </summary>
    public void MissingPlayer()
    {
        AirConsole.instance.Broadcast("missingplayer");
    }

    /// <summary>
    /// Sent a message to all AirConsole controllers telling them all the players are connected.
    /// </summary>
    public void AllPlayers()
    {
        AirConsole.instance.Broadcast("allplayers");
    }

    /// <summary>
    /// Sent a message to the AirConsole controllers it that the game is full.
    /// Meaning that 2 players are actually connected.
    /// </summary>
    public void GameFull(int device_id)
    {
        AirConsole.instance.Message(device_id,"gamefull");
    }

    /// <summary>
    /// Sent a message to all AirConsole controllers that the round and ready to be played.
    /// </summary>
    public void RoundReady()
    {
        AirConsole.instance.Broadcast("roundready");
    }

    /// <summary>
    /// Sent a message to all AirConsole controllers telling a specific one that it won and the other that it lost.
    /// </summary>
    public void MatchResult(PlayerNumber winningPlayer)
    {
       
        foreach (var keyValuePair in _players)
        {
            if (keyValuePair.Value == winningPlayer)
            {
                AirConsole.instance.Message(keyValuePair.Key, "winner");
            }
            else
            {
                AirConsole.instance.Message(keyValuePair.Key, "loser");
            }
        }
    }

} //class

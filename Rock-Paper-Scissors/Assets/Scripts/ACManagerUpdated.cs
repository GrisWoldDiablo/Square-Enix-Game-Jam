using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ACManagerUpdated : MonoBehaviour
{
    #region Singleton
    private static ACManagerUpdated _instance = null;
    public static ACManagerUpdated Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ACManagerUpdated>();
            }
            return _instance;
        }
    }
    #endregion

    [SerializeField] private GameObject _panelWarning;
    [SerializeField] private Text _textWarning;    
    
    private const int MAX_PLAYERS = 2;
    private readonly string[] SCENE_TO_LOAD = new string[] {
        "TestConnect",
        "main"
    };
    private Dictionary<int, PlayerNumber> _currentPlayers;
    private AsyncOperation _async;

    private void Awake()
    {
        _currentPlayers = new Dictionary<int, PlayerNumber>();
        StartCoroutine(LoadScenes());

        AirConsole.instance.onMessage += OnMessage;
        AirConsole.instance.onConnect += OnConnect;
        AirConsole.instance.onDisconnect += OnDisconnect;
    }

    private void NotifyPlayersStatus(string message) {
      AirConsole.instance.Broadcast(new { playerStatus = message });
    }

    public void NotifyChangeScene(string message)
    {
      AirConsole.instance.Broadcast(new { menu = message});
    }

    private void NotifyGameResultToPlayer(int device_id, string message) {
      AirConsole.instance.Message(device_id, new { gameResult = message});
    }

    private void OnMessage(int device_id, JToken data)
    {
      var menuValue = data["menu"];
      var moveValue = data["move"];

      if (menuValue != null) {
        Debug.Log($"Menu button value : {data["menu"]}");
        // TODO: Add proper function call here.
        // MenuManager.Instance.Action(data["menu"].ToString());
      }

      if (moveValue != null) {
        Debug.Log($"Move button value : {data["move"]}");
        GameLogic.Instance.PlayerMove(_currentPlayers[device_id], data["move"].ToString());
      }
    }

    private void OnConnect(int device_id) {
      // if there's no current active players set by screen
      if (AirConsole.instance.GetActivePlayerDeviceIds.Count == 0) {
        // and there are 2 or more connected devices
        if (AirConsole.instance.GetControllerDeviceIds().Count >= MAX_PLAYERS) {
          NotifyPlayersStatus("GAME IS FULL");
          StartPlayersAssignmentRoutine();
        } else {
          NotifyPlayersStatus("NEED MORE PLAYERS");
        }
      }
    }

    private void StartPlayersAssignmentRoutine() {
      // Set all connected controllers to be active players, max is 2
      // This will assign each device id with a player number (0, 1, 2, etc.) 
      // In our case, since we only have maximum 2 players, the player numbers are 0 and 1 respectively
      AirConsole.instance.SetActivePlayers(MAX_PLAYERS);
      foreach (int device_id in AirConsole.instance.GetActivePlayerDeviceIds) {
        // save connected active players here for reference. 
        // To get player number of a device_id, use ConvertDeviceIdToPlayerNumber(device_id)
        _currentPlayers.Add(device_id, (PlayerNumber)AirConsole.instance.ConvertDeviceIdToPlayerNumber(device_id)); 
      }
    }

	void OnDisconnect (int device_id) {
    _currentPlayers.Remove(device_id);
		int active_player = AirConsole.instance.ConvertDeviceIdToPlayerNumber (device_id);
		if (active_player != -1) {
			if (AirConsole.instance.GetControllerDeviceIds().Count >= MAX_PLAYERS) {
				StartPlayersAssignmentRoutine();
			} else {
				AirConsole.instance.SetActivePlayers(0);
        _panelWarning.SetActive(true);
				UpdateTextWarning("PLAYER LEFT - NEED MORE PLAYERS");
			}
		}
	}

  private void UpdateTextWarning(string message) {
      _textWarning.text = message;
  }

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

    public void MatchResult(PlayerNumber winningPlayer)
    {
        foreach (var pair in _currentPlayers) {
          PlayerNumber player = pair.Value;
          int device_id = pair.Key;
          if (player == winningPlayer) {
            NotifyGameResultToPlayer(device_id, "WINNER");
          } else {
            NotifyGameResultToPlayer(device_id, "LOSER");
          }
        }
    }

} //class

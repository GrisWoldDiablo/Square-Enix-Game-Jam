using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptTest : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    //private List<int> _players;
    private Dictionary<int, PlayerControl> _players;
    void Awake()
    {
        //_players = new List<int>();
        _players = new Dictionary<int, PlayerControl>();
        AirConsole.instance.onMessage += OnMessage;
        AirConsole.instance.onConnect += OnConnect;
        AirConsole.instance.onDisconnect += OnDisconnect;
    }

    void OnMessage(int device_id, JToken data)
    {
        Debug.Log($"Received message from {device_id}.\nMessage = {data}");
        if (_players.ContainsKey(device_id))
        {
            _players[device_id].PlayerAction(data["choice"].ToString());
        }
    }

    void OnConnect(int device_id)
    {
        Debug.Log($"Device {device_id} has connected.");
        if (!_players.ContainsKey(device_id))
        {
            _players.Add(device_id, Instantiate(_playerPrefab, null).GetComponent<PlayerControl>());
        }
    }

    void OnDisconnect(int device_id)
    {
        Debug.Log($"Device {device_id} has disconnected.");
    }
}

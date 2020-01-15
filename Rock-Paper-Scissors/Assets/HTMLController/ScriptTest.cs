using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptTest : MonoBehaviour
{
    [SerializeField] private List<GameObject> _cubes;
    private List<int> _players;
    private Dictionary<int,Coroutine> _playersCoroutines;
    void Awake()
    {
        _players = new List<int>();
        _playersCoroutines = new Dictionary<int, Coroutine>();
        AirConsole.instance.onMessage += OnMessage;
        AirConsole.instance.onConnect += OnConnect;
        AirConsole.instance.onDisconnect += OnDisconnect;
    }

    void OnMessage(int device_id, JToken data)
    {
        Debug.Log($"Received message from {device_id}.\nMessage = {data}");
        MoveCube(device_id, (int)data["touch"]);
    }

    void OnConnect(int device_id)
    {
        Debug.Log($"Device {device_id} has connected.");
        _players.Add(device_id);
    }

    void OnDisconnect(int device_id)
    {
        Debug.Log($"Device {device_id} has disconnected.");
    }

    void MoveCube(int id, int direction)
    {
        int index = _players.IndexOf(id);
        if (index > 1)
        {
            index = 0;
        }
        switch (direction)
        {
            case 0:
            case 2:
                StopCoroutine(_playersCoroutines[id]);
                break;
            case 1:
                if (!_playersCoroutines.ContainsKey(id))
                {
                    _playersCoroutines.Add(id, StartCoroutine(TransCube(_cubes[index],1)));
                }
                else
                {
                    _playersCoroutines[id] = StartCoroutine(TransCube(_cubes[index], 1));
                }
                break;
            case 3:
                if (!_playersCoroutines.ContainsKey(id))
                {
                    _playersCoroutines.Add(id, StartCoroutine(TransCube(_cubes[index], -1)));
                }
                else
                {
                    _playersCoroutines[id] = StartCoroutine(TransCube(_cubes[index], -1));

                }
                break;
            default:
                break;
        }
    }

    IEnumerator TransCube(GameObject theCube, int direction)
    {
        while (true)
        {
            theCube.transform.position += Vector3.up * direction * Time.deltaTime;
            yield return null;
        }
    }

} // class


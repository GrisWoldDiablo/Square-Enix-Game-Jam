using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class ScriptTest : MonoBehaviour
{
    void Awake()
    {
        AirConsole.instance.onMessage += OnMessage;
        AirConsole.instance.onConnect += OnConnect;
        AirConsole.instance.onDisconnect += OnDisconnect;
    }

    void OnMessage(int device_id, JToken data)
    {
        Debug.Log($"Received message from {device_id}.\nMessage = {data}");
    }

    void OnConnect(int device_id)
    {
        Debug.Log($"Device {device_id} has connected.");
    }

    void OnDisconnect(int device_id)
    {
        Debug.Log($"Device {device_id} has disconnected.");
    }
}

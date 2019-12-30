using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RockSpawnManager : MonoBehaviour
{
    [SerializeField] float spawnRate = .5f;
    [SerializeField] Transform start;
    [SerializeField] Transform end;
    [SerializeField] GameObject spawnObject;

    bool gameStarted;
    bool hasSpawned;
    float startTimer = 5;

    [SerializeField] float timer = 20;
    [SerializeField] TextMeshProUGUI timerUI;
    [SerializeField] TextMeshProUGUI instructionUI;
    [SerializeField] TextMeshProUGUI startTimerUI;

    public bool GameStarted { get => gameStarted; set => gameStarted = value; }

    private void Start()
    {
        timerUI.text = timer.ToString();
        hasSpawned = false;
        Invoke("StartGame", 5f);
    }

    void StartGame()
    {
        instructionUI.text = "";
        startTimerUI.text = "";

        GameStarted = true;
        if (!hasSpawned)
        {
            InvokeRepeating("Spawn", spawnRate, spawnRate);
            hasSpawned = true;
        }
    }

    void Spawn()
    {
        //spawnObject.transform.position = new Vector2(Random.Range(start.position.x, end.position.x), start.position.y);

        var spawnPosition = new Vector2(Random.Range(start.position.x, end.position.x), start.position.y);
        Instantiate(spawnObject,spawnPosition,Quaternion.identity, this.transform);
        hasSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStarted)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                timerUI.text = timer.ToString("F0");
            }
            else if (timer <= 0)
            {
                //Win logic
                timerUI.text = timer.ToString("Minigame Won");
                GameManager.Instance.MinigameEnd(true);
            }
        }
        else
        {

            if(startTimer > 0)
            {
                startTimer -= Time.deltaTime;
                startTimerUI.text = startTimer.ToString("F0");
            }
        }
    }
}

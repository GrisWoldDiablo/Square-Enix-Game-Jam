using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RockSpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float spawnRate = .5f;
    [SerializeField] Transform start;
    [SerializeField] Transform end;
    [SerializeField] GameObject spawnObject;

    bool hasSpawned;

    [SerializeField] float timer = 20;
    [SerializeField] TextMeshProUGUI timerUI;//uimanager?

    private void Start()
    {
        timerUI.text = timer.ToString();
        hasSpawned = false;
    }

    void Spawn()
    {
        //spawnObject.transform.position = new Vector2(Random.Range(start.position.x, end.position.x), start.position.y);
        //...Alex... Fix spawn Position and parent transform 
        var spawnPosition = new Vector2(Random.Range(start.position.x, end.position.x), start.position.y);
        Instantiate(spawnObject,spawnPosition,Quaternion.identity, this.transform);
        hasSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasSpawned)
        {
            Invoke("Spawn", spawnRate);
            hasSpawned = true;
        }

        if(timer > 0)
        {
            timer -= Time.deltaTime;
            timerUI.text = timer.ToString("F0");
        }
        else if(timer <= 0)
        {
            //Win logic
            timerUI.text = timer.ToString("Minigame Won");
            //Time.timeScale = 0; //...Alex... Remove time scale reduction
            GameManager.Instance.MinigameEnd(true); //...Alex... set winning status
        }


    }
}

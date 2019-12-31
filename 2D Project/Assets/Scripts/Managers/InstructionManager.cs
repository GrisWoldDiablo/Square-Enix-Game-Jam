using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InstructionManager : MonoBehaviour
{
    float startTimer = 5;
    [SerializeField] TextMeshProUGUI instructionUI;
    [SerializeField] TextMeshProUGUI startTimerUI;

    private bool gameStarted;

    public bool GameStarted { get => gameStarted; set => gameStarted = value; }
    public TextMeshProUGUI InstructionUI { get => instructionUI; set => instructionUI = value; }
    public TextMeshProUGUI StartTimerUI { get => startTimerUI; set => startTimerUI = value; }

    // Start is called before the first frame update
    void Start()
    {

        Invoke("StartGame", 5f);
    }

    void StartGame()
    {
        instructionUI.text = "";
        startTimerUI.text = "";
        GameStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer > 0)
        {
            startTimer -= Time.deltaTime;
            startTimerUI.text = startTimer.ToString("F0");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RockChoice()
    {
        Debug.Log(GameLogic.checkResult("Rock", GameLogic.RandomAI()));
    }

    public void PaperChoice()
    {
        Debug.Log(GameLogic.checkResult("Paper", GameLogic.RandomAI()));
    }

    public void ScissorChoice()
    {
        Debug.Log(GameLogic.checkResult("Scissor", GameLogic.RandomAI()));
    }
}

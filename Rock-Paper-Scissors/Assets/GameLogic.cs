using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public string rock = "Rock", paper = "Paper", scissor = "Scissor";
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static string RandomAI()
    {
        int AIChoice = Random.Range(1, 4);
        if(AIChoice == 1)
        {
            return "Rock";
        }
        else if(AIChoice == 2)
        {
            return "Paper";
        }
        else
        {
            return "Scissor";
        }

    }

    public static string checkResult(string a, string b)
    {
        if (a.Equals(b))
        {
            return "Draw";
        }
        else if (a.Equals("Rock") && b.Equals("Paper"))
        {
            return "Lose";
        }
        else if (a.Equals("Rock") && b.Equals("Scissor"))
        {
            return "Win";
        }
        else if (a.Equals("Paper") && b.Equals("Scissor"))
        {
            return "Lose";
        }
        else if(a.Equals("Paper") && b.Equals("Rock"))
        {
            return "Win";
        }
        else if (a.Equals("Scissor") && b.Equals("Rock"))
        {
            return "Lose";
        }
        else
        {
            return "Win";
        }
    }
}

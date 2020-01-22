using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public string rock = "Rock", paper = "Paper", scissor = "Scissor";
    private static Dictionary<string, string> play = new Dictionary<string, string>();

    public static Dictionary<string, string> Play { get => play; set => play = value; }

    #region Singleton
    private static GameLogic _instance = null;
    public static GameLogic Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameLogic>();
            }
            return _instance;
        }
    }
    #endregion




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlayerMove(string player, string move)
    {
        if (!play.ContainsKey(player))
        {
            play.Add(player, move);
        }
        if(play.Count == 2)
        {
            
            checkResultWithPlayer(play.Keys.ElementAt(0), play.Keys.ElementAt(1), play.Values.ElementAt(0), play.Values.ElementAt(1));
            play.Clear();
            
        }

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

    public static string checkResultAI(string playerName,string playerMove, string aiMove)
    {
        if (playerMove.Equals(aiMove))
        {
            return "Draw";
        }
        else if (playerMove.Equals("Rock") && aiMove.Equals("Paper"))
        {
            return "AI Wins";
        }
        else if (playerMove.Equals("Rock") && aiMove.Equals("Scissor"))
        {
            return playerName + " Wins";
        }
        else if (playerMove.Equals("Paper") && aiMove.Equals("Scissor"))
        {
            return "AI Wins";
        }
        else if (playerMove.Equals("Paper") && aiMove.Equals("Rock"))
        {
            return playerName + " Wins";
        }
        else if (playerMove.Equals("Scissor") && aiMove.Equals("Rock"))
        {
            return "AI Wins";
        }
        else
        {
            return playerName + " Wins";
        }
    }






    public static string checkResultWithPlayer(string playerOne, string playerTwo, string moveOne, string moveTwo)
    {
        if (moveOne.Equals(moveTwo))
        {
            return "Draw";
        }
        else if (moveOne.Equals("Rock") && moveTwo.Equals("Paper"))
        {
            return playerTwo + " Wins";
        }
        else if (moveOne.Equals("Rock") && moveTwo.Equals("Scissor"))
        {
            return playerOne + " Wins";
        }
        else if (moveOne.Equals("Paper") && moveTwo.Equals("Scissor"))
        {
            return playerTwo + " Wins";
        }
        else if(moveOne.Equals("Paper") && moveTwo.Equals("Rock"))
        {
            return playerOne + " Wins";
        }
        else if (moveOne.Equals("Scissor") && moveTwo.Equals("Rock"))
        {
            return playerTwo + " Wins";
        }
        else
        {
            return playerOne + " Wins";
        }
    }
}

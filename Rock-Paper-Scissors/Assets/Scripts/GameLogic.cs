using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public string rock = "Rock", paper = "Paper", scissor = "Scissor";
    private static Dictionary<PlayerNumber, string> play = new Dictionary<PlayerNumber, string>();

    public static Dictionary<PlayerNumber, string> Play { get => play; set => play = value; }

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

    public bool CanPlay { get => canPlay; set => canPlay = value; }
    #endregion

    bool canPlay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int NumberOfPlayer()
    {
        return play.Count;
    }


    public void PlayerMove(PlayerNumber player, string move)
    {
        if (!play.ContainsKey(player) && canPlay)
        {
            play.Add(player, move);
            Timer.Instance.TimerActivate();
        }
        if(play.Count == 2 && canPlay)
        {
            checkResultWithPlayer(play);
            play.Clear();
            Timer.Instance.TimerComplete();
        }
    }

    public void TimerExpire()
    {
        //Need to see if timer expire then && count == 1
        if (play.Keys.ElementAt(0) == PlayerNumber.Player1)
        {
            InterfaceManager.Instance.TakeDamage(PlayerNumber.Player2, play.Values.ElementAt(0), "Absolutely Nothing");
            play.Clear();
        }
        else
        {
            InterfaceManager.Instance.TakeDamage(PlayerNumber.Player1, play.Values.ElementAt(0), "Absolutely Nothing");
            play.Clear();
        }
    }
       
    public void checkResultWithPlayer(Dictionary<PlayerNumber, string> moves)
    {            
        
        //Rock 0 , Paper 1, Scissor 2
        if (moves.Values.ElementAt(0).Equals(moves.Values.ElementAt(1)))
        {
           InterfaceManager.Instance.TakeDamage(PlayerNumber.None, moves.Values.ElementAt(0), moves.Values.ElementAt(1));
        }
        //Rock vs Paper
        else if (moves.Values.ElementAt(0).Equals("0") && moves.Values.ElementAt(1).Equals("1"))
        {
            InterfaceManager.Instance.TakeDamage(moves.Keys.ElementAt(0), moves.Values.ElementAt(0), moves.Values.ElementAt(1));
        }
        //Rock vs Scissor
        else if (moves.Values.ElementAt(0).Equals("0") && moves.Values.ElementAt(1).Equals("2"))
        {
            InterfaceManager.Instance.TakeDamage(moves.Keys.ElementAt(1), moves.Values.ElementAt(0), moves.Values.ElementAt(1));
        }
        //Paper vs Scissor
        else if (moves.Values.ElementAt(0).Equals("1") && moves.Values.ElementAt(1).Equals("2"))
        {
            InterfaceManager.Instance.TakeDamage(moves.Keys.ElementAt(0), moves.Values.ElementAt(0), moves.Values.ElementAt(1));
        }
        //Paper vs Rock
        else if (moves.Values.ElementAt(0).Equals("1") && moves.Values.ElementAt(1).Equals("0"))
        {
            InterfaceManager.Instance.TakeDamage(moves.Keys.ElementAt(1), moves.Values.ElementAt(0), moves.Values.ElementAt(1));
        }
        //Scissor vs Paper
        else if (moves.Values.ElementAt(0).Equals("2") && moves.Values.ElementAt(1).Equals("1"))
        {
            InterfaceManager.Instance.TakeDamage(moves.Keys.ElementAt(1), moves.Values.ElementAt(0), moves.Values.ElementAt(1));
        }
        else if (moves.Values.ElementAt(1).Equals("0") && moves.Values.ElementAt(0).Equals("2"))
        {//Scissor vs Rock
            InterfaceManager.Instance.TakeDamage(moves.Keys.ElementAt(0), moves.Values.ElementAt(0), moves.Values.ElementAt(1));
        }
    }

    //public static string RandomAI()
    //{
    //    int AIChoice = Random.Range(1, 4);
    //    if(AIChoice == 1)
    //    {
    //        return "Rock";
    //    }
    //    else if(AIChoice == 2)
    //    {
    //        return "Paper";
    //    }
    //    else
    //    {
    //        return "Scissor";
    //    }

    //}

    //public static string checkResultAI(string playerName,string playerMove, string aiMove)
    //{
    //    if (playerMove.Equals(aiMove))
    //    {
    //        return "Draw";
    //    }
    //    else if (playerMove.Equals("Rock") && aiMove.Equals("Paper"))
    //    {
    //        return "AI Wins";
    //    }
    //    else if (playerMove.Equals("Rock") && aiMove.Equals("Scissor"))
    //    {
    //        return playerName + " Wins";
    //    }
    //    else if (playerMove.Equals("Paper") && aiMove.Equals("Scissor"))
    //    {
    //        return "AI Wins";
    //    }
    //    else if (playerMove.Equals("Paper") && aiMove.Equals("Rock"))
    //    {
    //        return playerName + " Wins";
    //    }
    //    else if (playerMove.Equals("Scissor") && aiMove.Equals("Rock"))
    //    {
    //        return "AI Wins";
    //    }
    //    else
    //    {
    //        return playerName + " Wins";
    //    }
    //}
}

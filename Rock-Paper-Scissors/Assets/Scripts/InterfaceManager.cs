using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    int[] playerHealth = new int[2];

    [SerializeField] int totalHealth;
    [SerializeField] Image[] heartsP1;
    [SerializeField] Image[] heartsP2;

    [SerializeField] TextMeshProUGUI ResultText;
    [SerializeField] Canvas gameInterface;

    #region Singleton
    private static InterfaceManager _instance = null;
    public static InterfaceManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<InterfaceManager>();
            }
            return _instance;
        }
    }
    #endregion

    private void Start()
    {
        InGame(false);
        playerHealth[0] = totalHealth;
        playerHealth[1] = totalHealth;
    }

    public void TakeDamage(PlayerNumber player, string m1, string m2)
    {
        if (m1.Equals("0")) m1 = m1.Replace("0", "Rock");
        else if (m1.Equals("1")) m1 = m1.Replace("1", "Paper");
        else if(m1.Equals("2")) m1 = m1.Replace("2", "Scissors");

        if(m2.Equals("0")) m2 = m2.Replace("0", "Rock");
        else if(m2.Equals("1")) m2 = m2.Replace("1", "Paper");
        else if(m2.Equals("2")) m2 = m2.Replace("2", "Scissors");

        //Player 1 Takes Damage
        if (player == PlayerNumber.Player1 && playerHealth[0] > 1)
        {
            heartsP1[playerHealth[0] - 1].enabled = false; //Remove heart at relevant index
            playerHealth[0]--;
            RoundText("Player 2 wins the round : " + m1 + " vs. " + m2);
        }
        else if (player == PlayerNumber.Player1)
        {
            //P1 LOSE LOGIC
            heartsP1[playerHealth[0] - 1].enabled = false; //Remove heart at relevant index
            playerHealth[0]--;
            RoundText("Player 1 wins the game! " + m1 + " vs. " + m2);
            GameLogic.Instance.CanPlay = false;
            Invoke("FinishGame", 3f);
            //ACManager InMenu, Talk to controller to display win on devices, show menu
        }

        //Player 2 Take damage
        if (player == PlayerNumber.Player2 && playerHealth[1] > 1)
        {
            heartsP2[playerHealth[1] - 1].enabled = false; //Remove heart at relevant index
            playerHealth[1]--;
            RoundText("Player 1 wins the round : " + m1 + " vs. " + m2);
        }
        else if (player == PlayerNumber.Player2)
        {
            //P2 LOSE LOGIC
            heartsP2[playerHealth[1] - 1].enabled = false; //Remove heart at relevant index
            playerHealth[1]--;
            RoundText("Player 1 wins the round : " + m1 + " vs. " + m2);
            GameLogic.Instance.CanPlay = false;
            Invoke("FinishGame", 3f);
        }

        else if (player == PlayerNumber.None)
        {
            RoundText("It's a draw! " + m1 + " vs. " + m2);
        }
    }

    void FinishGame()
    {
      
        foreach(var heart in heartsP1)
        {
            heart.enabled = true;
        }

        foreach(var heart in heartsP2)
        {
            heart.enabled = true;
        }
        playerHealth[0] = totalHealth;
        playerHealth[1] = totalHealth;

        InGame(false);
        MainMenuManager.Instance.InMenu(true);
    }

    public void InGame(bool game = true)
    {
        gameInterface.enabled = game;
    }

    void RoundText(string s)
    {
        ResultText.text = s;
        ResultText.enabled = true;
        Invoke("HideResult", 3f);
    }

    void HideResult()
    {
        ResultText.enabled = false;
    }

}

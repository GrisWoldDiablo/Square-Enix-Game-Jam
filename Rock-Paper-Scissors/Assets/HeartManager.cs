using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{

    int[] playerHealth = new int[2];

    [SerializeField] int totalHealth;
    [SerializeField] Image[] heartsP1;
    [SerializeField] Image[] heartsP2;

    [SerializeField] TextMeshProUGUI ResultText;

    #region Singleton
    private static HeartManager _instance = null;
    public static HeartManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<HeartManager>();
            }
            return _instance;
        }
    }
    #endregion


    private void Start()
    {
        playerHealth[0] = totalHealth;
        playerHealth[1] = totalHealth;
        
    }

    void TakeDamage(PlayerNumber player)
    {
        //Player 1 Takes Damage
        if (player == PlayerNumber.Player1 && playerHealth[0] > 0)
        {
            heartsP1[playerHealth[0] - 1].enabled = false; //Remove heart at relevant index
            playerHealth[0]--;
            ResultText.text = "Player 1 wins round";
        }
        else if (player == PlayerNumber.Player1)
        {
            //P1 LOSE LOGIC
            Debug.Log("Player 2 Wins the Game!");
        }

        //Player 2 Take damage
        if (player == PlayerNumber.Player2 && playerHealth[1] > 0)
        {
            heartsP2[playerHealth[1] - 1].enabled = false; //Remove heart at relevant index
            playerHealth[1]--;
            ResultText.text = "Player 2 wins round";
        }
        else if (player == PlayerNumber.Player2)
        {
            //P2 LOSE LOGIC
            Debug.Log("Player 1 Wins the Game!");
        }

        else if (player == PlayerNumber.None)
        {
            ResultText.text = "It's a draw!";
        }


        ResultText.enabled = true;
        //Use timer class to disable text after x seconds
    }

}

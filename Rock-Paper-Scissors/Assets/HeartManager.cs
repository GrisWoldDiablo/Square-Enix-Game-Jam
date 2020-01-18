using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{

    int[] playerHealth = new int[2];

    [SerializeField] int totalHealth;
    [SerializeField] Image[] heartsP1;
    [SerializeField] Image[] heartsP2;

    private void Start()
    {
        playerHealth[0] = totalHealth;
        playerHealth[1] = totalHealth;

        InvokeRepeating("Test", 3, 3);
    }

    void Test()
    {
        //TakeDamage(1);
        TakeDamage(2);
    }

    void TakeDamage(int player)
    {
        //Player 1 Takes Damage
        if (player == 1 && playerHealth[0] > 0)
        {
            heartsP1[playerHealth[0] - 1].enabled = false; //Remove heart
            playerHealth[0]--;
            Debug.Log("TakeDamage player1");
        }
        else if (player == 1)
        {
            Debug.Log("player1lose");
        }

        //Player 2 Take damage
        if (player == 2 && playerHealth[1] > 0)
        {
            heartsP1[playerHealth[1] - 1].enabled = false; //Remove heart
            playerHealth[1]--;
            Debug.Log("TakeDamage player2");
        }
        else if (player == 2)
        {
            Debug.Log("player2lose");
        }

    }
}

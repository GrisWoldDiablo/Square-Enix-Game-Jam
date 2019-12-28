using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPoint : MonoBehaviour
{
    bool playerEnterBlue , playerEnterYellow, playerEnterGreen = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Blue" && playerEnterBlue)
        {
            playerEnterBlue = false;
            Debug.Log("Bonus + 100");
        }

        if (collision.gameObject.tag == "Yellow" && playerEnterYellow)
        {
            playerEnterYellow = false;
            Debug.Log("Bonus + 250");
        }

        if (collision.gameObject.tag == "Green" && playerEnterGreen)
        {
            playerEnterGreen = false;
            Debug.Log("Bonus + 500");
        }
    }
}

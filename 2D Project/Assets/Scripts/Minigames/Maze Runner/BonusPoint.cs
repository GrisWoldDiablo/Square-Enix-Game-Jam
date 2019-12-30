using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPoint : MonoBehaviour
{
    //Bool to see if player enter the bonus zone.
    bool playerEnterBlue = false , playerEnterYellow = false , playerEnterGreen = false;
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
        //Check if player enter and only once for bonus
        if(collision.gameObject.tag == "Blue" && !playerEnterBlue)
        {
            playerEnterBlue = true;
            Debug.Log("Bonus + 100");
        }

        if (collision.gameObject.tag == "Yellow" && !playerEnterYellow)
        {
            playerEnterYellow = true;
            Debug.Log("Bonus + 250");
        }

        if (collision.gameObject.tag == "Green" && !playerEnterGreen)
        {
            playerEnterGreen = true;
            Debug.Log("Bonus + 500");
        }

        if(collision.gameObject.tag == "Goal")
        {
            Debug.Log("You win !");
            GameManager.Instance.MinigameEnd(true);
        }
    }
}

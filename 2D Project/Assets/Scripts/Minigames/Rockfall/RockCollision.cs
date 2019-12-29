using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Once we have fail logic, implement here
            Destroy(other.gameObject);
            GameManager.Instance.MinigameEnd(false,-2); //...Alex... set loosing status
        }

        Destroy(this.gameObject);


    }
}

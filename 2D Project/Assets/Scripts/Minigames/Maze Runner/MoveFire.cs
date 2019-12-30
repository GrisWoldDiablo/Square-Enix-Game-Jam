using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFire : MonoBehaviour
{
    //the fire
    [SerializeField] GameObject movingObject;
    //position of 2 points
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPOint;
    //Speed for moving fire
    [SerializeField] float moveSpeed;
    //To save the current positio of the platform/object
    private Vector2 currentTarget;
    private float timer = 6.0f;

    // Start is called before the first frame update
    void Start()
    {
        currentTarget = endPOint.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            movingObject.transform.position = Vector2.MoveTowards(movingObject.transform.position, currentTarget, moveSpeed * Time.deltaTime);

            //move the object from point a -> b
            if (movingObject.transform.position.x == endPOint.position.x)
            {
                //currentTarget = startPoint.position;
            }
            ////move the object from point b -> a
            //if (movingObject.transform.position.x == startPoint.position.x)
            //{

            //}
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            moveSpeed = 0;
           // Time.timeScale = 0;
            GameManager.Instance.MinigameEnd(false,-1);
            Debug.Log("Game Over");
        }
    }
}

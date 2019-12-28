using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    //the platform
    [SerializeField] GameObject movingObject;
    //position of 2 points
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPOint;
    //Speed for moving spines
    [SerializeField] float moveSpeed;
    //To save the current positio of the platform/object
    private Vector2 currentTarget;

    // Start is called before the first frame update
    void Start()
    {
        currentTarget = endPOint.position;
    }

    // Update is called once per frame
    void Update()
    {
        movingObject.transform.position = Vector2.MoveTowards(movingObject.transform.position, currentTarget, moveSpeed * Time.deltaTime);

        //move the object from point a -> b
        if (movingObject.transform.position.y == endPOint.position.y)
        {
            currentTarget = startPoint.position;
        }
        //move the object from point b -> a
        if (movingObject.transform.position.y == startPoint.position.y)
        {
            currentTarget = endPOint.position;
        }
    }
}

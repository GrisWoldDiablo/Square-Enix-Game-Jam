using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRotator : MonoBehaviour
{
    [SerializeField] private float time = 5;
    [SerializeField] private float speed = 5;

    void Update()
    {
            
    }

    public void RotateLeftCaller()
    {
        StartCoroutine(RotateLeft());
    }

    IEnumerator RotateLeft()
    {
        while (time > 0)
        {
            transform.Rotate(0, - (Time.deltaTime * speed), 0);
            time -= Time.deltaTime;

            yield return null;
        }

        time = 2;
    }

    public void RotateRight()
    {
        transform.Rotate(new Vector3(0, 90, 0));
    }
}

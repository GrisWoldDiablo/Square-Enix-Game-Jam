using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class MenuRotator : MonoBehaviour
{
    public static MenuRotator instance = null;

    private bool isRotating = false;

    private void Awake()
    {
        if (instance)
        {
            Debug.LogWarning("Only one instance of the MoveObject script in a scene is allowed");
            return;
        }
        instance = this;
    }

    public void RotateLeft()
    {
        if (!isRotating)
        {
            isRotating = true;
            StartCoroutine(Rotate(new Vector3(0, -90, 0), 0.5f));
        }
    }
    public IEnumerator Rotate(Vector3 direction, float time)
    {
        Quaternion start = transform.rotation;
        Quaternion end = start * Quaternion.Euler(direction);

        float rate = 1.0f / time;
        float timer = 0.0f;

        while (timer < 1.0)
        {
            timer += Time.deltaTime * rate;
            transform.rotation = Quaternion.Slerp(start, end, timer);
            yield return null;
        }

        isRotating = false;
    }

    public void RotateRight()
    {
        if (!isRotating)
        {
            isRotating = true;
            StartCoroutine(Rotate(new Vector3(0, 90, 0), 0.5f)); 
        }
    }


    void MenuIndex()
    {

    }
}

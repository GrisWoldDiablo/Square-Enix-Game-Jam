using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpin : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.InverseTransformDirection(Vector3.up), 45 * Time.deltaTime);
    }
}

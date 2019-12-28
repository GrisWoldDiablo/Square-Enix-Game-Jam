using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Rockfall : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 movement;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }
}

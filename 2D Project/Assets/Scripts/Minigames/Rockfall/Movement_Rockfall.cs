using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Rockfall : MonoBehaviour
{
    // Start is called before the first frame update
    //Vector2 movement;
    Rigidbody2D rb;

    [SerializeField] float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //movement.x = Input.GetAxisRaw("Horizontal");
        //rb.MovePosition(rb.position + movement * speed * Time.deltaTime);

        rb.velocity = new Vector2(Input.acceleration.x * speed, 0f);
    }
}

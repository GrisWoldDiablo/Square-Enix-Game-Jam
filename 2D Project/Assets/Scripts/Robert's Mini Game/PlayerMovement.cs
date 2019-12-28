using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public Rigidbody2D body;
    float directionX, directionY;
    Vector2 movement;
    bool gyroCheck;
    bool Accelerometer;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        //Check if they have Gyro
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
            gyroCheck = true;
        }

        if (SystemInfo.supportsAccelerometer)
        {
            Accelerometer = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (gyroCheck)
        //{
        //    transform.rotation = GyroUnity(Input.gyro.attitude);
        //}

        directionX = Input.acceleration.x * speed;
        directionY = Input.acceleration.y * speed;


        //Input arrow key to test
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        body.MovePosition(body.position + movement * speed * Time.deltaTime);


    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(body.velocity.x + directionX, body.velocity.y + directionY);
    }

    private Quaternion GyroUnity(Quaternion point)
    {
        return new Quaternion(point.x, point.y, -point.z, -point.w);
    }
}

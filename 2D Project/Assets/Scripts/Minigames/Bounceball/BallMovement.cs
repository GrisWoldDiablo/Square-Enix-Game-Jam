using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BallMovement : MonoBehaviour
{
    UIManager_BounceBall ui;
    InstructionManager instructions;

    Rigidbody2D rb;
    [SerializeField] int bounceCounter = 20;
    [SerializeField] float speed = 4;
    int lives = 3;



    // Start is called before the first frame update
    void Start()
    {
        ui = FindObjectOfType<UIManager_BounceBall>().GetComponent<UIManager_BounceBall>();
        rb = GetComponent<Rigidbody2D>();
        instructions = FindObjectOfType<InstructionManager>();
        ui.BouncesLeftUI.text = bounceCounter.ToString();
        ui.LivesLeftUI.text = "Lives: " + lives.ToString();

        //Add random start direction



    }

    // Update is called once per frame
    void Update()
    {
        //Lock speed
        
        if (instructions.GameStarted)
        {
            float startDir = Random.Range(-180, 0);
            rb.AddForce(new Vector2(startDir, startDir));
            instructions.GameStarted = false;
            
        }
        rb.velocity = rb.velocity.normalized * speed;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Slice")
        {
            if (bounceCounter > 0)
            {
                speed += 0.08f;
                bounceCounter--;
                ui.BouncesLeftUI.text = bounceCounter.ToString();

            }
            else
            {
                //Win
                GameManager.Instance.MinigameEnd(true);
            }

        }
        else if (lives > 0)
        {
            lives--;
            ui.LivesLeftUI.text = "Lives: " + lives.ToString();
        }
        else
        {
            //Lose here
            GameManager.Instance.MinigameEnd(false);
        }
    }
}

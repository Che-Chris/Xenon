using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float run_speed;
    public float jump_speed;
    public float dash_speed;

    private int jumps;
    private int dashes;
    private Rigidbody2D rb;

    private bool facing_right;

	// Use this for initialization
	void Start () {
        jumps = 0;
        dashes = 0;
        rb = GetComponent<Rigidbody2D>();
        facing_right = true;
    }

    private void Update()
    {
        // Player wants to jump
        bool jump = Input.GetKeyDown(KeyCode.Space);

        // Check if player is allowed to jump
        if (jump && jumps > 0)
        {
            rb.AddForce(new Vector2(0, 100) * jump_speed);
            this.jumps -= 1;
        }

        // Player wants to dash
        bool dash = Input.GetKeyDown(KeyCode.LeftShift);

        // Check if the player is allowed to dash
        if (dash && dashes > 0)
        {
            // check if we dash left or right
            var move = new Vector2(100, 0);

            if (!facing_right)
            {
                move *= -1;
            }

            rb.AddForce(move * dash_speed);
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        // Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Create a vector for horizontal movement
        Vector2 horizontalMovement = new Vector2(moveHorizontal, 0) * run_speed;

        // Check if we're still facing right
        if (moveHorizontal != 0)
        {
            facing_right = moveHorizontal > 0 ? true : false;
        }

        // Add movement
        rb.velocity = new Vector2(moveHorizontal * run_speed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            this.jumps = 1;
            this.dashes = 1;
        }

        rb.velocity = new Vector2(0, rb.velocity.y);
    }
}

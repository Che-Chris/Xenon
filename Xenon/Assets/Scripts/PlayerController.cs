using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float run_speed;
    public float jump_speed;

    private bool grounded;
    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        grounded = false;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Player wants to jump
        bool jump = Input.GetKeyDown(KeyCode.Space);

        // Check if player is allowed to jump
        if (jump && grounded)
        {
            rb.AddForce(new Vector2(0, 100) * jump_speed);
            this.grounded = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        // Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Create a vector for horizontal movement
        Vector2 horizontalMovement = new Vector2(moveHorizontal, 0) * run_speed;

        // Add movement
        transform.Translate(horizontalMovement);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            this.grounded = true;
        }
    }
}

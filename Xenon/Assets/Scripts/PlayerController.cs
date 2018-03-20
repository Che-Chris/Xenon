﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float run_speed;
    public float jump_speed;
    public float dash_speed;
	public float maxHorizontalAirSpeed;

    private int jumps;
    private int dashes;
    private Rigidbody2D rb;
	private bool grounded;
	private bool touchingWallRight;
	private bool touchingWallLeft;
    private bool facing_right;
	private float gravity;
	private float defaultGravity;
	private float groundedGravity;
    private float movementLocked;

	// Use this for initialization
	void Start () {
        jumps = 0;
        dashes = 0;
        movementLocked = 0.0f;
		defaultGravity = 35f;
		groundedGravity = 0.1f;
		grounded = true;
		touchingWallRight = false;
		touchingWallLeft = false;
        rb = GetComponent<Rigidbody2D>();
        facing_right = true;
    }

    private void Update()
    {
		gravity = grounded ? groundedGravity : defaultGravity;
        if (movementLocked > 0.0f)
        {
            movementLocked -= Time.deltaTime;
        }

        else
        {
            rb.gravityScale = 5.0f;
        }
		
        // Player wants to jump
        bool jump = Input.GetKeyDown(KeyCode.Space);

        // Ground Jump
		if (grounded && jump && jumps > 0) {
			rb.AddForce (transform.up * jump_speed, ForceMode2D.Impulse);
			this.jumps -= 1;
		}
        // Wall Jump (Right)
        else if (touchingWallRight && jump) {
            rb.velocity = new Vector2(-10, 20);
            movementLocked = .4f;
            facing_right = false;
        }
        // Wall Jump (Left)
        else if (touchingWallLeft && jump) {
			// rb.AddForce (new Vector2 (10, 10), ForceMode2D.Impulse);
			rb.velocity = new Vector2(10, 20);
            movementLocked = .4f;
			facing_right = true;
		}

        // Player wants to dash
        bool dash = Input.GetKeyDown(KeyCode.LeftShift);

        // Check if the player is allowed to dash
        if (dash && dashes > 0)
        {
            // check if we dash left or right
            var move = new Vector2(1, 0);

            if (!facing_right)
            {
                move *= -1;
            }

            rb.velocity = (move * dash_speed);
            rb.gravityScale = 0.0f;
            movementLocked = .25f;
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        // Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Check if movement is locked
        if (movementLocked <= 0.0f)
        {
            // Check if we're still facing right
            if (moveHorizontal != 0)
            {
                facing_right = moveHorizontal > 0 ? true : false;
            }

            // Update movement
            rb.velocity = new Vector2(moveHorizontal * run_speed, rb.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
		ContactPoint2D contact = collision.contacts [0];
		if (collision.transform.CompareTag ("Ground")) {
			if (Vector3.Dot (contact.normal, Vector3.up) > 0.5) {
				grounded = true;
				this.jumps = 1;
				this.dashes = 1;
			}
		} 
		if (collision.transform.CompareTag ("Wall")) {
			if (Vector3.Dot (contact.normal, Vector3.left) > 0.5) {
				touchingWallRight = true;
			} else if (Vector3.Dot (contact.normal, Vector3.right) > 0.5) {
				touchingWallLeft = true;
			}
		}

        // rb.velocity = new Vector2(0, rb.velocity.y);
    }

	private void OnCollisionExit2D(Collision2D collision) {
		if (collision.transform.CompareTag ("Ground")) {
			grounded = false;
		} else if (collision.transform.CompareTag ("Wall")) {
			touchingWallLeft = false;
			touchingWallRight = false;
		}
	}
}

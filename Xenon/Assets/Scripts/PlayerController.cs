using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float run_speed;
    public float jump_speed;
    public float dash_speed;
	public SeedUIController seedUI = null;

    private int jumps;
    private int dashes;
    private Rigidbody2D rb;
	private bool grounded;
	private bool touchingWallRight;
	private bool touchingWallLeft;
    private bool facing_right;
    private float movementLocked;
	private SpriteRenderer sr;
	private Animator animator;
	private SceneLoader sl;
    private int seeds;

	// Use this for initialization
	void Start () {
        jumps = 0;
        dashes = 0;
        movementLocked = 0.0f;
		grounded = true;
		touchingWallRight = false;
		touchingWallLeft = false;
        rb = GetComponent<Rigidbody2D>();
        facing_right = true;
		sr = GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator> ();
		sl = GetComponent<SceneLoader> ();
        seeds = 0;

    }

    private void Update()
    {
		float moveHorizontal = Input.GetAxis("Horizontal");

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

        if (jump && (grounded || touchingWallLeft || touchingWallRight))
        {
            rb.gravityScale = 5.0f;
        }

        // Ground Jump
		if (grounded && jump && jumps > 0) {
			rb.AddForce (transform.up * jump_speed, ForceMode2D.Impulse);
			this.jumps -= 1;
		}
        // Wall Jump (Right)
        else if (touchingWallRight && jump) {
            rb.velocity = new Vector2(-10, 20);
			sr.flipX = !sr.flipX;
			movementLocked = .4f;
			animator.SetBool ("wall_jumping", true);
        }
        // Wall Jump (Left)
        else if (touchingWallLeft && jump) {
			rb.velocity = new Vector2(10, 20);
			sr.flipX = !sr.flipX;
			movementLocked = .4f;
			animator.SetBool ("wall_jumping", true);
		}

        // Player wants to dash
        bool dash = Input.GetKeyDown(KeyCode.LeftShift);

        // Check if the player is allowed to dash
        if (dash && dashes > 0 && moveHorizontal != 0)
        {
            // check if we dash left or right
			var move = Vector2.right;

            if (!facing_right)
            {
                move *= -1;
            }

            rb.velocity = (move * dash_speed);
            rb.gravityScale = 0.0f;
            movementLocked = .25f;
            this.dashes -= 1;
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        // Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxisRaw("Horizontal");

        facing_right = moveHorizontal > 0 ? true : false;

        // Check if movement is locked
        if (movementLocked <= 0.0f)
        {
            // Check if we're still facing right
            if (moveHorizontal != 0)
            {
				bool flipSprite = (sr.flipX ? facing_right : !facing_right);

				if (flipSprite)
				{
					sr.flipX = !sr.flipX;
				}
            }

            // Update movement
            rb.velocity = new Vector2(moveHorizontal * run_speed, rb.velocity.y);
			animator.SetBool ("wall_jumping", false);
        }
			
		animator.SetBool ("grounded", grounded);
		animator.SetBool ("running", Mathf.Abs (moveHorizontal) > 0);
    }

    private void OnCollisionStay2D(Collision2D collision)
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

		if (collision.transform.CompareTag ("Invisible Wall")) {
			Physics2D.IgnoreCollision (collision.collider, this.GetComponent<BoxCollider2D> ());
		}

		if (collision.transform.CompareTag ("Enemy")) {
			Debug.Log ("collided");
			sl.LoadImmediate ();
		}

        // rb.velocity = new Vector2(0, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Seed")
        {
            Destroy(collision.gameObject);
            this.seeds += 1;
			if (seedUI != null) {
				seedUI.pushSeed (collision.GetComponent<SpriteRenderer> ().sprite);
			}
            if (seeds >= 4)
            {
                sl.scene = "LevelComplete";
                sl.LoadImmediate();
            }
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        // Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Create a vector for horizontal movement
        Vector2 movement = new Vector2(moveHorizontal, 0);

        // Add movement
        transform.Translate(movement * speed);
    }
}

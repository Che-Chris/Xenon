using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public GameObject player;
	public float rangeX;
	public float rangeY;

	private Vector3 offset;
	private Vector3 playerOffset;

	// Use this for initialization
	void Start () {
        offset = transform.position - player.transform.position;

	}

    void Update()
    {
		playerOffset = new Vector3 (player.transform.position.x - transform.position.x,player.transform.position.y - transform.position.y, 0);
		if (playerOffset.x > rangeX || playerOffset.y > rangeY|| playerOffset.x < rangeX || playerOffset.y < rangeY) {

			transform.position += new Vector3(playerOffset.x - rangeX, playerOffset.y - rangeY, 0);

		}
		/*
		if (playerOffset.y > rangeY) {

			transform.position = new Vector3(transform.position.x, playerOffset.y - rangeY, transform.position.z);

		}	
*/
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
       // transform.position = player.transform.position + offset;
    }
}

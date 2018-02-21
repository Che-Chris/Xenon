using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	public Transform target;
    public SceneLoader sl;
    private Vector2 direction;

	public int maxRange;
	public int minRange;

	// Use this for initialization
	void Start () {
		target = null;
	}
		
	
	// Update is called once per frame
	void Update () {
		if (target == null) {
			return;
		}

		float distance = Vector2.Distance (transform.position, target.position);
		bool tooClose = distance < minRange;

		if (transform.position.x < target.position.x) {
			direction = Vector2.right;
		} else {
			direction = Vector2.left;
		}

		transform.Translate (direction * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			target = other.transform;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			target = null;
		}
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            sl.LoadImmediate();
        }
    }
}

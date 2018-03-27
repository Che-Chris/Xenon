using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	private Vector2 direction;
	private int point;
	private bool chasing;

	private Transform target;
	public Transform[] points;

	// Use this for initialization
	void Start () {
		point = 1;
		target = points[point];
		chasing = false;
	}
		
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < target.position.x) {
			direction = Vector2.right;
		} else {
			direction = Vector2.left;
		}

		transform.Translate (direction * Time.deltaTime * 2);

		if (!chasing) {
			if (Mathf.Abs(transform.position.x - target.transform.position.x) < 0.1f) {
				point = (point + 1) % points.Length;
				target = points [point];
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			target = other.transform;
			chasing = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			target = points[point];
			chasing = false;
		}
	}
}

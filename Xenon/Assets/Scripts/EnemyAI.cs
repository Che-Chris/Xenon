using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public Transform target = null;

	public int maxRange;
	public int minRange;
	public float runSpeed;

	private Vector2 targetTran;
	private Vector2 direction;

	// Use this for initialization
	void Start () {
		
	}
		
	
	// Update is called once per frame
	void Update () {
		if (target == null) {
			return;
		}

		transform.LookAt (target);
		float distance = Vector2.Distance (transform.position, target.position);
		bool tooClose = distance < minRange;
		Vector2 direction = tooClose ? Vector2.right : Vector2.left;

		transform.Translate (direction);
	}
}

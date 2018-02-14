using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
			
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			this.transform.parent.GetComponent<EnemyAI>().target = other.transform;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			this.transform.parent.GetComponent<EnemyAI>().target = null;
		}
	}
}

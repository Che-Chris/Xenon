using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour {

	public SceneLoader sl;

	void OnTriggerEnter2D(Collider2D collider) {
		sl.LoadImmediate ();
	}


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedUIController : MonoBehaviour {

	private Image[] seedStack;
	private int[] seedMap;

	// Use this for initialization
	void Start () {
		seedStack = GetComponentsInChildren<Image> ();
		seedMap = new int[seedStack.Length];
		for (int i = 0; i < seedMap.Length; i++) {
			seedMap [i] = 0;
		}

	}

	private int getFirstEmpty() {
		for (int i = 0; i < seedMap.Length; i++) {
			if (seedMap [i] == 0) {
				return i;
			}
		}
		return -1;
	}

	public void pushSeed(Sprite seedsprite) {
		int seedIndex = getFirstEmpty ();
		if (seedIndex < 0) {
			Debug.Log ("seedMap is full");
			return;
		}
		seedStack [seedIndex].sprite = seedsprite;
		seedMap [seedIndex] = 1;
	}
	

}

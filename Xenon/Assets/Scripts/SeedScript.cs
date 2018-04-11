using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SeedScript : MonoBehaviour
{
    public Sprite[] seedz;

    private SpriteRenderer sr;
    private int seed;

	// Use this for initialization
	void Start ()
    {
        sr = GetComponent<SpriteRenderer>();
        seed = Random.Range(0, 27);
        sr.sprite = seedz[seed];
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

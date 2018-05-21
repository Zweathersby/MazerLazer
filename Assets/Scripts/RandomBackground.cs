using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBackground : MonoBehaviour {
	public GameObject background;
	//  array of materials to hold dif backgrounds
	public Material[] diffBackground;
	public MeshRenderer Render;
	// use this for initialization
	void Start() {
		// set render to background's meshrender
		Render = GetComponent<MeshRenderer>();
		// change the current material that background is using to a
		// randomly selected one from the array 
		Render.material = diffBackground[Random.Range(0, diffBackground.Length)];
	}
	// Update is called once per frame
	void Update() {
	}
}
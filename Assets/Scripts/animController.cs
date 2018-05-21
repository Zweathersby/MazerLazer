using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animController : MonoBehaviour {
	public Animator anim;
	private KeyCode left;
	private KeyCode right;
	private KeyCode up;
	private KeyCode down;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		left = this.transform.parent.GetComponent<PlayerMovement> ().left;
		right = this.transform.parent.GetComponent<PlayerMovement> ().right;
		up = this.transform.parent.GetComponent<PlayerMovement> ().up;
		down = this.transform.parent.GetComponent<PlayerMovement> ().down;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(left) || Input.GetKey(right) || Input.GetKey(up) || Input.GetKey(down)) {
			anim.Play ("Fire Animation 1");
		} else {
			anim.Play ("StillState");
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycastForward : MonoBehaviour {
	RaycastHit hit;
	public float theDistance;
	private KeyCode fire;
	public AudioSource audioSource;
	public AudioClip bomb_sound;
	public float vol;


	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("gameVolume"))
			vol = PlayerPrefs.GetFloat ("gameVolume");
		else
			vol = .5F;
		fire = this.transform.parent.GetComponent<PlayerMovement>().fire;
		theDistance = 4;
		audioSource = this.GetComponent<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update () {
		bool bomb = this.transform.parent.GetComponent<PlayerMovement>().bomb;
		Vector3 forward = transform.TransformDirection (Vector3.forward) * theDistance;
//		Debug.DrawRay (transform.position, forward, Color.red);

		if (bomb && Input.GetKeyUp(fire) && Physics.Raycast (transform.position, forward, out hit, theDistance) && hit.transform.tag == "Wall") {
			if (PlayerPrefs.GetFloat ("gameVolume") > .3F)
				vol = PlayerPrefs.GetFloat ("gameVolume") - .2F;
			else
				vol = PlayerPrefs.GetFloat ("gameVolume");
			
			audioSource.PlayOneShot (bomb_sound, vol);
			Destroy(hit.transform.gameObject); // destroy the object hit
			this.transform.parent.GetComponent<PlayerMovement> ().bomb = false;

		}
	}
}

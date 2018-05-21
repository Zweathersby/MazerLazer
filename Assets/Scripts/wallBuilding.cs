using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallBuilding : MonoBehaviour {
	private KeyCode fire;
	GameObject playerWall;
	public GameObject Wall;
	public AudioSource audioSource;
	public AudioClip wall_sound;
	public float vol;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("gameVolume"))
			vol = PlayerPrefs.GetFloat ("gameVolume");
		else
			vol = .2F;
		
		fire = this.transform.parent.GetComponent<PlayerMovement>().fire;
		audioSource = this.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		bool wall = this.transform.parent.GetComponent<PlayerMovement>().wall;

		if (wall && Input.GetKeyUp(fire)) {
			if (PlayerPrefs.GetFloat ("gameVolume") > .35F)
				vol = PlayerPrefs.GetFloat ("gameVolume") - .3F;
			else
				vol = PlayerPrefs.GetFloat ("gameVolume");

			audioSource.PlayOneShot (wall_sound, vol);

			audioSource.PlayOneShot (wall_sound, .5F);
			playerWall = Instantiate (Wall, transform.position, this.transform.parent.GetComponent<PlayerMovement>().go.transform.rotation) as GameObject;
			this.transform.parent.GetComponent<PlayerMovement> ().wall = false;
		}
	}
}

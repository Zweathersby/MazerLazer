using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class inGameMusic : MonoBehaviour {

	public AudioSource myMusic;


	void Start () {
		if (PlayerPrefs.HasKey ("gameVolume"))
			myMusic.volume = PlayerPrefs.GetFloat ("gameVolume");
		else
			myMusic.volume = 1F;
	}
	// Update is called once per frame
	void Update () {
		if(PlayerPrefs.GetFloat("gameVolume") < .8F)
			myMusic.volume = PlayerPrefs.GetFloat("gameVolume") + .2F;
		else
			myMusic.volume = PlayerPrefs.GetFloat("gameVolume");
//		Debug.Log ("In game" + myMusic.volume);
	}
}

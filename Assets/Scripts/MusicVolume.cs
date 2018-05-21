using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MusicVolume : MonoBehaviour {


	public Slider Volume;
	public AudioSource myMusic;

	void Start() {
		if (PlayerPrefs.HasKey ("gameVolume")) {
			Volume.value = PlayerPrefs.GetFloat ("gameVolume");
		} else {
			Volume.value = 1F;
			PlayerPrefs.SetFloat ("gameVolume", Volume.value);
		}
		
		Volume.onValueChanged.AddListener(delegate {ValueChangeCheck(); });
	}

	// Update is called once per frame
	void ValueChangeCheck () {
		PlayerPrefs.SetFloat ("gameVolume", Volume.value);
		myMusic.volume = PlayerPrefs.GetFloat("gameVolume");
		Debug.Log ("Menu: "+ myMusic.volume);
	}
}

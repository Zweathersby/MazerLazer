using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public Text scoreText;
	public Text restartText;
	public Text gameOverText;
	public GameObject restartButton;
	public GameObject homeButton;

	public bool gameOver;
	public bool restart;
	public int scoreBlue;
	public int scoreRed;

	// Use this for initialization
	void Start () {
		loadAttributes ();
		gameOver = false;
		restart = false;
		gameOverText.text = "";
		restartButton.SetActive (false);
		homeButton.SetActive (false);
//		Button Hbtn = homeButton.GetComponent<Button>();
//		Hbtn.onClick.AddListener(ResetOnClick);
	}
	
	// Update is called once per frame
	void Update () {
		if (restart && Input.GetKeyDown (KeyCode.R)) {
			ResetOnClick ();
		}
	}

	public void BlueWin () {
		scoreBlue++;
		gameOverText.text = "Blue wins!";
		scoreText.text = "Blue " + scoreBlue + " - Red " + scoreRed;
		Debug.Log ("Blue " + scoreBlue + " - Red " + scoreRed);
		restartButton.SetActive (true);
		homeButton.SetActive (true);
	}
	public void RedWin () {
		scoreRed++;
		gameOverText.text = "Red wins!";
		scoreText.text = "Blue " + scoreBlue + " - Red " + scoreRed;
		Debug.Log ("Blue " + scoreBlue + " - Red " + scoreRed);
		restartButton.SetActive (true);
		homeButton.SetActive (true);
	}

	//saving playerPrefs
	public void saveAttributes() {
		PlayerPrefs.SetInt("scoreBlue", scoreBlue);
		PlayerPrefs.SetInt("scoreRed", scoreRed);
	}
	// load playerPrefs
	public void loadAttributes () {
		scoreBlue = PlayerPrefs.GetInt("scoreBlue");
		scoreRed = PlayerPrefs.GetInt("scoreRed");
	}
	public void ResetOnClick() {
		Debug.Log ("RESET");
		PlayerPrefs.SetInt("scoreBlue", 0);
		PlayerPrefs.SetInt("scoreRed", 0);
		scoreText.text = "Blue " + 0 + " - Red " + 0;
	}
}

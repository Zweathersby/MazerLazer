using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {
	public Text gameTimerText;
	float gameTimer = 0f;
	public GameController gameController;
	public GameObject[] walls;
	public GameObject[] outerWalls;
	public AudioSource audioSource;
	public AudioClip wallsChanging;
	public AudioClip changeOccurs;
	public float vol;
	public GameObject HourglassPrefab = null;
	public GameObject BombPrefab = null;
	public GameObject WallPrefab = null;
	public GameObject BlackholePrefab = null;
	public bool wait;

	List<int> usedValuesX = new List<int>();
	List<int> usedValuesY = new List<int>();

	void Start () {
		walls = GameObject.FindGameObjectsWithTag("Wall");
		outerWalls = GameObject.FindGameObjectsWithTag("OuterWall");
		wait = true;

//		StartCoroutine (roundStart (wait));
		StartCoroutine (MazeTimer ());

		audioSource = this.GetComponent<AudioSource> ();
		if (PlayerPrefs.HasKey ("gameVolume"))
			vol = PlayerPrefs.GetFloat ("gameVolume");
		else
			vol = 1F;
	}
	// Update is called once per frame
	void Update () {
		// Stop the clock when round is over
		if (!gameController.gameOver) {
			gameTimer += Time.deltaTime;

			int seconds = (int)(gameTimer % 60);
			int	minutes = (int)(gameTimer / 60) % 60;

			string timerString = string.Format ("{0:00}:{1:00}", minutes, seconds);

			gameTimerText.text = timerString;
		}
	}

	IEnumerator MazeTimer() {
		int exit = 0;
		int length = outerWalls.Length - 1;
//		if (wait) {
//			yield return new WaitForSeconds (3.0f);
//		}

		while (!gameController.gameOver) {
			exit = Random.Range(0, length);
			Debug.Log (exit);
			outerWalls [exit].SetActive (false);

			yield return new WaitForSeconds (12.0f);
			if (!gameController.gameOver) {
				audioSource.PlayOneShot (wallsChanging, vol);

				yield return new WaitForSeconds (1.0f);
				audioSource.PlayOneShot (wallsChanging, vol);

				yield return new WaitForSeconds (1.0f);
				audioSource.PlayOneShot (wallsChanging, vol);

				yield return new WaitForSeconds (1.0f);
				audioSource.PlayOneShot (changeOccurs, vol);

				outerWalls [exit].SetActive (true);

				int amtRange = Random.Range (5, 10);
				for (int i = 0; i < amtRange; i++) {
					int idx = Random.Range (0, walls.Length);
					Destroy (walls [idx]);
				}


				GameObject[] powerups = GameObject.FindGameObjectsWithTag ("Powerup");
				foreach (GameObject powerup in powerups) {
					GameObject.Destroy (powerup);
				}

				// Bottom left quandrant
				for (var i = 0; i < 4; i++) {
					int x;
					int y;
					UniqueRandomCell(0,8,0,4, out x, out y);

					GameObject tmp;
					var pick = Random.value;

					if (pick > .55) {
						tmp = Instantiate (BombPrefab, new Vector3 (x * 4, 1, y * 4), Quaternion.Euler (90, 0, 0)) as GameObject;
						tmp.transform.parent = transform;
					} else if (pick > .2) {
						tmp = Instantiate (WallPrefab, new Vector3 (x * 4, 1, y * 4), Quaternion.Euler (90, 0, 0)) as GameObject;
						tmp.transform.parent = transform;
					} else {
						tmp = Instantiate (HourglassPrefab, new Vector3 (x * 4, 1, y * 4), Quaternion.Euler (90, 0, 0)) as GameObject;
						tmp.transform.parent = transform;
					}
				}
				usedValuesX = new List<int>();
				usedValuesY = new List<int>();

				// Bottom right quadrant
				for (var i = 0; i < 4; i++) {
					int x;
					int y;
					UniqueRandomCell(9,17,0,4, out x, out y);

					GameObject tmp;
					var pick = Random.value;

					if (pick > .55) {
						tmp = Instantiate (BombPrefab, new Vector3 (x * 4, 1, y * 4), Quaternion.Euler (90, 0, 0)) as GameObject;
						tmp.transform.parent = transform;
					} else if (pick > .2) {
						tmp = Instantiate (WallPrefab, new Vector3 (x * 4, 1, y * 4), Quaternion.Euler (90, 0, 0)) as GameObject;
						tmp.transform.parent = transform;
					} else {
						tmp = Instantiate (HourglassPrefab, new Vector3 (x * 4, 1, y * 4), Quaternion.Euler (90, 0, 0)) as GameObject;
						tmp.transform.parent = transform;
					}
				}
				usedValuesX = new List<int>();
				usedValuesY = new List<int>();

				// Top left quadrant
				for (var i = 0; i < 5; i++) {
					int x;
					int y;
					UniqueRandomCell(0,8,5,10, out x, out y);

					GameObject tmp;
					var pick = Random.value;

					if (pick > .55) {
						tmp = Instantiate (BombPrefab, new Vector3 (x * 4, 1, y * 4), Quaternion.Euler (90, 0, 0)) as GameObject;
						tmp.transform.parent = transform;
					} else if (pick > .2) {
						tmp = Instantiate (WallPrefab, new Vector3 (x * 4, 1, y * 4), Quaternion.Euler (90, 0, 0)) as GameObject;
						tmp.transform.parent = transform;
					} else {
						tmp = Instantiate (HourglassPrefab, new Vector3 (x * 4, 1, y * 4), Quaternion.Euler (90, 0, 0)) as GameObject;
						tmp.transform.parent = transform;
					}
				}
				usedValuesX = new List<int>();
				usedValuesY = new List<int>();

				// Top right quadrant
				for (var i = 0; i < 5; i++) {
					int x;
					int y;
					UniqueRandomCell(9,17,5,10, out x, out y);

					GameObject tmp;
					var pick = Random.value;

					if (pick > .55) {
						tmp = Instantiate (BombPrefab, new Vector3 (x * 4, 1, y * 4), Quaternion.Euler (90, 0, 0)) as GameObject;
						tmp.transform.parent = transform;
					} else if (pick > .2) {
						tmp = Instantiate (WallPrefab, new Vector3 (x * 4, 1, y * 4), Quaternion.Euler (90, 0, 0)) as GameObject;
						tmp.transform.parent = transform;
					} else {
						tmp = Instantiate (HourglassPrefab, new Vector3 (x * 4, 1, y * 4), Quaternion.Euler (90, 0, 0)) as GameObject;
						tmp.transform.parent = transform;
					}
				}
				usedValuesX = new List<int>();
				usedValuesY = new List<int>();
			}
		}
	}
		

	void UniqueRandomCell(int minX, int maxX, int minY, int maxY, out int return1, out int return2)
	{
		int x = Random.Range(minX, maxX + 1);
		int y = Random.Range(minY, maxY + 1);

		while(usedValuesX.Contains(x) && usedValuesY.Contains(y))
		{
			x = Random.Range(minX, maxX + 1);
			y = Random.Range(minY, maxY + 1);
		}

		usedValuesX.Add (x);
		usedValuesY.Add (y);

		return1 = x;
		return2 = y;
	}

	IEnumerator roundStart(bool wait) {
		yield return new WaitForSeconds (2.0f);
		wait = false;
	}
}

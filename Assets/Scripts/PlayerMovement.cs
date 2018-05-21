using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	private Rigidbody rb;
	public float speed;
	public string horizontal = "P1_Horizontal";
	public string vertical = "P1_Vertical";
	public bool powerup;
	public bool bomb;
	public bool wall;
	public KeyCode fire;
	public KeyCode left;
	public KeyCode right;
	public KeyCode up;
	public KeyCode down;
	public GameObject go;
	public GameObject otherPlayer;
	public GameController gameController;
	public float minPos;
	public float maxPos;
	public AudioSource audioSource;
	public AudioClip pickupPowerup;
	public AudioClip hg_sound;
	public AudioClip bh_sound;
	public AudioClip emptyPowerup;
	public float vol;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("gameVolume"))
			vol = PlayerPrefs.GetFloat ("gameVolume");
		else
			vol = .7F;
		rb = GetComponent<Rigidbody> ();
		powerup = false;
		bomb = false;
		go.SetActive (false);
		minPos = 0;
		maxPos = 60;
		audioSource = this.GetComponent<AudioSource> ();
	}

	void FixedUpdate () {
		if (!gameController.gameOver) {
			// Player rotation and transformation controls
			if (Input.GetKey (left)) {
				Vector3 go = new Vector3 (1, 90, 0);
				rb.AddForce (Vector3.left * speed);
				transform.rotation = Quaternion.LookRotation (go);
			}
			if (Input.GetKey (right)) {
				Vector3 go = new Vector3 (-1, 90, 0);
				rb.AddForce (Vector3.right * speed);
				transform.rotation = Quaternion.LookRotation (go);
			}
			if (Input.GetKey (up)) {
				Vector3 go = new Vector3 (0, 90, -1);
				rb.AddForce (Vector3.forward * speed);
				transform.rotation = Quaternion.LookRotation (go);
			}
			if (Input.GetKey (down)) {
				Vector3 go = new Vector3 (0, 90, 1);
				rb.AddForce (Vector3.back * speed);
				transform.rotation = Quaternion.LookRotation (go);
			}
		}

	}

	// Update is called once per frame
	void Update() {
		/* Use powerup */
		if (Input.GetKeyUp (fire) && powerup) {
			go.SetActive (false);
			powerup = false;
		} else if (Input.GetKeyUp (fire) && !powerup) {
			audioSource.PlayOneShot(emptyPowerup, vol);
		}
	}
	/* Fly over powerup to pickup */
	void OnTriggerEnter(Collider other) {
		// Grab some powerups (Powerups: Bomb, Wall Builder, Hourglass)
		if (other.gameObject.tag.Equals ("Powerup")) {
			if (other.gameObject.name.StartsWith ("Hour")) {
				audioSource.PlayOneShot (hg_sound, vol);
				Destroy (other.gameObject);
				PlayerMovement otherScript = otherPlayer.GetComponent<PlayerMovement> ();
				StartCoroutine (otherScript.ReduceSpeed ());
				StartCoroutine (this.GetComponent<PlayerMovement>().IncSpeed ());
			}
			else if (!powerup) {
				// Pick up powerup
				if (other.gameObject.name.StartsWith ("Bomb")) {
					bomb = true;
				} else if (other.gameObject.name.StartsWith("Wall")) {
					wall = true;
				}

				// Glow and destroy the powerup
				go.SetActive (true);
				powerup = true;
				audioSource.PlayOneShot(pickupPowerup, vol);
				Destroy (other.gameObject);
			}
		}
		// Enter the blackhole
		if (other.gameObject.tag.Equals ("Blackhole")) {
			audioSource.PlayOneShot (bh_sound, vol);
			Destroy (other.gameObject);
			EnterBlackHole ();
		}
		// If player has exited the maze
		if (other.gameObject.tag.Equals ("Exit")) {
			// End the Round
			gameController.gameOver = true;
			gameController.restart = true;

			// Freeze player
			rb.constraints = RigidbodyConstraints.FreezeAll;

			// Increment score
			if (name == "Player_Blue") {
				gameController.BlueWin();
				Debug.Log ("BLUE WINS!");
			} else if (name == "Player_Blue (1)") {
				Debug.Log ("RED WINS!");
				gameController.RedWin();
			}

			gameController.saveAttributes ();
		}
	}
	/* Pick up powerup immediately after using an equipped one */
	void OnTriggerStay(Collider other) {
		if (other.gameObject.name.StartsWith ("Hour")) {
			return;
		}
		// Grab some powerups (Powerups: Bomb, Wall Builder, Hourglass)
		if (other.gameObject.tag.Equals ("Powerup") && !powerup) {
			// Pick up powerup
			if (other.gameObject.name.Equals ("Bomb(Clone)")) {
				bomb = true;
			} else if (other.gameObject.name.StartsWith("Wall")) {
				wall = true;
			}

			// Glow and destroy the powerup
			go.SetActive (true);
			powerup = true;
			audioSource.PlayOneShot(pickupPowerup, vol);
			Destroy (other.gameObject);
		}
	}

	IEnumerator ReduceSpeed() {
		speed = speed / 4;
		yield return new WaitForSeconds (3.0f);
		speed = speed * 4;

		yield return null;
	}
	IEnumerator IncSpeed() {
		speed = speed * 2;
		yield return new WaitForSeconds (3.0f);
		speed = speed / 2;

		yield return null;
	}

	void EnterBlackHole() {
		var newPos = new Vector3 (Random.Range(minPos,maxPos),1,Random.Range(minPos,30));
		transform.position = newPos;
	}
}

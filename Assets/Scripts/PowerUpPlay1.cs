using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpPlay1 : MonoBehaviour {
	public PlayerMovement playerMovement;
	public Image playerOnePowerUp;
	public Sprite bomb_new;
	public Sprite hour_glass;
	public Sprite wall_building;
	public Sprite blank;
	// Update is called once per frame
	void Update () {
		// check to see what powerup the play has
		// case 1. Bomb
		if(playerMovement.bomb == true)
			playerOnePowerUp.sprite = bomb_new;
		
		// case 3. wall building
		else if (playerMovement.wall == true)
			playerOnePowerUp.sprite = wall_building;

		// case 4: no powerup
		else 
			playerOnePowerUp.sprite = blank;
	}
}
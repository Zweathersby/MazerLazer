﻿using UnityEngine;
using System.Collections;

//<summary>
//Game object, that creates maze and instantiates it in scene
//</summary>
public class MazeSpawner : MonoBehaviour {
	public enum MazeGenerationAlgorithm{
		PureRecursive,
		RecursiveTree,
		RandomTree,
		OldestTree,
		RecursiveDivision,
	}

	public MazeGenerationAlgorithm Algorithm = MazeGenerationAlgorithm.PureRecursive;
	public bool FullRandom = false;
	public int RandomSeed = 12345;
	public GameObject Floor = null;
	public GameObject Wall = null;
	public GameObject OuterWall = null;
	public GameObject Pillar = null;
	public int Rows = 5;
	public int Columns = 5;
	public float CellWidth = 5;
	public float CellHeight = 5;
	public bool AddGaps = true;
	public GameObject HourglassPrefab = null;
	public GameObject BombPrefab = null;
	public GameObject PlayerWallPrefab = null;
	public GameObject BlackholePrefab = null;
	private int bhCount = 0;


	private BasicMazeGenerator mMazeGenerator = null;

	void Start () {
		if (!FullRandom) {
			Random.seed = RandomSeed;
		}
		switch (Algorithm) {
		case MazeGenerationAlgorithm.PureRecursive:
			mMazeGenerator = new RecursiveMazeGenerator (Rows, Columns);
			break;
		case MazeGenerationAlgorithm.RecursiveTree:
			mMazeGenerator = new RecursiveTreeMazeGenerator (Rows, Columns);
			break;
		case MazeGenerationAlgorithm.RandomTree:
			mMazeGenerator = new RandomTreeMazeGenerator (Rows, Columns);
			break;
		case MazeGenerationAlgorithm.OldestTree:
			mMazeGenerator = new OldestTreeMazeGenerator (Rows, Columns);
			break;
		case MazeGenerationAlgorithm.RecursiveDivision:
			mMazeGenerator = new DivisionMazeGenerator (Rows, Columns);
			break;
		}

		mMazeGenerator.GenerateMaze ();

		for (int row = 0; row < Rows; row++) {
			for(int column = 0; column < Columns; column++){
				float x = column*(CellWidth+(AddGaps?.2f:0));
				float z = row*(CellHeight+(AddGaps?.2f:0));
				MazeCell cell = mMazeGenerator.GetMazeCell(row,column);
				GameObject tmp;
//				tmp = Instantiate(Floor,new Vector3(x,0,z), Quaternion.Euler(0,0,0)) as GameObject;
//				tmp.transform.parent = transform;

				if(cell.WallRight){
					if(column == Columns-1) {
						tmp = Instantiate(OuterWall,new Vector3(x+CellWidth/2,0,z)+Wall.transform.position,Quaternion.Euler(0,90,0)) as GameObject;// right
						tmp.transform.parent = transform;
					} else {
						tmp = Instantiate(Wall,new Vector3(x+CellWidth/2,0,z)+Wall.transform.position,Quaternion.Euler(0,90,0)) as GameObject;// right
						tmp.transform.parent = transform;
					}
				}
				if(cell.WallFront){
					if(row == Rows-1) {
						tmp = Instantiate(OuterWall,new Vector3(x,0,z+CellHeight/2)+Wall.transform.position,Quaternion.Euler(0,0,0)) as GameObject;// front
						tmp.transform.parent = transform;
					} else {
						tmp = Instantiate(Wall,new Vector3(x,0,z+CellHeight/2)+Wall.transform.position,Quaternion.Euler(0,0,0)) as GameObject;// front
						tmp.transform.parent = transform;
					}
				}
				if(cell.WallLeft){
					if(column == 0) {
						tmp = Instantiate(OuterWall,new Vector3(x-CellWidth/2,0,z)+Wall.transform.position,Quaternion.Euler(0,270,0)) as GameObject;// left
						tmp.transform.parent = transform;
					} else {
						tmp = Instantiate(Wall,new Vector3(x-CellWidth/2,0,z)+Wall.transform.position,Quaternion.Euler(0,270,0)) as GameObject;// left
						tmp.transform.parent = transform;
					}
				}
				if(cell.WallBack){
					if(row == 0) {
						tmp = Instantiate(OuterWall,new Vector3(x,0,z-CellHeight/2)+Wall.transform.position,Quaternion.Euler(0,180,0)) as GameObject;// back
						tmp.transform.parent = transform;
					} else {
						tmp = Instantiate(Wall,new Vector3(x,0,z-CellHeight/2)+Wall.transform.position,Quaternion.Euler(0,180,0)) as GameObject;// back
						tmp.transform.parent = transform;
					}
				}
					
				if(cell.IsGoal){
					var pick = Random.value;

					if(pick > .6) {
						tmp = Instantiate(BombPrefab,new Vector3(x,1,z), Quaternion.Euler(90,0,0)) as GameObject;
						tmp.transform.parent = transform;
					} else if(pick > .4) {
						tmp = Instantiate(PlayerWallPrefab,new Vector3(x,1,z), Quaternion.Euler(90,0,0)) as GameObject;
						tmp.transform.parent = transform;
					} else if(pick > .2) {
						tmp = Instantiate(HourglassPrefab,new Vector3(x,1,z), Quaternion.Euler(90,0,0)) as GameObject;
						tmp.transform.parent = transform;
					} else if(pick > 0 && bhCount == 0) {
						tmp = Instantiate(BlackholePrefab,new Vector3(x,1,z), Quaternion.Euler(90,0,0)) as GameObject;
						tmp.transform.parent = transform;
						bhCount++;
					}

//					switch (Random.Range(0,3)) {
//					case 0:
//						tmp = Instantiate(HourglassPrefab,new Vector3(x,1,z), Quaternion.Euler(90,0,0)) as GameObject;
//						tmp.transform.parent = transform;
//						break;
//					case 1:
//						tmp = Instantiate(BombPrefab,new Vector3(x,1,z), Quaternion.Euler(90,0,0)) as GameObject;
//						tmp.transform.parent = transform;
//						break;
//					case 2:
//						tmp = Instantiate(PlayerWallPrefab,new Vector3(x,1,z), Quaternion.Euler(90,0,0)) as GameObject;
//						tmp.transform.parent = transform;
//						break;
//					case 3:
//						if(bhCount == 0) {
//							tmp = Instantiate(BlackholePrefab,new Vector3(x,1,z), Quaternion.Euler(90,0,0)) as GameObject;
//							tmp.transform.parent = transform;
//							bhCount++;
//						}
//						break;
//					}
				}
			}
		}
		if(Pillar != null){
			for (int row = 0; row < Rows+1; row++) {
				for (int column = 0; column < Columns+1; column++) {
					float x = column*(CellWidth+(AddGaps?.2f:0));
					float z = row*(CellHeight+(AddGaps?.2f:0));
					GameObject tmp = Instantiate(Pillar,new Vector3(x-CellWidth/2,0,z-CellHeight/2),Quaternion.identity) as GameObject;
					tmp.transform.parent = transform;
				}
			}
		}

		StartCoroutine(MazeChange());
	}

	IEnumerator MazeChange() {
		yield return new WaitForSeconds (1.0f);
	}
}

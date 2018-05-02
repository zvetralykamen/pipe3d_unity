using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagment : MonoBehaviour {

	public GameObject gridSpace;

	public int levelX;
	public int levelY;
	public int levelZ;

	// Use this for initialization
	void Start () {
		InitializeGridSpace ();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void InitializeGridSpace(){
		Vector3 currentVector = Vector3.zero; 

		//Random starting position of the flow.
		Vector3 startingSpace = new Vector3 (Random.Range (0, levelX), Random.Range (0, levelY), Random.Range (0, levelZ));
		Debug.Log (startingSpace);

		//Generating grid.
		for (var i = 0; i < levelY; i++) {
			for (var j = 0; j < levelZ; j++) {
				for (var k = 0; k < levelX; k++) {
					currentVector = new Vector3 (k, i, j);
					GameObject gs_object = Instantiate (gridSpace, currentVector, Quaternion.identity);
					GridSpace gs = gs_object.GetComponent<GridSpace>();

					//Find starting space.
					if (currentVector == startingSpace) {
						Debug.Log ("Starting space found!");
						gs.isStartingPoint = true;
						gs.isEmpty = false;
						gs_object.GetComponentInChildren<Renderer> ().material.color = Color.cyan;
					}
				}
			}
		}
	}
}

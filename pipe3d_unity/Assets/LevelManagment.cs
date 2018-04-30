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
		for (var i = 0; i < levelY; i++) {
			for (var j = 0; j < levelZ; j++) {
				for (var k = 0; k < levelX; k++) {
					Instantiate (gridSpace, new Vector3(k,i,j), Quaternion.identity);
				}
			}
		}
	}
}

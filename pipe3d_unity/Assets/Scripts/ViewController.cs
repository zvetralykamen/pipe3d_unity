using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour {

	//Get LevelManagment script.
	public LevelManagment lm;

	private BoxCollider	boxcol;
	private Vector3 currentcenter;

	// Use this for initialization
	void Start () {
		boxcol = gameObject.GetComponent<BoxCollider> ();
		boxcol.size = new Vector3 (lm.levelX, lm.levelY, lm.levelZ);
		currentcenter = new Vector3 (lm.levelX / 2, lm.levelY + lm.levelY / 2, lm.levelZ / 2);
	}
	
	// Update is called once per frame
	void Update () {
		boxcol.center = currentcenter;

		if (Input.GetKeyDown(KeyCode.O)) {
			currentcenter = new Vector3(currentcenter.x,currentcenter.y + 1,currentcenter.z);
		}
		if (Input.GetKeyDown(KeyCode.P)) {
			currentcenter = new Vector3(currentcenter.x,currentcenter.y - 1,currentcenter.z);
		}
		
	}
}

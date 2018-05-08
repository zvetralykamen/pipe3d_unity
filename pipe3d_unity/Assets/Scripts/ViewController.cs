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
		currentcenter = new Vector3 (lm.levelX / 2, lm.levelY + lm.levelY / 2 + 0.5f, lm.levelZ / 2);
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

		currentcenter.y = Mathf.Clamp(currentcenter.y, 1.5f , lm.levelY + lm.levelY / 2 + 0.5f);
	}

	void OnCollisionEnter(Collision col){

		if (col.gameObject.CompareTag("GridSpace")){
			GridSpace gs = col.gameObject.GetComponentInChildren<GridSpace> ();
			gs.SetVisibility (false);
            col.gameObject.layer = 2; //Layer 2 is ignoreRaychast so that you can place stuff under it
		}
	}

	void OnCollisionExit(Collision col){

		if (col.gameObject.CompareTag ("GridSpace")) {
			GridSpace gs = col.gameObject.GetComponentInChildren<GridSpace> ();
			gs.SetVisibility (true);
            col.gameObject.layer = 0;
        }
	}
}

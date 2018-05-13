using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpace : MonoBehaviour {

	public GameObject gridCube;
	public GameObject gridPipe;

	public bool isEmpty = true;
	public bool isStartingPoint = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!isEmpty && !isStartingPoint) {
			gridCube.SetActive (false);
		}
	}

	public void SetCubeVisibility(bool isVisible){
		int childCount = gameObject.transform.childCount;
		for(var i = 0; i < childCount; i++){
			gameObject.transform.GetChild(i).gameObject.SetActive (isVisible);	
		}
	}
}

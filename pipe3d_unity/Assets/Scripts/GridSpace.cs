﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpace : MonoBehaviour {

	public bool isEmpty = true;
	public bool isStartingPoint = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetVisibility(bool isVisible){
		gameObject.transform.GetChild(0).gameObject.SetActive (isVisible);
	}
}

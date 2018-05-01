using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Camera camera1;
    public float distance = 1f;
    public float rotationSpeed;

    private bool change_transform = false;
    private Vector3 new_pos = new Vector3(0, 0, 0);

    public float sensetivity = 5f;
    private Vector3 rot = new Vector3(0, 0, 0);


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        keyboard_input();
        mouse_input();
    }

    void FixedUpdate()
    {
        /*
        if (change_transform) {
            this.transform.position = new_pos;
        }
        */
    }

    void keyboard_input() {

        new_pos = transform.position;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            new_pos += new Vector3(distance, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            new_pos += new Vector3(-distance, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            new_pos += new Vector3(0, 0, distance);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            new_pos += new Vector3(0, 0, -distance);
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            new_pos += new Vector3(0, distance, 0);
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            new_pos += new Vector3(0, -distance, 0);
        }

        new_pos.x = Mathf.Clamp(new_pos.x, 0, 7-1); //-----------------------------------------Add Reference to Levelmanager
        new_pos.y = Mathf.Clamp(new_pos.y, 0, 3-1);
        new_pos.z = Mathf.Clamp(new_pos.z, 0, 7-1);

        this.transform.position = new_pos;
        //change_transform = true;
    }

    void mouse_input()
    {
        if (Input.GetMouseButton(1))
        {
            float _x = Input.GetAxisRaw("Mouse X") * sensetivity;
            float _y = Input.GetAxisRaw("Mouse Y") * sensetivity;

            rot = new Vector3(0, rot.y + _x, rot.z + _y);

            this.transform.rotation = Quaternion.Euler(0, rot.y, rot.z);
            //camera1.transform.RotateAround(this.transform.position, transform.up, _x * rotationSpeed);
        }
        
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingSystem : MonoBehaviour {

    public Camera camera1;

    //pipe prefabs
    public GameObject pipe_i; //1
    public GameObject pipe_c; //2
    public GameObject pipe_t; //3
    public GameObject pipe_x; //4
    //public GameObject pipe_cross_plus1; //5 // I don't know how to call it; it's a cross but with one additional connection up or down
    //public GameObject pipe_allconnected; //6
    public GameObject pipe_end; //7


    //vars for placing options
    public int amount_of_options = 7;
    public float[] rotations = new float[] { 0, 90, 180, 270 };

    public GameObject option_1;
    public GameObject option_2;
    public GameObject option_3;
    private bool option_available = true;
    private bool option_2_available = true;
    private bool option_3_available = true;

    //vars for options refill
    private Vector3 option_position;

    //vars for selecting
    private bool selecting = true;
    private GameObject selected;

    void Start() { }

    void Update() {
        setOptions(); //random rotation is n- ... i don't know i think it works now...

        movingOptions();
    }


    void setOptions() {
        if (option_available == true) {
            instantiate_rndOption(option_1.transform.position);
            option_available = false;
        }
        else if (option_2_available == true) {
            instantiate_rndOption(option_2.transform.position);
            option_2_available = false;
        }
        else if (option_3_available == true) {
            instantiate_rndOption(option_3.transform.position);
            option_3_available = false;
        }
    }

    void instantiate_rndOption(Vector3 position) {
        int rnd = Random.Range(1, amount_of_options);
        Quaternion rndRotation = Quaternion.Euler(rotations[Random.Range(0, 3)], rotations[Random.Range(0, 3)], rotations[Random.Range(0, 3)]);

        switch (rnd) {
            case 1:
                Instantiate(pipe_i, position, rndRotation);
                break;
            case 2:
                Instantiate(pipe_end, position, rndRotation); //just so there is something
                //Instantiate(pipe_c, position, rndRotation);
                break;
            case 3:
                Instantiate(pipe_t, position, rndRotation);
                break;
            case 4:
                Instantiate(pipe_x, position, rndRotation);
                break;
            case 5:
                Instantiate(pipe_end, position, rndRotation); //just so there is something
                //Instantiate(pipe_cross_plus1, position, rndRotation);
                break;
            case 6:
                Instantiate(pipe_end, position, rndRotation); //just so there is something
                //Instantiate(pipe_allconnected, position, rndRotation);
                break;

            case 7:
                Instantiate(pipe_end, position, rndRotation);
                break;
        }
    }

    void movingOptions() { //I had to change _ViewController's Layer to 2/IgnoreRaycast bc the ray didn't went throught it's collider

        if (Input.GetMouseButtonDown(0)) {
            Ray ray1 = camera1.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            if (Physics.Raycast(ray1, out hitinfo)) {
                Debug.Log(hitinfo.transform.gameObject.name);

                if (selecting == true) {
                    if (hitinfo.transform.gameObject.CompareTag("Pipe")) {
                        option_position = hitinfo.transform.position;
                        selected = hitinfo.transform.gameObject;
                        selecting = false;
                        Debug.Log("Selected: " + selected.name);
                    }
                }
                else if (selecting == false) {
                    if (hitinfo.transform.gameObject.CompareTag("GridSpace")) {
                        GameObject go = hitinfo.transform.gameObject;
                        GridSpace gs = go.GetComponentInParent<GridSpace>();
                        Destroy(selected);

						//Overriding the previous pipe. It will also have effect on score.
						if (!gs.isEmpty) {
							Destroy (gs.gridPipe);
						}
						GameObject newPipe = Instantiate(selected, hitinfo.transform.position, selected.transform.rotation);
                        gs.isEmpty = false;
						gs.gridPipe = newPipe;
                        gs.SetCubeVisibility(false);
						newPipe.transform.SetParent (gs.gameObject.transform);
                        setOptionRefill();
                        selected = null;
                        selecting = true;
                    }
                }
            }
        }
    }
    void setOptionRefill() {
        if (option_position == option_1.transform.position) {
            option_available = true;
        }
        else if(option_position == option_2.transform.position) {
            option_2_available = true;
        }
        else if (option_position == option_3.transform.position) {
            option_3_available = true;
        }
    } 
}
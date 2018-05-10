using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingSystem : MonoBehaviour {

    public Camera camera1;

    //pipe prefabs
    public GameObject pipe_straight; //1
    /*
    public GameObject pipe_curve; //2
    public GameObject pipe_T; //3
    public GameObject pipe_cross; //4
    public GameObject pipe_cross_plus1; //5 // I don't know how to call it; it's a cross but with one additional connection up or down
    public GameObject pipe_allconnected; //6
    */
    public GameObject pipe_end; //7


    //vars for placing options
    public int amount_of_options = 7;
    public float[] rotations = new float[] { 0, 90, 180, 270 };

    public GameObject option_1;
    public GameObject option_2;
    public GameObject option_3;
    private bool option_1_available = true;
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
        if (option_1_available == true) {
            instantiate_rndOption(option_1.transform.position);
            option_1_available = false;
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
        int rnd = 1; //int rnd = Random.Range(1, amount_of_options);
        Quaternion rndRotation = Quaternion.Euler(rotations[Random.Range(0, 3)], rotations[Random.Range(0, 3)], rotations[Random.Range(0, 3)]);

        switch (rnd) {
            case 1:
                Instantiate(pipe_straight, position, rndRotation);
                break;
            /*
         case 2:
             Instantiate(pipe_curve, position, rndRotation);
             break;
         case 3:
             Instantiate(pipe_T, position, Quaternion.identity);
             break;
         case 4:
             Instantiate(pipe_cross, position, Quaternion.identity);
             break;
         case 5:
             Instantiate(pipe_cross_plus1, position, Quaternion.identity);
             break;
         case 6:
             Instantiate(pipe_allconnected, position, Quaternion.identity);
             break;
             */
            case 7:
                Instantiate(pipe_end, position, Quaternion.identity);
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
                        Instantiate(selected, hitinfo.transform.position, selected.transform.rotation);
                        gs.isEmpty = false;
                        gs.SetVisibility(false);
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
            option_1_available = true;
        }
        else if(option_position == option_2.transform.position) {
            option_2_available = true;
        }
        else if (option_position == option_3.transform.position) {
            option_3_available = true;
        }
    } 
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingSystemVer2 : MonoBehaviour {

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
    
    public GameObject coordinates_option;
    public GameObject coordinates_next_1;
    public GameObject coordinates_next_2;

    private GameObject go_option = null;
    private GameObject go_next_1 = null;
    private GameObject go_next_2 = null;

    private bool option_available = true;
    private bool next_1_available = true;
    private bool next_2_available = true;

    private bool option_full = false;
    private bool next_1_full = false;
    private bool next_2_full = false;

    
    //vars for options refill
    private bool do_refill = true;

    //vars for selecting
    private bool selecting = true;
    private GameObject selected;

    void Start() {
        /*
        go_option = returnRndOptionGO(coordinates_option.transform.position);
        go_next_1 = returnRndOptionGO(coordinates_next_1.transform.position);
        go_next_2 = returnRndOptionGO(coordinates_next_2.transform.position);
        Instantiate(go_option);
        Instantiate(go_next_1);
        Instantiate(go_next_2);
        option_available = false;
        next_1_available = false;
        next_2_available = false;
        */
    }

    void Update() {
        setOptions2();

        movingOptions();

        setOptionRefill();
    }


    void setOptions() {
        if (option_available == true) {
            if (next_1_available == false) {
                if (next_2_available == false) {
                    if (go_next_1 == null) { Debug.Log("...1"); } else go_option = go_next_1; 
                    go_option.transform.position = coordinates_option.transform.position;
                    go_next_1 = null;
                    option_available = false;
                }
            }
        }
        if (next_1_available == true) {
            if (next_2_available == false) {
                if (go_next_2 == null) { Debug.Log("...2"); } else go_next_1 = go_next_2;
                go_next_1.transform.position = coordinates_option.transform.position;
                go_next_2 = null;
                option_available = false;
            }
        }
        if (next_2_available == true) {
            go_next_2 = returnRndOptionGO(coordinates_next_2.transform.position);
            Instantiate(go_next_2);
            next_2_available = false;
        }
    }

    void setOptions2() {

        if (do_refill == true) {
            go_next_2 = returnRndOptionGO(coordinates_next_2.transform.position);
            Instantiate(go_next_2);
            do_refill = false;
            next_2_full = true;
        }

        if (next_1_full == false && next_2_full == true) {
            go_next_1 = go_next_2;
            Instantiate(go_next_1, coordinates_next_1.transform.position, go_next_1.transform.rotation);
            //Neither Destroy nor poition changing is working for me and I don't know why
            //---------------------------------------------------------------------------------------------------------------------------------
            //Destroy(go_next_2); 
            //go_next_1.transform.position = coordinates_next_1.transform.position; 
            //---------------------------------------------------------------------------------------------------------------------------------
            Debug.Log("?");
            next_1_full = true;
            next_2_full = false;
        }
    }

    GameObject returnRndOptionGO(Vector3 position) {
        GameObject go = new GameObject();
        int rnd = Random.Range(1, amount_of_options);
        Quaternion rndRotation = Quaternion.Euler(rotations[Random.Range(0, 3)], rotations[Random.Range(0, 3)], rotations[Random.Range(0, 3)]);

        switch (rnd) {
            case 1:
                go = pipe_i;
                break;
            case 2:
                //go = pipe_end; //just so there is something
                go = pipe_c;
                break;
            case 3:
                go = pipe_t;
                break;
            case 4:
                go = pipe_x;
                break;
            case 5:
                go = pipe_end; //just so there is something
                //go = pipe_cross_plus1;
                break;
            case 6:
                go = pipe_end; //just so there is something
                //go = pipe_allconnected;
                break;
            case 7:
                go = pipe_end;
                break;
        }

        go.transform.position = position;
        go.transform.rotation = rndRotation;
        return go;
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

    void movingOptions() {

        if (Input.GetMouseButtonDown(0)) {
            Ray ray1 = camera1.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            if (Physics.Raycast(ray1, out hitinfo)) {
                //Debug.Log(hitinfo.transform.gameObject.name);

                if (selecting == true) {
                    if (hitinfo.transform.gameObject.CompareTag("Pipe")) {
                        //option_position = hitinfo.transform.position;
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
                            Destroy(gs.gridPipe);
                        }
                        GameObject newPipe = Instantiate(selected, hitinfo.transform.position, selected.transform.rotation);
                        gs.isEmpty = false;
                        gs.gridPipe = newPipe;
                        gs.SetCubeVisibility(false);

                        do_refill = true;
                        Debug.Log("refill_1");
                        //setOptionRefill();

                        newPipe.transform.SetParent(gs.gameObject.transform);
                        selected = null;
                        selecting = true;
                    }
                }
            }
        }
    }

    void setOptionRefill() {
        if (next_2_full == false) {
            do_refill = true;
            Debug.Log("refill_2");
        }



        /*
        if (option_position == coordinates_option.transform.position) {
            option_available = true;
        }
        if (go_next_1 == null) {
            next_1_available = true;
        }
        
        if (go_next_2 == null) {
            next_2_available = true;
        }
        */
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameraturn : MonoBehaviour {

    //Recieved help for code from the following website:
    //https://www.mvcode.com/lessons/first-person-camera-controls-jamie
    //

    //x and y boundaries
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    //x and y sensitivity
    public float sensitivityX;
    public float sensitivityY;
    //rotation variables
    private float rotationX;
    private float rotationY;

    public Camera cam;

	// Use this for initialization
	void Start () {
        //initialization of variables
        minX = -100f;
        maxX = 100f;
        minY = -360f;
        maxY = 360f;
        sensitivityX = 10f;
        sensitivityY = 10f;
        rotationX = 0f;
        rotationY = 0f;

        //lock mouse cursor and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
        //unlock mouse cursor when escape is pressed
        if(Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
}
	
	// Update is called once per frame
	void Update () {
        //get axis from mouse x and y and use sensitivity to get desired speed
        //it's backwards which is confusing but w/e
        rotationY += Input.GetAxis("Mouse X") * sensitivityX;
        rotationX += Input.GetAxis("Mouse Y") * sensitivityY;
        //clamp values to maximum and minimum values
        rotationX = Mathf.Clamp(rotationX, minX, maxX);
        rotationY = Mathf.Clamp(rotationY, minY, maxY);
        //rotate player around y axis, camera around x and y axis
        transform.localEulerAngles = new Vector3(0f, rotationY, 0f);
        cam.transform.localEulerAngles = new Vector3(-rotationX, rotationY, 0f);
        Debug.Log(Input.GetAxis("Mouse X") + " " + Input.GetAxis("Mouse Y"));
    }
}

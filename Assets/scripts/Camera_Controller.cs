using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour {
    
    public enum RotationAxis
    {
        RotMouseX = 1, 
        RotMouseY = 2
    }

    public RotationAxis axis = RotationAxis.RotMouseX;

    //set the maximum and minimum vertical angles the player can look
    private float minimumV = -65f;
    private float maximumV = 65f;

    //set the camera sensitivity
    [SerializeField] private float sensitivityH;
    [SerializeField] private float sensitivityV;

    public float rotationX = 0;
    void Update() {

        //rotate horizontally
        if (axis == RotationAxis.RotMouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityH, 0);
        }       
        else if (axis == RotationAxis.RotMouseY)
        {
            rotationX -= Input.GetAxis("Mouse Y") * sensitivityV;
            //clamps the rotations to the set values
            rotationX = Mathf.Clamp(rotationX, minimumV, maximumV);

            //rotate vertically
            float rotationY = transform.localEulerAngles.y;
            transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
        }
	}
}

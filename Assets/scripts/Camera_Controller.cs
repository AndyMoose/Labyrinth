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

    [SerializeField] private float minimumV;
    private float maximumV = 70f;
    private float sensitivityH = 10f;
    private float sensitivityV = 10f;

    public float rotationX = 0;
    // Update is called once per frame
    void Update() {

        if (axis == RotationAxis.RotMouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityH, 0);
        } else if (axis == RotationAxis.RotMouseY)
        {
            rotationX -= Input.GetAxis("Mouse Y") * sensitivityV;
            rotationX = Mathf.Clamp(rotationX, minimumV, maximumV);

            float rotationY = transform.localEulerAngles.y;
            transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
        }
	}
}

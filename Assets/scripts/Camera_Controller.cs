using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{

    public enum RotationAxis
    {
        RotMouseX = 1,
        RotMouseY = 2
    }

    public RotationAxis axis = RotationAxis.RotMouseX;

    [SerializeField] PlayerController player;
    [SerializeField] Animator anim;
    [SerializeField] Camera FPScam;
    private Vector3 cameraPos;
    private Vector3 cameraOffset;
    //maximum and minimum vertical angles the player can look
    private float minimumV;
    private float maximumV;

    public float sensitivity; 

    //set the camera sensitivity
    [SerializeField] private float sensitivityH;
    [SerializeField] private float sensitivityV;
    public float rotationX = 0;

    void Start()
    {
        minimumV = -40f;
        maximumV = 40f;

        cameraPos = anim.GetBoneTransform(HumanBodyBones.Head).position;
        //lock mouse cursor and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
        //unlock mouse cursor when escape is pressed
        sensitivityH = sensitivity;
        sensitivityV = sensitivity;

       
    }
    //put in slider
    
    void Update()
    {
        //rotate horizontally   
        sensitivityH = sensitivity;
        sensitivityV = sensitivity;
        if (!player.isDead && !player.hasWon)
        {
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
            //sets the camera position to slightly above and ahead from the players head.
            cameraPos = anim.GetBoneTransform(HumanBodyBones.Head).position;
            cameraOffset = player.transform.forward / 6f;
            cameraOffset.y = .25f;
            FPScam.transform.position = cameraPos + cameraOffset;
        } else
        {
            cameraPos = anim.GetBoneTransform(HumanBodyBones.Head).position;
            cameraOffset = player.transform.forward / 25f;
            cameraOffset.y = -0.25f;
            FPScam.transform.position = cameraPos + cameraOffset;
        }
		if (Input.GetKey(KeyCode.Escape))
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		if (Input.GetKey(KeyCode.L) && Cursor.lockState == CursorLockMode.None)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
    }
}

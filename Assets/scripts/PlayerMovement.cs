using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private CharacterController characterCont;
    private float maxSpeed;
    private float gravity = -9.8f;
    private bool isMoving;
    [SerializeField] Animator animations;

    void Start () {
        characterCont = GetComponent<CharacterController>();
        maxSpeed = 5f;
        isMoving = false;
	}
	
	void Update () {

        //get the forward and sideways movement values
        float Xinput = Input.GetAxis("Horizontal") * maxSpeed;
        float Zinput = Input.GetAxis("Vertical") * maxSpeed;

        //sets velocity vector
        Vector3 velocity = new Vector3(Xinput, 0, Zinput);
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        //keeps player on the ground
        velocity.y = gravity;

        //move the player
        velocity *= Time.deltaTime;
        velocity = transform.TransformDirection(velocity);
        characterCont.Move(velocity);

        if (velocity != Vector3.zero && !isMoving)
        {
            animations.SetBool("isMoving", true);
            isMoving = true;
        }
        else if (velocity == Vector3.zero)
        {
            animations.SetBool("isMoving", false);
            isMoving = false;
        }        
    }
}

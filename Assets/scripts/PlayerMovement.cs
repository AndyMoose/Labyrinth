using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private float maxSpeed;
    private float gravity = -9.8f;
    [SerializeField] private Animator animations;
    [SerializeField] private CharacterController characterCont;

    void Start () {
        maxSpeed = 5f;
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

        //changes the state of the movement animations based on the velocity.
        animations.SetFloat("velX", velocity.x);
        animations.SetFloat("velZ", velocity.z);

        //move the player
        velocity *= Time.deltaTime;
        velocity = transform.TransformDirection(velocity);
        characterCont.Move(velocity);
    }
}

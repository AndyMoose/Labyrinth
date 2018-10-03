using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private CharacterController characterCont;
    private float maxSpeed;
    private float gravity = -9.8f;

    // Use this for initialization
    void Start () {
        characterCont = GetComponent<CharacterController>();
        maxSpeed = 5f;
	}
	
	// Update is called once per frame
	void Update () {
        float Xinput = Input.GetAxis("Horizontal") * maxSpeed;
        float Zinput = Input.GetAxis("Vertical") * maxSpeed;

        Vector3 velocity = new Vector3(Xinput, 0, Zinput);
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        velocity.y = gravity;

        velocity *= Time.deltaTime;
        velocity = transform.TransformDirection(velocity);
        characterCont.Move(velocity);

        
    }
}

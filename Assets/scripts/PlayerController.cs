﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float maxSpeed;
    private float gravity;
    [SerializeField] private Animator animations;
    [SerializeField] private CharacterController characterCont;

    private bool isAttacking;
    private bool isHit;

    void Start()
    {
        maxSpeed = 5f;
        gravity = -9.8f;
        isAttacking = false;
        isHit = false;
    }

    void Update()
    {
        PlayerAttack();
        PlayerMovement();
    }

    void PlayerMovement()
    {
        //get the forward and sideways movement values
        float Xinput = Input.GetAxis("Horizontal") * maxSpeed;
        float Zinput = Input.GetAxis("Vertical") * maxSpeed;

        //sets velocity vector
        Vector3 velocity = new Vector3(Xinput, 0, Zinput);

        if (velocity.z < 0)
        {
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed/2f);
        } else
        {
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        }
        

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

    void PlayerAttack()
    {
        //gets left mouse button 
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking)
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
        animations.SetBool("isAttacking", isAttacking);
    }

    void PlayerHit(Collision collision)
    {
        //need to get collision between minotaur and player, if the minotaur is charging then set isHit to true, otherwise its false.
        //Player will not get up when they are hit.
        if(collision.gameObject.tag == "minotaur")
        {
            isHit = true;
            animations.SetBool("isHit", isHit);
            isHit = false;
        } else
        {
            isHit = false;
            return;
        }
    }
}

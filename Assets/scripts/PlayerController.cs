using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject sword;
    private float maxSpeed;
    private float gravity;
    [SerializeField] private Animator animations;
    [SerializeField] private CharacterController characterCont;

    private bool isAttacking;
    public bool isDead;

    void Start()
    {    
        maxSpeed = 5f;
        gravity = -9.8f;
        isAttacking = false;
        isDead = false;
    }

    void Update()
    {
        PlayerAttack();
        PlayerMovement();
    }

    void PlayerMovement()
    {
        //get the forward and sideways movement values
        if (!isDead)
        {
            float Xinput = Input.GetAxis("Horizontal") * maxSpeed;
            float Zinput = Input.GetAxis("Vertical") * maxSpeed;

            //sets velocity vector
            Vector3 velocity = new Vector3(Xinput, 0, Zinput);

            if (velocity.z < 0)
            {
                velocity = Vector3.ClampMagnitude(velocity, maxSpeed / 2f);
            }
            else
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
    }

    void PlayerAttack()
    {
        //gets left mouse button 
        if (!isDead)
        {
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
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //need to get collision between minotaur and player, if the minotaur is charging then set isHit to true, otherwise its false.
        //Player will not get up when they are hit.
        if (hit.gameObject.tag == "minotaur")
        {
            animations.SetTrigger("isHit");
            isDead = true;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
         //deletes sword on collision with player
        if (other.gameObject.tag == "pickup")
        {
            sword.SetActive(true);
            Destroy(other.gameObject);
        }
    }

}

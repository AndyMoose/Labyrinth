using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur_Script : MonoBehaviour {

    public Animator animations;
    public CharacterController cc;
    public Transform player;
    
    //used for blending running and walking
    private float t;
    //variables for animations
    public bool beingHit;
    private bool running;
    private bool seesPlayer;
    public bool turning;
    //variables for speed and gravity
    private float speed;
    private float gravity;
    //variables for "sight"
    private float maxDistance;
    private float arc;

    // Use this for initialization
    void Start()
    { 
        running = false;
        t = 1;
        beingHit = false;
        seesPlayer = false;
        turning = false;
        speed = 0;
        gravity = -9.8f;
        maxDistance = 50f;
        arc = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        //runs towards player if player is within distance and within a certain angle
        if (Vector3.Distance(transform.position, player.position) < maxDistance)
        {
            // player is within distance
            if (Vector3.Angle(transform.forward, player.position - transform.position) < arc)
            {
                // player is ahead of me and in my field of view
                // TODO: raycast to check for obstacles (for once we have a real maze)
                running = true;
                seesPlayer = true;
            }
            else
            {
                running = false;
                seesPlayer = false;
            }
            

        }
        else
        {
            running = false;
            seesPlayer = false;
        }
        //checks to see if minotaur is being hit or turning (beingHit not currently used)
        if (!beingHit && !turning)
        {
            AnimatorStateInfo stateInfo = animations.GetCurrentAnimatorStateInfo(0);

            //sets "blend" variable in blend tree depending on if minotaur is walking or running
            if (running && t <= 2)
            {
                animations.SetFloat("Blend", t);
                t += .02f;
            }
            else if (!running && t >= 1)
            {
                animations.SetFloat("Blend", t);
                t -= .02f;
            }

            //changes speed based on running or walking
            if (!running)
                speed = .015f;
            else
                speed = .2f;
        }
        Vector3 vel = transform.forward * speed;
        vel.y = gravity;
        cc.Move(vel);
    }

    //checks for collsion with weapon and plays animation if so
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Weapon")
        {
            animations.SetTrigger("HitTrigger");
        }
        
    }

    //checks for collsion with wall and plays animation if so
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Wall")
        {
            animations.SetTrigger("LeftTurn");
        }
    }
}

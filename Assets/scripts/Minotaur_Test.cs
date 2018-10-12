using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur_Test : MonoBehaviour {

    public Animator animations;
    public CharacterController cc;
    private bool running;
    private float t;

    // Use this for initialization
    void Start()
    { 
        running = false;
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = animations.GetCurrentAnimatorStateInfo(0);
        if (Input.GetKeyDown(KeyCode.Space) && !running)
        {
            running = true;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && t >= 0)
        { 
            running = false;
        }
        if (running && t <= 1)
        {
            animations.SetFloat("Blend", t);
            t += .02f;
        }
        else if (!running && t >= 0)
        {
            animations.SetFloat("Blend", t);
            t -= .02f;
        }
        
        cc.Move(new Vector3(0.015f,0f,0.0f));
    }
}

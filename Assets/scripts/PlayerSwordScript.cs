using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordScript : MonoBehaviour {

    [SerializeField] Animator anim;
    [SerializeField] GameObject sword;
    private Vector3 swordPos;
    private Quaternion swordRot;
    // Use this for initialization
    void Start () {
        swordPos = anim.GetBoneTransform(HumanBodyBones.RightHand).position;
        swordRot.eulerAngles = anim.GetBoneTransform(HumanBodyBones.RightHand).position;

    }
	
	// Update is called once per frame
	void Update () {
        swordPos = anim.GetBoneTransform(HumanBodyBones.RightHand).position;
        swordRot.eulerAngles = anim.GetBoneTransform(HumanBodyBones.RightLowerArm).position;
        sword.transform.eulerAngles = swordRot.eulerAngles;
        sword.transform.position = swordPos;
    }
}

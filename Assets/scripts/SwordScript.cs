using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour {

    public float spinSpeed;

	// Update is called once per frame
	void Update () {
        //spins sword
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime, Space.World);
	}

   
}

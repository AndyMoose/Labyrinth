using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour {

    public float spinSpeed;
    public GameObject sword;

	// Update is called once per frame
	void Update () {
        //spins sword
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime, Space.World);
	}

    //deletes sword on collision with player
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            sword.SetActive(true);
            Destroy(gameObject);
        }
    }
}

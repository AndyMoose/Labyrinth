using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour {

	public EventSystem eventsystem;
	public GameObject selectedObject;
	private bool buttonselected = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetAxisRaw ("Vertical") != 0 && buttonselected == false)
		{
			eventsystem.SetSelectedGameObject (selectedObject);
			buttonselected = true;
		}
	}

	private void onDisable()
	{
		buttonselected = false;
	}
}

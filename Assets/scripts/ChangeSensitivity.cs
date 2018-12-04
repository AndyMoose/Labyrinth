using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChangeSensitivity : MonoBehaviour {

	public Slider volumeSlider;
	public Camera_Controller camera;
	public void OnValueChanged()
	{
		camera.sensitivity = 10 * volumeSlider.value;
	}
}

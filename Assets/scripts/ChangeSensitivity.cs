using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSensitivity : MonoBehaviour {
	public Camera_Controller camera;
	public Slider sens1Slider;
	public Slider sens2Slider;


	public void SetSensitivityX(float sliderValue)
	{
		camera.sensitivityx = 10f * sens1Slider.value;
	}

	public void SetSensitivityY(float sliderValue)
	{
		camera.sensitivityy = 10f * sens2Slider.value;
	}
		
}


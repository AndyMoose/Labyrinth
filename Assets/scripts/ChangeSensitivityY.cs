using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSensitivityY : MonoBehaviour {

	public Camera_Controller camera;
	public Slider sensxSlider;
	public void OnValueChanged()
	{
		camera.sensitivityy = 10 *sensxSlider.value;
	}

}

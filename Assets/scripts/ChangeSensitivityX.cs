using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSensitivityX : MonoBehaviour {

	public Camera_Controller camera;
	public Slider sensxSlider;
	public void OnValueChanged()
	{
		camera.sensitivityx = 10 *sensxSlider.value;
	}

}

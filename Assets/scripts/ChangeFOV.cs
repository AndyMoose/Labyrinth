using UnityEngine;

using UnityEngine.UI;

public class ChangeFOV : MonoBehaviour {

    // Use this for initialization
    public Slider FOVSlider;
    public Slider SensSlider;
    public Camera FPScam;
    public Camera_Controller cameraCont;

    public void OnValueChanged()
    {
        FPScam.fieldOfView = FOVSlider.value;
    }

    public void Sensitivity()
    {
        cameraCont.sensitivityx = SensSlider.value;
    }
}

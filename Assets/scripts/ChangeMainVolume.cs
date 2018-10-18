using UnityEngine;

using UnityEngine.UI;

using System.Collections;

public class ChangeMainVolume : MonoBehaviour
{

    public Slider volumeSlider;
    public void OnValueChanged()
    {
        MusicManager.Instance.mainVolume = volumeSlider.value;
    }
}
using UnityEngine;

using UnityEngine.UI;

using System.Collections;

public class ChangeSFXVolume : MonoBehaviour
{

    public Slider volumeSlider;
    public void OnValueChanged()
    {
        MusicManager.Instance.sfxVolume = volumeSlider.value;
    }
}
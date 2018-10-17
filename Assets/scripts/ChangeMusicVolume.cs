using UnityEngine;

using UnityEngine.UI;

using System.Collections;

public class ChangeMusicVolume : MonoBehaviour
{

    public Slider volumeSlider;
    public AudioSource music;
    public void OnValueChanged()
    {
        music.volume = MusicManager.Instance.musicVolume * MusicManager.Instance.mainVolume;
    }
}
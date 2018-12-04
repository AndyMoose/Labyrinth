using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadSceneOnClick : MonoBehaviour {

    public AudioClip music;


	public void LoadSceneByIndex(int sceneIndex)
	{
        MusicManager.Instance.MusicSource.Stop();
		SceneManager.LoadScene (sceneIndex);
		MusicManager.Instance.Play(music);
            

	}
}

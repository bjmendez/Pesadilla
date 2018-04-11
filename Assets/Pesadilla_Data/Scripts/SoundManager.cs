using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
	public AudioSource efxsource;
	public AudioSource musicSource;

	public static SoundManager instance = null;


	// Use this for initialization
	void Awake () {
		if (instance == null) {
			instance = this;
		} 
		else if (instance != this) {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
	}

	public void PlaySingle(AudioClip clip){
		efxsource.clip = clip;
		efxsource.Play ();
	}
	// Update is called once per frame
	void Update () {
		
	}
}

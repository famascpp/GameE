using UnityEngine;
using System.Collections;

public class TitleMusic : MonoBehaviour {

	private AudioSource musicSource;
	public AudioClip music;

	// Use this for initialization
	void Start () {
		musicSource = gameObject.AddComponent<AudioSource>();

		musicSource.Stop();
		musicSource.clip = music;
		musicSource.Play();
	}
	
	// Update is called once per frame
	void Update () {
		if(!(musicSource.clip == music))
			musicSource.Play();
	}
}

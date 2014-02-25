using UnityEngine;
using System.Collections;

public class TitleMusic : MonoBehaviour {

	private AudioSource musicSource;
	public AudioClip music;
	private AudioSource handclapSource;
	public AudioClip handclap; // 拍手音.
	public static bool clapFlag = false; // trueで拍手音.

	// Use this for initialization
	void Start () {
		musicSource = gameObject.AddComponent<AudioSource>();
		handclapSource = gameObject.AddComponent<AudioSource>();

		musicSource.loop = true;
		handclapSource.loop = false;

		musicSource.Stop();
		handclapSource.Stop();

		musicSource.clip = music;
		handclapSource.clip = handclap;

	}
	
	// Update is called once per frame
	void Update () {
		if( !ReadArduino.GetNowLoading() && !musicSource.isPlaying){
			musicSource.Play();
		}


		if( clapFlag ){
			handclapSource.Play();
			clapFlag = false;
		}
	}

	public static void SetClapFlag( bool flg ){
		clapFlag = flg;
	}

}

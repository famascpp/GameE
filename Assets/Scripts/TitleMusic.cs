using UnityEngine;
using System.Collections;

public class TitleMusic : MonoBehaviour {

	private AudioSource musicSource;
	public AudioClip music;
	private AudioSource handclapSource;
	public AudioClip handclap; // 拍手音.
	private AudioSource beatSource;
	public AudioClip beat; // かた、こし、ひざ.
	public static bool clapFlag = false; // trueで拍手音.
	public static bool beatFlag = false; // trueで叩く音.

	// Use this for initialization
	void Start () {
		musicSource = gameObject.AddComponent<AudioSource>();
		handclapSource = gameObject.AddComponent<AudioSource>();
		beatSource = gameObject.AddComponent<AudioSource>();

		musicSource.loop = true;
		handclapSource.loop = false;
		beatSource.loop = false;
		beatSource.volume = 100;

		musicSource.Stop();
		handclapSource.Stop();
		beatSource.Stop();

		musicSource.clip = music;
		handclapSource.clip = handclap;
		beatSource.clip = beat;

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

		if( beatFlag ){
			beatSource.Play();
			beatFlag = false;
		}
	}

	public static void SetClapFlag( bool flg ){
		clapFlag = flg;
	}
	public static void SetBeatFlag( bool flg ){
		beatFlag = flg;
	}
}

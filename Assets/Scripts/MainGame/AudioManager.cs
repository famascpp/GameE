using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	// 1beat = 60 / bpm
	// 4/4 measure = beat * 4
	float bpm;
	float beat;
	float measure;

	float audioTime = 0.0f;
	public float AudioTime{
		get { return this.audioTime; }
	}

	// Use this for initialization
	void Start () {
		bpm = 120;
		beat = 4;
	}
	
	// Update is called once per frame
	void Update () {
		audioTime += Time.deltaTime;
	}

	float get1MeasureTime()
	{
		return ( 60.0f / bpm ) * beat;
	}

	int getMeasure()
	{
		return (int)(audioTime / this.get1MeasureTime());
	}
}

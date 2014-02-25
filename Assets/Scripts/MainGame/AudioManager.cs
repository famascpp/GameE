using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	// 1beat = 60 / bpm
	// 4/4 measure = beat * 4
	float bpm = 125;
	float beat = 4;
	float measure;

	float startTime = 0.0f;

	float startDelayedTime = 3.0f;
	float startMusicDelayedTime = 0.0f;

	bool startedMusic = false;	// Began music?

	float audioTime = 0.0f;

	
	GameMusicManager gameAudio;
	

	public float AudioTime{
		get { return this.audioTime; }
	}

	void Awake()
	{
		startTime = Time.time + startDelayedTime;
	}

	// Use this for initialization
	void Start () {
		GameObject gameObj = Instantiate(Resources.Load("Prefabs/BGM/GameMusic01")) as GameObject;
		gameAudio = gameObj.GetComponent<GameMusicManager>();
	}
	
	// Update is called once per frame
	void Update () {
		audioTime = Time.time - startTime;

		if( startedMusic == false )
		{
			//Use PlayDelayed Once at 1 second remaining.
			if( audioTime > -1.0f )
			{
				startMusicDelayedTime = gameAudio.StartMusic(startTime - Time.time);
				startedMusic = true;
			}
		}

	}

	//1 measure time
	public float get1MeasureTime()
	{
		float ret = ( 60.0f / bpm ) * beat;
		return ret;
	}

	//what measure now?
	public int getMeasure()
	{
		return (int)(audioTime / this.get1MeasureTime());
	}

	//i put the time frome measure and beat
	public float getMeasureBeat( int measure , int measureDivision , int measureDivisionMax )
	{
		return this.get1MeasureTime() * (float)measure + 
			this.get1MeasureTime() * (1.0f / (float)measureDivisionMax ) * (float)measureDivision;
	}

	void OnGUI()
	{
	}

}

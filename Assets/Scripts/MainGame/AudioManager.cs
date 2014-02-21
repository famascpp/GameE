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

	bool startedMusic = false;	//音を再生し始めたかどうか.

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
			//残り1秒になったらPlayDelayedを使う.
			if( audioTime > -1.0f )
			{
				startMusicDelayedTime = gameAudio.StartMusic(startTime - Time.time);
				startedMusic = true;
			}
		}

	}

	//1小節の秒数.
	public float get1MeasureTime()
	{
		float ret = ( 60.0f / bpm ) * beat;
		return ret;
	}

	//今何小節目？.
	public int getMeasure()
	{
		return (int)(audioTime / this.get1MeasureTime());
	}

	//小節と拍子から時間を出す.
	public float getMeasureBeat( int measure , int measureDivision , int measureDivisionMax )
	{
		return this.get1MeasureTime() * (float)measure + 
			this.get1MeasureTime() * (1.0f / (float)measureDivisionMax ) * (float)measureDivision;
	}

	void OnGUI()
	{
		GUIStyle style;
		style = new GUIStyle();

		string str = "";

		str += "startTime" + "" + startTime + "\n";
		str += "startDelayedTime" + "" + startDelayedTime + "\n";
		str += "startMusicDelayedTime" + "" + startMusicDelayedTime + "\n";
		str += "Time.time" + Time.time + "\n";

		GUI.Label( new Rect(0,0,300,300) , str ,style );
	}

}

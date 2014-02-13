using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	// 1beat = 60 / bpm
	// 4/4 measure = beat * 4
	float bpm = 125;
	float beat = 4;
	float measure;

	float audioTime = 0.0f;

	
	public GameObject gameAudio;
	

	public float AudioTime{
		get { return this.audioTime; }
	}

	void Awake()
	{
		Instantiate(gameAudio);
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		audioTime += Time.deltaTime;
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

	void OnGUI(){
		GUI.Label(new Rect(0,0,100,100),""+audioTime);
	}
}

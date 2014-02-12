using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IconScore : MonoBehaviour {

	//譜面.
	int[][] score;
	
	//オーディオ.
	public AudioManager audioMgr;
	
	//次にアイコンを押す配列.
	int nextMeasure;
	int nextMeasureDivision;
	int nextMeasureDivisionNum;	//１小節になんこある?.

	// Use this for initialization
	void Start () {
	
	}
	
	void Awake()
	{
		GameObject obj = GameObject.Find("AudioManager");
		audioMgr = obj.GetComponent<AudioManager>();
	}
	

	// Update is called once per frame
	void Update () {
		if( getNextTime() < audioMgr.AudioTime )
			nextPush();
	}


	void nextPush()
	{
		bool loop = true;
		for( int i = nextMeasure ; i < this.score.Length && loop ; i++ )
		{
			for( int j = nextMeasureDivision ; j < this.score[i].Length && loop ; j++ )
			{
				nextMeasureDivisionNum = this.score[i].Length;
				if( this.score[i][j] != 0 )
				{
					nextMeasure = i;
					nextMeasureDivision = j;
					loop = false;
				}
			}
		}
	}

	public float getNextTime()
	{
		return audioMgr.get1MeasureTime() * (float)nextMeasure + 
			audioMgr.get1MeasureTime() * (1.0f / (float)nextMeasureDivisionNum ) * (float)nextMeasureDivision;
	}

	public void setScore(List<List<int>> score)
	{
		this.score = new int[score.Count][];
		for( int i = 0 ; i < score.Count ; i++ )
		{
			this.score[i] = new int[score[i].Count];
			for( int j = 0 ; j < score[i].Count ; j++ )
			{
				int a = score[i][j];
				this.score[i][j] = a;
			}
		}

		nextPush();
	}
	
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IconScore {

	//譜面.
	int[][] score;

	//次のアイコンがあるかどうか.
	bool next = false;
	public bool Next{
		get { return next; }
	}
	
	//オーディオ.
	public AudioManager audioMgr;
	
	//次にアイコンを押す配列.
	int nextMeasure;
	int nextMeasureDivision;
	int nextMeasureDivisionNum;	//１小節になんこある?.

	public void setScore(List<List<int>> score)
	{
		this.score = new int[score.Count][];
		for( int i = 0 ; i < score.Count ; i++ )
		{
			this.score[i] = new int[score[i].Count];
			for( int j = 0 ; j < score[i].Count ; j++ )
			{
				this.score[i][j] = score[i][j];
			}
		}
		
		nextPush();

		GameObject obj = GameObject.Find("GameManager");
		audioMgr = obj.GetComponent<AudioManager>();
	}

	// Update is called once per frame
	void Update () {
		//if( getNextTime() < 0.0f ) nextPush();
	}


	public void nextPush()
	{
		string str = "";

		int i = nextMeasure;
		int j = nextMeasureDivision + 1;

		for(  ; i < this.score.Length ; i++ )
		{
			for( ; j < this.score[i].Length ; j++ )
			{
				nextMeasureDivisionNum = this.score[i].Length;
				if( this.score[i][j] != 0 )
				{
					nextMeasure = i;
					nextMeasureDivision = j;
					str += "" + i + ":" + j + "\n";
					next = true;
					goto LOOPEND;	//多重ループ脱出用.
				}
			}
			j = 0;
		}

		//譜面がもうありません.
		next = false;

		LOOPEND:;
		Debug.Log(str);
	}

	public float getNextTime()
	{
		return getNextTotalTime() - audioMgr.AudioTime;
	}

	public float getNextTotalTime()
	{
		return audioMgr.get1MeasureTime() * (float)nextMeasure + 
			audioMgr.get1MeasureTime() * (1.0f / (float)nextMeasureDivisionNum ) * (float)nextMeasureDivision;
	}


}

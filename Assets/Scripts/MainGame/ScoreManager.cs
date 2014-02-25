﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

struct ScoreSet {
	public int score;
	public bool Pushed;
	public float PushTime;
}

public class ScoreManager : MonoBehaviour {

	ScoreSet[][][] ss;
	int[] ssMin;		//now Measure array.

	uint[] inputButton;

	public Texture circle;

	public Texture moveIconTexture;

	public GameObject pushSE;

	GameManager gameMgr;

	IconManager iconMgr;

	AudioManager audioManager;

	bool isUniduino = false;

	bool end = false;
	public bool isEnd{ get { return end; } }

	bool lrDistribution = false;	//Left and right distribution.

	void Awake()
	{
		GameObject tempUniduino = GameObject.Find("Uniduino");
		if( tempUniduino != null ) isUniduino = true;
	}

	// Use this for initialization
	void Start () {

		audioManager = this.GetComponent<AudioManager>();
		gameMgr = this.GetComponent<GameManager>();
		iconMgr = this.GetComponent<IconManager>();

		MusicScore canonLock = new MusicScore("music/test/test.bms");

		List<List<List<int>>> score = canonLock.Score;

		this.ssMin = new int[score.Count];
		for( int i = 0 ; i < ssMin.Length ; i++ ) ssMin[i] = 0;

		//　Add to array a score file you have loaded
		this.ss = new ScoreSet[score.Count][][];
		for( int i = 0 ; i < this.ss.Length ; i++ )
		{
			this.ss[i] = new ScoreSet[score[i].Count][];
			for( int j = 0 ; j < this.ss[i].Length ; j++ )
			{
				this.ss[i][j] = new ScoreSet[score[i][j].Count];
				for( int l = 0 ; l < this.ss[i][j].Length ; l++ )
				{
					this.ss[i][j][l].score = score[i][j][l];
					this.ss[i][j][l].Pushed = false;
					this.ss[i][j][l].PushTime = audioManager.getMeasureBeat(j,l,this.ss[i][j].Length);
				}
			}
		}

		inputButton = new uint[(int)IconEnum.Max];
		for( int i = 0 ; i < inputButton.Length ; i++ ) inputButton[i] = 0;
	}
	
	// Update is called once per frame
	void Update () {

		InputUpdate();

		//get scope
		float min = -2.0f;
		float max = 2.0f;

		bool colLoop;

		for( int i = 0 ; i < this.ss.Length ; i++ )
		{
			colLoop = true;
			for( int j = ssMin[i] ; j < this.ss[i].Length && colLoop ; j++ )
			{
				for( int l = 0 ; l < this.ss[i][j].Length && colLoop ; l++ )
				{
					//in string?.
					if( this.ss[i][j][l].score != 0 )
					{
						float nextTime = this.ss[i][j][l].PushTime - audioManager.AudioTime;
						if( (int)IconEnum.Max == i )
						{
							end = true;
						}

						if( this.ss[i][j][l].Pushed == false ){
							if( nextTime < max ){
								if( min < nextTime ){
									if( inputButton[i] == 2 )
										this.ss[i][j][l].Pushed = PushScoreIcon(this.ss[i][j][l],nextTime,i,j,l);
								}else{
									ssMin[i] = j;
								}
							}else{
								colLoop = false;
							}
						}
					}
				}
			}
		}
	}

	void InputUpdate()
	{
		//key input
		for( int i = 0 ; i < (int)IconEnum.Max ; i++ )
		{
			//Left and right distribution
			if( lrDistribution )
			{
				if( ( isUniduino && InputA.GetButton((IconEnum)i) ) ||
				   Input.GetKey( (KeyCode)((int)KeyCode.Alpha1 + i) )
				   ) inputButton[i]++;
				else inputButton[i] = 0;
			}
		}

	}

	bool PushScoreIcon(ScoreSet scoreSet,float nextTime,int i,int j,int l)
	{
		bool ret = false;

		float max = 0.2f;
		float min = -0.2f;
		if( min < nextTime && nextTime < max )
		{
			ret = true;
			Instantiate(pushSE);
		}
		return ret;
	}

	void OnGUI()
	{
		GUI.depth = -10;

		bool colLoop;	//nextTime n over not loop

		for( int i = 0 ; i < this.ss.Length ; i++ )
		{
			colLoop = true;
			for( int j = ssMin[i] ; j < this.ss[i].Length && colLoop ; j++ )
			{
				for( int l = 0 ; l < this.ss[i][j].Length && colLoop ; l++ )
				{
					if( this.ss[i][j][l].Pushed == false )
					{
						float min = -2.0f;
						float max = 2.0f;
						
						float nextTime = this.ss[i][j][l].PushTime - audioManager.AudioTime;

						if( nextTime < max ){
							if( min < nextTime ){
								//DrawCircle(this.ss[i][j][l],nextTime,i);
								DrwaMoveIcon(this.ss[i][j][l],nextTime,i);
							}
						}else{
							colLoop = false;
						}
					}
				}
			}
		}
	}

	void DrawCircle(ScoreSet scoreSet,float nextTime,int col)
	{
		float min = -2.5f;
		float max = 2.0f;

		if( nextTime < max ){
			if( min < nextTime ){
				if( scoreSet.score != 0 ){
					Icon icon = iconMgr.GetReceiveIcon(col);
					Rect circleRect = new Rect();
					circleRect.x = icon.pos2D().x;
					circleRect.y = icon.pos2D().y;
					circleRect.width = icon.size2D().x;
					circleRect.height = icon.size2D().y;
					
					float scaleCircle = 1.0f + nextTime * 2.0f;
					
					if( 0.0f < scaleCircle )
					{
						circleRect.width *= scaleCircle;
						circleRect.height *= scaleCircle;
						
						circleRect.x -= circleRect.width / 2.0f;
						circleRect.y -= circleRect.height / 2.0f;
						
						GUI.DrawTexture( circleRect ,circle);
					}
				}
			}
		}
	}

	void DrwaMoveIcon(ScoreSet scoreSet,float nextTime,int col)
	{
		float min = -2.5f;
		float max = 2.0f;
		
		if( nextTime < max ){
			if( min < nextTime ){
				if( scoreSet.score != 0 ){
					Icon icon = iconMgr.GetReceiveIcon(col);

					Icon sendIcon = iconMgr.GetSendIcon(col);
					Vector2 center = sendIcon.pos2D();

					Vector2 iconPos = icon.pos2D();
					Vector2 iconSize = icon.size2D();
					Vector2 pos = ( iconPos - center ) * (1.0f - nextTime / max) + center - iconSize/2.0f;

					GUI.DrawTexture( new Rect( pos.x,pos.y,iconSize.x,iconSize.y ) ,moveIconTexture);
				}
			}
		}
	}
}

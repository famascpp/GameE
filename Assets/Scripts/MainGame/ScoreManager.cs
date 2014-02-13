using UnityEngine;
using System.Collections;
using System.Collections.Generic;

struct ScoreSet {
	public int score;
	public bool Pushed;
	public float PushTime;
}

public class ScoreManager : MonoBehaviour {


	ScoreSet[][][] ss;
	int[] ssMin;		//現在の譜面小節場所記憶用.

	public Texture circle;

	GameManager gameMgr;

	IconManager iconMgr;

	AudioManager audioManager;

	// Use this for initialization
	void Start () {
		MusicScore canonLock = new MusicScore("music/test/test");

		audioManager = this.GetComponent<AudioManager>();
		gameMgr = this.GetComponent<GameManager>();
		iconMgr = this.GetComponent<IconManager>();

		List<List<List<int>>> score = canonLock.Score;

		this.ssMin = new int[score.Count];
		for( int i = 0 ; i < ssMin.Length ; i++ ) ssMin[i] = 0;

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

	}
	
	// Update is called once per frame
	void Update () {
		float min = 0.0f;
		float max = 2.0f;
		bool colLoop;

		for( int i = 0 ; i < this.ss.Length ; i++ )
		{
			colLoop = true;
			for( int j = ssMin[i] ; j < this.ss[i].Length && colLoop ; j++ )
			{
				for( int l = 0 ; l < this.ss[i][j].Length && colLoop ; l++ )
				{
					float nextTime = this.ss[i][j][l].PushTime - audioManager.AudioTime;
					
					if( nextTime < max ){
						if( min < nextTime ){
							if( this.ss[i][j][l].score != 0 ){
							}
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

	void OnGUI()
	{
		GUI.depth = -10;

		bool colLoop;	//nextTimeがn以上超えていたら次からはそれ以上の値しか出ないので打ち切り用変数.

		float min = 0.0f;
		float max = 2.0f;

		for( int i = 0 ; i < this.ss.Length ; i++ )
		{
			colLoop = true;
			for( int j = ssMin[i] ; j < this.ss[i].Length && colLoop ; j++ )
			{
				for( int l = 0 ; l < this.ss[i][j].Length && colLoop ; l++ )
				{
					float nextTime = this.ss[i][j][l].PushTime - audioManager.AudioTime;

					if( nextTime < max ){
						if( min < nextTime ){
							if( this.ss[i][j][l].score != 0 ){
								Icon icon = iconMgr.getIcon(i);
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
					}else{
						colLoop = false;
					}
				}
			}
		}


	}
}

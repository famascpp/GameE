using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HandCursor{

	//次のアイコン.
	IconEnum nextIcon;
	public IconEnum NextIcon{
		get { return nextIcon; }
	}
	int NextIconInt;

	//次のアイコンへ移動時、前回と同じアイコンだったか.
	bool sameIcon = false;

	//次のアイコンがあるかどうか.
	bool next = false;


	//次までの時間と減少値.
	float nextTimeMax;
	float nextTime;
	
	Vector3 nowVec;
	float nowAng;
	
	IconEnum[] pressButton;

	public IconScore[] iconScore;
	
	//初期化.
	public HandCursor(IconManager iconMgr,Icon cursor ,MusicScore mScore ,params IconEnum[] args)
	{
		this.nextIcon = IconEnum.Max;
		
		this.nextTimeMax = 1.0f;
		this.nextTime = 1.0f;
		
		this.nowVec = Vector3.zero;

		this.pressButton = new IconEnum[args.Length];
		for( int i = 0 ; i < this.pressButton.Length ; i++ )
		{
			this.pressButton[i] = args[i];
		}

		iconScore = new IconScore[pressButton.Length];
		SetScore(mScore);

		nextPush(iconMgr,cursor);
	}
	
	public void SetNowVec(IconManager iconMgr,Icon cursor)
	{
		nowVec = cursor.transform.position - iconMgr.getIconE(nextIcon).transform.position;
	}

	public void SetNowAng(IconManager iconMgr,Icon cursor)
	{
		float L = 0.1f;
		Vector3 A = Vector3.zero;
		Vector3 B = iconMgr.getIconE(nextIcon).transform.position;
		
		nowVec = (A-B).normalized * L + B;
		
		Vector3 Vec = B-nowVec;
		
		nowAng = Mathf.Atan2( Vec.y,Vec.x);
	}
	
	public void SetNextPos(IconManager iconMgr,Icon cursor)
	{
		cursor.transform.position = nowVec * ( nextTime / nextTimeMax ) + iconMgr.getIconE(nextIcon).transform.position;
	}
	
	public void SetSamePos(IconManager iconMgr,Icon cursor)
	{
		float L = 0.1f;
		float tAng = nowAng + Mathf.PI * 2.0f * ( nextTime / nextTimeMax );
		cursor.transform.position = nowVec + new Vector3( Mathf.Cos( tAng ) , Mathf.Sin( tAng ) , 0.0f ) * L ;
	}
	
	void SetNextTime( float time )
	{
		nextTimeMax = time;
		nextTime = time;
	}
	
	public void Update(IconManager iconMgr,Icon cursor,AudioManager audioMgr)
	{
		if( 0.0f <= nextTimeMax && next ){
			if( sameIcon ) SetSamePos(iconMgr,cursor);
			else SetNextPos(iconMgr,cursor);

			
			nextTime -= Time.deltaTime;

			//アイコンへたどり着いたら次のアイコンへ.
			if( nextTime <= 0.0f )
			{
				nextTime = 0.0f;
				//位置更新.
				if( sameIcon ) SetSamePos(iconMgr,cursor);
				else SetNextPos(iconMgr,cursor);

				//現在追っていたアイコンの譜面を更新.
				int nextInt = NextIconInt;
				iconScore[nextInt].nextPush();

				//次のアイコンへ.
				nextPush(iconMgr,cursor);
			}
		}
	}

	public void nextPush(IconManager iconMgr,Icon cursor)
	{
		if( 1 <= iconScore.Length)
		{
			next = false;
			float m =  0.0f;
			int nm = 0;
			bool first = true;	//一番最初に入れられるやつは比較なしで入れられるようにする.
			for( int i = 0 ; i < iconScore.Length ; i++ )
			{
				if( iconScore[i].Next ){
					float tm = iconScore[i].getNextTime();
					if( tm < m || first)
					{
						m = tm;
						nm = i;
						next = true;
						first = false;
					}
				}
			}

			NextIconInt = nm;
			if( nextIcon == this.pressButton[NextIconInt] ) sameIcon = true;
			else sameIcon = false;
			nextIcon = this.pressButton[NextIconInt];

			if( m == 0.0f )
			{
				Debug.Log(""+m);
			}

			SetNextTime(m);

			//座標設定.
			if( sameIcon ) SetNowAng(iconMgr,cursor);
			else SetNowVec(iconMgr,cursor);
		}
	}

	void SetScore( MusicScore mScore )
	{
		for( int i = 0 ; i < this.iconScore.Length ; i++ )
		{
			List<List<int>> tScore = mScore.getScoreCol( this.pressButton[i] );
			iconScore[i] = new IconScore();
			iconScore[i].setScore( tScore );
		}
	}
}


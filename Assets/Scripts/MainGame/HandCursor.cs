using UnityEngine;
using System.Collections;

public class HandCursor{

	//次のアイコン.
	IconEnum nextIcon;
	public IconEnum NextIcon{
		get { return nextIcon; }
	}

	//次のアイコンへ移動時、前回と同じアイコンだったか.
	bool sameIcon = false;


	//次までの時間と減少値.
	float nextTimeMax;
	float nextTime;
	
	Vector3 nowVec;
	float nowAng;
	
	int pressButtonMax;
	IconEnum[] pressButton;
	
	//初期化.
	public HandCursor(IconManager iconMgr,Icon cursor ,params IconEnum[] args)
	{
		this.nextIcon = IconEnum.Max;
		
		this.nextTimeMax = 1.0f;
		this.nextTime = 1.0f;
		
		this.nowVec = Vector3.zero;

		this.pressButtonMax = args.Length;
		this.pressButton = new IconEnum[this.pressButtonMax];
		for( int i = 0 ; i < this.pressButtonMax ; i++ )
		{
			this.pressButton[i] = args[i];
		}

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
			IconScore iconScore = iconMgr.getIconE(nextIcon).gameObject.GetComponent<IconScore>();
			if ( iconScore != null )
			{
				iconScore.nextPush();
			}

			//次のアイコンへ.
			nextPush(iconMgr,cursor);
		}
	}

	void nextPush(IconManager iconMgr,Icon cursor)
	{
		if( 1 <= this.pressButton.Length)
		{

			float m =  iconMgr.getIconE(this.pressButton[0]).GetComponent<IconScore>().getNextTime();
			int nm = 0;
			for( int i = 1 ; i < this.pressButton.Length ; i++ )
			{
				float tm = iconMgr.getIconE(this.pressButton[i]).GetComponent<IconScore>().getNextTime();
				if( tm < m )
				{
					m = tm;
					nm = i;
				}
			}

			IconEnum tNextIcon = this.pressButton[nm];
			if( nextIcon == tNextIcon ) sameIcon = true;
			else sameIcon = false;
			nextIcon = tNextIcon;
			
			SetNextTime(m);

			//座標設定.
			if( sameIcon ) SetNowAng(iconMgr,cursor);
			else SetNowVec(iconMgr,cursor);
		}
	}
}


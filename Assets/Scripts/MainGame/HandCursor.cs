using UnityEngine;
using System.Collections;

public class HandCursor{

	IconEnum nextIcon;

	float nextTimeMax;
	float nextTime;
	
	Vector3 nowVec;
	
	int pressButtonMax;
	IconEnum[] pressButton;
	
	//初期化.
	public HandCursor(IconManager iconMgr,Icon cursor ,params IconEnum[] args)
	{
		this.nextIcon = IconEnum.hand;
		
		this.nextTimeMax = 1.0f;
		this.nextTime = 1.0f;
		
		this.nowVec = Vector3.zero;

		this.pressButtonMax = args.Length;
		this.pressButton = new IconEnum[this.pressButtonMax];
		for( int i = 0 ; i < this.pressButtonMax ; i++ )
		{
			this.pressButton[i] = args[i];
		}
		
		SetNowVec(iconMgr,cursor);
	}
	
	public void SetNowVec(IconManager iconMgr,Icon cursor)
	{
		nowVec = cursor.transform.position - iconMgr.getIconE(nextIcon).transform.position;
	}
	
	public void SetNextPos(IconManager iconMgr,Icon cursor)
	{
		cursor.transform.position = nowVec * ( nextTime / nextTimeMax ) + iconMgr.getIconE(nextIcon).transform.position;
	}
	
	void SetNextTime( float time )
	{
		nextTimeMax = time;
		nextTime = time;
	}
	
	public void Update(IconManager iconMgr,Icon cursor)
	{
		SetNextPos(iconMgr,cursor);
		
		nextTime -= Time.deltaTime;
		
		if( nextTime <= 0.0f )
		{
			//アイコンピッタリの位置にする.
			nextTime = 0.0f;
			SetNextPos(iconMgr,cursor);
			
			//次のアイコンへ.
			nextIcon = this.pressButton[Random.Range(0,this.pressButtonMax)];
			
			SetNextTime(Random.Range(0.1f,0.5f));
			SetNowVec(iconMgr,cursor);
		}
	}
}


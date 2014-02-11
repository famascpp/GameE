using UnityEngine;
using System.Collections;

struct HandCursor{
	Icon cursor;

	public IconEnum nextIcon;
	public IconManager IconMgr;

	public float nextTimeMax;
	public float nextTime;
	
	public Vector3 nowVec;

	public int pressButtonMax;
	public IconEnum[] pressButton;

	public void Init(IconManager iconMgr,Icon cursor ,params IconEnum[] args)
	{
		this.IconMgr = iconMgr;
		this.cursor = cursor;

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

		SetNowVec();
	}

	public void SetNowVec()
	{
		nowVec = cursor.transform.position - IconMgr.getIconE(nextIcon).transform.position;
	}

	public void SetNextPos()
	{
		cursor.transform.position = nowVec * ( nextTime / nextTimeMax ) + IconMgr.getIconE(nextIcon).transform.position;
	}

	void SetNextTime( float time )
	{
		nextTimeMax = time;
		nextTime = time;
	}

	public void Update()
	{
		SetNextPos();

		nextTime -= Time.deltaTime;

		if( nextTime <= 0.0f )
		{
			//アイコンピッタリの位置にする.
			nextTime = 0.0f;
			SetNextPos();

			//次のアイコンへ.
			nextIcon = this.pressButton[Random.Range(0,this.pressButtonMax)];

			SetNextTime(Random.Range(0.1f,0.5f));
			SetNowVec();
		}
	}
}

public class GameManager : MonoBehaviour {

	public IconManager IconMgr;

	HandCursor lHand;
	HandCursor rHand;


	// Use this for initialization
	void Start () {
		lHand.Init(IconMgr, IconMgr.getCursor(0) , IconEnum.hand , IconEnum.rShoulder , IconEnum.lHip,IconEnum.lKnee );
		rHand.Init(IconMgr, IconMgr.getCursor(1) , IconEnum.hand , IconEnum.lShoulder , IconEnum.rHip,IconEnum.rKnee );

	}
	
	// Update is called once per frame
	void Update () {
		lHand.Update();
		rHand.Update();
	}

	void OnGUI()
	{
	}



}

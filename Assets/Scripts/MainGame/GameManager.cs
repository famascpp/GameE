using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public IconManager iconMgr;

	public AudioManager audioMgr;

	HandCursor lHand;
	HandCursor rHand;

	// Use this for initialization
	void Start () {

		MusicScore canonLock = new MusicScore("music/test/test");

		//両手初期化.
		lHand = new HandCursor(iconMgr, iconMgr.getCursor(0) ,canonLock, IconEnum.hand , IconEnum.rShoulder , IconEnum.lHip,IconEnum.lKnee );
		rHand = new HandCursor(iconMgr, iconMgr.getCursor(1) ,canonLock, IconEnum.hand , IconEnum.lShoulder , IconEnum.rHip,IconEnum.rKnee );

	}

	void Awake()
	{
	}
	
	// Update is called once per frame
	void Update () {
		lHand.Update(iconMgr, iconMgr.getCursor(0),audioMgr);
		rHand.Update(iconMgr, iconMgr.getCursor(1),audioMgr);
	}

	void OnGUI()
	{
		GUI.Label( new Rect(0,0,100,100),"lhand:"+lHand.NextIcon+"\nrhand:"+rHand.NextIcon);



	}



}


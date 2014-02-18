using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	IconManager iconMgr;

	AudioManager audioMgr;

	HandCursor lHand;
	HandCursor rHand;

	public GameObject uniduino;
	bool isUniduino;

	void Awake()
	{
		iconMgr = this.GetComponent<IconManager>();
		audioMgr = this.GetComponent<AudioManager>();

	}

	// Use this for initialization
	void Start () {

		MusicScore canonLock = new MusicScore("music/test/test");

		//両手初期化.
		lHand = new HandCursor(iconMgr, iconMgr.getCursor(0) ,canonLock, IconEnum.hand , IconEnum.rShoulder , IconEnum.lHip,IconEnum.lKnee );
		rHand = new HandCursor(iconMgr, iconMgr.getCursor(1) ,canonLock, IconEnum.hand , IconEnum.lShoulder , IconEnum.rHip,IconEnum.rKnee );


	}

	// Update is called once per frame
	void Update () {
		lHand.Update(iconMgr, iconMgr.getCursor(0),audioMgr);
		rHand.Update(iconMgr, iconMgr.getCursor(1),audioMgr);
	}

	void OnGUI()
	{
	}



}


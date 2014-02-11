using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public IconManager iconMgr;

	HandCursor lHand;
	HandCursor rHand;

	// Use this for initialization
	void Start () {

		//両手初期化.
		lHand = new HandCursor(iconMgr, iconMgr.getCursor(0) , IconEnum.hand , IconEnum.rShoulder , IconEnum.lHip,IconEnum.lKnee );
		rHand = new HandCursor(iconMgr, iconMgr.getCursor(1) , IconEnum.hand , IconEnum.lShoulder , IconEnum.rHip,IconEnum.rKnee );

	}

	void Awake()
	{
	}
	
	// Update is called once per frame
	void Update () {
		lHand.Update(iconMgr, iconMgr.getCursor(0));
		rHand.Update(iconMgr, iconMgr.getCursor(1));
	}

	void OnGUI()
	{
	}



}


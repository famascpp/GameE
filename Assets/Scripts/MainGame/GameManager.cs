using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public IconManager IconMgr;

	IconEnum nextIcon = IconEnum.hand;

	float nextTime = 10.0f;

	Vector3 nowVec = Vector3.zero;

	// Use this for initialization
	void Start () {
		
		nowVec = IconMgr.getCursor.transform.position - IconMgr.getIconE(nextIcon).transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		IconMgr.getCursor.transform.position = nowVec + IconMgr.getIconE(nextIcon).transform.position;

	}

	void OnGUI()
	{
	}



}

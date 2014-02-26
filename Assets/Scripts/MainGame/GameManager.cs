using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	IconManager iconMgr;

	AudioManager audioMgr;

	ScoreManager scoreMgr;

	void Awake()
	{
		iconMgr = this.GetComponent<IconManager>();
		audioMgr = this.GetComponent<AudioManager>();
		scoreMgr = this.GetComponent<ScoreManager>();
	}

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if( this.scoreMgr.isEnd )
		{
			Application.LoadLevel("PointsScene");
		}

	}

	void FixedUpdate()
	{
		Time.timeScale = 30;
	}

	void OnGUI()
	{
		GUIStyle style;
		style = new GUIStyle();
		
		string str = "" + Points.GetPoints();
		

		GUI.Label( new Rect(0,0,300,300) , str ,style );
	}



}


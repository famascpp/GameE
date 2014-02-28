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
		Points.Init();
	}

	// Update is called once per frame
	void Update () {
		if( this.scoreMgr.isEnd || Input.GetKey(KeyCode.Escape) )
		{
			Application.LoadLevel("PointsScene");
		}

	}

	void FixedUpdate()
	{
		//Time.timeScale = 30;
	}

	void OnGUI()
	{
		GUI.depth = -100;
		GUIStyle style;
		style = new GUIStyle();
		style.fontSize = (int)(Screen.height / 10.0f);
		
		string str = "";
		str += Points.GetPoints().ToString("00000") + " point\n";

		str += Points.GetCombo().ToString("   0") + " combo!\n";


		GUI.Label( new Rect(0,0,Screen.width,Screen.height) , str ,style );
	}



}


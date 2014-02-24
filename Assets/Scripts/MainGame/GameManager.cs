using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	IconManager iconMgr;

	AudioManager audioMgr;

	ScoreManager scoreMgr;

	public GameObject uniduino;
	bool isUniduino;

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
			//Application.LoadLevel("Scenes/titleScene");
		}

	}

	void FixedUpdate()
	{
		//Time.timeScale = 10;
	}

	void OnGUI()
	{
	}



}


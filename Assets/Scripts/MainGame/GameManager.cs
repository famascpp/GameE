using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	IconManager iconMgr;

	AudioManager audioMgr;

	ScoreManager scoreMgr;

	Texture[] numTex;
	Texture scoreTex;

	void Awake()
	{
		iconMgr = this.GetComponent<IconManager>();
		audioMgr = this.GetComponent<AudioManager>();
		scoreMgr = this.GetComponent<ScoreManager>();


		numTex = new Texture[10];
		for( int i = 0 ; i < 10; i++)
		{
			numTex[i] = Resources.Load<Texture>("Textures/Score/"+i);
		}
		scoreTex = Resources.Load<Texture>("Textures/Score/score");
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

		MyGUI.DrawTexture(new Rect(-0.7f,-0.43f,0.5f,0.5f),scoreTex);

		int point = (int)Points.GetPoints();
		for( int i = 0 ; i < 5 ; i++ )
		{
			MyGUI.DrawTexture(new Rect(-0.4f*((float)i/4.0f),-0.43f,0.3f,0.3f),numTex[point%10]);
			point /= 10;
		}

		int combo = (int)Points.GetCombo();
		for( int i = 0 ; i < 3 ; i++ )
		{
			MyGUI.DrawTexture(new Rect(-0.5f - ((float)i/10.0f),0.1f,0.3f,0.3f),numTex[combo%10]);
			combo /= 10;
		}
		

	}



}


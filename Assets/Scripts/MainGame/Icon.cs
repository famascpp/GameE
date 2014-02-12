using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Icon : MonoBehaviour {

	public Texture icon;

	public float scale = 0.1f;

	public int depthLayer = 0;

	//譜面.
	int[][] score;

	//オーディオ.
	public AudioManager audioMgr;

	//次叩くまでの時間.
	float nextTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if( score != null )
		{
			Debug.Log(score);
		}
	}

	void OnGUI()
	{
		GUI.depth = depthLayer;
		if( icon )
		{
			Vector2 size = new Vector2( Screen.height * scale , Screen.height * scale);

			Vector2 pos = new Vector2(this.transform.position.x,this.transform.position.y);
			pos.y = 1-pos.y;	//yの上下反転.

			Rect texRect = new Rect(
				Screen.width / 2.0f + Screen.height * pos.x - size.x / 2.0f, 
				-Screen.height  / 2.0f + Screen.height * pos.y - size.y / 2.0f,
				size.x,
				size.y
			);

			GUI.DrawTexture(texRect,icon);
		}
	}

	public void setScore(List<List<int>> score)
	{
		this.score = new int[score.Count][];
		for( int i = 0 ; i < score.Count ; i++ )
		{
			this.score[i] = new int[score[i].Count];
			for( int j = 0 ; j < score[i].Count ; j++ )
			{
				int a = score[i][j];
				this.score[i][j] = a;
			}
		}
	}

	
}

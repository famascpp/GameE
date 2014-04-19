using UnityEngine;
using System.Collections;

public class PointsMain : MonoBehaviour {

	float startTime = 0.0f;

	Texture bgt;

	Texture[] numTex;
	Texture scoreTex;

	void Awake(){
		numTex = new Texture[10];
		for( int i = 0 ; i < 10; i++)
		{
			numTex[i] = Resources.Load<Texture>("Textures/Score/"+i);
		}
		scoreTex = Resources.Load<Texture>("Textures/Score/score");
	}

	// Use this for initialization
	void Start () {
		startTime = Time.time;

		bgt = Resources.Load<Texture>("Textures/end");
	}
	
	// Update is called once per frame
	void Update () {
		if( Time.time - startTime > 10.0f || Input.GetKey(KeyCode.Escape) )
		{
//			GameObject uniduino = GameObject.Find("Uniduino");
//			if( uniduino != null )
//			{
//				Destroy(uniduino);
//			}
			Application.LoadLevel("titleScene");
		}
	}

	void OnGUI()
	{
		MyGUI.DrawTextureAspect(new Rect(0,0,1,1),bgt,960.0f/640.0f);

		GUI.depth = -100;
		MyGUI.DrawTexture(new Rect(-0.5f,-0.43f,0.5f,0.5f),scoreTex);
		
		int point = (int)Points.GetPoints();
		for( int i = 0 ; i < 5 ; i++ )
		{
			MyGUI.DrawTexture(new Rect(-0.1f - ((float)i/10.0f),-0.3f,0.3f,0.3f),numTex[point%10]);
			point /= 10;
		}
		
		int combo = (int)Points.GetMaximumCombo();
		for( int i = 0 ; i < 3 ; i++ )
		{
			MyGUI.DrawTexture(new Rect(-0.2f - ((float)i/10.0f),-0.05f,0.3f,0.3f),numTex[combo%10]);
			combo /= 10;
		}


	}
}

using UnityEngine;
using System.Collections;

public class CopyModel : MonoBehaviour {

	uint[] inputButton;

	Vector2[] partPos;

	Texture[] model = null;
	int modelMax = 0;

	bool isUniduino = false;

	Vector2 texSize = Vector2.zero;

	AudioManager audioMgr;


	// Use this for initialization
	void Start () {
		this.transform.position = new Vector3(-0.5f,0f,0f);

		GameObject tempUniduino = GameObject.Find("Uniduino");
		if( tempUniduino != null ) isUniduino = true;

		modelMax = 3;
		model = new Texture[modelMax];
		model[0] = Resources.Load<Texture>("Textures/anime/anime01");
		model[1] = Resources.Load<Texture>("Textures/anime/anime02");
		model[2] = Resources.Load<Texture>("Textures/anime/anime03");

		texSize = new Vector2(512,1024);

		inputButton = new uint[(int)IconEnum.Max];
		for( int i = 0 ; i < inputButton.Length ; i++ ) inputButton[i] = 0;

		int ii = 0;
		partPos = new Vector2[(int)IconEnum.Max];
		partPos[ii++] = new Vector2(256,96);
		partPos[ii++] = new Vector2(221,423);
		partPos[ii++] = new Vector2(289,419);
		partPos[ii++] = new Vector2(190,625);
		partPos[ii++] = new Vector2(327,617);
		partPos[ii++] = new Vector2(216,736);
		partPos[ii++] = new Vector2(335,730);
		for( int i = 0 ; i < inputButton.Length ; i++ ){
			partPos[i] -= new Vector2(texSize.x/2.0f,texSize.y/2.0f);
			partPos[i].x /= texSize.y;
			partPos[i].y /= texSize.y;
		}

		audioMgr = this.GetComponent<AudioManager>();

	}
	
	// Update is called once per frame
	void Update () {
		InputUpdate();
		Vector2 pos = new Vector2(this.transform.position.x,this.transform.position.y);

		for( int i = 0 ; i < (int)IconEnum.Max ; i++ )
		{
			if( inputButton[i] == 2 )
			{
				GameObject go = new GameObject();
				go.name = "spanking effect";

				Vector3 vec2 = pos + partPos[i];

				go.transform.position = new Vector3( vec2.x , vec2.y ,0.0f);
				go.AddComponent<SpankingEffect>();

				Instantiate( Resources.Load("Prefabs/SE/Push") );
			}
		}
	}

	void InputUpdate()
	{
		//key input
		for( int i = 0 ; i < (int)IconEnum.Max ; i++ )
		{
			if( ( isUniduino && InputA.GetButton((IconEnum)i) ) ||
			   Input.GetKey( (KeyCode)((int)KeyCode.Alpha1 + i) )
			   ) inputButton[i]++;
			else inputButton[i] = 0;
		}
	}

	void OnGUI()
	{
		GUI.depth = -0;

		Vector2 pos = new Vector2(this.transform.position.x,this.transform.position.y);

		int nowModel = Mathf.Abs( audioMgr.getNowBeat() + audioMgr.getNowMeasure() * (int)audioMgr.getBeat() );



		MyGUI.DrawTextureAspect( new Rect(0+pos.x,0+pos.y,1.0f,1.0f) , this.model[nowModel%2 + 1] , texSize.x / texSize.y );
	}

}

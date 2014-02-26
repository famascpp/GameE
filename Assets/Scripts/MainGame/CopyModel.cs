using UnityEngine;
using System.Collections;

public class CopyModel : MonoBehaviour {

	uint[] inputButton;

	Vector2[] partPos;

	Texture model = null;

	bool isUniduino = false;


	// Use this for initialization
	void Start () {
		this.transform.position = new Vector3(-0.5f,0f,0f);

		GameObject tempUniduino = GameObject.Find("Uniduino");
		if( tempUniduino != null ) isUniduino = true;

		model = Resources.Load<Texture>("Textures/otehon");

		inputButton = new uint[(int)IconEnum.Max];
		for( int i = 0 ; i < inputButton.Length ; i++ ) inputButton[i] = 0;

		int ii = 0;
		partPos = new Vector2[(int)IconEnum.Max];
		partPos[ii++] = new Vector2(517,120);
		partPos[ii++] = new Vector2(552,434);
		partPos[ii++] = new Vector2(485,434);
		partPos[ii++] = new Vector2(593,624);
		partPos[ii++] = new Vector2(450,632);
		partPos[ii++] = new Vector2(599,735);
		partPos[ii++] = new Vector2(478,750);
		for( int i = 0 ; i < inputButton.Length ; i++ ){
			partPos[i] -= new Vector2(1280.0f/2.0f,1024.0f/2.0f);
			partPos[i].x /= model.height;
			partPos[i].y /= model.height;
		}

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
		GUI.depth = -50;

		Vector2 pos = new Vector2(this.transform.position.x,this.transform.position.y);

		MyGUI.DrawTextureAspect( new Rect(0+pos.x,0+pos.y,1.0f,1.0f) , this.model , 1280.0f / 1024.0f );
	}

}

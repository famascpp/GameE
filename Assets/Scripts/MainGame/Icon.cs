using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Icon : MonoBehaviour {

	public Texture icon;

	public float scale = 0.1f;

	public int depthLayer = 0;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
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


}

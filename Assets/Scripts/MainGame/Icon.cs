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

	public Vector2 pos2D()
	{
		Vector2 pos = new Vector2(this.transform.position.x,this.transform.position.y);
		pos.y = 1-pos.y;	//yの上下反転.

		return new Vector2(Screen.width / 2.0f + Screen.height * pos.x,-Screen.height  / 2.0f + Screen.height * pos.y );
	}

	void OnGUI()
	{
		GUI.depth = depthLayer;
		if( icon )
		{
			Vector2 size = new Vector2( Screen.height * scale , Screen.height * scale);

			Vector2 pos = pos2D();

			Rect texRect = new Rect(
				pos.x - size.x / 2.0f, 
				pos.y - size.y / 2.0f,
				size.x,
				size.y
			);

			GUI.DrawTexture(texRect,icon);
		}
	}


}

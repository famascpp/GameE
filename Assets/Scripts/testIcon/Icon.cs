using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Icon : MonoBehaviour {

	public Texture icon;

	public float scale = 0.2f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI()
	{
		if( icon )
		{
			Vector2 size = new Vector2( Screen.height * scale , Screen.height * scale);

			Vector2 pos = new Vector2(this.transform.position.x,this.transform.position.y);
			pos.y = 1-pos.y;	//yの上下反転.

			//0.5を中心に.
			pos.x += 0.5f;
			pos.y -= 0.5f;




			Rect texRect = new Rect(
				Screen.width * pos.x - size.x / 2.0f, 
				Screen.height * pos.y - size.y / 2.0f,
				size.x,
				size.y
			);

			GUI.DrawTexture(texRect,icon);
		}
	}
}

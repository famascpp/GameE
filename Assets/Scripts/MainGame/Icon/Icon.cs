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

	public Vector2 pos()
	{
		return this.transform.position;
	}
	
	public float size()
	{
		return scale;
	}


	void OnGUI()
	{
		GUI.depth = depthLayer;
		if( icon )
		{
			MyGUI.DrawTexture(
				new Rect( 
			         this.transform.position.x ,
			         this.transform.position.y ,
			         this.scale , this.scale
			         ) , 
				this.icon
				);
		}
	}


}

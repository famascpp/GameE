using UnityEngine;
using System.Collections;

public class SpankingEffect : MonoBehaviour {

	float startTime = 0.0f;
	float endTime = 0.2f;

	Texture tex;

	float scale = 0.0f;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
		tex = Resources.Load<Texture>("Textures/effect");
	}

	// Update is called once per frame
	void Update () {

		scale = ( totalTime() / endTime ) * 0.1f + 0.1f;

		if( totalTime() > endTime )
		{
			Destroy(this.gameObject);
		}
	}

	float totalTime()
	{
		return Time.time - startTime;
	}
	

	void OnGUI()
	{
		GUI.depth = -60;
		MyGUI.DrawTexture(new Rect(this.transform.position.x,this.transform.position.y,scale,scale),tex);
	}
}

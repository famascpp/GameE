using UnityEngine;
using System.Collections;

public class PointsMain : MonoBehaviour {

	float startTime = 0.0f;

	Texture bgt;

	// Use this for initialization
	void Start () {
		startTime = Time.time;

		bgt = Resources.Load<Texture>("Textures/end");
	}
	
	// Update is called once per frame
	void Update () {
		if( Time.time - startTime > 10.0f || Input.GetKey(KeyCode.Escape) )
		{
			Application.LoadLevel("titleScene");
		}
	}

	void OnGUI()
	{
		MyGUI.DrawTextureAspect(new Rect(0,0,1,1),bgt,960.0f/640.0f);

		GUI.depth = -100;
		GUIStyle style;
		style = new GUIStyle();
		style.fontSize = (int)(Screen.height / 10.0f);
		
		string str = "";
		str += Points.GetPoints().ToString("0") + " point\n";
		
		str += "" + Points.GetMaximumCombo().ToString("0") + "combo maximum!!\n";
		
		
		GUI.Label( new Rect( Screen.width / 2.0f - 960.0f/640.0f * Screen.height / 2.0f ,0 ,Screen.width,Screen.height) , str ,style );

	}
}

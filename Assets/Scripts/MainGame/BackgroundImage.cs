using UnityEngine;
using System.Collections;

public class BackgroundImage : MonoBehaviour {

	public Texture backgroundImage;

	void OnGUI () 
	{
		if(Event.current.type == EventType.Repaint){
			Graphics.DrawTexture(
				new Rect(0, 0,
			         Screen.width, Screen.height),
			         backgroundImage);
		}
	}
}

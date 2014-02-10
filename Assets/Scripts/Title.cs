using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {

	public Texture logo;

		float x, w, y, h;
	

	void Start () {

		x = Screen.width/2-logo.width/2;
		w = logo.width;
		y = Screen.height/2-logo.height/2-100;
		h = logo.height;
	}

	// タイトルロゴを動かす.
	private IEnumerator MoveLogo () {
		yield return 0;
	}

	void OnGUI () {
		// タイトルロゴ貼り付け.
		if(Event.current.type == EventType.Repaint){
			Graphics.DrawTexture(
				new Rect(x, y,
			         w, h),
				logo);
		}
	}
}

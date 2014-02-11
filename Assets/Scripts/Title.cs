using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {
	public Texture backgroundTexture; // 背景画像（未定）.
	public Texture startTexture; // スタートロゴ画像（未定）.
	private Arduino arduino;
	private GameObject camera;
	private bool drawUserGuideFlag = false; // true:操作説明画像表示.

	// ロゴの構造体.
	struct Logo{
		public static Texture texture;
		public static float x, w, y, h;
		static Logo(){
			Logo.texture = Resources.Load<Texture>("Textures/title");
			Logo.x = Screen.width/2-Logo.texture.width/2;
			Logo.w = Logo.texture.width;
			Logo.y = Screen.height/2-Logo.texture.height/2-100;
			Logo.h = Logo.texture.height;
		}
	}
	void Start () {
		camera = GameObject.FindWithTag("MainCamera");
		arduino = camera.GetComponent<Arduino>();
	}

	void Update () {
		UserGuide(); // 操作説明に遷移.
	}

	private void UserGuide () {
		if( InputA.GetArduinoState(arduino.GetPin(), InputA.HAND) ){ // 拍手したら.
			/*操作説明表示.*/
			drawUserGuideFlag = true;
		}
	}

	void DrawBackground () {
		// 背景画像（未定）貼り付け.
		if(Event.current.type == EventType.Repaint){
			Graphics.DrawTexture(
				new Rect(0, 0,
			         Screen.width, Screen.height),
				backgroundTexture);
		}
	}

	void DrawTitleLogo () {
		// タイトルロゴ貼り付け.
		if(Event.current.type == EventType.Repaint){
			Graphics.DrawTexture(
				new Rect(Logo.x, Logo.y,
			         Logo.w, Logo.h),
				Logo.texture);
		}
	}

	void DrawStartLogo () {
		// スタートロゴ（未定）貼り付け.
		if(Event.current.type == EventType.Repaint){
			Graphics.DrawTexture(
				new Rect(Screen.width/2 - startTexture.width, Screen.height/1.5f,
			         startTexture.width*2, startTexture.height*2),
				startTexture);
		}
	}
	void DrawUserGuide () {
		if(drawUserGuideFlag){
			if(Event.current.type == EventType.Repaint){
				Graphics.DrawTexture(
					new Rect(0, 0,
				         Screen.width, Screen.height),
					startTexture);
			}
		}
	}
	void OnGUI () {
		DrawBackground (); // 背景画像（未定）貼り付け.
		DrawTitleLogo ();  // タイトルロゴ貼り付け.
		DrawStartLogo ();  // 背景画像（未定）貼り付け.
		DrawUserGuide ();  // 操作説明（未定）貼り付け.
	}
	
}

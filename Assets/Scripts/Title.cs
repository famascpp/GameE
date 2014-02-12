﻿using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {
	public Texture backgroundTexture; // 背景画像.
	public Texture userGuideTexture1; // 注意画像.
	public Texture startTexture; // スタートロゴ画像（未定）.
	private bool drawUserGuideFlag = false; // true:操作説明画像表示.

	// ロゴの構造体.
	struct Logo{
		public static Texture texture;
		public static float x, w, y, h;
		public static float vy;
		public static float POSITION_Y;
		static Logo(){
			Logo.texture = Resources.Load<Texture>("Textures/title");
			Logo.x = Screen.width/2-Logo.texture.width/2;
			Logo.w = Logo.texture.width;
			Logo.POSITION_Y = Screen.height/2-Logo.texture.height/2-100;
			Logo.y = Logo.POSITION_Y;
			Logo.h = Logo.texture.height;
			Logo.vy = 1.0f;
		}
	}
	void Start () {

	}

	void Update () {
		UserGuide(); // 操作説明に遷移.

		if(Logo.POSITION_Y+10.0f <= Logo.y) Logo.vy = -1.0f;
		if(Logo.POSITION_Y-10.0f >= Logo.y) Logo.vy = +1.0f;
		Logo.y += Logo.vy;
	}

	private void UserGuide () {

		//if( InputA.GetArduinoState(arduino.GetPin(), InputA.HAND) ){ // 拍手したら.
		if( InputA.GetHand() ){ // 拍手したら.
			/*操作説明表示.*/
			drawUserGuideFlag = true;
		}
	}

	void DrawBackground () {
		// 背景画像貼り付け.
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
			GUIUtility.RotateAroundPivot(13.5f, new Vector2(Screen.width/2 - startTexture.width/2.8f, Screen.height/2.0f)); // 回転.
			Graphics.DrawTexture(
				new Rect(Screen.width/2 - startTexture.width/2.8f, Screen.height/2.5f,
			         startTexture.width-300, startTexture.height-600),
				startTexture);
			GUIUtility.RotateAroundPivot(-13.5f, new Vector2(Screen.width/2 - startTexture.width/2.8f, Screen.height/2.0f)); // 回転終了.
		}
	}
	void DrawUserGuide () {
		if(drawUserGuideFlag){
			if(Event.current.type == EventType.Repaint){
				Graphics.DrawTexture(
					new Rect(0, 0,
				         Screen.width, Screen.height),
					userGuideTexture1);
			}
		}
	}
	void OnGUI () {
		DrawBackground (); // 背景画像貼り付け.
		DrawStartLogo ();  // 背景画像貼り付け.
		DrawTitleLogo ();  // タイトルロゴ貼り付け.

		DrawUserGuide ();  // 操作説明貼り付け.
	}
	
}

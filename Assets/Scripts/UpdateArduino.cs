using UnityEngine;
using System.Collections;

public class UpdateArduino : MonoBehaviour {

	public bool shoulder, hip, knee;
	void Awake () {
		shoulder = false;
		hip = false;
		knee = false;
	}

	// Arduinoからの入力を得るためにはこの関数を使用.
	void Update () {
		if( !ReadArduino.GetNowLoading() && !Title.GetDrawWarningFlag() ){
			// 拍手判定.
			if( InputA.GetButton((IconEnum)0) || Input.GetKey(KeyCode.Alpha0) ) {
				Title.SetDrawUserGuideFlag( true );
				TitleMusic.SetClapFlag ( true );
				Debug.Log("UpadateArduino::GetHand");
			}

			// ↓かた、こし、ひざのタッチ判定.
			if( Title.GetDrawUserGuideFlag() ) {
				if( InputA.GetButton((IconEnum)1) || InputA.GetButton((IconEnum)2) || Input.GetKey(KeyCode.Alpha1) ) {
					Debug.Log("かたタッチ");
					shoulder = true;
					Title.SetShoulderAlpha(.6f); // 半透明にする.
				}
				if( InputA.GetButton((IconEnum)4) || InputA.GetButton((IconEnum)3) || Input.GetKey(KeyCode.Alpha2) ) {
					Debug.Log("こしタッチ");
					hip = true;
					Title.SetHipAlpha(.6f); // 半透明にする.
				}
				if( InputA.GetButton((IconEnum)5) || InputA.GetButton((IconEnum)6) || Input.GetKey(KeyCode.Alpha3) ) {
					Debug.Log("ひざタッチ");
					knee = true;
					Title.SetKneeAlpha(.6f); // 半透明にする.
				}
				if( shoulder && hip && knee && Title.GetStartFlag() ){

					if( InputA.GetButton((IconEnum)0) || Input.GetKey(KeyCode.Alpha0) ) {
						// ↓かた、こし、ひざ、を叩いたらゲームスタート.
						StartCoroutine( Title.StartGame() );
					}
				}
			}
		}
	}
}

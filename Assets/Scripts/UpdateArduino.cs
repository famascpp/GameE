using UnityEngine;
using System.Collections;

public class UpdateArduino : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	// Arduinoからの入力を得るためにはこの関数を使用.
	void Update () {
		if( !ReadArduino.GetNowLoading() && !Title.GetDrawWarningFlag() ){
			// 拍手判定.
			if( InputA.GetButton((IconEnum)0) || Input.GetKey(KeyCode.Alpha0) ) {
				Title.SetDrawUserGuideFlag( true );
				TitleMusic.ClapFlag ();
				Debug.Log("UpadateArduino::GetHand");
			}

			// ↓かた、こし、ひざのタッチ判定.
		}
	}
}

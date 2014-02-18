using UnityEngine;
using System.Collections;

public class UpdateArduino : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	// Arduinoからの入力を得るためにはこの関数を使用.
	void Update () {
		if( InputA.GetButton((IconEnum)0) ){
			Title.UserGuideFlag();
			TitleMusic.ClapFlag();
			//Debug.Log("UpadateArduino::GetHand");
		}
	}
}

using UnityEngine;
using System.Collections;

public class UpdateArduino : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Arduinoからの入力を得るためにはこの関数を使用.
	void Update () {
		if( InputA.GetHand() ){
			Title.UserGuideFlag();
			TitleMusic.ClapFlag();
		}
		if( InputA.GetKneeR() ){

		}
	}
}

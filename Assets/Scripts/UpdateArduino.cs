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
			Debug.Log("UpadateArduino::GetHand");
		}
		if( InputA.GetKneeL() ){
			Debug.Log("UpadateArduino::GetKneeL");
		}
		if( InputA.GetKneeR() ){
			Debug.Log("UpadateArduino::GetKneeR");
		}
		if( InputA.GetShoulderL() ){
			Debug.Log("UpadateArduino::GetKneeL");
		}
		if( InputA.GetShoulderR() ){
			Debug.Log("UpadateArduino::GetKneeR");
		}
		if( InputA.GetWaistL() ){
			Debug.Log("UpadateArduino::GetWaistL");
		}
		if( InputA.GetWaistR() ){
			Debug.Log("UpadateArduino::GetWaistR");
		}
	}
}

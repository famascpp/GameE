using UnityEngine;
using System.Collections;

public static class InputA {
	public const int HAND = 7;
	public const int SHOULDER_L = 2;
	public const int SHOULDER_R = 19;
	public const int WAIST_L = 3;
	public const int WAIST_R = 18;
	public const int KNEE_L = 4;
	public const int KNEE_R = 17;

//	public static bool GetArduinoState(int pinNum, int BODY){
//		if(pinNum == BODY){
//			return true;
//		}
//		return false;
//	}

	
	// 拍手情報.
	public static bool GetHand () {
		if(Arduino.GetPin() == HAND){
			return true;
		}
		return false;
	}
	
	// 肩タッチ情報.
	public static bool GetShoulderL () {
		if(Arduino.GetPin() == SHOULDER_L) return true;
		return false;
	}
	public static bool GetShoulderR () {
		if(Arduino.GetPin() == SHOULDER_R) return true;
		return false;
	}
	
	// 腰のタッチ情報.
	public static bool GetWaistL () {
		if(Arduino.GetPin() == WAIST_L) return true;
		return false;
	}
	public static bool GetWaistR () {
		if(Arduino.GetPin() == WAIST_R) return true;
		return false;
	}
	
	// 膝のタッチ情報.
	public static bool GetKneeL () {
		if(Arduino.GetPin() == KNEE_L) return true;
		return false;
	}
	public static bool GetKneeR () {
		if(Arduino.GetPin() == KNEE_R) return true;
		return false;
	}
 }

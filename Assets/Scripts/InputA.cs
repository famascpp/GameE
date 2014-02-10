using UnityEngine;
using System.Collections;

public static class InputA {
	const int HAND = 1;

	// 拍手情報.
	public static bool GetHand () {
		if(1 == HAND) return true;
		return false;
	}
	
	// 肩タッチ情報.
	public static bool GetShoulderL () {
		return true;
	}
	public static bool GetShoulderR () {
		return true;
	}
	
	// 腰のタッチ情報.
	public static bool GetWaistL () {
		return true;
	}
	public static bool GetWaistR () {
		return true;
	}
	
	// 膝のタッチ情報.
	public static bool GetKneeL () {
		return true;
	}
	public static bool GetKneeR () {
		return true;
	}
}

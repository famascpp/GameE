using UnityEngine;
using System.Collections;

public static class InputA {
	private static bool[] inputButton;
	private static bool arduino = false;
	private static bool[] downCheck;

	public static void Init () {
		downCheck = new bool[(int)IconEnum.Max];
		inputButton = new bool[(int)IconEnum.Max];
	}

	public static void SetButton(bool[] inbtn)
	{
		for( int i = 0 ; i < inputButton.Length ; i++ )
		{
			inputButton[i] = inbtn[i];
		}
	}

	public static bool GetButton( IconEnum ie )
	{
		return inputButton[(int)ie];
	}

	public static bool GetButtonDown( IconEnum ie )
	{
		bool buf = true;
		if( inputButton[(int)ie] == downCheck[(int)ie] ){
			buf = false;
		}
		downCheck[(int)ie] = inputButton[(int)ie];
		return buf;
	}

	public static void SetArduino ( bool temp)
	{
		arduino = temp;
	}

	public static bool GetArduino()
	{
		return arduino;
	}
 }

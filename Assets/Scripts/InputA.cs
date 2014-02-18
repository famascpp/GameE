﻿using UnityEngine;
using System.Collections;

public static class InputA {
	private static bool[] inputButton;
	private static bool arduino = false;
	public static void Init () {
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

	public static void SetArduino ( bool temp)
	{
		arduino = temp;
	}

	public static bool GetArduino()
	{
		return arduino;
	}
 }

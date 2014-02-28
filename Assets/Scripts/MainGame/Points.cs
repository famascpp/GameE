using UnityEngine;
using System.Collections;

public static class Points {

	static float points = 0.0f;

	static int combo = 0;

	static int maximumCombo = 0;

	public static void Init()
	{
		points = 0.0f;
		combo = 0;
		maximumCombo = 0;
	}

	public static void AddPoints( float points )
	{
		Points.points += points;
	}

	public static void AddCombo()
	{
		combo++;
	}

	public static void ResetCombo()
	{
		if( maximumCombo < combo )
		{
			maximumCombo = combo;
		}
		combo = 0;
	}

	public static float GetPoints()
	{
		return Points.points;
	}

	public static int GetCombo()
	{
		return combo;
	}

	public static int GetMaximumCombo()
	{
		return maximumCombo;
	}
}

using UnityEngine;
using System.Collections;

public static class Points {

	static float points = 0.0f;

	public static void AddPoints( float points )
	{
		Points.points += points;
	}

	public static float GetPoints()
	{
		return Points.points;
	}
}

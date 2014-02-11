using UnityEngine;
using System.Collections;

public enum IconEnum
{
	hand = 0,
	lShoulder,
	rShoulder,
	lHip,
	rHip,
	lKnee,
	rKnee,
	Max,
}

public class MusicText {

	public TextAsset musicScore;

	public void Load(string textPath)
	{
		musicScore = Resources.Load<TextAsset>("textPath");
	}

}

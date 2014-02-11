using UnityEngine;
using System.Collections;

public class MusicText {

	public TextAsset musicScore;

	public void Load(string textPath)
	{
		musicScore = Resources.Load<TextAsset>("textPath");
	}

}

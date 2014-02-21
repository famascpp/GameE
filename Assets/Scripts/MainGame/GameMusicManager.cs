using UnityEngine;
using System.Collections;

public class GameMusicManager : MonoBehaviour {

	public float StartMusic(float delayedTime)
	{
		this.audio.PlayDelayed(delayedTime);
		return Time.time + delayedTime;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

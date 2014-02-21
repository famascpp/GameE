using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	IconManager iconMgr;

	AudioManager audioMgr;

	public GameObject uniduino;
	bool isUniduino;

	void Awake()
	{
		iconMgr = this.GetComponent<IconManager>();
		audioMgr = this.GetComponent<AudioManager>();
	}

	// Use this for initialization
	void Start () {
		MusicScore canonLock = new MusicScore("music/test/test");
	}

	// Update is called once per frame
	void Update () {
	}

	void OnGUI()
	{
	}



}


using UnityEngine;
using System.Collections;

public class TimeDestroyObject : MonoBehaviour {

	public float time = 5.0f;

	float startTime = 0.0f;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if( Time.time - startTime > time )
		{
			Destroy(this.gameObject);
		}
	}
}

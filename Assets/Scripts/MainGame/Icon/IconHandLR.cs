using UnityEngine;
using System.Collections;

public class IconHandLR : MonoBehaviour {

	public int depthLayer = 0;
	public Texture tex;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		GUI.depth = depthLayer;
		Icon parent = this.transform.parent.gameObject.GetComponent<Icon>();
		if( parent != null )
		{
			GUI.DrawTexture(parent.TexRect(),tex);
		}
	}
}

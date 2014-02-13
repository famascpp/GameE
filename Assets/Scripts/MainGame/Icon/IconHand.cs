using UnityEngine;
using System.Collections;

public class IconHand : MonoBehaviour {

	public Texture texBack,texForward;

	public int depthLayer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setIcon()
	{
		IconHandLR[] iHandLR = this.GetComponentsInChildren<IconHandLR>();
		for( int i = 0 ; i < iHandLR.Length ; i++ )
		{
			if( iHandLR[i].name == "handBack" ){
				iHandLR[i].tex = texBack;
				iHandLR[i].depthLayer = this.depthLayer + 1;
			}
			if( iHandLR[i].name == "handForward" )
				iHandLR[i].tex = texForward;
		}
	}
}

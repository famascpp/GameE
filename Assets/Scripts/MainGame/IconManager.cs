using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class IconManager : MonoBehaviour {

	public const int iconMax = 7;

	public Texture[] iconTex = new Texture[iconMax];

	public GameObject iconPrefab;

	public Icon[] icon = new Icon[iconMax];

	// Use this for initialization
	void Start () {

		int[] dispos = {0,1,3,5,6,4,2};
		float length = 0.4f;
		float scale = 0.1f;

		//アイコン配置.
		for( int i = 0 ; i < iconMax ; i++ )
		{
			float x,y;
			float ang = ( ( Mathf.PI * 2.0f ) / (float)iconMax ) * (float)i + Mathf.PI / 2.0f;
			x = Mathf.Cos( ang ) * length;
			y = Mathf.Sin( ang ) * length;

			GameObject obj;

			obj = Instantiate(
				iconPrefab,
				new Vector3(x,y,0.0f),
				Quaternion.identity
				) as GameObject;

			icon[dispos[i]] = obj.GetComponent<Icon>();
		}

		//テクスチャ貼り付け.
		for( int i = 0 ; i < iconMax ; i++ )
		{
			icon[i].icon = iconTex[i];
			icon[i].scale = scale;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

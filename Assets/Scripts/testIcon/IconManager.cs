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


		int[] dispos = {0,2,4,6,5,3,1};

		//アイコン配置
		for( int i = 0 ; i < iconMax ; i++ )
		{
			float x,y;
			float ang = ( ( Mathf.PI * 2.0f ) / (float)iconMax ) * (float)i + Mathf.PI / 2.0f;
			float length = 0.3f;

			x = Mathf.Cos( ang ) * length;
			y = Mathf.Sin( ang ) * length;

			Object obj;

			obj = Instantiate(
				iconPrefab,
				new Vector3(x,y,0.0f),
				Quaternion.identity
				);

			icon[dispos[i]] = (Icon)obj;
		}

		//テクスチャ貼り付け.
		for( int i = 0 ; i < iconMax ; i++ )
		{
			icon[i].icon = iconTex[i];
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

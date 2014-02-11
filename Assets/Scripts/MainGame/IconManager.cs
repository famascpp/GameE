using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class IconManager : MonoBehaviour {

	public const int iconMax = 7;

	public Texture[] iconTex = new Texture[iconMax];

	public GameObject iconPrefab;

	Icon[] icon = new Icon[iconMax];
	public Icon[] getIcon{
		get{ return icon; }
	}

	public Icon getIconE( IconEnum ie )
	{
		return icon[(int)ie];
	}

	public Texture cursorTex;
	Icon cursor;
	public Icon getCursor{
		get{ return cursor; }
	}

	// Use this for initialization
	void Start () {
		GUI.depth = 1;

		int[] dispos = {0,1,3,5,6,4,2};
		float length = 0.4f;
		float scale = 0.1f;
		GameObject obj;

		//アイコン配置.
		for( int i = 0 ; i < iconMax ; i++ )
		{
			float x,y;
			float ang = ( ( Mathf.PI * 2.0f ) / (float)iconMax ) * (float)i + Mathf.PI / 2.0f;
			x = Mathf.Cos( ang ) * length;
			y = Mathf.Sin( ang ) * length;

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
			icon[i].gameObject.name = "icon"+i+""+((IconEnum)i).ToString();
			icon[i].depthLayer = 1;
		}

		//カーソル作成.
		obj = Instantiate(
			iconPrefab,
			new Vector3(0.0f,0.0f,0.0f),
			Quaternion.identity
			) as GameObject;
		
		cursor = obj.GetComponent<Icon>();
		cursor.icon = cursorTex;
		cursor.scale = scale;
		cursor.gameObject.name = "cursor";

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class IconManager : MonoBehaviour {

	public const int iconMax = (int)IconEnum.Max;



	public Texture[] iconTex = new Texture[iconMax];

	public GameObject iconPrefab;
	public GameObject iconHandPrefab;

	Icon[] icon = new Icon[iconMax];
	public Icon getIconE( IconEnum ie )
	{
		int i = (int)ie;
		return getIcon(i);
	}
	public Icon getIcon(int i){
		return icon[i];
	}

	// Use this for initialization
	void Awake () {
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

		//テクスチャ貼り付けとその他設定.
		for( int i = 0 ; i < iconMax ; i++ )
		{
			string strname = "icon"+i+""+((IconEnum)i).ToString();

			icon[i].icon = iconTex[i];
			icon[i].scale = scale;
			icon[i].gameObject.name = strname;
			icon[i].depthLayer = 1;
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

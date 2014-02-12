using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class IconManager : MonoBehaviour {

	public const int iconMax = 7;

	public const int cursorMax = 2;

	public Texture[] iconTex = new Texture[iconMax];

	public GameObject iconPrefab;

	public AudioManager audioMgr;

	Icon[] icon = new Icon[iconMax];
	public Icon getIconE( IconEnum ie )
	{
		return icon[(int)ie];
	}

	public Texture[] cursorTex = new Texture[cursorMax];
	Icon[] cursor = new Icon[cursorMax];
	public Icon getCursor( int i ){
		return cursor[i];
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

		MusicScore canonLock = new MusicScore("music/test/test");
		//テクスチャ貼り付けとその他設定.
		for( int i = 0 ; i < iconMax ; i++ )
		{
			icon[i].icon = iconTex[i];
			icon[i].scale = scale;
			icon[i].gameObject.name = "icon"+i+""+((IconEnum)i).ToString();
			icon[i].depthLayer = 1;
			icon[i].setScore(canonLock.getScoreCol(i));
			icon[i].audioMgr = audioMgr;
		}

		for( int i = 0 ; i < cursorMax ; i++ )
		{
			//カーソル作成.
			obj = Instantiate(
				iconPrefab,
				new Vector3(-0.1f + 0.2f*(float)i,0.0f,0.0f),
				Quaternion.identity
				) as GameObject;
			
			cursor[i] = obj.GetComponent<Icon>();
			cursor[i].icon = cursorTex[i];
			cursor[i].scale = scale;
			cursor[i].gameObject.name = "cursor" + i;

			if( i % 2 == 1 )
			{
				cursor[i].scale *= -1;	
			}

		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

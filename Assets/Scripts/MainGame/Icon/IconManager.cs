using UnityEngine;
using System.Collections;

public class IconManager : MonoBehaviour {

	public const int iconMax = (int)IconEnum.Max;

	public Texture[] iconTex = new Texture[iconMax];

	public GameObject iconPrefab;
	public GameObject iconHandPrefab;

	public Icon[] receiveIcon = new Icon[iconMax];
	public Icon[] sendIcon = new Icon[iconMax];

	public static float SendToReceiveTime = 3.0f;

	public Icon GetReceiveIconE( IconEnum ie )
	{
		int i = (int)ie;
		return GetReceiveIcon(i);
	}
	public Icon GetReceiveIcon(int i){
		return receiveIcon[i];
	}
	
	public Icon GetSendIconE( IconEnum ie )
	{
		int i = (int)ie;
		return GetSendIcon(i);
	}
	public Icon GetSendIcon(int i){
		return sendIcon[i];
	}
	


	// Use this for initialization
	void Awake () {
		int[] dispos = {0,1,2,3,4,5,6};
		int iconCnt = 7;
		float blank = 0.2f;
		float scale = 0.1f;
		GameObject obj;

		//texture and etc setting
		for( int i = 0 ; i < iconMax ; i++ )
		{
			float x,y;
			float height = - 0.5f + blank + ( ( 1.0f - blank * 2.0f ) / (float)( iconCnt - 1 ) ) * dispos[i];

			if( receiveIcon[i] == null )
			{
				x = -0.2f;
				y = height;

				obj = Instantiate(
					iconPrefab,
					new Vector3(x,y,0.0f),
					Quaternion.identity
					) as GameObject;
				
				receiveIcon[i] = obj.GetComponent<Icon>();

				string strname = "ReceiveIcon"+i+""+((IconEnum)i).ToString();

				receiveIcon[i].icon = iconTex[i];
				receiveIcon[i].scale = scale;
				receiveIcon[i].gameObject.name = strname;
				receiveIcon[i].depthLayer = 1;
			}

			if( sendIcon[i] == null )
			{
				x = 1.33f;
				y = height;

				obj = Instantiate(
					iconPrefab,
					new Vector3(x,y,0.0f),
					Quaternion.identity
					) as GameObject;
				
				sendIcon[i] = obj.GetComponent<Icon>();
				
				string strname = "SendIcon"+i+""+((IconEnum)i).ToString();
				
				sendIcon[i].scale = scale;
				sendIcon[i].gameObject.name = strname;
				sendIcon[i].depthLayer = 1;
			}
		}

	}
	
}

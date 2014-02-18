using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {
	public Texture backgroundTexture; // 背景画像.
	public Texture userGuideTexture1; // 注意画像.
	public Texture userGuideTexture2; // 注意画像.
	public Texture startTexture; // スタートロゴ画像.
	public Texture nameTexture;
	public Texture teamNameTexture;

	private Texture2D blackTexture;
	private float blackAlpha = 1;
	private bool blackFadeFlag = false;

	public Texture[] nowLoadingTexture = new Texture[2];
	private int nowLoadingNum = 0;

	public static bool drawUserGuideFlag = false, drawUserGuideFlag2 = false; // true:操作説明画像表示.


	// ロゴの構造体.
	struct Logo{
		public static Texture texture;
		public static float x, w, y, h;
		public static float vy;
		public static float POSITION_Y;
		static Logo(){
			Logo.texture = Resources.Load<Texture>("Textures/title");
			Logo.x = Screen.width/2-Logo.texture.width/2;
			Logo.w = Logo.texture.width;
			Logo.POSITION_Y = Screen.height/2-Logo.texture.height/2-100;
			Logo.y = Logo.POSITION_Y;
			Logo.h = Logo.texture.height;
			Logo.vy = 1.0f;
		}
	}
	void Start () {
		blackTexture = new Texture2D(32,32,TextureFormat.ARGB32,false);
		//blackTexture.ReadPixels(new Rect(0,0,32,32),0,0,false);
		blackTexture.SetPixel(0,0,Color.white);
		blackTexture.Apply();

		StartCoroutine(NowLoadingTextureChange()); // ローディングのテクスチャ切り替え.
	}
	
	// ゲームスタート.
	IEnumerator StartGame(){
		yield return new WaitForSeconds(3.0f);
		Application.LoadLevel(1);
	}

	void Update () {

		if(Logo.POSITION_Y+10.0f <= Logo.y) Logo.vy = -1.0f;
		if(Logo.POSITION_Y-10.0f >= Logo.y) Logo.vy = +1.0f;
		Logo.y += Logo.vy;

		if( drawUserGuideFlag2 ){ // 一定時間説明画面がでたら遷移.
			StartCoroutine( StartGame() );
		}
	}

	public static void UserGuideFlag () {
		/*操作説明表示.*/
		drawUserGuideFlag = true;
	}



	void DrawBackground () {
		// 背景画像貼り付け.
		if(Event.current.type == EventType.Repaint){

			Graphics.DrawTexture(
				new Rect(0, 0,
			         Screen.width, Screen.height),
				backgroundTexture);

		}
	}

	void DrawTitleLogo () {
		// タイトルロゴ貼り付け.
		if ( Event.current.type == EventType.Repaint ) {
			Graphics.DrawTexture(
				new Rect(Logo.x, Logo.y,
			         Logo.w, Logo.h),
				Logo.texture);
		}
	}

	void DrawStartLogo () {
		// スタートロゴ貼り付け.
		if ( Event.current.type == EventType.Repaint ) {
			GUIUtility.RotateAroundPivot(10.5f, new Vector2(Screen.width/2 - startTexture.width/2.8f, Screen.height/2.0f)); // 回転.
			Graphics.DrawTexture(
				new Rect(Screen.width/2 - startTexture.width/3.0f, Screen.height/2.5f,
			         startTexture.width-300, startTexture.height-600),
				startTexture);
			GUIUtility.RotateAroundPivot(-10.5f, new Vector2(Screen.width/2 - startTexture.width/2.8f, Screen.height/2.0f)); // 回転終了.
		}
	}
	void DrawUserGuide () {
		if ( drawUserGuideFlag ) {
			if(Event.current.type == EventType.Repaint){
				Graphics.DrawTexture(
					new Rect(0, 0,
				         Screen.width, Screen.height),
					userGuideTexture1);

				StartCoroutine( DrawUserGuide2 () ); // 次の説明表示.
			}
		}
	}
	
	// ②枚目の説明.
	IEnumerator DrawUserGuide2 () {
		while ( true ) {

			if(drawUserGuideFlag2 == true)
			{
			Graphics.DrawTexture(
				new Rect(0, 0,
			         Screen.width, Screen.height),
					userGuideTexture2);
			}
			yield return new WaitForSeconds(3.0f);
			drawUserGuideFlag2 = true;
		}
	}

	// ローディング画面.
	void DrawNowLoading(){
		if(Event.current.type == EventType.Repaint){
			Graphics.DrawTexture(
				new Rect(80.0f, 390,
			         nameTexture.width/1.5f, nameTexture.height/1.5f),
				nameTexture);
		
			Graphics.DrawTexture(
				new Rect(-20.0f, -30.0f,
			         teamNameTexture.width, teamNameTexture.height),
				teamNameTexture);
		
			Graphics.DrawTexture(
				new Rect(Screen.width - nowLoadingTexture[nowLoadingNum].width/1.5f, Screen.height - nowLoadingTexture[nowLoadingNum].height/1.5f,
			         nowLoadingTexture[nowLoadingNum].width/1.5f, nowLoadingTexture[nowLoadingNum].height/1.5f),
				nowLoadingTexture[nowLoadingNum]);
		}
	}
	IEnumerator NowLoadingTextureChange(){
		while(true){
			yield return new WaitForSeconds(1.0f);
			if(nowLoadingNum == 0) nowLoadingNum = 1;
			else  nowLoadingNum = 0;
		}
	}

	void OnGUI () {
		if(!ReadArduino.GetNowLoading()){ // ローディング中じゃない場合.
			DrawBackground (); // 背景画像貼り付け.
			DrawStartLogo ();  // 背景画像貼り付け.
			DrawTitleLogo ();  // タイトルロゴ貼り付け.
			DrawUserGuide ();  // 操作説明貼り付け.

			blackFadeFlag = true;
		}

		if( blackFadeFlag && blackAlpha >= 0)
			blackAlpha-=0.005f;
		GUI.color = new Color(0,0,0,blackAlpha);
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), blackTexture);

		if(ReadArduino.GetNowLoading()){
			DrawNowLoading (); // ローディング画面貼り付け.
		}
	}
	
}

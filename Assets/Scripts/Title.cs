using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {
	public Texture backgroundTexture; // 背景画像.
	public Texture userGuideTexture1; // 注意画像.
	public Texture userGuideTexture2; // 注意画像.
	public Texture sousahouhouTexture;
	public Texture sousasetumeiTexture;
	public Texture startTexture; // スタートロゴ画像.
	public Texture nameTexture;
	public Texture teamNameTexture;

	public Texture2D shoulderTexture ,hipTexture ,kneeTexture;
	public static float shoulderAlpha = .0f, hipAlpha = .0f, kneeAlpha = .0f;

	private Texture2D blackTexture, blackTexture2;
	private float blackAlpha = 1;
	private bool blackFadeFlag = false;

	public Texture[] nowLoadingTexture = new Texture[2];
	private int nowLoadingNum = 0;

	private static bool drawWarningFlag = false, drawUserGuideFlag = false; // true:操作説明画像表示.
	private static bool startFlag = false;
	private bool[] seBeatFlag = {true, true, true};

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

		blackTexture2 = new Texture2D(Screen.width,Screen.height,TextureFormat.ARGB32,true);
		blackTexture2.SetPixel(0,0,Color.black);
		blackTexture2.Apply();

//		hipTexture = new Texture2D(32,32,TextureFormat.ARGB32,false);
//		hipTexture.SetPixel(0,0,Color.white);
//		hipTexture.Apply();

		StartCoroutine(NowLoadingTextureChange()); // ローディングのテクスチャ切り替え.
	}
	
	// ゲームスタート.
	public static IEnumerator StartGame(){
		yield return new WaitForSeconds(1.0f);
		Application.LoadLevel(1);
	}

	void Update () {

		if(Logo.POSITION_Y+10.0f <= Logo.y) Logo.vy = -1.0f;
		if(Logo.POSITION_Y-10.0f >= Logo.y) Logo.vy = +1.0f;
		Logo.y += Logo.vy;
	}

	// 操作説明を表示するフラグをセット.
	public static void SetDrawUserGuideFlag ( bool flg ) {
		/*操作説明表示.*/
		drawUserGuideFlag = flg;
	}
	// シーン遷移出来る状況か.
	public static bool GetStartFlag () {
		return startFlag;
	}
	public static void SetShoulderAlpha( float alpha ){
		shoulderAlpha = alpha;
	}
	public static void SetHipAlpha( float alpha ){
		hipAlpha = alpha;
	}
	public static void SetKneeAlpha( float alpha ){
		kneeAlpha = alpha;
	}
	public static bool GetDrawWarningFlag () {
		return drawWarningFlag;
	}
	public static bool GetDrawUserGuideFlag () {
		return drawUserGuideFlag;
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
	void DrawWarning () {
		if ( drawWarningFlag ) {
			if(Event.current.type == EventType.Repaint){
				Graphics.DrawTexture(
					new Rect(0, 0,
				         Screen.width, Screen.height),
					userGuideTexture1);

				StartCoroutine( TitleTextureChange () ); // タイトル表示.
			}
		}
	}
	
	// 操作説明.
	void DrawUserGuide () {
		Color normalColor; // 通常時の色 半透明ではない.
		normalColor = GUI.color;

		if(drawUserGuideFlag == true)
		{
			Graphics.DrawTexture( // 操作説明.
                     new Rect(0, 0,
			         Screen.width, Screen.height),
                     blackTexture2);
			Graphics.DrawTexture( // 操作説明.
			    new Rect(0, 0,
			        Screen.width/3, Screen.height),
			        userGuideTexture2);
			Graphics.DrawTexture( // 操作方法.
			                     new Rect(Screen.width/4, 0,
			         sousahouhouTexture.width/2, sousahouhouTexture.height/2),
			         sousahouhouTexture);
			Graphics.DrawTexture( // 操作説明.
			                     new Rect(Screen.width/4, -150,
			         sousasetumeiTexture.width/2, sousasetumeiTexture.height/2),
                     sousasetumeiTexture);
			GUI.color = normalColor;
			if( shoulderAlpha > .0f){
				GUI.color -= new Color(0,0,0,shoulderAlpha);
				if( seBeatFlag[0] ){
					TitleMusic.SetBeatFlag( true ); // 叩く音.
				}
				seBeatFlag[0] = false;
			}
			GUI.DrawTexture( // 肩タッチ.
				new Rect(Screen.width/1.8f, Screen.height/6,
		        shoulderTexture.width/3, shoulderTexture.height/3),
     			shoulderTexture);

			GUI.color = normalColor;
			if( hipAlpha > .0f){
				GUI.color -= new Color(0,0,0,hipAlpha);
				if( seBeatFlag[1] ){
					TitleMusic.SetBeatFlag( true ); // 叩く音.
				}
				seBeatFlag[1] = false;
			}
			GUI.DrawTexture(
				new Rect(Screen.width/1.8f, Screen.height/6+120,
		        hipTexture.width/3, hipTexture.height/3),
        		hipTexture);

			GUI.color = normalColor;
			if( kneeAlpha > .0f){
				GUI.color -= new Color(0,0,0,kneeAlpha);
				if( seBeatFlag[2] ){
					TitleMusic.SetBeatFlag( true ); // 叩く音.
				}
				seBeatFlag[2] = false;
			}

			GUI.DrawTexture( // 膝タッチ.
                new Rect(Screen.width/1.8f, Screen.height/6+240,
		        kneeTexture.width/3, kneeTexture.height/3),
                kneeTexture);

			GUI.color = normalColor;
			if( shoulderAlpha > .0f && hipAlpha > .0f && kneeAlpha > .0f ){
				DrawStartLogo (); // 肩、腰、膝を押したら表示するように設定する.
				startFlag = true;
			}
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

			drawWarningFlag = true;

		}
	}

	// タイトル画面に1秒後変える.
	IEnumerator TitleTextureChange(){
		while(true){
			yield return new WaitForSeconds(3.0f);
			drawWarningFlag = false;
		}
	}
	IEnumerator NowLoadingTextureChange(){
		while(true){
			yield return new WaitForSeconds(1.0f);
			if(nowLoadingNum == 0) nowLoadingNum = 1;
			else  nowLoadingNum = 0;
		}
	}

	// タイトル描画.
	void DrawTitle () {
		if ( !drawWarningFlag ){
			DrawBackground (); // 背景画像貼り付け.
			DrawStartLogo ();  // スタートロゴ貼り付け.
			DrawTitleLogo ();  // タイトルロゴ貼り付け.
		}
	}

	// 画像表示.
	void OnGUI () {

		if(!ReadArduino.GetNowLoading()){ // ローディング中じゃない場合.
			DrawTitle ();
			DrawWarning ();  // 操作説明貼り付け.
			DrawUserGuide ();
			blackFadeFlag = true;
		}

		// フェード処理.
		if( blackFadeFlag && blackAlpha >= 0)
			blackAlpha-=0.005f;
		GUI.color = new Color(0,0,0,blackAlpha);
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), blackTexture);

		// ローディング画面.
		if(ReadArduino.GetNowLoading()){
			DrawNowLoading (); // ローディング画面貼り付け.
		}
	}
}

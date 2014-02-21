using UnityEngine;
using System.Collections;

public static class MyGUI {

	//高さに合わせて画像を拡縮,xとyは0~1,サイズも0~1.
	public static void DrawTexture( Rect rect , Texture tex )
	{
		GUI.DrawTexture( RectHeightZeroToOne(rect) , tex );
	}


	public static Rect RectHeightZeroToOne(Rect rect)
	{

		rect.x = Screen.width / 2.0f + Screen.height * ( rect.x - rect.width / 2.0f  );
		rect.y = Screen.height / 2.0f + Screen.height * ( rect.y - rect.height / 2.0f );
		rect.height = Screen.height * ( rect.height );
		rect.width = Screen.height * ( rect.width );

		return rect;
	}

}

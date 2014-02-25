using UnityEngine;
using System.Collections;

public static class MyGUI {

	public static void DrawTextureAspect( Rect rect , Texture tex ,float aspect)
	{
		GUI.DrawTexture( RectHeightZeroToOne(rect, aspect) , tex );
	}

	public static void DrawTextureAspect( Rect rect , Texture tex )
	{
		GUI.DrawTexture( RectHeightZeroToOne(rect, tex.width / tex.height) , tex );
	}

	//y scaling, and x is 0 to 1, 0 to 1 the size of the image to fit he height.
	public static void DrawTexture( Rect rect , Texture tex )
	{
		GUI.DrawTexture( RectHeightZeroToOne(rect, 1.0f) , tex );
	}


	public static Rect RectHeightZeroToOne(Rect rect,float aspect)
	{

		rect.x = Screen.width / 2.0f + Screen.height * ( rect.x - rect.width / 2.0f  );
		rect.y = Screen.height / 2.0f + Screen.height * ( rect.y - rect.height / 2.0f );
		rect.height = Screen.height * ( rect.height );
		rect.width = Screen.height * ( rect.width ) * aspect;

		return rect;
	}

}

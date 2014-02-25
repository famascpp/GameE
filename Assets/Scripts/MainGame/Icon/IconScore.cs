using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum IconStates 
{
	move,
	pushed,
}

public class IconScore : MonoBehaviour {

	Vector2 sendPos;
	float sendSize;

	Vector2 receivePos;

	float startTime;

	float nextTime;

	Texture tex;

	IconStates states;

	int col;	//hand shoulder leg...
	public int Col(){ return col; }

	bool lrDistribution = false;
	public bool LRDistribution(){ return this.lrDistribution; }

	public void Init( Vector2 sendPos,float sendSize,Vector2 receivePos,float nextTime , int col , bool lrDistribution )
	{
		this.sendPos = sendPos;
		this.sendSize = sendSize;
		this.receivePos = receivePos;
		this.nextTime = nextTime;
		this.col = col;
		this.lrDistribution = lrDistribution;

		if( this.lrDistribution )
		{
			if( col == 0 ) this.tex = Resources.Load<Texture>("Textures/Icon/icon_0");
			else if( col % 2 == 1 ) this.tex = Resources.Load<Texture>("Textures/Icon/icon_l3");
			else this.tex = Resources.Load<Texture>("Textures/Icon/icon_r3");
		}
		else
		{
			this.tex = Resources.Load<Texture>("Textures/Icon/icon_0");
		}

		startTime = Time.time;

		states = IconStates.move;

	}

	void Start()
	{
	}

	void Update()
	{
		this.transform.position = new Vector3( TexPos().x,TexPos().y,0.0f);
	}

	public bool Push()
	{
		bool ret = false;
		float length = 0.3f;

		if( Mathf.Abs( AtNextTime() ) < length )
		{
			float addPt = 1.0f - Mathf.Abs( AtNextTime() ) / length;

			Points.AddPoints( addPt * 10.0f );

			YesEnum yes;
			if( addPt > 0.7f ) yes = YesEnum.yeah;
			else if( addPt > 0.4f ) yes = YesEnum.yes;
			else yes = YesEnum.oh;

			GameObject gmYes =  new GameObject("" + yes.ToString());
			gmYes.transform.position = this.transform.position;

			YesIcon gmYesIcon = gmYes.AddComponent<YesIcon>();
			gmYesIcon.Init( yes );

			this.states = IconStates.pushed;

			ret = true;
		}

		return ret;
	}

	void OnGUI()
	{
		GUI.depth = -10;
		switch( this.states )
		{
		case IconStates.move:
			DrawMoveIcon();
			break;
		case IconStates.pushed:
			break;
		}
	}


	void DrawMoveIcon()
	{
		MyGUI.DrawTexture( new Rect( TexPos().x,TexPos().y,sendSize,sendSize ) ,this.tex);
	}

	float AtNextTime()
	{
		return nextTime - ( Time.time - startTime );
	}

	Vector2 TexPos()
	{
		float min = -2.5f;
		float max = IconManager.SendToReceiveTime;
		
		Vector2 pos = ( receivePos - sendPos ) * (1.0f - AtNextTime() / max) + sendPos;

		return pos;
	}

}

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

	//row and. col or.
	IconEnum[][] buttonToReact;

	bool lButton = false;
	bool rButton = false;

	int col;	//hand shoulder leg...
	public int Col(){ return col; }

	bool lrDistribution = false;
	public bool LRDistribution(){ return this.lrDistribution; }

	public void Init( Vector2 sendPos,float sendSize,Vector2 receivePos,float nextTime , int col , PushMode pushMode , IconEnum[][] buttonToReact )
	{
		this.sendPos = sendPos;
		this.sendSize = sendSize;
		this.receivePos = receivePos;
		this.nextTime = nextTime;
		this.col = col;
		this.lrDistribution = lrDistribution;

		startTime = Time.time;

		states = IconStates.move;

		this.buttonToReact = new IconEnum[buttonToReact.Length][];
		for( int i = 0 ; i < buttonToReact.Length ; i++ )
		{
			this.buttonToReact[i] = new IconEnum[buttonToReact[i].Length];
			buttonToReact[i].CopyTo(this.buttonToReact[i],0);
		}

		for( int i = 0 ; i < buttonToReact.Length ; i++ )
		{
			for( int j = 0 ; j < buttonToReact[i].Length ; j++ )
			{
				if( (int)buttonToReact[i][j] == 0 )
				{
					lButton = true;
					rButton = true;
				}
				else if( (int)buttonToReact[i][j] % 2 == 1 ) lButton = true;
				else rButton = true;
			}
			if( !( rButton && lButton ) )
			{
				lButton = false;
				rButton = false;
			}
		}

		switch( pushMode )
		{
		case PushMode.SeparateLeftAndRight:
			{
				if( rButton && lButton ) this.tex = Resources.Load<Texture>("Textures/Icon/hand_6");
				else if( lButton ) this.tex = Resources.Load<Texture>("Textures/Icon/icon_l3");
				else this.tex = Resources.Load<Texture>("Textures/Icon/icon_r3");
			}
			break;
		case PushMode.TaikoNoTatsujin:
			{
				if( rButton && lButton )
					this.tex = Resources.Load<Texture>("Textures/Icon/hand_6");
				else
					this.tex = Resources.Load<Texture>("Textures/Icon/hand_3");
			}
			break;
		}

	}

	void Start()
	{
	}

	void Update()
	{
		this.transform.position = new Vector3( TexPos().x,TexPos().y,0.0f);
	}

	public bool Push(uint[] inputButton)
	{
		bool ret = false;



		bool input = false;
		for( int i = 0 ; i < buttonToReact.Length ; i++ )
		{
			if( buttonToReact[i].Length == 1 )
			{
				if( inputButton[(int)buttonToReact[i][0]] == 1 )
				{
					input = true;
					break;
				}
			}
			else
			{
				bool oneMilliSecond = false;
				bool oneToFive = false;
				for( int j = 0 ; j < buttonToReact[i].Length ; j++ )
				{
					int ii = (int)buttonToReact[i][j];
					if( oneMilliSecond == false && inputButton[ii] == 1 )
					{
						oneMilliSecond = true;
					}
					else if( 1 <= inputButton[ii] && inputButton[ii] <= 5 )
					{
						oneToFive = true;
					}
				}
				if( oneMilliSecond && oneToFive )
				{
					input = true;
					break;
				}
			}
		}
		   
		float length = 0.3f;


		if( ( input && Mathf.Abs( AtNextTime() ) < length ) || AtNextTime() < -length )
		{
			float addPt = 1.0f - Mathf.Abs( AtNextTime() ) / length;

			YesEnum yes;
			if( addPt > 0.9f ) yes = YesEnum.yeah;
			else if( addPt > 0.6f ) yes = YesEnum.yes;
			else yes = YesEnum.oh;

			//combo
			if( yes == YesEnum.oh ) Points.ResetCombo();
			else Points.AddCombo();

			//point
			float tAddPt = addPt;
			switch( yes )
			{
			case YesEnum.yeah:
				tAddPt *= 2.0f;
				break;
			case YesEnum.yes:
				tAddPt *= 1.0f;
				break;
			case YesEnum.oh:
				tAddPt *= 0.0f;
				break;
			}

			Points.AddPoints( tAddPt * (float)(Points.GetCombo()/10+1) );


			GameObject gmYes =  new GameObject("" + yes.ToString());
			gmYes.transform.position = this.transform.position;

			YesIcon gmYesIcon = gmYes.AddComponent<YesIcon>();
			gmYesIcon.Init( yes );

			this.states = IconStates.pushed;

			ret = true;

			for( int i = 0 ; i < buttonToReact.Length ; i++ )
			{
				for( int j = 0 ; j < buttonToReact[i].Length ; j++ )
				{
					int ii = (int)buttonToReact[i][j];
					inputButton[ii] += 10;
				}
			}
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

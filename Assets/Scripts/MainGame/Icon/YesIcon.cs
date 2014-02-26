using UnityEngine;
using System.Collections;

public enum YesEnum
{
	oh,
	yeah,
	yes,
}

public class YesIcon : MonoBehaviour {

	float endTimeValue = 1.0f;
	float endtime;
	YesEnum yes;

	Texture tex;
	Vector3 endPos;

	public void Init( YesEnum yes )
	{
		this.yes = yes;

		string str = "Textures/Icon/";

		switch( yes )
		{
		case YesEnum.oh:
			str += "oh";
			break;
		case YesEnum.yes:
			str += "yes";
			break;
		case YesEnum.yeah:
			str += "yeah";
			break;
		}

		tex = Resources.Load<Texture>(str);


		endPos = this.transform.position + Vector3.left * 0.2f;
	}

	// Use this for initialization
	void Start () {
		endtime = Time.time + endTimeValue;
	}
	
	// Update is called once per frame
	void Update () {

		if( endtime < Time.time ) Destroy(this.gameObject);

		this.transform.position += ( endPos - this.transform.position ) / 10.0f;
	}

	void OnGUI()
	{
		GUI.depth = -20;
		if( tex != null )
		{
			MyGUI.DrawTexture(
				new Rect(
				this.transform.position.x,
				this.transform.position.y,
				0.2f,0.2f
				),
				tex
				);
		}
	}
}

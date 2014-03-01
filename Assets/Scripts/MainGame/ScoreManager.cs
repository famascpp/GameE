using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO; //System.IO.FileInfo, System.IO.StreamReader, System.IO.StreamWriter
using System; //Exception
using System.Text; //Encoding

struct ScoreSet {
	public int score;
	public bool Instantiated;
	public float PushTime;
}

public enum PushMode
{
	SeparateLeftAndRight,
	TaikoNoTatsujin,
}

public class ScoreManager : MonoBehaviour {

	public bool scoreTextAsset = true;

	ScoreSet[][][] ss;
	int[] ssMin;		//now Measure array.

	uint[] inputButton;

	List<uint[]> listInputButton;

	public Texture circle;

	public GameObject pushSE;

	public GameObject icon;

	GameManager gameMgr;

	IconManager iconMgr;

	AudioManager audioManager;

	bool isUniduino = false;

	bool end = false;
	public bool isEnd{ get { return end; } }

	public PushMode pushMode = PushMode.TaikoNoTatsujin;

	List<IconScore> listIconScore;

	void Awake()
	{
		GameObject tempUniduino = GameObject.FindGameObjectWithTag("Uniduino");
		if( tempUniduino != null ) isUniduino = true;
	}

	// Use this for initialization
	void Start () {

		audioManager = this.GetComponent<AudioManager>();
		gameMgr = this.GetComponent<GameManager>();
		iconMgr = this.GetComponent<IconManager>();

		MusicScore canonLock;
		canonLock = new MusicScore("test",scoreTextAsset);

		List<List<List<int>>> score = canonLock.Score;

		this.ssMin = new int[score.Count];
		for( int i = 0 ; i < ssMin.Length ; i++ ) ssMin[i] = 0;

		//　Add to array a score file you have loaded
		this.ss = new ScoreSet[score.Count][][];
		for( int i = 0 ; i < this.ss.Length ; i++ )
		{
			this.ss[i] = new ScoreSet[score[i].Count][];
			for( int j = 0 ; j < this.ss[i].Length ; j++ )
			{
				this.ss[i][j] = new ScoreSet[score[i][j].Count];
				for( int l = 0 ; l < this.ss[i][j].Length ; l++ )
				{
					this.ss[i][j][l].score = score[i][j][l];
					this.ss[i][j][l].Instantiated = false;
					this.ss[i][j][l].PushTime = audioManager.getMeasureBeat(j,l,this.ss[i][j].Length);
				}
			}
		}

		inputButton = new uint[(int)IconEnum.Max];
		for( int i = 0 ; i < inputButton.Length ; i++ ) inputButton[i] = 0;

		listInputButton = new List<uint[]>();

		listIconScore = new List<IconScore>();
	}
	
	// Update is called once per frame
	void Update () {

		InputUpdate();

		List<int> deleteList = new List<int>();

		//icon Push
		for( int i = 0 ; i < listIconScore.Count ; i++ )
		{
			if( listIconScore[i].Push(inputButton) ){
				deleteList.Add(i);
			}
		}

		//pushed icon delete
		for (int i = listIconScore.Count - 1; i >= 0; i--)
		{
			for( int j = 0 ; j < deleteList.Count ; j++ )
			{
				if( deleteList[j] == i )
				{
					listIconScore.RemoveAt(i);
					break;
				}
			}
		}

		//get scope
		float min = -2.0f;
		float max = IconManager.SendToReceiveTime;

		bool colLoop;

		if( end == false ){
			for( int i = 0 ; i < this.ss.Length ; i++ )
			{
				colLoop = true;
				for( int j = ssMin[i] ; j < this.ss[i].Length && colLoop ; j++ )
				{
					for( int l = 0 ; l < this.ss[i][j].Length && colLoop ; l++ )
					{
						//in string?.
						if( this.ss[i][j][l].score != 0 )
						{
							float nextTime = this.ss[i][j][l].PushTime - audioManager.AudioTime;

							// Score end?
							if( (int)IconEnum.Max == i && nextTime < 0.0f ){
								end = true;
								OutPutInputButton();
								colLoop = false;
								break;
							}

							// Score Icon 
							if( this.ss[i][j][l].Instantiated == false && i < (int)IconEnum.Max ){
								if( nextTime < max ){
									InstantiateIcon(nextTime,i,i,j,l);

									if( min < nextTime ){
									}else{
										ssMin[i] = j;
									}
								}else{
									colLoop = false;
								}
							}
						}
					}
				}
			}
		}
	}

	void InstantiateIcon(float nextTime,int col,int ii,int jj,int ll)
	{
		this.ss[ii][jj][ll].Instantiated = true;

		bool doublePush = false;
		if( pushMode == PushMode.TaikoNoTatsujin ){

			if( col != 0  )
			{
				int col2;
				if( col % 2 == 1 )
				{
					col2 = col+1;
				}
				else
				{
					col2 = col-1;
				}



				if( this.ss[col2][jj].Length != 0 )
				{
					if(
						( this.ss[col][jj].Length % this.ss[col2][jj].Length == 0 ) &&
						( ll % ( this.ss[col][jj].Length / this.ss[col2][jj].Length ) == 0 )
					   )
					{
						int ll2 = ll / ( this.ss[col][jj].Length / this.ss[col2][jj].Length );
						if( this.ss[col2][jj][ll2].score != 0 )
						{
							doublePush = true;
							this.ss[col2][jj][ll2].Instantiated = true;
						}
					}
				}
			}
		}
		

		IconEnum[][] buttonToReact;
		switch( pushMode )
		{
		case PushMode.SeparateLeftAndRight:
			buttonToReact = new IconEnum[1][];
			buttonToReact[0] = new IconEnum[1];
			buttonToReact[0][0] = (IconEnum)col;
			break;
		case PushMode.TaikoNoTatsujin:
		{
			if( col == 0 )
			{
				buttonToReact = new IconEnum[1][];
				buttonToReact[0] = new IconEnum[1];
				buttonToReact[0][0] = (IconEnum)col;
			}
			else
			{
				int col2;
				if( col % 2 == 1 )
				{
					col2 = col+1;
				}
				else
				{
					col2 = col-1;
				}

				if( doublePush )
				{
					buttonToReact = new IconEnum[1][];
					buttonToReact[0] = new IconEnum[2];

					buttonToReact[0][0] = (IconEnum)(col);
					buttonToReact[0][1] = (IconEnum)(col2);
				}
				else
				{
					buttonToReact = new IconEnum[2][];
					buttonToReact[0] = new IconEnum[1];
					buttonToReact[1] = new IconEnum[1];

					buttonToReact[1][0] = (IconEnum)(col);
					buttonToReact[0][0] = (IconEnum)(col2);
				}
			}

			break;
		}
		default:
			buttonToReact = new IconEnum[0][];
			break;
		}

		ScoreSet scoreSet = this.ss[ii][jj][ll];

		GameObject gameObj = Instantiate( icon ) as GameObject;

		IconScore iconScore = gameObj.AddComponent<IconScore>();
		iconScore.Init(
			iconMgr.GetSendIcon(col).pos() ,
			iconMgr.GetSendIcon(col).size() ,
			iconMgr.GetReceiveIcon(col).pos() ,
			nextTime,
			col,
			pushMode,
			buttonToReact
			);

		listIconScore.Add( iconScore );


	}

	void InputUpdate()
	{
		//key input
		for( int i = 0 ; i < (int)IconEnum.Max ; i++ )
		{
			if( ( isUniduino && InputA.GetButton((IconEnum)i) ) ||
			   Input.GetKey( (KeyCode)((int)KeyCode.Alpha1 + i) )
			   ) inputButton[i]++;
			else inputButton[i] = 0;
		}

		listInputButton.Add((uint[])inputButton.Clone());
	}


	void OutPutInputButton()
	{
		string dateTime = DateTime.Now.Day + "" + DateTime.Now.Hour + "" + DateTime.Now.Minute + "" + DateTime.Now.Second;
		string fileName = "" + dateTime + ".txt";
		string fileNamePath = Application.dataPath + "/InputController/" + fileName;

		FileStream fs = new FileStream(
			fileNamePath,
			FileMode.Create,
			FileAccess.ReadWrite
			);
		fs.Close();

		FileInfo fi = new FileInfo(fileNamePath);
		StreamWriter sw = fi.AppendText();

		string str = "";
		for( int i = 0 ; i < listInputButton.Count ; i++ )
		{
			for( int j = 0 ; j < listInputButton[i].Length ; j++ )
			{
				str += "" + listInputButton[i][j] + ",";
			}

			sw.WriteLine(str);
			str = "";
		}

		sw.Close();

		Debug.Log(str);
	}

}

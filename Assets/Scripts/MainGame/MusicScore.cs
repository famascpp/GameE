using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum IconEnum
{
	hand = 0,
	lShoulder,
	rShoulder,
	lHip,
	rHip,
	lKnee,
	rKnee,
	Max,
}

public class MusicScore {

	public TextAsset musicScore;

	List<List<List<int>>> score;
	public List<List<List<int>>> Score {
		get { return score; }
	}

	public MusicScore(string textPath)
	{
		score = new List<List<List<int>>>();

		musicScore = Resources.Load(textPath) as TextAsset;

		char[] split = {'\n'};

		//改行でわける.
		string[] ms = musicScore.text.Split(split);

		int msNumCol = 0;	//小節列カウント.
		int measureNum = 0;	//小節比較用.

		foreach( string mst in ms )
		{
			//6未満なら違うもの
			if( mst.Length < 6 ) continue;
			//先頭が#じゃないなら.
			if( mst[0] != '#' ) continue;

			//01以外なら違う.
			if( ( mst[4] +""+ mst[5] ) != "01" ) continue;

			string measureNumStr = mst[1] +""+ mst[2] +""+ mst[3];

			int tMeasureNum = 0;
			try
			{
				tMeasureNum = int.Parse( measureNumStr );
			}
			catch(System.FormatException)
			{
				continue;
			}
			//次の小節に移ったら列を初期化.
			if( measureNum != tMeasureNum ) msNumCol = 0;

			//列が足りなければ増やす.
			while( score.Count <= msNumCol ) score.Add( new List<List<int>>() );

			//小節が足りなければ増やす.
			while( score[msNumCol].Count <= tMeasureNum ) score[msNumCol].Add( new List<int>() );

			for( int i = 7 ; mst[i] != '\r' ; i+=2 )
			{
				try
				{
					score[msNumCol][tMeasureNum].Add( int.Parse( mst[i] +""+ mst[i+1] ) );
				}
				catch(System.FormatException)
				{
					break;
				}
			}

			msNumCol++;
			measureNum = tMeasureNum;
		}

	}

	//譜面を列で返す.
	public List<List<int>> getScoreCol(IconEnum iconE )
	{
		return getScoreCol((int)iconE);
	}

	public List<List<int>> getScoreCol(int i )
	{
		return score[i];
	}

	

}

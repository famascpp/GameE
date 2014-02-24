using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO; //System.IO.FileInfo, System.IO.StreamReader, System.IO.StreamWriter
using System; //Exception
using System.Text; //Encoding

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

	List<List<List<int>>> score;
	public List<List<List<int>>> Score {
		get { return score; }
	}

	public MusicScore(string textPath)
	{
		string musicScore = ReadFile(textPath);

		score = new List<List<List<int>>>();

		char[] split = {'\n'};

		//split line
		string[] ms = musicScore.Split(split);

		int msNumCol = 0;	//Measure count.
		int measureNum = 0;	//Measure comparison.

		foreach( string mst in ms )
		{
			//
			if( mst.Length < 6 ) continue;

			//
			if( mst[0] != '#' ) continue;

			//
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

			//next measure
			if( measureNum != tMeasureNum ) msNumCol = 0;

			//col not enough
			while( score.Count <= msNumCol ) score.Add( new List<List<int>>() );

			//col add
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

	//return col
	public List<List<int>> getScoreCol(IconEnum iconE )
	{
		return getScoreCol((int)iconE);
	}

	public List<List<int>> getScoreCol(int i )
	{
		return score[i];
	}

	string ReadFile( string textPath )
	{
		string score = "";

		textPath = Application.dataPath + "/" + textPath;

		FileInfo fi = new FileInfo( textPath );

		StreamReader sr = new StreamReader(fi.OpenRead());
		score = sr.ReadToEnd();

		sr.Close();

		return score;
	}
	
}

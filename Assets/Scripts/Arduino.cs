﻿using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class Arduino : MonoBehaviour {
	
	private static SerialPort sp = new SerialPort("COM7",9600);
	private string myData;
	private static int i;
	
	// Use this for initialization
	void Start () {
		sp.Parity = System.IO.Ports.Parity.None;
		sp.DataBits = 8;
		sp.StopBits = System.IO.Ports.StopBits.One;
		//sp.NewLine = "\r\n";
		
		i = 32;
		myData = "1";
		OpenConnection();
	}
	
	// Update is called once per frame
	void Update () {
		/*
		myData = sp.ReadLine();
		if(int.TryParse(myData,out i))
			i = int.Parse(myData);
		else
			i = -1;
		
		if(i!=0) Debug.Log(i);
		*/
		StartCoroutine(arduino());
	}
	
	public IEnumerator arduino(){
		while(sp.IsOpen){
			myData = sp.ReadLine();
			if(int.TryParse(myData,out i))
				i = int.Parse(myData);
			else
				i = -1;
			
			if(i!=0) Debug.Log(i);
			
			yield return 0;
		}
	}
	
	void OnApplicationQuit()
	{
		sp.Close();
	}
	
	void OpenConnection(){
		if(sp != null){
			if(sp.IsOpen){
				sp.Close();
				Debug.LogError("Failed to open Serial Port, already open!");
			}else{
				sp.Open();
				//sp.DtrEnable = true;
				//sp.RtsEnable = true;
				//sp.ReadTimeout = 10;
				Debug.Log("Open Serial port");
			}
		}
	}

	public static int GetPin () {
		return i;
	}
}

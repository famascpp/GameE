using UnityEngine;
using System.Collections;
using System.IO.Ports;
using Uniduino;

public class ReadArduino : MonoBehaviour {

	const int bON = 1;
	const int bOFF = 0;
	const int NUM = 7;

	private static int i;
	private static bool drawLoadingFlag;    // true:ローディング画面.

	public Arduino arduino;
	private int[] buttonPin = {7,2,19,3,18,4,17};
	private int[] buttonState = {1,1,1,1,1,1,1};

	private int analogState = 0;
	int pinNum0 = 0, pinNum1 = 0; // 格納されているanalogPin.
	private bool[] inputButton = {false};

	public static ReadArduino instance;

	void Awake () {
		if( instance != null ){
			GameObject.Destroy( instance );
			return;
		}
		DontDestroyOnLoad( gameObject );
		instance = this;
	}
	// Use this for initialization
	void Start () {

		i = 0;
		drawLoadingFlag = true;

		arduino = Arduino.global;
		arduino.PortName = "COM4";
		arduino.Connect();
		arduino.Setup(arduinoSetup);
	}

	void arduinoSetup(){
		for(int j=0; j<NUM; j++){
			arduino.pinMode(buttonPin[j],PinMode.INPUT);
			arduino.digitalWrite(buttonPin[j], Arduino.HIGH);
			arduino.reportDigital((byte)(buttonPin[j]/8), 1);
		}
	}

	IEnumerator LoadingCheck(){
		yield return new WaitForSeconds(7.0f);

		drawLoadingFlag = false; // ローディング終了.
	
		for(int j=0; j<NUM; j++){
			buttonState[j] = arduino.digitalRead(buttonPin[j]);
		}
	}
	// Update is called once per frame
	void Update () {

		StartCoroutine(LoadingCheck());
		
		bool check = false;
		for(int j=0; j<NUM; j++){
			if( buttonState[j] == bOFF ){
				//analogState = arduino.analogRead(buttonPin[i]);
				//if(pinNum0 != buttonPin[j] && pinNum1 != buttonPin[j])
					i = buttonPin[j];
				check = true;
				if(pinNum0 == 0 && pinNum1 != buttonPin[j])pinNum0 = buttonPin[j];
				else if(pinNum1 == 0 && pinNum0 != buttonPin[j])pinNum1 = buttonPin[j];
			}
		}
		if( !check ){
			i = 0;
			pinNum0 = 0;
			pinNum1 = 0;
		}

		bool[] inbtn = new bool[buttonState.Length];
		for( int j=0; j < inbtn.Length ; j ++ )
		{
			if( buttonState[j] != 0 ) inbtn[j] = true;
			else inbtn[j] = false;
		}
		InputA.SetButton(inbtn);
	}

	// ローディング中かどうか取得.
	public static bool GetNowLoading(){
		return drawLoadingFlag;
	}
	// Pinの値を取得.
	public static int GetPin () {
		int n = i;
		i = 0;
		//Debug.Log("GetPin:"+n);
		
		return n;
	}
}

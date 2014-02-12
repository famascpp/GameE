using UnityEngine;
using System.Collections;
using System.IO.Ports;
using Uniduino;

public class ReadArduino : MonoBehaviour {

	const int bON = 1;
	const int bOFF = 0;
	const int NUM = 7;

	private static int i;

	public Arduino arduino;
	private int[] buttonPin = {2,3,4,17,18,19,7};
	private int[] buttonState = {1,1,1,1,1,1,1};
	private int analogState = 0;
	int pinNum0 = 0, pinNum1 = 0; // 格納されているanalogPin.

	// Use this for initialization
	void Start () {

		i = 0;

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

	IEnumerator Loop(){
		yield return new WaitForSeconds(7.0f);

		for(int j=0; j<NUM; j++){
			buttonState[j] = arduino.digitalRead(buttonPin[j]);
		}
	}
	// Update is called once per frame
	void Update () {

		StartCoroutine(Loop());
		
		bool check = false;
		for(int j=0; j<NUM; j++){
			if( buttonState[j] == bOFF ){
				//analogState = arduino.analogRead(buttonPin[i]);
				if(pinNum0 != buttonPin[j] && pinNum1 != buttonPin[j])i = buttonPin[j];
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
	}

	// Pinの値を取得.
	public static int GetPin () {
		int n = i;
		i = 0;
		Debug.Log("GetPin:"+n);
		
		return n;
	}
}

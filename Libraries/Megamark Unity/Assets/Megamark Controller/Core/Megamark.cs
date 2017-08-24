using CielaSpike;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO.Ports;
using System.Collections;

public class Megamark : MonoBehaviour {

	//Asynchronous threading variables
	public float initializationTime = 2; //Time before data is serialized and sent. Set inside inspector.
	public int refreshDelay = 5; //Time between each data sending in miliseconds
	private Task writerThread;
	private Task readerThread; 

	//Arduino variables
	private SerialPort serial;
	public string portName;
	public int timeout = 1000;
	public int baudrate = 57600;
	public bool connected = false;
	public bool connectOnAwake = false;

	// Incoming Data Parsing Variables
	string dataStr = "";
	private int dataIndex = 0;
	public int[] data = { 0, 0 }; // [ Left Laser, Right Laser ]

	// Wheels (-255 to 255)
	public int leftWheel, rightWheel = 0;
	// Shoulders (0 to 180)
	public int leftShoulder, rightShoulder = 90;
	// Elbows (0 to 180)
	public int leftElbow, rightElbow = 90;
	// Left Grippers (0 to 180)
	public int L0, L1, L2, L3, L4, L5 = 90;
	// Right Grippers (0 to 180)
	public int R0, R1, R2, R3, R4, R5 = 90;
	// Lasers (variable based on sensor, usually 0-1500 mm)
	public int leftLaser, rightLaser = -1;
		
	// Sets all servo values to their initial positions: both arms parallel to the ground to the left and right of the robot
	public void reset() {
		//Wheels (-255 to 255)
		leftWheel = 0;
		rightWheel = 0;
		//Shoulders (0 to 180)
		leftShoulder = 90; 
		rightShoulder = 90;
		//Elbows (0 to 180)
		leftElbow = 90; 
		rightElbow = 90;
		//Left Grippers (0 to 180)
		L0 = 90; L1 = 90; L2 = 90; L3 = 90; L4 = 90; L5 = 90;
		//Right Grippers (0-180)
		R0 = 90; R1 = 90; R2 = 90; R3 = 90; R4 = 90; L5 = 90;
		// Lasers (variable based on sensor, usually 0-1500 mm)
		leftLaser = -1;
		rightLaser = -1;
	}
	// Helper function that clamps all robot values
	public void ClampAll() {
		// Clamp Wheels
		leftWheel = Mathf.Clamp(leftWheel,-255,255);
		rightWheel = Mathf.Clamp(rightWheel,-255,255);
		// Clamp Shoulders
		leftShoulder = Mathf.Clamp(leftShoulder,0,180);
		rightShoulder = Mathf.Clamp(rightShoulder,0,180);
		// Clamp Elbows
		leftElbow = Mathf.Clamp(leftElbow,0,180);
		rightElbow = Mathf.Clamp(rightElbow,0,180);
		// Clamp Left Grippers 
		L0 = Mathf.Clamp(L0,0,180);
		L1 = Mathf.Clamp(L1,0,180);
		L2 = Mathf.Clamp(L2,0,180);
		L3 = Mathf.Clamp(L3,0,180);
		L4 = Mathf.Clamp(L4,0,180);
		L5 = Mathf.Clamp(L5,0,180);
		// Clamp Right Grippers 
		R0 = Mathf.Clamp(R0,0,180);
		R1 = Mathf.Clamp(R1,0,180);
		R2 = Mathf.Clamp(R2,0,180);
		R3 = Mathf.Clamp(R3,0,180);
		R4 = Mathf.Clamp(R4,0,180);
		R5 = Mathf.Clamp(R5,0,180);
	}
	// Helper function maps one range of values to another
	int Translate(float value, float inMin, float inMax, float outMin, float outMax) {
		// Clamp value within input range
		value = Mathf.Clamp(value, inMin, inMax);
		// Figure out how 'wide' each range is
		float inSpan = inMax - inMin;
		float outSpan = outMax - outMin;
		// Convert the left range into a 0-1 range (float)
		float valueScaled = (value - inMin) / inSpan;
		// Convert the 0-1 range into a value in the right range.
		return (int)(outMin + (valueScaled * outSpan)); 
	}

	// spin motors in meters/sec (-0.2 to 0.2 m/s)
	public void setWheelVelocities(float leftSpeed, float rightSpeed) {
		//convert speed to robot units
		leftWheel = Translate(leftSpeed, -0.2f, 0.2f, -255, 255);
		rightWheel = Translate(rightSpeed, -0.2f, 0.2f, -255, 255);
	}
		
	// rotate left shoulder in degrees (0 to 120 degrees)
	public void rotateLeftShoulder(float angle) {
		leftShoulder = Translate(angle, 0,120, 0,180);
	}
	// get left shoulder in degrees (0 to 120 degrees)
	public  float getLeftShoulder() {
		return Translate(leftShoulder, 0,180, 0,120);
	}

	// rotate right shoulder in degrees (0 to 120 degrees)
	public void rotateRightShoulder(float angle) {
		rightShoulder = Translate(angle, 0,120, 0,180);
	}
	// get right shoulder in degrees (0 to 120 degrees)
	public float getRightShoulder() {
		return Translate(rightShoulder, 0,180, 0,120);
	}

	// rotate left elbow in degrees (-60 to 60 degrees)
	public void rotateLeftElbow(float angle) {
		leftElbow = Translate(angle, -60,60, 0,180);
	}
	// get left elbow in degrees (-60 to 60 degrees)
	public float getLeftElbow() {
		return Translate(leftElbow, 0,180, -60,60);
	}

	// rotate right elbow in degrees (-60 to 60 degrees)
	public void rotateRightElbow(float angle) {
		rightElbow = Translate(angle, -60,60, 0,180);
	}
	// get right elbow in degrees (-60 to 60 degrees)
	public float getRightElbow() {
		return Translate(rightElbow, 0,180, -60,60);
	}

	// control left grippers (Up to 6 servos, all range from 0-180)
	public void setLeftGrippers(int l0 = 0,int l1 = 0,int l2 = 0,int l3 = 0,int l4 = 0, int l5 = 0) {
		L0 = l0; L1 = l1; L2 = l2; L3 = l3; L4 = l4; L5 = l5;
	}
	// control right grippers (Up to 6 servos, all range from 0-180)
	public void setRightGrippers(int r0 = 0,int r1 = 0,int r2 = 0,int r3 = 0,int r4 = 0, int r5 = 0) {
		R0 = r0; R1 = r1; R2 = r2; R3 = r3; R4 = r4; R5 = r5;
	}
	// get left laser sensor data (variable based on sensor, usually 0-1500 mm)
	public int getLeftLaser() {
		leftLaser = data[0];
		return leftLaser;
	}
	// get right laser sensor data (variable based on sensor, usually 0-1500 mm)
	public int getRightLaser() {
		rightLaser = data[1];
		return rightLaser;
	}	

	// Sends data to Arduino in Robot units, which updates position 
	public void write() {
		// Clamp all values before sending data.
		ClampAll();

		// Prepare data to send:
		//Wheels
		int[] send = { leftWheel, rightWheel, 
			//Shoulders
			leftShoulder, rightShoulder,
			//Elbows
			leftElbow, rightElbow,
			//Left Grippers
			L0, L1, L2, L3, L4, L5,
			//Right Grippers
			R0, R1, R2, R3, R4, R5 };

		// begin dataString
		string dataString = "{ ";
		// Add values to data list
		for (int i = 0; i < send.Length; i++) {
			// Add data as a string
			dataString += send[i].ToString();
			if (i < send.Length - 1) {
				dataString += ", "; // to next value
			} else {
				dataString += " }"; // finish string
			}
		}
		// send data to Arduino
		try {
			serial.Write(dataString);
		} catch (Exception e) {
			if (serial != null && serial.IsOpen) {
				//Debug.LogWarning (e);
				serial.DiscardInBuffer ();
				serial.DiscardOutBuffer ();
			}
		}
	}
	// Sends data to Arduino in Robot units, which updates position 
	public void read() {
		// Make backup if data in case parsing fails:
		int[] oldData = data;    
		// Read data
		try {
			String str = "";
			str = serial.ReadLine();
			for (int i = 0; i < str.Length; i++) {
				// get ith character of serial read string
				char chr = str[i];
				// begin parsing
				if(chr == '{') {
					dataStr = "";
					dataIndex = 0; 
				} 
				// extract number from data
				else if(chr == ',' || chr == '}') {
					data[dataIndex] = int.Parse(dataStr);
					dataIndex += 1; dataStr = "";
					// Finished reading message
					if(chr == '}') { 
						dataStr = "";
						dataIndex = 0;
					}
				}
				// add char to current string
				else if (!char.IsWhiteSpace(chr) && chr != '\n') {
					dataStr += chr;
				}
			}
		} catch(Exception e) {
			data = oldData;
			if (serial != null && serial.IsOpen) {
				//Debug.LogWarning (e);
				serial.DiscardInBuffer ();
				serial.DiscardOutBuffer ();
			}
		}
	}

	// A threaded loop that updates the Arduino through serial connection
	IEnumerator Writer(){
		//continuously update the serial writer thread.
		while (serial.IsOpen) {
			write(); 
			yield return new WaitForSeconds((float)refreshDelay / 1000);
		}
	}
	// A threaded loop that updates the Arduino through serial connection
	IEnumerator Reader(){
		//continuously update the serial reader thread.
		while (serial.IsOpen) {
			read();
			yield return new WaitForSeconds((float)refreshDelay / 1000);
		}
	}
	// Starts reader/writer threads.
	private void startThreads() {
		this.StartCoroutineAsync(Writer(), out writerThread);
		this.StartCoroutineAsync(Reader(), out readerThread);
	}

	// Attempt connection to Arduino through the specified port
	public void Connect () {
		if (!connected) {
			//Create a new Serial port
			serial = new SerialPort (portName, baudrate, Parity.None, 8, StopBits.One);
			serial.WriteTimeout = timeout;
			serial.ReadTimeout = timeout;
			try {
				Debug.Log ("Attempting to connect to robot at port " + portName + "...");
				serial.Open ();
				Debug.Log ("Successfully connected to robot at port " + portName + ".");
				connected = true;
				Invoke ("startThreads", initializationTime);
			} catch (System.Exception e) {
				Debug.LogException (e);
				Debug.Log ("Could not connect to robot at " + portName + "! Exiting.");
				Debug.Log ("(Check USB connection to Arduino Mega and restart program.)");
			}
			reset ();
		} else {
			Debug.Log ("Robot is already connected at " + portName + ".");
			Debug.Log ("(Use another port and/or another instance of the Megamark script.)");
		}
	}

	// Close the serial connection and destroy all serial threads
	public void Disconnect () {
		if (serial.IsOpen) {
			writerThread.Cancel();
			readerThread.Cancel();
			serial.Close();
			connected = false;
			Debug.Log("Disconnected.\n");
		}
	}
	// Making sure to disconnect threads on exit.
	public void OnApplicationQuit() { Disconnect(); }

	// If connectOnAwake is enabled, connect to the robot upon playing the program
	public void Start() { if (connectOnAwake) { Connect(); } }

}

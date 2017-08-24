using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This example tests the robot's wheels. The robot moves forward, 
 * moves backwards, rotates left, and finally rotates right. 
 * These motions run in an infinite loop. 
 */

public class Wheels : MonoBehaviour {

	public Megamark megamark;
	private IEnumerator coroutine;

	// Use this for initialization
	void Start () {
		coroutine = RotateWheels();
		StartCoroutine(coroutine);
	}

	// Tests moving forwards, moving backwards, rotating left, and rotating right.  
	IEnumerator RotateWheels() {
		while (true) {
			// Both wheels positive moves forward.
			Debug.Log("Moving forwards...");
			megamark.setWheelVelocities(0.2f,0.2f);
			yield return new WaitForSeconds (2);
			// Both wheels negative moves backward.
			Debug.Log("Moving backwards...");
			megamark.setWheelVelocities(-0.2f,-0.2f);
			yield return new WaitForSeconds (2);
			// Left wheel negative and right wheel positive rotates left.
			Debug.Log("Rotating left...");
			megamark.setWheelVelocities(-0.2f,0.2f);
			yield return new WaitForSeconds (2);
			// Left wheel positive and right wheel negative rotates right.
			Debug.Log("Rotating right...");
			megamark.setWheelVelocities(0.2f,-0.2f);
			yield return new WaitForSeconds (2);
		}
	}
		
}

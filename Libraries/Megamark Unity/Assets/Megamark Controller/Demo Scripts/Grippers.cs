using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This example opens and closes all left and right gripper servos, 6 each, 12 total.
 */

public class Grippers : MonoBehaviour {

	public Megamark megamark;
	private IEnumerator coroutine;

	// Use this for initialization
	void Start () {
		coroutine = RotateGrippers();
		StartCoroutine(coroutine);
	}

	// Opens and closes all gripper servos, switching directions every two seconds  
	IEnumerator RotateGrippers() {
		while (true) {
			// Fully open all left grippers.
			Debug.Log ("Opening all left gripper servos...");
			megamark.setLeftGrippers (180,180,180,180,180,180);
			yield return new WaitForSeconds (2);
			// Fully close all left grippers.
			Debug.Log ("Closing all left gripper servos...");
			megamark.setLeftGrippers (0,0,0,0,0,0);
			yield return new WaitForSeconds (2);
			// Fully open all right grippers.
			Debug.Log ("Opening all right gripper servos...");
			megamark.setRightGrippers (180,180,180,180,180,180);
			yield return new WaitForSeconds (2);
			// Fully close all right grippers.
			Debug.Log ("Closing all right gripper servos...");
			megamark.setRightGrippers (0,0,0,0,0,0);
			yield return new WaitForSeconds (2);
		}
	}
		
}

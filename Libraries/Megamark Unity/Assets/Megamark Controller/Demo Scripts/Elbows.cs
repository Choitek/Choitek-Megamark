using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This example rotates both left and right elbows up and down in a loop.
 */

public class Elbows : MonoBehaviour {

	public Megamark megamark;
	private IEnumerator coroutine;

	// Use this for initialization
	void Start () {
		coroutine = RotateElbows();
		StartCoroutine(coroutine);
	}

	// Rotate both elbows in and out, switching directions every two seconds  
	IEnumerator RotateElbows() {
		while (true) {
			// Rotate left elbow fully down.
			Debug.Log ("Rotating left elbow down...");
			megamark.rotateLeftElbow (-60);
			yield return new WaitForSeconds (2);
			// Rotate left elbow fully up.
			Debug.Log ("Rotating left elbow up...");
			megamark.rotateLeftElbow (60);
			yield return new WaitForSeconds (2);
			// Rotate right elbow fully down.
			Debug.Log ("Rotating right elbow down...");
			megamark.rotateRightElbow (-60);
			yield return new WaitForSeconds (2);
			// Rotate right elbow fully up.
			Debug.Log ("Rotating right elbow up...");
			megamark.rotateRightElbow (60);
			yield return new WaitForSeconds (2);
		}
	}
		
}

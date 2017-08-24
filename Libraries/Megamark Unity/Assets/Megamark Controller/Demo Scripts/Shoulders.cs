using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This example rotates both left and right shoulders in and out in a loop.
 */

public class Shoulders : MonoBehaviour {

	public Megamark megamark;
	private IEnumerator coroutine;

	// Use this for initialization
	void Start () {
		coroutine = RotateShoulders();
		StartCoroutine(coroutine);
	}

	// Rotate both shoulders in and out, switching directions every two seconds  
	IEnumerator RotateShoulders() {
		while (true) {
			// Rotate left shoulder fully out.
			Debug.Log ("Rotating left shoulder out...");
			megamark.rotateLeftShoulder (0);
			yield return new WaitForSeconds (2);
			// Rotate left shoulder fully in.
			Debug.Log ("Rotating left shoulder in...");
			megamark.rotateLeftShoulder (120);
			yield return new WaitForSeconds (2);
			// Rotate right shoulder fully out.
			Debug.Log ("Rotating right shoulder out...");
			megamark.rotateRightShoulder (0);
			yield return new WaitForSeconds (2);
			// Rotate right shoulder fully in.
			Debug.Log ("Rotating right shoulder in...");
			megamark.rotateRightShoulder (120);
			yield return new WaitForSeconds (2);
		}
	}
		
}

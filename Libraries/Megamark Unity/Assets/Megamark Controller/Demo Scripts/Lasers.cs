using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This example checks the range of two attached laser rangefinders.
 * The collected data is then printed via the Processing Console in real-time.
 */

/*** NOTE: THIS EXAMPLE DOES NOT WORK, AS UNITY'S SERIAL.READLINE COMMAND FREEZES UNITY! ***/

public class Lasers : MonoBehaviour {

	public Megamark megamark;
	private IEnumerator coroutine;

	// Use this for initialization
	void Start () {
		coroutine = GetLasers();
		StartCoroutine(coroutine);
	}

	// Get both laser rangefinder data and print them to the console window every second.  
	IEnumerator GetLasers() {
		while (true) {
			// Get the left range...
			int left = megamark.getLeftLaser();
			// Get the right range...
			int right = megamark.getRightLaser();
			// Convert both to strings and print to console.
			Debug.Log("Left Laser: " + left + " | Right Laser: " + right);
			yield return new WaitForSeconds (2);
		}
	}
		
}

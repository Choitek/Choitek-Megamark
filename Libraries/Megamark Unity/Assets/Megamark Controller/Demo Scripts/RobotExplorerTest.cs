using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This example demonstrates coordination between sensor data and actuator data.
 * The robot will move forward until it detects an obstacle in front of it with the laser.
 * While the obstacle is detected, the robot will rotate right until the obstacle is no longer detected. 
 * This example assumes there is a laser on pin A4 attached on the right gripper facing forward. 
 */

/*** NOTE: THIS EXAMPLE DOES NOT WORK, AS UNITY'S SERIAL.READLINE COMMAND FREEZES UNITY! ***/

public class RobotExplorerTest : MonoBehaviour {

	public Megamark megamark;
	private IEnumerator coroutine;

	public bool obstacleDetected = true;

	// Use this for initialization
	void Start () {
		coroutine = Explore();
		StartCoroutine(coroutine);
	}

	// Check whether an obstacle has been detected. If so, rotate to avoid obstacle.
	IEnumerator Explore() {
		while (true) {
			
			int right = megamark.getRightLaser();

			// move forward if no obstacle detected
			if (right > 200) {
				megamark.setWheelVelocities(0.2f,0.2f);
				if(obstacleDetected == true) {
					obstacleDetected = false;
					Debug.Log("No obstacle detected! Moving forward...");
				}
			}
			// rotate right if obstacle detected
			else {
				megamark.setWheelVelocities(0.2f,-0.2f);
				if(obstacleDetected == false) {
					obstacleDetected = true;
					Debug.Log("Obstacle detected at " + right + "mm! Rotating right...");
				}
			}

		}
	}
		
}

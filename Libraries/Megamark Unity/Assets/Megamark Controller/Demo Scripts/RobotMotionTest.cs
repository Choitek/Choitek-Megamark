using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This example tests all actuators on the robot: wheels, shoulders, elbows, and grippers.
 * The shoulders, elbows, and grippers all test their maximum range of motion. 
 * The wheels move forward, backwards, and rotate left and right. 
 */

public class RobotMotionTest : MonoBehaviour {

	public Megamark megamark;
	private IEnumerator coroutine;

	// Use this for initialization
	void Start () {
		coroutine = TestAllJoints();
		StartCoroutine(coroutine);
	}

	IEnumerator TestAllJoints() {

		Debug.Log("Beginning Robot Motion Test!");

		//-----------SHOULDERS------------//
		// Rotate left shoulder fully out.
		Debug.Log("Rotating left shoulder out...");
		megamark.rotateLeftShoulder(0);
		yield return new WaitForSeconds (2);
		// Rotate left shoulder fully in.
		Debug.Log("Rotating left shoulder in...");
		megamark.rotateLeftShoulder(120);
		yield return new WaitForSeconds (2);
		// Rotate right shoulder fully out.
		Debug.Log("Rotating right shoulder out...");
		megamark.rotateRightShoulder(0);
		yield return new WaitForSeconds (2);
		// Rotate right shoulder fully in.
		Debug.Log("Rotating right shoulder in...");
		megamark.rotateRightShoulder(120);
		yield return new WaitForSeconds (2);
		// Rotate both shoulders to middle. 
		Debug.Log("Rotating shoulders to middle...");
		megamark.rotateLeftShoulder(60);
		megamark.rotateRightShoulder(60);
		yield return new WaitForSeconds (2);

		//-----------ELBOWS------------//
		// Rotate left elbow fully down.
		Debug.Log("Rotating left elbow down...");
		megamark.rotateLeftElbow(-60);
		yield return new WaitForSeconds (2);
		// Rotate left elbow fully up.
		Debug.Log("Rotating left elbow up...");
		megamark.rotateLeftElbow(60);
		yield return new WaitForSeconds (2);
		// Rotate right elbow fully down.
		Debug.Log("Rotating right elbow down...");
		megamark.rotateRightElbow(-60);
		yield return new WaitForSeconds (2);
		// Rotate right elbow fully up.
		Debug.Log("Rotating right elbow up...");
		megamark.rotateRightElbow(60);
		yield return new WaitForSeconds (2);
		// Rotate both elbows to middle. 
		Debug.Log("Rotating elbows to middle...");
		megamark.rotateLeftElbow(0);
		megamark.rotateRightElbow(0);
		yield return new WaitForSeconds (2);

		//-----------GRIPPERS------------//
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

		//-----------WHEELS------------//
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
		// Left wheel positive and right wheel negative rotates right.
		Debug.Log("Stopping wheels...");
		megamark.setWheelVelocities(0.0f,0.0f);
		yield return new WaitForSeconds (2);

		// Finish the motion test and close the connection.
		Debug.Log("Finished Robot Motion Test!");

	}
		
}

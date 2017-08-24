using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This example allows the user to control the robot's shoulders, elbows, grippers and wheels 
 * with standard keyboard or joystick control. You can control the Input mappings in the 
 * Unity Input Manager in Edit -> Project Settings -> Input. See Unity's manual on game
 * input for more info: https://docs.unity3d.com/Manual/ConventionalGameInput.html
 */

public class RobotControllerTest : MonoBehaviour {

	public Megamark megamark;

	/* Control all of the robot's joints in realtime with keyboard or joystick control:
     * Default Keyboard Mappings:
     *     Up/Down = Vertical
     *     Left/Right = Horizontal
     *     a/d = Left Shoulder
     *     w/s = Left Elbow
     *     q/e = Left Grippers
     *     j/l = Right Shoulder
     *     i/k = Right Elbow
     *     u/o = Right Grippers
     */
	void Update() {
		//-----------SHOULDERS------------//
		megamark.leftShoulder = (int)((float)megamark.leftShoulder + Input.GetAxis("Left Shoulder"));
		megamark.rightShoulder = (int)((float)megamark.rightShoulder + Input.GetAxis("Right Shoulder"));

		//-----------ELBOWS------------//
		megamark.leftElbow = (int)((float)megamark.leftElbow + Input.GetAxis("Left Elbow"));
		megamark.rightElbow = (int)((float)megamark.rightElbow + Input.GetAxis("Right Elbow"));

		//-----------GRIPPERS------------//
		megamark.L0 = (int)((float)megamark.L0 + Input.GetAxis("Left Grippers"));
		megamark.L1 = (int)((float)megamark.L1 + Input.GetAxis("Left Grippers"));
		megamark.L2 = (int)((float)megamark.L2 + Input.GetAxis("Left Grippers"));
		megamark.L3 = (int)((float)megamark.L3 + Input.GetAxis("Left Grippers"));
		megamark.L4 = (int)((float)megamark.L4 + Input.GetAxis("Left Grippers"));
		megamark.L5 = (int)((float)megamark.L5 + Input.GetAxis("Left Grippers"));
		megamark.R0 = (int)((float)megamark.R0 + Input.GetAxis("Right Grippers"));
		megamark.R1 = (int)((float)megamark.R1 + Input.GetAxis("Right Grippers"));
		megamark.R2 = (int)((float)megamark.R2 + Input.GetAxis("Right Grippers"));
		megamark.R3 = (int)((float)megamark.R3 + Input.GetAxis("Right Grippers"));
		megamark.R4 = (int)((float)megamark.R4 + Input.GetAxis("Right Grippers"));
		megamark.R5 = (int)((float)megamark.R5 + Input.GetAxis("Right Grippers"));

		//-----------WHEELS------------//
		float forward = Input.GetAxis ("Vertical");
		float turning = Input.GetAxis ("Horizontal");
		megamark.setWheelVelocities(forward + turning, forward - turning);
	}
		
}

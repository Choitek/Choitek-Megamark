using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulator : MonoBehaviour {

	public Megamark megamark;
	public float rotateSpeed;

	public Transform leftShoulder;
	public float leftShoulderOffset;
	private float leftShoulderAngle = 45;

	public Transform rightShoulder;
	public float rightShoulderOffset;
	private float rightShoulderAngle = 45;

	public Transform leftElbow;
	public float leftElbowOffset;
	private float leftElbowAngle = 0;

	public Transform rightElbow;
	public float rightElbowOffset;
	private float rightElbowAngle = 0;

	public Transform leftWheel;
	public Transform rightWheel;
	public float wheelSpeed;

	public Transform leftFan;
	public Transform rightFan;
	public float fanSpeed;

	// Update is called once per frame
	void Update () {
		//rotate shoulders
		leftShoulderAngle = Mathf.Lerp(leftShoulderAngle, megamark.getLeftShoulder() + leftShoulderOffset, Time.deltaTime*rotateSpeed);
		rightShoulderAngle = Mathf.Lerp(rightShoulderAngle, -megamark.getRightShoulder() + rightShoulderOffset, Time.deltaTime*rotateSpeed);
		leftShoulder.localEulerAngles = new Vector3(leftShoulder.localEulerAngles.x, leftShoulderAngle,leftShoulder.localEulerAngles.z);
		rightShoulder.localEulerAngles = new Vector3(rightShoulder.localEulerAngles.x, rightShoulderAngle,rightShoulder.localEulerAngles.z);

		//rotate elbows 
	    leftElbowAngle = Mathf.Lerp(leftElbowAngle, -megamark.getLeftElbow() + leftElbowOffset, Time.deltaTime*rotateSpeed);
		rightElbowAngle = Mathf.Lerp(rightElbowAngle, megamark.getRightElbow() + rightElbowOffset, Time.deltaTime*rotateSpeed);
		leftElbow.localEulerAngles = new Vector3(leftElbowAngle, leftElbow.localEulerAngles.y, leftElbow.localEulerAngles.z);
		rightElbow.localEulerAngles = new Vector3(rightElbowAngle, rightElbow.localEulerAngles.y, rightElbow.localEulerAngles.z);

		//rotate wheels
		leftWheel.Rotate(0,0,megamark.leftWheel*wheelSpeed);
		rightWheel.Rotate(0,0,megamark.rightWheel*wheelSpeed);

		//rotate fans
		leftFan.Rotate(Time.deltaTime*fanSpeed,0,0);
		rightFan.Rotate(-Time.deltaTime*fanSpeed,0,0);
	}
}

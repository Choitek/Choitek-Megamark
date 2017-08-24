#pragma config(CircuitBoardType, typeCktBoardMega)
#pragma config(Motor,  servo_2,         leftShoulder,  tmotorServoStandard, openLoop, reversed, IOPins, dgtl38, None)
#pragma config(Motor,  servo_3,         rightShoulder, tmotorServoStandard, openLoop, IOPins, dgtl22, None)
#pragma config(Motor,  servo_5,         leftElbow,     tmotorServoStandard, openLoop, reversed, IOPins, dgtl40, None)
#pragma config(Motor,  servo_6,         rightElbow,    tmotorServoStandard, openLoop, IOPins, dgtl24, None)
#pragma config(Motor,  servo_7,         L1,            tmotorServoStandard, openLoop, IOPins, dgtl42, None)
#pragma config(Motor,  servo_8,         L2,            tmotorServoStandard, openLoop, IOPins, dgtl44, None)
#pragma config(Motor,  servo_11,        L3,            tmotorServoStandard, openLoop, IOPins, dgtl46, None)
#pragma config(Motor,  servo_12,        L4,            tmotorServoStandard, openLoop, IOPins, dgtl48, None)
#pragma config(Motor,  servo_13,        R1,            tmotorServoStandard, openLoop, IOPins, dgtl26, None)
#pragma config(Motor,  motor_4,         R2,            tmotorServoStandard, openLoop, IOPins, dgtl28, None)
#pragma config(Motor,  motor_9,         R3,            tmotorServoStandard, openLoop, IOPins, dgtl30, None)
#pragma config(Motor,  motor_10,        R4,            tmotorServoStandard, openLoop, IOPins, dgtl32, None)
#pragma config(Sensor, dgtl5,  leftDir,           sensorDigitalOut)
#pragma config(Sensor, dgtl4,  leftOn,           sensorDigitalOut)
#pragma config(Sensor, dgtl3,  rightDir,           sensorDigitalOut)
#pragma config(Sensor, dgtl2,  rightOn,           sensorDigitalOut)

/* This example tests all actuators on the robot: wheels, shoulders, elbows, and grippers.
 * The shoulders, elbows, and grippers all test their maximum range of motion.
 * The wheels move forward, backwards, and rotate left and right.
 * This chain of motions loop indefinitely. */

task main()
{
  while(true)
  {
    ////----------SHOULDERS----------////
    // Rotate left shoulder fully out.
    motor[leftShoulder] = -127;
    wait1Msec(2000);
    // Rotate left shoulder fully in.
    motor[leftShoulder] = 127;
    wait1Msec(2000);
    // Rotate right shoulder fully out.
    motor[rightShoulder] = -127;
    wait1Msec(2000);
    // Rotate right shoulder fully in.
    motor[rightShoulder] = 127;
    wait1Msec(2000);
    // Rotate both shoulders to middle.
    motor[leftShoulder] = 0;
    motor[rightShoulder] = 0;
    wait1Msec(2000);

    ////----------ELBOWS----------////
    // Rotate left elbow fully down.
    motor[leftElbow] = -127;
    wait1Msec(2000);
    // Rotate left elbow fully up.
    motor[leftElbow] = 127;
    wait1Msec(2000);
    // Rotate right elbow fully down.
    motor[rightElbow] = -127;
    wait1Msec(2000);
    // Rotate right elbow fully up.
    motor[rightElbow] = 127;
    wait1Msec(2000);
    // Rotate both elbows to middle.
    motor[leftElbow] = 0;
    motor[rightElbow] = 0;
    wait1Msec(2000);

    ////----------GRIPPERS----------////
    // Opening all left gripper servos.
    motor[L1] = -127;
    motor[L2] = -127;
    motor[L3] = -127;
    motor[L4] = -127;
    wait1Msec(2000);
    // Closing all left gripper servos.
    motor[L1] = 127;
    motor[L2] = 127;
    motor[L3] = 127;
    motor[L4] = 127;
    wait1Msec(2000);
    // Opening all right gripper servos.
    motor[R1] = -127;
    motor[R2] = -127;
    motor[R3] = -127;
    motor[R4] = -127;
    wait1Msec(2000);
    // Closing all right gripper servos.
    motor[R1] = 127;
    motor[R2] = 127;
    motor[R3] = 127;
    motor[R4] = 127;
    wait1Msec(2000);

    ////----------WHEELS----------////
  	//Moving forward.
    SensorValue[leftDir]  = true;  //Left wheel forward direction.
    SensorValue[leftOn]   = true;  //Left wheel full speed.
    SensorValue[rightDir] = true;  //Right wheel forward direction.
    SensorValue[rightOn]  = true;  //Right wheel full speed.
    wait1Msec(2000);
    //Moving backwards.
    SensorValue[leftDir]  = false; //Left wheel backward direction.
    SensorValue[leftOn]   = true;  //Left wheel full speed.
    SensorValue[rightDir] = false; //Right wheel backward direction.
    SensorValue[rightOn]  = true;  //Right wheel full speed.
    wait1Msec(2000);
    //Rotating left.
    SensorValue[leftDir]  = false; //Left wheel backward direction.
    SensorValue[leftOn]   = true;  //Left wheel full speed.
    SensorValue[rightDir] = true;  //Right wheel forward direction.
    SensorValue[rightOn]  = true;  //Right wheel full speed.
    wait1Msec(2000);
    //Rotating right.
    SensorValue[leftDir]  = true;  //Left wheel forward direction.
    SensorValue[leftOn]   = true;  //Left wheel full speed.
    SensorValue[rightDir] = false; //Right wheel backward direction.
    SensorValue[rightOn]  = true;  //Right wheel full speed.
    wait1Msec(2000);
    //Stop moving.
    SensorValue[leftDir]  = true;  //Left wheel forward direction.
    SensorValue[leftOn]   = false; //Left wheel no speed.
    SensorValue[rightDir] = true;  //Right wheel forward direction.
    SensorValue[rightOn]  = false; //Right wheel no speed.
    wait1Msec(2000);
  }
}

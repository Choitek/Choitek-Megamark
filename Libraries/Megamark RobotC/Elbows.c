#pragma config(CircuitBoardType, typeCktBoardMega)
#pragma config(Motor,  servo_5,         leftElbow,     tmotorServoStandard, openLoop, reversed, IOPins, dgtl40, None)
#pragma config(Motor,  servo_6,         rightElbow,    tmotorServoStandard, openLoop, IOPins, dgtl24, None)

/* This example rotates both left and right elbows up and down in a loop. */

task main()
{
  while(true)
  {
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
  }
}

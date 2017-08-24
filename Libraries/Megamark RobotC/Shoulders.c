#pragma config(CircuitBoardType, typeCktBoardMega)
#pragma config(Motor,  servo_2,         leftShoulder,  tmotorServoStandard, openLoop, reversed, IOPins, dgtl38, None)
#pragma config(Motor,  servo_3,         rightShoulder, tmotorServoStandard, openLoop, IOPins, dgtl22, None)

/* This example rotates both left and right shoulders in and out in a loop. */

task main()
{
  while(true)
  {
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
  }
}

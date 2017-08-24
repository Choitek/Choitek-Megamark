#pragma config(CircuitBoardType, typeCktBoardMega)
#pragma config(Motor,  servo_7,         L1,            tmotorServoStandard, openLoop, IOPins, dgtl42, None)
#pragma config(Motor,  servo_8,         L2,            tmotorServoStandard, openLoop, IOPins, dgtl44, None)
#pragma config(Motor,  servo_11,        L3,            tmotorServoStandard, openLoop, IOPins, dgtl46, None)
#pragma config(Motor,  servo_12,        L4,            tmotorServoStandard, openLoop, IOPins, dgtl48, None)
#pragma config(Motor,  servo_13,        R1,            tmotorServoStandard, openLoop, IOPins, dgtl26, None)
#pragma config(Motor,  motor_4,         R2,            tmotorServoStandard, openLoop, IOPins, dgtl28, None)
#pragma config(Motor,  motor_9,         R3,            tmotorServoStandard, openLoop, IOPins, dgtl30, None)
#pragma config(Motor,  motor_10,        R4,            tmotorServoStandard, openLoop, IOPins, dgtl32, None)

/* This example rotates opens and closes all gripper servos, 4 for each arm, up to 8 total. */

task main()
{
  while(true)
  {
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
  }
}

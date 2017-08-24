#pragma config(CircuitBoardType, typeCktBoardMega)
#pragma config(Motor,  servo_3,         rightShoulder, tmotorServoStandard, openLoop, IOPins, dgtl22, None)
#pragma config(Motor,  servo_6,         rightElbow,    tmotorServoStandard, openLoop, IOPins, dgtl24, None)
#pragma config(Motor,  servo_13,        R1,            tmotorServoStandard, openLoop, IOPins, dgtl26, None)
#pragma config(Motor,  motor_4,         R2,            tmotorServoStandard, openLoop, IOPins, dgtl28, None)
#pragma config(Motor,  motor_9,         R3,            tmotorServoStandard, openLoop, IOPins, dgtl30, None)
#pragma config(Motor,  motor_10,        R4,            tmotorServoStandard, openLoop, IOPins, dgtl32, None)
#pragma config(Sensor, dgtl5,  leftDir,           sensorDigitalOut)
#pragma config(Sensor, dgtl4,  leftOn,           sensorDigitalOut)
#pragma config(Sensor, dgtl3,  rightDir,           sensorDigitalOut)
#pragma config(Sensor, dgtl2,  rightOn,           sensorDigitalOut)
#pragma config(Sensor, anlg4,  rightLaser,     sensorAnalog)

/* This example demonstrates coordination between sensor data and actuator data.
 * The robot will move forward until it detects an obstacle in front of it with the laser.
 * While the obstacle is detected, the robot will rotate right until the obstacle is no longer detected.
 * This example assumes there is a laser on pin A4 attached on the right gripper facing forward. */

task main()
{
  while(true)
  {
    //move forward if no obstacle is detected
    if(SensorValue[rightLaser] > 200)
    {
      SensorValue[leftDir]  = true;  //Left wheel forward direction.
      SensorValue[leftOn]   = true;  //Left wheel full speed.
      SensorValue[rightDir] = true;  //Right wheel forward direction.
      SensorValue[rightOn]  = true;  //Right wheel full speed.
    }
    //rotate right if obstacle is detected
    else
    {
      SensorValue[leftDir]  = true;  //Left wheel forward direction.
      SensorValue[leftOn]   = true;  //Left wheel full speed.
      SensorValue[rightDir] = false; //Right wheel backward direction.
      SensorValue[rightOn]  = true;  //Right wheel full speed.
    }
  }
}

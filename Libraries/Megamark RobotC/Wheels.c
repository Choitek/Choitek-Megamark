#pragma config(CircuitBoardType, typeCktBoardMega)
#pragma config(Sensor, dgtl5,  leftDir,           sensorDigitalOut)
#pragma config(Sensor, dgtl4,  leftOn,           sensorDigitalOut)
#pragma config(Sensor, dgtl3,  rightDir,           sensorDigitalOut)
#pragma config(Sensor, dgtl2,  rightOn,           sensorDigitalOut)

/* This example tests the robot's wheels. The robot moves forward,
 * moves backwards, rotates left, and finally rotates right.
 * These motors run in an infinite loop. */

task main()
{
  while(true)
  {
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

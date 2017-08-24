#pragma config(CircuitBoardType, typeCktBoardMega)
#pragma config(Sensor, anlg3,  leftLaser,      sensorAnalog)
#pragma config(Sensor, anlg4,  rightLaser,     sensorAnalog)

/* This example checks the range of two attached laser rangefinders.
 * The collected data is then printed to the Debug Stream in real-time. */

task main()
{
  int leftValue;
  int rightValue;

  while(true)
  {
    leftValue = SensorValue[leftLaser];       //Get the left value.
    rightValue = SensorValue[rightLaser];     //Get the right value.
    writeDebugStream("[ %d , ", leftValue);   //Show the left value.
    writeDebugStreamLine("%d ]", rightValue); //Show the right value.
    wait1Msec(100);
  }
}

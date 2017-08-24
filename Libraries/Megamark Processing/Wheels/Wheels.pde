/*
 * This example tests the robot's wheels. The robot moves forward, 
 * moves backwards, rotates left, and finally rotates right. 
 * These motions run in an infinite loop. 
 */

void start() {
  /* Change 'COM3' to your COM Port. This is something like "/dev/ttyACM0" on Linux and Mac.
   * If you don't know what your COM Port is, plug in the USB to the Arduino Mega 2560 and
   * follow these instructions: https://www.arduino.cc/en/Guide/Troubleshooting#toc1 */
  init("COM3");
}

// Tests moving forwards, moving backwards, rotating left, and rotating right.  
void draw() {
  
  // Both wheels positive moves forward.
  println("Moving forwards...");
  setWheelVelocities(0.2,0.2);
  delay(2000);
  // Both wheels negative moves backward.
  println("Moving backwards...");
  setWheelVelocities(-0.2,-0.2);
  delay(2000);
  // Left wheel negative and right wheel positive rotates left.
  println("Rotating left...");
  setWheelVelocities(-0.2,0.2);
  delay(2000);
  // Left wheel positive and right wheel negative rotates right.
  println("Rotating right...");
  setWheelVelocities(0.2,-0.2);
  delay(2000);

}
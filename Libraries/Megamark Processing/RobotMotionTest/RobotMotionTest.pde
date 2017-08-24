/*
 * This example tests all actuators on the robot: wheels, shoulders, elbows, and grippers.
 * The shoulders, elbows, and grippers all test their maximum range of motion. 
 * The wheels move forward, backwards, and rotate left and right. 
 */

void start() {
  /* Change 'COM3' to your COM Port. This is something like "/dev/ttyACM0" on Linux and Mac.
   * If you don't know what your COM Port is, plug in the USB to the Arduino Mega 2560 and
   * follow these instructions: https://www.arduino.cc/en/Guide/Troubleshooting#toc1 */
  init("COM3");

  println("Beginning Robot Motion Test!");
  
  //-----------SHOULDERS------------//
  // Rotate left shoulder fully out.
  println("Rotating left shoulder out...");
  rotateLeftShoulder(0);
  delay(2000);
  // Rotate left shoulder fully in.
  println("Rotating left shoulder in...");
  rotateLeftShoulder(120);
  delay(2000);
  // Rotate right shoulder fully out.
  println("Rotating right shoulder out...");
  rotateRightShoulder(0);
  delay(2000);
  // Rotate right shoulder fully in.
  println("Rotating right shoulder in...");
  rotateRightShoulder(120);
  delay(2000);
  // Rotate both shoulders to middle. 
  println("Rotating shoulders to middle...");
  rotateLeftShoulder(60);
  rotateRightShoulder(60);
  delay(2000);
  
  //-----------ELBOWS------------//
  // Rotate left elbow fully down.
  println("Rotating left elbow down...");
  rotateLeftElbow(-60);
  delay(2000);
  // Rotate left elbow fully up.
  println("Rotating left elbow up...");
  rotateLeftElbow(60);
  delay(2000);
  // Rotate right elbow fully down.
  println("Rotating right elbow down...");
  rotateRightElbow(-60);
  delay(2000);
  // Rotate right elbow fully up.
  println("Rotating right elbow up...");
  rotateRightElbow(60);
  delay(2000);
  // Rotate both elbows to middle. 
  println("Rotating elbows to middle...");
  rotateLeftElbow(0);
  rotateRightElbow(0);
  delay(2000);
  
  //-----------GRIPPERS------------//
  // Fully open all left grippers.
  println("Opening all left gripper servos...");
  for(int i = 0; i < 6; i++) { setLeftGripper(i, 180); }
  delay(2000);
  // Fully close all left grippers.
  println("Closing all left gripper servos...");
  for(int i = 0; i < 6; i++) { setLeftGripper(i, 0); }
  delay(2000);
  // Fully open all right grippers.
  println("Opening all right gripper servos...");
  for(int i = 0; i < 6; i++) { setRightGripper(i, 180); }
  delay(2000);
  // Fully close all right grippers.
  println("Closing all right gripper servos...ln");
  for(int i = 0; i < 6; i++) { setRightGripper(i, 0); }
  delay(2000);
  
  //-----------WHEELS------------//
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
  // Left wheel positive and right wheel negative rotates right.
  println("Stopping wheels...");
  setWheelVelocities(0.0,0.0);
  delay(2000);
  
  // Finish the motion test and close the connection.
  println("Finished Robot Motion Test!");
  delay(2000);
  stop();
  exit();

}
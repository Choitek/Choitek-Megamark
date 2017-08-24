/*
 * This example opens and closes all left and right gripper servos, 6 each, 12 total.
 */

void start() {
  /* Change 'COM3' to your COM Port. This is something like "/dev/ttyACM0" on Linux and Mac.
   * If you don't know what your COM Port is, plug in the USB to the Arduino Mega 2560 and
   * follow these instructions: https://www.arduino.cc/en/Guide/Troubleshooting#toc1 */
  init("COM3");
}

// Opens and closes all gripper servos, switching directions every two seconds  
void draw() {
  
  // Fully open all left grippers.
  println("Opening all left gripper servos...");
  for(int i = 0; i < 6; i++) { setLeftGripper(i,180); }
  delay(2000);
  // Fully close all left grippers.
  println("Closing all left gripper servos...");
  for(int i = 0; i < 6; i++) { setLeftGripper(i,0); }
  delay(2000);
  // Fully open all right grippers.
  println("Opening all right gripper servos...");
  for(int i = 0; i < 6; i++) { setRightGripper(i,180); }
  delay(2000);
  // Fully close all right grippers.
  println("Closing all right gripper servos...");
  for(int i = 0; i < 6; i++) { setRightGripper(i,0); }
  delay(2000);

}
/*
 * This example rotates both left and right elbows up and down in a loop.
 */

void start() {
  /* Change 'COM3' to your COM Port. This is something like "/dev/ttyACM0" on Linux and Mac.
   * If you don't know what your COM Port is, plug in the USB to the Arduino Mega 2560 and
   * follow these instructions: https://www.arduino.cc/en/Guide/Troubleshooting#toc1 */
  init("COM3");
}

// Rotate both elbows in and out, switching directions every two seconds  
void draw() {
  
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
  
}
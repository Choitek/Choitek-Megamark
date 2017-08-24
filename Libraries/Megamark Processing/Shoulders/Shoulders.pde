/*
 * This example rotates both left and right shoulders in and out in a loop.
 */

void start() {
  /* Change 'COM3' to your COM Port. This is something like "/dev/ttyACM0" on Linux and Mac.
   * If you don't know what your COM Port is, plug in the USB to the Arduino Mega 2560 and
   * follow these instructions: https://www.arduino.cc/en/Guide/Troubleshooting#toc1 */
  init("COM3");
}

// Rotate both shoulders in and out, switching directions every two seconds  
void draw() {
  
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
  
}
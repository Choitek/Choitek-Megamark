/*
 * This example checks the range of two attached laser rangefinders.
 * The collected data is then printed via the Processing Console in real-time.
 */

void start() {
  /* Change 'COM3' to your COM Port. This is something like "/dev/ttyACM0" on Linux and Mac.
   * If you don't know what your COM Port is, plug in the USB to the Arduino Mega 2560 and
   * follow these instructions: https://www.arduino.cc/en/Guide/Troubleshooting#toc1 */
  init("COM3");
}

// Get both laser rangefinder data and print them to the console window every second.  
void draw() {
  
  // Get the left range...
  int left = getLeftLaser();
  // Get the right range...
  int right = getRightLaser();
  // Convert both to strings and print to console.
  println("Left Laser: " + str(left) + " | Right Laser: " + str(right));
  delay(1000);

}
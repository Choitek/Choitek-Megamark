/*
 * This example demonstrates coordination between sensor data and actuator data.
 * The robot will move forward until it detects an obstacle in front of it with the laser.
 * While the obstacle is detected, the robot will rotate right until the obstacle is no longer detected. 
 * This example assumes there is a laser on pin A4 attached on the right gripper facing forward. 
 */

boolean obstacleDetected = true;

void start() {
  /* Change 'COM3' to your COM Port. This is something like "/dev/ttyACM0" on Linux and Mac.
   * If you don't know what your COM Port is, plug in the USB to the Arduino Mega 2560 and
   * follow these instructions: https://www.arduino.cc/en/Guide/Troubleshooting#toc1 */
  init("COM3");
}

// Check whether an obstacle has been detected. If so, rotate to avoid obstacle.
void draw() {
      
  int right = getRightLaser();
  
  // move forward if no obstacle detected
  if (right > 200) {
    setWheelVelocities(0.2,0.2);
    if(obstacleDetected == true) {
      obstacleDetected = false;
      println("No obstacle detected! Moving forward...");
    }
  }
  // rotate right if obstacle detected
  else {
    setWheelVelocities(0.2,-0.2);
    if(obstacleDetected == false) {
      obstacleDetected = true;
      println("Obstacle detected at " + str(right) + "mm! Rotating right...");
    }
  }
  
}
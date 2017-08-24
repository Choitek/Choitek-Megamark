'''
This example demonstrates coordination between sensor data and actuator data.
The robot will move forward until it detects an obstacle in front of it with the laser.
While the obstacle is detected, the robot will rotate right until the obstacle is no longer detected. 
This example assumes there is a laser on pin A4 attached on the right gripper facing forward. 
'''

### Load Libraries ###
from Megamark import Megamark #Contains all Megamark robot control definitions.
                              #Put Megamark.py in the same directory as this script.

import time                   #Used to create delays for proper timing of robot actions.
                              #More info: https://docs.python.org/2/library/time.html

#Change 'COM3' to your COM Port. This is something like "/dev/ttyACM0" on Linux and Mac.
#If you don't know what your COM Port is, plug in the USB to the Arduino Mega 2560 and
#follow these instructions: https://www.arduino.cc/en/Guide/Troubleshooting#toc1
megamark = Megamark('COM3') 

obstacleDetected = True 

#Check whether an obstacle has been detected. If so, rotate to avoid obstacle.
while(True):
  
  rightLaser = megamark.getRightLaser()
  #move forward if no obstacle detected
  if (rightLaser > 200):
    megamark.setWheelVelocities(0.2,0.2)
    if(obstacleDetected == True):
      obstacleDetected = False
      print("No obstacle detected! Moving forward...")
  #rotate right if obstacle detected
  else:
    megamark.setWheelVelocities(0.2,-0.2)
    if(obstacleDetected == False):
      obstacleDetected = True
      print("Obstacle detected at " + str(rightLaser) + "mm! Rotating right...")

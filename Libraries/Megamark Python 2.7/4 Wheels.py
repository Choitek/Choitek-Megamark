'''
This example tests the robot's wheels. The robot moves forward, 
moves backwards, rotates left, and finally rotates right. 
These motions run in an infinite loop. 
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

#Tests moving forwards, moving backwards, rotating left, and rotating right.
while(True):
  # Both wheels positive moves forward.
  print("Moving forwards...")
  megamark.setWheelVelocities(0.2,0.2);
  time.sleep(2)
  # Both wheels negative moves backward.
  print("Moving backwards...")
  megamark.setWheelVelocities(-0.2,-0.2);
  time.sleep(2)
  # Left wheel negative and right wheel positive rotates left.
  print("Rotating left...")
  megamark.setWheelVelocities(-0.2,0.2);
  time.sleep(2)
  # Left wheel positive and right wheel negative rotates right.
  print("Rotating right...")
  megamark.setWheelVelocities(0.2,-0.2);
  time.sleep(2)


'''
This example opens and closes all left and right gripper servos, 6 each, 12 total.
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

#Opens and closes all gripper servos, switching directions every two seconds
while(True):
  # Fully open all left grippers.
  print("Opening all left gripper servos...")
  megamark.setLeftGrippers(180,180,180,180,180,180)
  time.sleep(2)
  # Fully close all left grippers.
  print("Closing all left gripper servos...")
  megamark.setLeftGrippers(0,0,0,0,0,0)
  time.sleep(2)
  # Fully open all right grippers.
  print("Opening all right gripper servos...")
  megamark.setRightGrippers(180,180,180,180,180,180)
  time.sleep(2)
  # Fully close all right grippers.
  print("Closing all right gripper servos...")
  megamark.setRightGrippers(0,0,0,0,0,0)
  time.sleep(2)

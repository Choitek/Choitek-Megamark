'''
This example rotates both left and right shoulders in and out in a loop.
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

#Rotate both shoulders in and out, switching directions every two seconds
while(True):
  # Rotate left shoulder fully out.
  print("Rotating left shoulder out...")
  megamark.rotateLeftShoulder(0)
  time.sleep(2)
  # Rotate left shoulder fully in.
  print("Rotating left shoulder in...")
  megamark.rotateLeftShoulder(120)
  time.sleep(2)
  # Rotate right shoulder fully out.
  print("Rotating right shoulder out...")
  megamark.rotateRightShoulder(0)
  time.sleep(2)
  # Rotate right shoulder fully in.
  print("Rotating right shoulder in...")
  megamark.rotateRightShoulder(120)
  time.sleep(2)










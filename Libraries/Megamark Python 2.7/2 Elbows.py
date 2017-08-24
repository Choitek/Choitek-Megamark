'''
This example rotates both left and right elbows up and down in a loop.
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

#Rotate both elbows in and out, switching directions every two seconds
while(True):
  # Rotate left elbow fully down.
  print("Rotating left elbow down...")
  megamark.rotateLeftElbow(-60)
  time.sleep(2)
  # Rotate left elbow fully up.
  print("Rotating left elbow up...")
  megamark.rotateLeftElbow(60)
  time.sleep(2)
  # Rotate right elbow fully down.
  print("Rotating right elbow down...")
  megamark.rotateRightElbow(-60)
  time.sleep(2)
  # Rotate right elbow fully up.
  print("Rotating right elbow up...")
  megamark.rotateRightElbow(60)
  time.sleep(2)










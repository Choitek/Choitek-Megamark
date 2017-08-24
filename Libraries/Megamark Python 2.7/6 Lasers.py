'''
This example checks the range of two attached laser rangefinders.
The collected data is then printed via the Python Shell in real-time.
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

#Get both laser rangefinder data and print them to the console window every second.
while(True):
  # Get the left range...
  left = megamark.getLeftLaser()
  # Get the right range...
  right = megamark.getRightLaser()
  # Convert both to strings and print to console.
  print("Left Laser: " + str(left) + " | Right Laser: " + str(right))
  time.sleep(1)

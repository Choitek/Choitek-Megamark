'''
This example tests all actuators on the robot: wheels, shoulders, elbows, and grippers.
The shoulders, elbows, and grippers all test their maximum range of motion. 
The wheels move forward, backwards, and rotate left and right. 
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

print("Beginning Robot Motion Test!")

###-----------SHOULDERS------------###
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
# Rotate both shoulders to middle. 
print("Rotating shoulders to middle...")
megamark.rotateLeftShoulder(60)
megamark.rotateRightShoulder(60)
time.sleep(2)

###-----------ELBOWS------------###
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
# Rotate both elbows to middle. 
print("Rotating elbows to middle...")
megamark.rotateLeftElbow(0)
megamark.rotateRightElbow(0)
time.sleep(2)

###-----------GRIPPERS------------###
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

###-----------WHEELS------------###
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
# Left wheel positive and right wheel negative rotates right.
print("Stopping wheels...")
megamark.setWheelVelocities(0.0,0.0);
time.sleep(2)  

# Finish the motion test and close the connection.
print("Finished Robot Motion Test!")
time.sleep(2)
megamark.exit()
quit()


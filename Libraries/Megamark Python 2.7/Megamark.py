'''
The MMM.py script contains all bindings to control the Megamark robot with Python 2.7.
Include this script in same directory of your other Python scripts referencing the Megamark.
Note: The script Serial.ino from the Megamark Library must be loaded on the Arduino to work!
'''

### Load Libraries ###
import serial    #Needed to communicate with the robot's Arduino Mega 2560.
                 #Install it here: https://pypi.python.org/pypi/pyserial/2.7
				 
import threading #Used for concurrent robot communication with other processes. 
                 #More info: https://docs.python.org/2/library/threading.html
       
import time      #Used to create delays for proper timing of robot actions.
                 #More info: https://docs.python.org/2/library/time.html

import sys       #Used for certain specific calls to the system.
                 #More info: https://docs.python.org/2/library/sys.html

### The Multipurpose Mobile Manipulator Class ###
class Megamark:
  # Incoming Data Parsing Variables
  dataStr = ""
  dataIndex = 0
  data = [ 0,0 ] #[ Left Laser, Right Laser ]
  
  # Resets all values
  def reset(self):
    #Wheels (-255 to 255)
    self.leftWheel = 0
    self.rightWheel = 0
    #Shoulders (0 to 180)
    self.leftShoulder = 90
    self.rightShoulder = 90
    #Elbows (0 to 180)
    self.leftElbow = 90
    self.rightElbow =90
    #Left Grippers (0 to 180)
    self.L1 = 90
    self.L2 = 90
    self.L3 = 90
    self.L4 = 90
    self.L5 = 90
    self.L6 = 90
    #Right Grippers (0 to 180)
    self.R1 = 90
    self.R2 = 90
    self.R3 = 90
    self.R4 = 90
    self.R5 = 90
    self.R6 = 90
    #Lasers (variable based on sensor, usually 0-1500 mm)
    self.leftLaser = -1;
    self.rightLaser = -1;
    
  # helper function clamps a value to a min/max
  def clamp(self, value, minimum, maximum):
    return min(max(value, minimum), maximum)
  # helper function clamps all robot values
  def clampAll(self):
    # Clamp Wheels
    self.leftWheel = self.clamp(self.leftWheel,-255,255)
    self.rightWheel = self.clamp(self.rightWheel,-255,255)
    # Clamp Shoulders
    self.leftShoulder = self.clamp(self.leftShoulder,0,180)
    self.rightShoulder = self.clamp(self.rightShoulder,0,180)
    # Clamp Elbows
    self.leftElbow = self.clamp(self.leftElbow,0,180)
    self.rightElbow = self.clamp(self.rightElbow,0,180)
    # Clamp Left Grippers 
    self.L1 = self.clamp(self.L1,0,180)
    self.L2 = self.clamp(self.L2,0,180)
    self.L3 = self.clamp(self.L3,0,180)
    self.L4 = self.clamp(self.L4,0,180)
    self.L5 = self.clamp(self.L5,0,180)
    self.L6 = self.clamp(self.L6,0,180)
    # Clamp Right Grippers 
    self.R1 = self.clamp(self.R1,0,180)
    self.R2 = self.clamp(self.R2,0,180)
    self.R3 = self.clamp(self.R3,0,180)
    self.R4 = self.clamp(self.R4,0,180)
    self.R5 = self.clamp(self.R5,0,180)
    self.R6 = self.clamp(self.R6,0,180)
  # helper function maps one range of values to another
  def translate(self, value, inMin, inMax, outMin, outMax):
    # Clamp value within input range
    value = self.clamp(value, inMin, inMax)
    # Figure out how 'wide' each range is
    inSpan = inMax - inMin
    outSpan = outMax - outMin
    # Convert the left range into a 0-1 range (float)
    valueScaled = float(value - inMin) / float(inSpan)
    # Convert the 0-1 range into a value in the right range.
    return outMin + (valueScaled * outSpan) 
    
  # spin motors in meters/sec (-0.2 to 0.2 m/s)
  def setWheelVelocities(self, leftSpeed, rightSpeed):
    #convert speed to robot units
    self.leftWheel = int(self.translate(leftSpeed, -0.2,0.2, -255.0,255.0))
    self.rightWheel = int(self.translate(rightSpeed, -0.2,0.2, -255.0,255.0))   
  # rotate left shoulder in degrees (0 to 120 degrees)
  def rotateLeftShoulder(self, angle):
    self.leftShoulder = int(self.translate(angle, 0,120, 0,180))
  # rotate right shoulder in degrees (0 to 120 degrees)
  def rotateRightShoulder(self, angle):
    self.rightShoulder = int(self.translate(angle, 0,120, 0,180)) 
  # rotate left elbow in degrees (-60 to 60 degrees)
  def rotateLeftElbow(self, angle):
    self.leftElbow = int(self.translate(angle, -60,60, 0,180))
  # rotate right elbow in degrees (-60 to 60 degrees)
  def rotateRightElbow(self, angle):
    self.rightElbow = int(self.translate(angle, -60,60, 0,180)) 
  # control left grippers (Up to 6 servos, all range from 0-180)
  def setLeftGrippers(self, l1 = 0,l2 = 0,l3 = 0,l4 = 0,l5 = 0,l6 = 0):
    self.L1 = l1;
    self.L2 = l2;
    self.L3 = l3;
    self.L4 = l4;
    self.L5 = l5;
    self.L6 = l6;
  # control right grippers (Up to 6 servos, all range from 0-180)
  def setRightGrippers(self,r1 = 0,r2 = 0,r3 = 0,r4 = 0,r5 = 0,r6 = 0):
    self.R1 = r1;
    self.R2 = r2;
    self.R3 = r3;
    self.R4 = r4;
    self.R5 = r5;
    self.R6 = r6;
  # get left laser sensor data (variable based on sensor, usually 0-1500 mm)
  def getLeftLaser(self):
    self.leftLaser = self.data[0]
    return self.leftLaser
  # get right laser sensor data (variable based on sensor, usually 0-1500 mm)
  def getRightLaser(self):  
    self.rightLaser = self.data[1]
    return self.rightLaser
  
  # Reads data coming from Arduino
  def read(self):
    #Make backup if data in case parsing fails.
    oldData = list(self.data)
    
    #Read data
    try:
      for chr in self.ser.readline():   
        #begin parsing
        if(chr == '{'):
          self.dataStr = ""
          self.dataIndex = 0 
        #extract number from data
        elif(chr == ',' or chr == '}'):
          self.data[self.dataIndex] = int(self.dataStr);
          self.dataIndex+=1; self.dataStr = "";
          #Finished reading message
          if(chr == '}'): 
            self.dataStr = "" 
            self.dataIndex = 0
        #add char to current string
        elif (chr.isspace() == False and chr != '\n'):
          self.dataStr += chr;
    except:
      self.data = oldData
      self.ser.flushInput();
    
  # Sends data to Arduino in Robot units, which updates position 
  def write(self):
    # Clamp all values before sending data.
    self.clampAll();  
	  
    # Prepare data
             #Wheels
    data = [ self.leftWheel, self.rightWheel,
             #Shoulders
             self.leftShoulder, self.rightShoulder, 
             #Elbows
             self.leftElbow, self.rightElbow,
             #Left Grippers
             self.L1, self.L2, self.L3, self.L4, self.L5, self.L6,
             #Right Grippers
             self.R1, self.R2, self.R3, self.R4, self.R5, self.R6 ]
                             
    # begin dataString
    dataString = "{ "
    # Add values to data list
    for dataIndex in range(len(data)):
      # Add data as a string
      dataString += str(int(data[dataIndex]))
      if (dataIndex < len(data)-1):
        dataString += ", "; # to next value
      else: 
        dataString += " }"  # finish string
      
    # send data to Arduino
    try:
      self.ser.flush()
      self.ser.flushInput()
      self.ser.write(dataString)
    except:
      print("Could not update robot at " + self.ser.name + "! Exiting.")
      print("(Check USB connection to Arduino Mega and restart program.)")
      self.ser.close()
      quit()
  
  # Writer function that runs continuously on another thread.
  def writer(self):
    while True:
      self.write();
      time.sleep(0.1);
  # Reader function that runs continuously on another thread.
  def reader(self):
    while True:
      self.read(); 
      time.sleep(0.1);
  
  # Initializes the MMM class  
  def __init__(self, portName):
    #Create a new serial connection to the robot's Arduino Mega at the specified port:
    try:
      self.ser = serial.Serial(portName,57600,timeout=0)
    except:
      print("Could not connect to robot at " + portName + "! Exiting.")
      print("(Check USB connection to Arduino Mega and restart program.)")
      quit()

    print("Successfully connected to robot at port " + portName + ".")	  
    time.sleep(2)       #Wait two seconds to allow connection to initialize.    
    self.ser.readline() #Clear the serial buffer by reading one line.
    self.reset()        #Initialize all robot positions.
    
    #Create a new serial writer thread to update robot actuators continuously:
    try: 
      #begin threads to update continuously:
      self.writeThread = threading.Thread(target=self.writer, args=())
      self.writeThread.daemon = True            
      self.writeThread.start()
    except:
      print("Could not begin writer thread for serial communication! Exiting.")
      self.ser.close()
      quit()
	
    #Create a new serial reader thread to get robot data continuously:
    try: 
      self.readThread = threading.Thread(target=self.reader, args=())
      self.readThread.daemon = True            
      self.readThread.start()	
    except:
      print("Could not begin reader thread for serial communication! Exiting.")
      self.ser.close()
      quit()

  # Function to safely terminate the Megamark instance.
  def exit(self):
    print("Megamark instance is now attempting to exit...")
    try:
      print("Closing serial connection to Arduino...")
      self.ser.close()
      print("Megamark instance successfully closed.")
    except:
      print("Megamark failed to quit. Please terminate the program manually.")
      
      



  
		

/* The Megamark.pde script contains all bindings to control the Megamark robot with Processing 3.
 * Include this script in same directory of your other Processing scripts referencing the Megamark.
 * Note: The script Serial.ino from the Megamark Library must be loaded on the Arduino to work! */

// Load Libraries
import processing.serial.*; // Needed to communicate with the robot's Arduino Mega 2560.
                            // More info: https://processing.org/reference/libraries/serial/Serial.html
                
// Serial Port for the Megamark's Arduino Mega 2560
Serial serial;
String portName;

// Incoming Data Parsing Variables
String dataStr = "";
int dataIndex = 0;
int[] data = { 0,0 }; // [ Left Laser, Right Laser ]
 
// Wheels (-255 to 255)
int leftWheel = 0;
int rightWheel = 0;
// Shoulders (0 to 180)
int leftShoulder = 90;
int rightShoulder = 90;
// Elbows (0 to 180)
int leftElbow = 90;
int rightElbow = 90;
// Left Grippers (0 to 180)
int leftGrippers[] = {90,90,90,90,90,90};
// Right Grippers (0 to 180)
int rightGrippers[] = {90,90,90,90,90,90};
// Lasers (variable based on sensor, usually 0-1500 mm)
int leftLaser = -1;
int rightLaser = -1;

//Resets all values
void reset() {
  // Wheels (-255 to 255)
  leftWheel = 0;
  rightWheel = 0;
  // Shoulders (0 to 180)
  leftShoulder = 90;
  rightShoulder = 90;
  // Elbows (0 to 180)
  leftElbow = 90;
  rightElbow = 90;
  // Left Grippers (0 to 180)
  for (int i = 0; i < 6; i++) { leftGrippers[i] = 90; }
  // Right Grippers (0 to 180)
  for (int i = 0; i < 6; i++) { rightGrippers[i] = 90; }
  // Lasers (variable based on sensor, usually 0-1500 mm)
  leftLaser = -1;
  rightLaser = -1;
}
// Helper function that constrains all robot values
void constrainAll() {
  // Clamp Wheels
  leftWheel = constrain(leftWheel,-255,255);
  rightWheel = constrain(rightWheel,-255,255);
  // Clamp Shoulders
  leftShoulder = constrain(leftShoulder,0,180);
  rightShoulder = constrain(rightShoulder,0,180);
  // Clamp Elbows
  leftElbow = constrain(leftElbow,0,180);
  rightElbow = constrain(rightElbow,0,180);
  // Clamp Left Grippers 
  for (int i = 0; i < 6; i++) { leftGrippers[i] = constrain(leftGrippers[i],0,180); }
  // Clamp Right Grippers 
  for (int i = 0; i < 6; i++) { rightGrippers[i] = constrain(rightGrippers[i],0,180); }
}

// spin motors in meters/sec (-0.2 to 0.2 m/s)
void setWheelVelocities(float leftSpeed, float rightSpeed) {
  // convert speed to robot units
  leftWheel = int(map(leftSpeed, -0.2,0.2, -255.0,255.0));
  rightWheel = int(map(rightSpeed, -0.2,0.2, -255.0,255.0));
}
// rotate left shoulder in degrees (0 to 120 degrees)
void rotateLeftShoulder(float angle) {
  leftShoulder = int(map(angle, 0,120, 0,180));
}
// rotate right shoulder in degrees (0 to 120 degrees)
void rotateRightShoulder(float angle) {
  rightShoulder = int(map(angle, 0,120, 0,180));
}
// rotate left elbow in degrees (-60 to 60 degrees)
void rotateLeftElbow(float angle) {
  leftElbow = int(map(angle, -60,60, 0,180));
}
// rotate right elbow in degrees (-60 to 60 degrees)
void rotateRightElbow(float angle) {
  rightElbow = int(map(angle, -60,60, 0,180));
}
// control left grippers (Up to 6 servos, all range from 0-180)
void setLeftGripper(int leftGripper, int angle) {
  leftGrippers[leftGripper] = angle;
}
// control right grippers (Up to 6 servos, all range from 0-180)
void setRightGripper(int rightGripper, int angle) {
  rightGrippers[rightGripper] = angle;
}
// get left laser sensor data (variable based on sensor, usually 0-1500 mm)
int getLeftLaser() {
  leftLaser = data[0];
  return leftLaser;
}
// get right laser sensor data (variable based on sensor, usually 0-1500 mm)
int getRightLaser() {
  rightLaser = data[1];
  return rightLaser;
}

// Reads data coming from Arduino
void read() {
  // Make backup if data in case parsing fails:
  int oldData[] = data;    
  // Read data
  try {
    String str = serial.readString();
    for (int i = 0; i < str.length(); i++) {
      // get ith character of serial read string
      char chr = str.charAt(i);
      // begin parsing
      if(chr == '{') {
        dataStr = "";
        dataIndex = 0; 
      } 
      // extract number from data
      else if(chr == ',' || chr == '}') {
        data[dataIndex] = int(dataStr);
        dataIndex += 1; dataStr = "";
        // Finished reading message
        if(chr == '}') { 
          dataStr = "";
          dataIndex = 0;
        }
      }
      // add char to current string
      else if (Character.isWhitespace(chr) == false && chr != '\n') {
        dataStr += chr;
      }
    }
  } catch(Exception e) {
    data = oldData;
    serial.clear();
  }
}

// Sends data to Arduino in Robot units, which updates position 
void write() {
  // Constrain all values before sending data.
  constrainAll();  
  
  // Prepare data to send
                 // Wheels
  int[] send = { leftWheel, rightWheel,
                 // Shoulders
                 leftShoulder, rightShoulder, 
                 // Elbows
                 leftElbow, rightElbow,
                 // Left Grippers
                 leftGrippers[0], leftGrippers[1], leftGrippers[2], 
                 leftGrippers[3], leftGrippers[4], leftGrippers[5],
                 // Right Grippers
                 rightGrippers[0], rightGrippers[1], rightGrippers[2], 
                 rightGrippers[3], rightGrippers[4], rightGrippers[5] };
                                             
  // begin dataString
  String dataString = "{ ";
  // Add values to data list
  for (int i = 0; i < send.length; i++) {
    // Add data as a string
    dataString += str(int(send[i]));
    if (i < send.length - 1) {
      dataString += ", "; // to next value
    } else {
      dataString += " }"; // finish string
    }
  }
  // send data to Arduino
  try {
    serial.clear();
    serial.write(dataString);
  } catch(Exception e) {
    println("Could not update robot at " + portName + "! Exiting.");
    println("(Check USB connection to Arduino Mega and restart program.)");
    serial.stop();
    exit();
  }
}

// Writer function that runs continuously on another thread.
void writer() {
  while(true) {
    write();
    delay(10);
  }
}
// Reader function that runs continuously on another thread.
void reader() {
  while(true) {
    read(); 
    delay(10);
  }
}

// Function to safely terminate the Megamark instance.
void stop() {
  println("Megamark instance is now attempting to exit...");
  try {
    println("Closing serial connection to Arduino...");
    serial.stop();
    println("Megamark instance successfully closed.");
  } catch(Exception e) {
    println("Megamark failed to quit. Please terminate the program manually.");
  }
}

// Initializes the MMM class  
void init (String PortName) {
  // Create a new serial connection to the robot's Arduino Mega at the specified port:
  try {
    portName = PortName;
    serial = new Serial(this, portName, 57600);
  } catch(Exception e) {
    println("Could not connect to robot at " + portName + "! Exiting.");
    println("(Check USB connection to Arduino Mega and restart program.)");
    stop();
  }

  println("Successfully connected to robot at port " + portName + ".");
  delay(2000);    // Wait two seconds to allow connection to initialize.    
  serial.clear(); // Clear the serial buffer by reading one line.
  reset();        // Initialize all robot positions.
  
  // Create a new serial writer thread to update robot actuators continuously:
  try {
    // begin threads to update continuously:
    thread("writer");
  } catch(Exception e) {
    println("Could not begin writer thread for serial communication! Exiting.");
    serial.stop();
    stop();
  }

  // Create a new serial reader thread to get robot data continuously:
  try {
    thread("reader");  
  } catch(Exception e) {
    println("Could not begin reader thread for serial communication! Exiting.");
    serial.stop();
    stop();
  }
}  
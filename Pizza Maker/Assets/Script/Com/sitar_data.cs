using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;

[StructLayout(LayoutKind.Sequential, Pack = 8)] //structure for sitar data, this will be updated to include new values as required
struct sitar_data
{
  public sitar_data(double def_val) //struct intialiser
  {
    frame_number = 0;
    time = 0;
    ETmarker1PosMM = new double[3];
    ETmarker2PosMM = new double[3];
    ETmarker3PosMM = new double[3];
    ETmarker4PosMM = new double[3];
    ETmarker5PosMM = new double[3];
    ETmarker6PosMM = new double[3];
    ETmarker7PosMM = new double[3];
    ETmarker8PosMM = new double[3];
    ETmarker9PosMM = new double[3];
    ETmarker10PosMM = new double[3];
    ETmarker11PosMM = new double[3];
    ETmarker12PosMM = new double[3];
    ETmarker13PosMM = new double[3];
    ETmarker14PosMM = new double[3];
    ETmarker15PosMM = new double[3];
    ETmarker16PosMM = new double[3];

    ETmarker1PosPix = new double[2];
    ETmarker2PosPix = new double[2];
    ETmarker3PosPix = new double[2];
    ETmarker4PosPix = new double[2];
    ETmarker5PosPix = new double[2];
    ETmarker6PosPix = new double[2];
    ETmarker7PosPix = new double[2];
    ETmarker8PosPix = new double[2];
    ETmarker9PosPix = new double[2];
    ETmarker10PosPix = new double[2];
    ETmarker11PosPix = new double[2];
    ETmarker12PosPix = new double[2];
    ETmarker13PosPix = new double[2];
    ETmarker14PosPix = new double[2];
    ETmarker15PosPix = new double[2];
    ETmarker16PosPix = new double[2];

	touched = 0;
    touchPositionMM = new double[2]; 
    touchPositionPix= new double[2]; 
    touchPositionMMFilt= new double[2]; 
    touchPositionPixFilt= new double[2];

    touchForce = 0; //touch force in N 
    touchForceFilt = 0; //Lowpass filtered touch force in N 

		//added by Mike
		touchForceLL = 0;
		touchForceLR = 0;
		touchForceUR = 0;
		touchForceUL = 0;

	touchRawForceLL = 0;
	touchRawForceLR = 0;
	touchRawForceUR = 0;
	touchRawForceUL = 0;

    object1PosMM = new double[2]; //Object 1 [xy] CoP position in mm
    object2PosMM = new double[2]; //Object 2 [xy] CoP position in mm
    object3PosMM = new double[2]; //Object 3 [xy] CoP position in mm
    object4PosMM = new double[2]; //Object 4 [xy] CoP position in mm
    object5PosMM = new double[2]; //Object 5 [xy] CoP position in mm
    object6PosMM= new double[2]; //Object 62 [xy] CoP position in mm

    object1PosPix = new double[2]; //Object 1 [xy] CoP position in Pixels
    object2PosPix = new double[2]; //Object 2 [xy] CoP position in Pixels
    object3PosPix = new double[2]; //Object 3 [xy] CoP position in Pixels
    object4PosPix = new double[2]; //Object 4 [xy] CoP position in Pixels
    object5PosPix = new double[2]; //Object 5 [xy] CoP position in Pixels
    object6PosPix = new double[2]; //Object 6 [xy] CoP position in Pixels

    object1ForceN = 0; //Object 1 average force in Newtons
    object2ForceN = 0; //Object 2 average force in Newtons
    object3ForceN = 0; //Object 3 average force in Newtons
    object4ForceN = 0; //Object 4 average force in Newtons
    object5ForceN = 0; //Object 5 average force in Newtons
    object6ForceN = 0; //Object 6 average force in Newtons

    object1Present = 0; //Object 1 present on glass. 0=No, 1=Yes
    object2Present = 0; //Object 2 present on glass. 0=No, 1=Yes
    object3Present = 0; //Object 3 present on glass. 0=No, 1=Yes
    object4Present = 0; //Object 4 present on glass. 0=No, 1=Yes
    object5Present = 0; //Object 5 present on glass. 0=No, 1=Yes
    object6Present = 0; //Object 6 present on glass. 0=No, 1=Yes

    object1Type = 0; //Object 1 type (0=MYSTERY,1=IBOX,2=IJAR,3=ICAN,4=IKEY
    object2Type = 0; //Object 2 type
    object3Type = 0; //Object 3 type
    object4Type = 0; //Object 4 type
    object5Type = 0; //Object 5 type
    object6Type = 0; //Object 6 type

    object1ID = 0; //Object 1 ID
    object2ID = 0; //Object 2 ID
    object3ID = 0; //Object 3 ID
    object4ID = 0; //Object 4 ID
    object5ID = 0; //Object 5 ID
    object6ID = 0; //Object 6 ID

    object1Acl = new double[3]; //Object 1 [xyz] accelerometer reading in g
    object2Acl = new double[3]; //Object 2 [xyz] accelerometer reading in g
    object3Acl = new double[3]; //Object 3 [xyz] accelerometer reading in g
    object4Acl = new double[3]; //Object 4 [xyz] accelerometer reading in g
    object5Acl = new double[3]; //Object 5 [xyz] accelerometer reading in g
    object6Acl = new double[3]; //Object 6 [xyz] accelerometer reading in g

    object1Gyr = new double[3]; //Object 1 [xyz] gyro reading in o/sec
    object2Gyr = new double[3]; //Object 2 [xyz] gyro reading in o/sec
    object3Gyr = new double[3]; //Object 3 [xyz] gyro reading in o/sec
    object4Gyr = new double[3]; //Object 4 [xyz] gyro reading in o/sec
    object5Gyr = new double[3]; //Object 5 [xyz] gyro reading in o/sec
    object6Gyr = new double[3]; //Object 6 [xyz] gyro reading in o/sec

    object1Mag = new double[3]; //Object 1 [xyz] mag reading in gauss
    object2Mag = new double[3]; //Object 2 [xyz] mag reading in gauss
    object3Mag = new double[3]; //Object 3 [xyz] mag reading in gauss
    object4Mag = new double[3]; //Object 4 [xyz] mag reading in gauss
    object5Mag = new double[3]; //Object 5 [xyz] mag reading in gauss
    object6Mag = new double[3]; //Object 6 [xyz] mag reading in gauss

    object1LC = new double[6]; //Object 1 Loadcell readings in Newtons
    object2LC = new double[6]; //Object 2 Loadcell readings in Newtons
    object3LC = new double[6]; //Object 3 Loadcell readings in Newtons
    object4LC = new double[6]; //Object 4 Loadcell readings in Newtons
    object5LC = new double[6]; //Object 5 Loadcell readings in Newtons
    object6LC = new double[6]; //Object 6 Loadcell readings in Newtons

    object1ADC = new double[8]; //Object 1 Internal ADC readings in Counts
    object2ADC = new double[8]; //Object 2 Internal ADC readings in Counts
    object3ADC = new double[8]; //Object 3 Internal ADC readings in Counts
    object4ADC = new double[8]; //Object 4 Internal ADC readings in Counts
    object5ADC = new double[8]; //Object 5 Internal ADC readings in Counts
    object6ADC = new double[8]; //Object 6 Internal ADC readings in Counts
            
    object1YawPitchRoll = new double[3]; //Object 1 yaw, pitch and roll orientation in degrees
    object2YawPitchRoll = new double[3]; //Object 2 yaw, pitch and roll orientation in degrees
    object3YawPitchRoll = new double[3]; //Object 3 yaw, pitch and roll orientation in degrees
    object4YawPitchRoll = new double[3]; //Object 4 yaw, pitch and roll orientation in degrees
    object5YawPitchRoll = new double[3]; //Object 5 yaw, pitch and roll orientation in degrees
    object6YawPitchRoll = new double[3]; //Object 6 yaw, pitch and roll orientation in degrees
            
    //contol flags 
    sysReset = 0; //don't reset
    startSaveData = 0; //Flag to start saving data
    stopSaveData = 0; //Flag to stop saving data
    Calibrate = 0; //Flag to enter calibration mode
    GUIMode = 1; //Flag to select GUIMode
    filename = new char[255];// filename for output file. Absolute path on host machine, requires \\ or / directory convention 

    EnableObjDet=0; //selects whether objects are detected or not
    DisableObjDet=0;

    EnablePose=0; //Activates ahrs pose estimate
    DisablePose=0; //Deactivates ahrs pose estimate
    ResetPose=0; //sets calibartion pose of objects
            
    objectSensitivity=0; //SD level for object detection
    objectDelay = 0;  //Timeout for object detection 
    objectForceThresh = 0; //Threshold force for object detect 
    objectForceResolution = 0; //Resolution of object discrimination
            
    SampleRate=0; //Sample rate for data recording
    Terminate=0; //Flag to exit Application 
  }
  //structure is "Marshaled" to match c++ memory mapping
  [MarshalAs(UnmanagedType.I4)] public int frame_number;        //number of sample;
  public double time;             //time of sample
  //3D marker locations in mm relative to display center
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] ETmarker1PosMM; //Easy Track marker 1 [xyz] position in (mm)
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] ETmarker2PosMM; //Easy Track marker 2 [xyz] position in (mm)
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] ETmarker3PosMM; //Easy Track marker 3 [xyz] position in (mm)
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] ETmarker4PosMM; //Easy Track marker 4 [xyz] position in (mm)
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] ETmarker5PosMM; //Easy Track marker 5 [xyz] position in (mm)
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] ETmarker6PosMM; //Easy Track marker 6 [xyz] position in (mm)
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] ETmarker7PosMM; //Easy Track marker 7 [xyz] position in (mm)
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] ETmarker8PosMM; //Easy Track marker 8 [xyz] position in (mm)
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] ETmarker9PosMM; //Easy Track marker 9 [xyz] position in (mm)
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] ETmarker10PosMM; //Easy Track marker 10 [xyz] position in (mm)
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] ETmarker11PosMM; //Easy Track marker 11 [xyz] position in (mm)
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] ETmarker12PosMM; //Easy Track marker 12 [xyz] position in (mm) (Current hand "tip" marker)
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] ETmarker13PosMM; //Easy Track marker 13 [xyz] position in (mm)
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] ETmarker14PosMM; //Easy Track marker 14 [xyz] position in (mm)
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] ETmarker15PosMM; //Easy Track marker 15 [xyz] position in (mm)
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] ETmarker16PosMM; //Easy Track marker 16 [xyz] position in (mm)

  //2D marker locations in pixels relative to display top left corner
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public double[] ETmarker1PosPix; //Easy Track marker 1 [xyz] position in pixels
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public double[] ETmarker2PosPix; //Easy Track marker 2 [xyz] position in pixels
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public double[] ETmarker3PosPix; //Easy Track marker 3 [xyz] position in pixels
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public double[] ETmarker4PosPix; //Easy Track marker 4 [xyz] position in pixels
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public double[] ETmarker5PosPix; //Easy Track marker 5 [xyz] position in pixels
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public double[] ETmarker6PosPix; //Easy Track marker 6 [xyz] position in pixels
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public double[] ETmarker7PosPix; //Easy Track marker 7 [xyz] position in pixels
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public double[] ETmarker8PosPix; //Easy Track marker 8 [xyz] position in pixels
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public double[] ETmarker9PosPix; //Easy Track marker 9 [xyz] position in pixels
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public double[] ETmarker10PosPix; //Easy Track marker 10 [xyz] position in pixels
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public double[] ETmarker11PosPix; //Easy Track marker 11 [xyz] position in pixels
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public double[] ETmarker12PosPix; //Easy Track marker 12 [xyz] position in pixels (Current hand "tip" marker)
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public double[] ETmarker13PosPix; //Easy Track marker 13 [xyz] position in pixels
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public double[] ETmarker14PosPix; //Easy Track marker 14 [xyz] position in pixels
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public double[] ETmarker15PosPix; //Easy Track marker 15 [xyz] position in pixels
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public double[] ETmarker16PosPix; //Easy Track marker 16 [xyz] position in pixels

  //touch position and force
  [MarshalAs(UnmanagedType.I4)] public int touched; //whether board is touched or not. 0=no touch, 1=touched
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public double[] touchPositionMM; //[xy] touched position in (mm)
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public double[] touchPositionPix; //[xy] touched position in Pixels
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public double[] touchPositionMMFilt; //Lowpass filtered [xy] touched position in (mm)
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public double[] touchPositionPixFilt; //Lowpass filtered [xy] touched position in Pixels
  public double touchForce; //touch force in N 
  public double touchForceFilt; //Lowpass filtered touch force in N 
	//added by Mike

	public double touchForceLL;
	public double touchForceLR;
	public double touchForceUR;
	public double touchForceUL;

	public double touchRawForceLL;
	public double touchRawForceLR;
	public double touchRawForceUR;
	public double touchRawForceUL;

  //object parameters, up to six, ordered by id (not appearance)
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public double[] object1PosMM; //Object 1 [xy] CoP position in mm
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public double[] object2PosMM; //Object 2 [xy] CoP position in mm
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public double[] object3PosMM; //Object 3 [xy] CoP position in mm
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public double[] object4PosMM; //Object 4 [xy] CoP position in mm
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public double[] object5PosMM; //Object 5 [xy] CoP position in mm
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public double[] object6PosMM; //Object 62 [xy] CoP position in mm

  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public double[] object1PosPix; //Object 1 [xy] CoP position in Pixels
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public double[] object2PosPix; //Object 2 [xy] CoP position in Pixels
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public double[] object3PosPix; //Object 3 [xy] CoP position in Pixels
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public double[] object4PosPix; //Object 4 [xy] CoP position in Pixels
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public double[] object5PosPix; //Object 5 [xy] CoP position in Pixels
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public double[] object6PosPix; //Object 6 [xy] CoP position in Pixels

  public double object1ForceN; //Object 1 average force in Newtons
  public double object2ForceN; //Object 2 average force in Newtons
  public double object3ForceN; //Object 3 average force in Newtons
  public double object4ForceN; //Object 4 average force in Newtons
  public double object5ForceN; //Object 5 average force in Newtons
  public double object6ForceN; //Object 6 average force in Newtons
		
  [MarshalAs(UnmanagedType.I4)] public int object1Present; //Object 1 present on glass. 0=No, 1=Yes
  [MarshalAs(UnmanagedType.I4)] public int object2Present; //Object 2 present on glass. 0=No, 1=Yes
  [MarshalAs(UnmanagedType.I4)] public int object3Present; //Object 3 present on glass. 0=No, 1=Yes
  [MarshalAs(UnmanagedType.I4)] public int object4Present; //Object 4 present on glass. 0=No, 1=Yes
  [MarshalAs(UnmanagedType.I4)] public int object5Present; //Object 5 present on glass. 0=No, 1=Yes
  [MarshalAs(UnmanagedType.I4)] public int object6Present; //Object 6 present on glass. 0=No, 1=Yes

  [MarshalAs(UnmanagedType.I4)] public int object1Type; //Object 1 type (0=MYSTERY,1=IBOX,2=IJAR,3=ICAN,4=IKEY
  [MarshalAs(UnmanagedType.I4)] public int object2Type; //Object 2 type (0=MYSTERY,1=IBOX,2=IJAR,3=ICAN,4=IKEY
  [MarshalAs(UnmanagedType.I4)] public int object3Type; //Object 3 type (0=MYSTERY,1=IBOX,2=IJAR,3=ICAN,4=IKEY
  [MarshalAs(UnmanagedType.I4)] public int object4Type; //Object 4 type (0=MYSTERY,1=IBOX,2=IJAR,3=ICAN,4=IKEY
  [MarshalAs(UnmanagedType.I4)] public int object5Type; //Object 5 type (0=MYSTERY,1=IBOX,2=IJAR,3=ICAN,4=IKEY
  [MarshalAs(UnmanagedType.I4)] public int object6Type; //Object 6 type (0=MYSTERY,1=IBOX,2=IJAR,3=ICAN,4=IKEY
        
  [MarshalAs(UnmanagedType.I4)] public int object1ID; //Object 1 ID
  [MarshalAs(UnmanagedType.I4)] public int object2ID; //Object 2 ID
  [MarshalAs(UnmanagedType.I4)] public int object3ID; //Object 3 ID
  [MarshalAs(UnmanagedType.I4)] public int object4ID; //Object 4 ID
  [MarshalAs(UnmanagedType.I4)] public int object5ID; //Object 5 ID
  [MarshalAs(UnmanagedType.I4)] public int object6ID; //Object 6 ID

  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] object1Acl; //Object 1 [xyz] accelerometer reading in g
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] object2Acl; //Object 2 [xyz] accelerometer reading in g
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] object3Acl; //Object 3 [xyz] accelerometer reading in g
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] object4Acl; //Object 4 [xyz] accelerometer reading in g
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] object5Acl; //Object 5 [xyz] accelerometer reading in g
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] object6Acl; //Object 6 [xyz] accelerometer reading in g

  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] object1Gyr; //Object 1 [xyz] gyro reading in o/sec
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] object2Gyr; //Object 2 [xyz] gyro reading in o/sec
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] object3Gyr; //Object 3 [xyz] gyro reading in o/sec
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] object4Gyr; //Object 4 [xyz] gyro reading in o/sec
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] object5Gyr; //Object 5 [xyz] gyro reading in o/sec
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] object6Gyr; //Object 6 [xyz] gyro reading in o/sec

  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] object1Mag; //Object 1 [xyz] mag reading in gauss
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] object2Mag; //Object 2 [xyz] mag reading in gauss
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] object3Mag; //Object 3 [xyz] mag reading in gauss
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] object4Mag; //Object 4 [xyz] mag reading in gauss
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] object5Mag; //Object 5 [xyz] mag reading in gauss
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] object6Mag; //Object 6 [xyz] mag reading in gauss

  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public double[] object1LC; //Object 1 Loadcell readings in Newtons
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public double[] object2LC; //Object 2 Loadcell readings in Newtons
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public double[] object3LC; //Object 3 Loadcell readings in Newtons
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public double[] object4LC; //Object 4 Loadcell readings in Newtons
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public double[] object5LC; //Object 5 Loadcell readings in Newtons
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public double[] object6LC; //Object 6 Loadcell readings in Newtons

  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public double[] object1ADC; //Object 1 Loadcell readings in Newtons
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public double[] object2ADC; //Object 2 Loadcell readings in Newtons
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public double[] object3ADC; //Object 3 Loadcell readings in Newtons
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public double[] object4ADC; //Object 4 Loadcell readings in Newtons
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public double[] object5ADC; //Object 5 Loadcell readings in Newtons
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public double[] object6ADC; //Object 6 Loadcell readings in Newtons

  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] object1YawPitchRoll; //Object 1 yaw, pitch and roll orientation in degrees
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] object2YawPitchRoll; //Object 2 yaw, pitch and roll orientation in degrees
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] object3YawPitchRoll; //Object 3 yaw, pitch and roll orientation in degrees
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] object4YawPitchRoll; //Object 4 yaw, pitch and roll orientation in degrees
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] object5YawPitchRoll; //Object 5 yaw, pitch and roll orientation in degrees
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public double[] object6YawPitchRoll; //Object 6 yaw, pitch and roll orientation in degrees
                    
  //-----------------return variables---------------------------
  [MarshalAs(UnmanagedType.I4)] public int sysReset; //Flag to reset sitar
  [MarshalAs(UnmanagedType.I4)] public int startSaveData; //Flag to start saving data
  [MarshalAs(UnmanagedType.I4)] public int stopSaveData; //Flag to stop saving data
  [MarshalAs(UnmanagedType.I4)] public int Calibrate; //Flag to enter calibration mode
  [MarshalAs(UnmanagedType.I4)] public int GUIMode; //Flag to select GUIMode
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 255)]
  public char[] filename;// filename for output file. Absolute path on host machine, requires \\ or / directory convention 

  [MarshalAs(UnmanagedType.I4)] public int EnableObjDet; //selects whether objects are detected or not
  [MarshalAs(UnmanagedType.I4)] public int DisableObjDet;

  [MarshalAs(UnmanagedType.I4)] public int EnablePose; //Activates ahrs pose estimate
  [MarshalAs(UnmanagedType.I4)] public int DisablePose; //Deactivates ahrs pose estimate
  [MarshalAs(UnmanagedType.I4)] public int ResetPose; //sets calibration pose of objects
		
  public double objectSensitivity; //SD level for object detection (NOTE: These values are now unsupported and are included only for compatibility) 
  public double objectDelay; //Timeout for object detection 
  public double objectForceThresh; //Threshold force for object detect 
  public double objectForceResolution; //Resolution of object discrimination

  public double SampleRate; //Sample rate for data recording
  [MarshalAs(UnmanagedType.I4)] public int Terminate; //Flag to exit Application
		
}
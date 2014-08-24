using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;

class ReceiveUDP : MonoBehaviour
{
  //constants
  const int UDP_LISTEN = 1; //constants for UDP modes 
  const int UDP_BROADCAST = 2;
  const int UDP_STOP = 0;

  [Range(0,2)] public int UDPMode; //mode of coms class, can be UDP_STOP, UDP_LISTEN or UDP_BROADCAST

  int struct_sz; //size of structure in bytes

  private UdpClient udp; //the udp client 
        
  private IPEndPoint groupEP; // the target address

  private Socket listener; //listening socket
  private Socket broadcaster; //broadcasting socket
  public static sitar_data SitarData; //sitar data structure for input/output 
  private Byte[] receiveBytes; //buffer for received packet 
  private Byte[] transmitBytes; //buffer for transmitted packet

  public string TargetIP = "127.0.0.1"; //target address to send data

  public int GroupPort = 0x1978; //the recieve port

  public int ListenerFramesReceived; //number of frames recvieved since port opened 
  public int BroadcasterFramesSent; //number of frames sent since port opened 

  private Thread ListenThread; //Thread for listener

  //******************************************************************
  public ReceiveUDP() //Constructor
  {
    SitarData = new sitar_data((double)(0)); //Create main data structure (must by inited with double arg)
    UDPMode = UDP_STOP; //Put class in stopped mode
    struct_sz = Marshal.SizeOf(SitarData); //get size of sitar data structure

  }

  //******************************************************************
  ~ReceiveUDP() //Destructor
  {
    if (UDPMode == UDP_LISTEN) //kill any current transmit/receives 
      {
	StopListening();
      }

    if (UDPMode == UDP_BROADCAST)
      {
	StopBroadcasting();
      }


  }

  void Start()
  {
    StartListening();
  }

  void OnDestroy()
  {
    StopListening();
  }

  //******************************************************************
  public void StartListening() //start listening for data
  {
    if (UDPMode == UDP_STOP) //check mode
      {
	udp = new UdpClient(); //create udp client

	groupEP = new IPEndPoint(IPAddress.Any, GroupPort); //listen to any transmitting address on port "GroupPort"

	listener = new Socket(AddressFamily.InterNetwork,
			      SocketType.Dgram, ProtocolType.Udp); //create UDP socket   

	listener.Bind(groupEP); //Bind socket 

	receiveBytes = new Byte[struct_sz]; //create receive buffer for packet

	listener.ReceiveBufferSize = struct_sz; //set receive buffer size

	UDPMode = UDP_LISTEN; //select listen mode

	ListenerFramesReceived = 0; //reset frame recived count

	ListenThread = new Thread(new ThreadStart(recieve_packet)); //start up listen thread
	ListenThread.Start();

      }

  }

  //******************************************************************
  public void StartBroadcasting() //prepare for sending data
  {
    if (UDPMode == UDP_STOP) //check mode
      {
	udp = new UdpClient(); //create udp client

	groupEP = new IPEndPoint(IPAddress.Parse(TargetIP), GroupPort); //send to "TargetIP" address on port "GroupPort"

	broadcaster = new Socket(AddressFamily.InterNetwork,
				 SocketType.Dgram, ProtocolType.Udp); //create UDP socket  

	broadcaster.Connect(groupEP); //connect

	transmitBytes = new Byte[struct_sz]; //create transmit buffer

	broadcaster.SendBufferSize = struct_sz; //set buffer size

	UDPMode = UDP_BROADCAST; //select broadcast mode

	BroadcasterFramesSent = 0; //reset frame transmit count

      }

  }

  //******************************************************************

  public int SendData() //sends contents of "SitarData" when in broadcast mode
  {
    if (UDPMode == UDP_BROADCAST)
      {
	IntPtr ptr = Marshal.AllocHGlobal(struct_sz); //bytewise copy of SitarData struct to transmit buffer

	Marshal.StructureToPtr(SitarData, ptr, true);

	Marshal.Copy(ptr, transmitBytes, 0, struct_sz);

	broadcaster.Send(transmitBytes, SocketFlags.None); //send buffer

	BroadcasterFramesSent = BroadcasterFramesSent + 1; //increment transmit count

	return (1);
      }
    else
      {
	return (0);
      }

  }

  //******************************************************************
  public void StopListening()  //Function to close port for data reception and term thread 
  {
    if (UDPMode == UDP_LISTEN)
      {
	ListenThread.Abort(); //Kill listen thread
	listener.Close(); //close socket
	udp.Close(); //close udp client
	UDPMode = UDP_STOP; //set class stop mode
      }

  }
  //******************************************************************

  public void StopBroadcasting()  //Function to close port for data transmision 
  {
    if (UDPMode == UDP_BROADCAST)
      {
	broadcaster.Close(); //close socket
	udp.Close(); //close udp client
	UDPMode = UDP_STOP; //set class stop mode
      }
  }

  //******************************************************************

  private void recieve_packet() //function to parse received packet 
  {

    while (UDPMode == UDP_LISTEN) //check mode
      {
	listener.Receive(receiveBytes); //wait for next packet (blocking fuction)

	GCHandle pinnedPacket = GCHandle.Alloc(receiveBytes, GCHandleType.Pinned); //copy buffer to SitarData struct
	SitarData = (sitar_data)Marshal.PtrToStructure(
						       pinnedPacket.AddrOfPinnedObject(),
						       typeof(sitar_data));

	ListenerFramesReceived++; //increment received count

			InInput.controller.UpdatePacket();

	// Display data recived for testing
	//     string returnData = "Packet No:" + ListenerFramesReceived.ToString() + ", Frame:" + SitarData.frame_number.ToString() + ", Time:" + SitarData.time.ToString() +
	//         ". v1: " + SitarData.at_marker1_pos[0].ToString() + ", " + SitarData.at_marker1_pos[1].ToString() + ", " + SitarData.at_marker1_pos[2].ToString() +
	//        ". v2: " + SitarData.at_marker2_pos[0].ToString() + ", " + SitarData.at_marker2_pos[1].ToString() + ", " + SitarData.at_marker2_pos[2].ToString();
	//     Console.WriteLine(returnData);


      }
    //****************************************************************** 
  }
}

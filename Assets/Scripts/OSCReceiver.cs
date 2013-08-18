/* 
 * Classe per la ricezione dell'OSC
 */

using UnityEngine;
using System.Collections;

public class OSCReceiver : MonoBehaviour {
	
	public string			RemoteIP 				= "127.0.0.1";
	public int 				SendToPort 				= 57131;
	public int 				ListenerPort 			= 57130;
	public Main				main;
	private Osc 			handler;
	public static Vector3 	cubePos;
	public UDPPacketIO udp;
	
	void Start () 
	{		
		udp = gameObject.AddComponent("UDPPacketIO") as UDPPacketIO;
		udp.init(RemoteIP, SendToPort, ListenerPort);
    	handler = gameObject.AddComponent("Osc") as Osc;
		handler.init(udp);
		handler.SetAddressHandler("/rightHandPos", ListenEvent);		
	}
	
	void Update () {
	
	}
	
	public void ListenEvent(OscMessage oscMessage)
	{	
		string address = oscMessage.Address;
		
		if(address == "/rightHandPos")
		{
			float cubePosX = Utility.ofMap((float)System.Convert.ToSingle(oscMessage.Values[0]), -1, 1, 7, -7, true);
			float cubePosY = Utility.ofMap((float)System.Convert.ToSingle(oscMessage.Values[1]), 1, -1, 0, 10, true);
			float cubePosZ = Utility.ofMap((float)System.Convert.ToSingle(oscMessage.Values[2]), 1, 0, Main.limitBack - 5, Main.limitFront + 5, true);
			
			if(!main.freeDepth)
				cubePosZ = Main.getRangeDepth(cubePosZ);
			
			CubeBehaviour.cubeTarget = new Vector3(cubePosX, cubePosY, cubePosZ);
		}	
	} 
}

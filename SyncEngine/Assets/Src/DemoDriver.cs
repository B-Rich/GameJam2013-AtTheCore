﻿using UnityEngine;
using System.Collections;
using System.Threading;

public class DemoDriver : MonoBehaviour {
	
	CommunicationChannel clientA;
	CommunicationChannel clientB;
	
	SyncEngine syncEngine1;
	
	UdpAsyncConnection u2; 
		

	int packetMode =0;
	
	// SPAM
	Thread spamThread;
	
	// Use this for initialization
	void Start () {
		
	//	u2 = new UdpAsyncConnection();
		
		//u2.Initialize("192.168.12.75",5005,5005);
		
//		Debug.Log("START DEMO");
//		syncEngine1 = new SyncEngine();
//		
//		//clientB = new CommunicationChannel();
//		clientA = new CommunicationChannel();	
//		
//		clientA.init("192.168.12.75",5006, "192.168.12.75",5008 , syncEngine1);
		//clientB.init("192.168.12.75",5008, "192.168.12.75",5006 , syncEngine1);
		
		
		
	}
	
	
	static byte[] GetBytes(string str)
{
    byte[] bytes = new byte[str.Length * sizeof(char)];
    System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
    return bytes;
}

static string GetString(byte[] bytes)
{
    char[] chars = new char[bytes.Length / sizeof(char)];
    System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
    return new string(chars);
} 
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")){
			//packetMode = 1 - packetMode;
			if (packetMode == 0){
				packetMode = 1;
				spamThread = new Thread(new ThreadStart(spamLoop));
				spamThread.Start();
			}else {
				packetMode = 0;
			}
				
		}
	

	}
	byte[] spambytes = GetBytes("HEY TEST");
	int spamSize = GetBytes("HEY TEST").Length;
	
	void spamLoop(){
		while(packetMode >0){
				Debug.Log ("SS4");
			u2.sendData(spambytes, spamSize);
			
		}	
		
	}
	
	
	void OnApplicationQuit(){
		

		
	}

	
}

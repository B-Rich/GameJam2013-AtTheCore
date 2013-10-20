﻿using UnityEngine;
using System.Collections;
using System;
using System.IO;


public struct byteArrayMetaData  {
	public byte[] bytes;
	public int bytesize;
}; 

public class SyncEngine : MonoBehaviour, DataDelegate {
	
	public string remoteIp = "192.168.12.75";
	public int remotePort = 5005;
	public int localPort = 5005;
	public UdpAsyncConnection connection;
	public DataModel dataModel;
	
	public Client client;
	
	void Start(){
		Debug.Log ("START"); 
		connection = new UdpAsyncConnection();
		connection.Initialize(remoteIp,remotePort,localPort);
		connection.setCallBackDelegate(this);
	}

	public void syncRemotely(DataItem dataItem){
		Debug.Log("LocalChange" + dataItem.velocity);
		byteArrayMetaData bamd = new byteArrayMetaData();
		pack(dataItem, ref bamd);
		
		connection.sendData(bamd);
	}
	public void syncLocally(DataItem c){
		dataModel._LocalUpdateItem (c);
	}
	
	
	public void ProcessBytes(byte[] bytes, int byteSize){
		Debug.Log("SYNC");
	
		try {
		MemoryStream memStream = new MemoryStream(bytes, false);
		BinaryReader br = new BinaryReader(memStream);
		Debug.Log ("PLN: 1" );
		
		var item = new DataItem ();
		item.did = br.ReadInt32 ();
		item.position.x = br.ReadSingle();
		item.position.y = br.ReadSingle();
	    item.position.z = br.ReadSingle();
		item.velocity.x = br.ReadSingle();
		item.velocity.y = br.ReadSingle();
		item.velocity.z = br.ReadSingle();
			
		//Debug.Log ("@" + (client== null));
		
		syncLocally(item);	
		
		}catch (Exception e){
			
			Debug.Log(e);
			
		}
		
		
	}
			
			
	private void pack(DataItem c, ref byteArrayMetaData bamd){
		
		byte[] buffer = new byte[866];
		MemoryStream memStream = new MemoryStream(buffer, true);
		BinaryWriter bw = new BinaryWriter(memStream);
		
		bw.Write(c.did);
		bw.Write(c.position.x);
		bw.Write(c.position.y);
		bw.Write(c.position.z);
		
		bw.Write(c.velocity.x);
		bw.Write(c.velocity.y);
		bw.Write(c.velocity.z);
		
		bw.Flush();
		bw.Close();
		memStream.Flush();
		memStream.Close();
		
		bamd.bytes = buffer;
		bamd.bytesize = 24 + 4;
		
		
	}
	
//	private void pack(MetaData c, ref byteArrayMetaData bamd){
//		
//		byte[] buffer = new byte[866];
//		MemoryStream memStream = new MemoryStream(buffer, true);
//		BinaryWriter bw = new BinaryWriter(memStream);
//		
//		bw.Write(c.dataModel.GetItem(c).did);
//		bw.Write(c.transform.position.x);
//		bw.Write(c.transform.position.y);
//		bw.Write(c.transform.position.z);
//		
//		bw.Write(c.velocity.x);
//		bw.Write(c.velocity.y);
//		bw.Write(c.velocity.z);
//		
//		bw.Flush();
//		bw.Close();
//		memStream.Flush();
//		memStream.Close();
//		
//		bamd.bytes = buffer;
//		bamd.bytesize = 24 + 4;
//		
//		
//	}
	
	
	public int putBytes (float valueType, byte[] buffer, int offset){
		byte[] newData = System.BitConverter.GetBytes(valueType);
		newData.CopyTo(buffer,offset);
		return offset + sizeof(float);
 	}
	
	
	
	
	
	
}


			
			

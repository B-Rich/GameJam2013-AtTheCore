﻿using UnityEngine;
using System.Collections;
using System.Threading;

public class MetaData : MonoBehaviour {

	public int uid;
	public Vector3 velocity = new Vector3(0,0,0);
	public DataModel dataModel; 
	public GameObject gameObj;
	public bool isActive = false;
	public Client client;
	public bool toBeDeleted;
	// Use this for initialization
	
	public int dirty = 0;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		var item = dataModel.GetItem (this);
		
		// reset if needed.
		if( Interlocked.CompareExchange(ref dirty, 0, 1) == 1)	{
//			Debug.Log ("UPDATE$$$" + client.prefab + item.uid + " "+ gameObj.transform.position.x + " " + transform.position.x + " "+ item.position.x );
			transform.position =  item.position;
			velocity = item.velocity;
		}

		//Debug.Log ("VAL:"+(isActive==true));
		if(isActive){
			ProcessInput();	
		}

		transform.Translate(velocity * Time.deltaTime);
		
		//Debug.Log("RVR:" + transform.position.x + " " +item.position.x + " " + client.prefab);
	}
	
	void saveDataToDataModel() {
		DataItem dm = new DataItem();
		dm.uid = uid;
		dm.position = gameObj.transform.position;
		dm.velocity = velocity;
		
		
		dataModel.UpdateItem(dm);
	}
	
	void ProcessInput(){
		//Debug.Log ("IN");
		float dV= 0.25f;
		
		DataItem d;
		if(Input.anyKeyDown){
			d = dataModel.GetItem(this);
			Debug.Log ("PREL-- X:" + d.velocity.x +" Y:"+ d.velocity.y );
			if (Input.GetKeyDown (KeyCode.W)){
				d.velocity.y += dV;
			}else if(Input.GetKeyDown (KeyCode.S)){
				d.velocity.y += -dV;
			}else if(Input.GetKeyDown (KeyCode.D)){
				d.velocity.x += dV;
			}else if(Input.GetKeyDown (KeyCode.A)){
				d.velocity.x += -dV;	
			}
			Debug.Log ("POST-- X:" + d.velocity.x +" Y:"+ d.velocity.y );
			Debug.Log ("PINTPU");
			DataItem di = new DataItem();
			di.uid = d.uid;
			di.position = d.position;
			di.velocity = d.velocity;
			dataModel.UpdateItem(di);
			saveDataToDataModel();
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Networking;

public class PlanningPhaseManager : MonoBehaviour {

	public string objectName;
	public Vector3 destinationPosition;

//	[Command]
	public void CmdSetPosition(){
		//Find gameobject using objectName
		//then send the destination to the object
	}

	public void Update(){
		GetName ();
	}

	public void GetName(){
		if ( Input.GetMouseButtonDown (0)){ 
			RaycastHit hit; 
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
			if ( Physics.Raycast (ray,out hit,1200.0f)) {
				objectName = hit.transform.name;
			}
		}
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlanningPhaseManager : NetworkBehaviour {

	public static PlanningPhaseManager instance;

	public GameObject tankType, sniperType, artileryType;
	public string objectName;
	public Vector3 destinationPosition;
	AIStats aiStats;



//	[Command]
	public void CmdSetPosition(){
		//Find gameobject using objectName
		//then send the destination to the object
	}

	void Start()
	{
		instance = this;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
			
	[Command]
	public void CmdSpawnAI(string OwnerName, int UnitType, Vector3 position)
	{
		Debug.Log (OwnerName + " " + UnitType);
		if(UnitType == 1){
			GameObject go = Instantiate (tankType, position, Quaternion.identity);
			go.GetComponent<AIStats> ().CmdSetOwner(OwnerName);
			NetworkServer.Spawn (go);
		}
		if(UnitType == 2){
			GameObject go = Instantiate (sniperType, position, Quaternion.identity);
			go.GetComponent<AIStats> ().CmdSetOwner(OwnerName);	
			NetworkServer.Spawn (go);
		}
		Debug.Log (GameManager.GM.localPlayerStatsScript);
//		GameManager.GM.localPlayerStatsScript.CmdAddUnit ();
		for(int i = 0; i < TeamManager.instance.players.Count; i++){
			if(TeamManager.instance.players[i].name == OwnerName){
				Debug.Log ("Found It");
				TeamManager.instance.players [i].GetComponent<PlayerStats> ().CmdAddUnit ();
			}
		}
	}



}

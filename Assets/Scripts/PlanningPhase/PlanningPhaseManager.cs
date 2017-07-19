using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
<<<<<<< HEAD

public class PlanningPhaseManager : NetworkBehaviour {
	
	public List <GameObject> spawnObject;
	public string objectName;
	public Vector3 destinationPosition;
	AIStats aiStats;
=======

public class PlanningPhaseManager : NetworkBehaviour {

	public static PlanningPhaseManager instance;

	public GameObject tankType, sniperType, artileryType;
	public string objectName;
	public Vector3 destinationPosition;
	AIStats aiStats;


>>>>>>> e409efdcc9aa6f6b31c31fe273612a241139ccff

//	[Command]
	public void CmdSetPosition(){
		//Find gameobject using objectName
		//then send the destination to the object
	}

<<<<<<< HEAD
	public void start()
	{
		CmdSpawnAI ();
	}

	public void Update(){
		GetAI ();
	}

	public void GetAI(){
		if (Input.GetMouseButtonDown (0)) { 
			RaycastHit hit; 
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition); 

			if (Physics.Raycast (ray, out hit, 1200.0f)) {
				Debug.Log (hit.transform.gameObject.name);
				aiStats = hit.transform.gameObject.GetComponent<AIStats>();
				if (aiStats != null) {
					objectName = aiStats.AIName;
				} else {
					for (int i = 0; i < spawnObject.Count; i++) {
						if (spawnObject [i].GetComponent<AIStats> ().AIName == objectName) {
							spawnObject [i].GetComponent<AIWalkPoint> ().newPosition = hit.point;
						}
					}
				}
=======
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
>>>>>>> e409efdcc9aa6f6b31c31fe273612a241139ccff
			}
		}
	}
			
	[Command]
	public void CmdSpawnAI()
	{
		for (int i = 0; i < AIManager.instance.AIUnits.Count; i++)
		{
			GameObject go = Instantiate (AIManager.instance.AIUnits[i]);
			spawnObject.Add (go);
			NetworkServer.Spawn (go);
		}
	}





}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlanningPhase_UI_AIStats : NetworkBehaviour {

	public string aiName;
	public int teamID;
	public string ownerName;
	public Vector3 destination;
	public GameObject aiObject;
	[Header("AI set position")]
	public int unitType, spawnPointPosition;

	// Use this for initialization
	void Start () {
//		if (isServer) {
//			Debug.Log ("SpawnAI");
//			SpawnAI ();
//		}
		StartCoroutine("spawnTimer", 1f);
	}
	
	public void SpawnAI(){
		GameObject go = Instantiate (aiObject);
		NetworkServer.Spawn (go);
		aiName = go.name + go.GetComponent<NetworkIdentity>().netId;
		Debug.Log (aiName + teamID);
		CmdSetTeamID (aiName, teamID, Vector3.zero);
	}
		
	[Command]
	public void CmdSetTeamID(string name, int teamID2, Vector3 position){
		for(int i = 0; i < AIManager.instance.AIUnits.Count; i++){
			if(AIManager.instance.AIUnits[i].name == name){
				Debug.Log ("CMDSetTeam");
				AIManager.instance.AIUnits [i].GetComponent<AIStats> ().teamID = teamID2;

				Debug.Log ("Debug team ID: " + AIManager.instance.AIUnits [i].GetComponent<AIStats> ().teamID);

//				if(teamID == 1){
//					AIManager.instance.AIUnits[i].transform.position = position;
//				}
//				else if(teamID == 2){
//					AIManager.instance.AIUnits[i].transform.position = GameManager.GM.respawnPosition_Team2 [0].transform.position;
//				}
			}
		}
		RpcSetTeamID (name, teamID2, position);
	}

	[ClientRpc]
	public void RpcSetTeamID(string name, int teamID2, Vector3 position){
		for(int i = 0; i < AIManager.instance.AIUnits.Count; i++){
			if(AIManager.instance.AIUnits[i].name == name){
				Debug.Log ("CMDSetTeam");
				AIManager.instance.AIUnits [i].GetComponent<AIStats> ().teamID = teamID2;

				Debug.Log ("Debug team ID: " + AIManager.instance.AIUnits [i].GetComponent<AIStats> ().teamID);

				//				if(teamID == 1){
				AIManager.instance.AIUnits[i].transform.position = position;
				//				}
				//				else if(teamID == 2){
				//					AIManager.instance.AIUnits[i].transform.position = GameManager.GM.respawnPosition_Team2 [0].transform.position;
				//				}
			}
		}
	}

	public void SetTeamID(Vector3 position){
		teamID = GameManager.GM.localPlayerStatsScript.teamID;
//		CmdSetTeamID (aiName, teamID, position);
	}

	IEnumerator spawnTimer(float t){
		yield return new WaitForSeconds (t);
		ownerName = GameManager.GM.localPlayer.name;
		CmdSpawnAI (ownerName, unitType);
	}

//	[Command]
	public void CmdSpawnAI(string ownerName, int unitType){
		Debug.Log ("Spawn");
//		PlanningPhaseManager.instance.CmdSpawnAI(ownerName, unitType);
		GameManager.GM.localPlayerStatsScript.CmdSpawnUnits(ownerName, unitType);
	}

	public void SetPosition(){
		Debug.Log ("SetPos");
//		GameManager.GM.SetAIPosition (unitType, spawnPointPosition);
		for(int i = 0; i < TeamManager.instance.players.Count; i++){
			if(TeamManager.instance.players[i].name == ownerName){
				Debug.Log ("foundPOs");
				TeamManager.instance.players [i].GetComponent<PlayerStats> ().CmdSetUnitPosition (unitType, spawnPointPosition);
			}
		}
	}
}

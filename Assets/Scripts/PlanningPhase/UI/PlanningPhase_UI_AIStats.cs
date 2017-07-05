using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlanningPhase_UI_AIStats : NetworkBehaviour {

	public string aiName;
	public int teamID;
	public Vector3 destination;
	public GameObject aiObject;

	// Use this for initialization
	void Start () {
		SpawnAI ();
	}
	
	public void SpawnAI(){
		Debug.Log ("SpawnAI");
		GameObject go = Instantiate (aiObject);
		NetworkServer.Spawn (go);
		aiName = go.name;
		Debug.Log (aiName + teamID);
		CmdSetTeamID (aiName, teamID);
	}
		
	[Command]
	public void CmdSetTeamID(string name, int teamID2){
		for(int i = 0; i < AIManager.instance.AIUnits.Count; i++){
			if(AIManager.instance.AIUnits[i].name == name){
				Debug.Log ("CMDSetTeam");
				AIManager.instance.AIUnits [i].GetComponent<AIStats> ().teamID = teamID2;

				Debug.Log ("Debug team ID: " + AIManager.instance.AIUnits [i].GetComponent<AIStats> ().teamID);

				if(teamID == 1){
					AIManager.instance.AIUnits[i].transform.position = GameManager.GM.respawnPosition_Team1 [0].transform.position;
				}
				else if(teamID == 2){
					AIManager.instance.AIUnits[i].transform.position = GameManager.GM.respawnPosition_Team2 [0].transform.position;
				}
			}
		}
	}

	public void SetTeamID(){
		teamID = GameManager.GM.localPlayerStatsScript.teamID;
		CmdSetTeamID (aiName, teamID);
	}
}

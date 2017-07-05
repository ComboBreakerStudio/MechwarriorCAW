using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TeamManager : NetworkBehaviour {

	public static TeamManager instance;

	// Use this for initialization
	void Awake () {
		instance = this;
	}

	public List<GameObject> players;

	public List<GameObject> team1,team2;

	//TestPurpose
	public bool otherTeam;

	public void AddPlayerToList (GameObject go){
		players.Add (go);

		if(isServer){
			SetPlayerTeam(go);
		}
	}

	public void DeletePlayerFromList(GameObject go){
		players.Remove (go);

	}

	public void SetPlayerTeam(GameObject ga){
		if (otherTeam) {
			otherTeam = false;
			team1.Add (ga);

			//Set team1 team ID
			for(int i = 0; i < team1.Count; i++){
				if(team1[i] == ga){
					team1 [i].GetComponent<PlayerStats> ().teamID = 1;
					team1 [i].GetComponent<PlayerStats> ().RpcRespawnPlayer();
					team1 [i].GetComponent<PlayerStats> ().RpcStartStuff ();
					Invoke ("RespawnPlayerTimer", 2.0f);
				}
			}
		} 
		else {
			otherTeam = true;
			team2.Add (ga);

			//Set team2 team ID
			for(int i = 0; i < team2.Count; i++){
				if(team2[i] == ga){
					team2 [i].GetComponent<PlayerStats> ().teamID = 2;
					team2 [i].GetComponent<PlayerStats> ().RpcRespawnPlayer();
					team2 [i].GetComponent<PlayerStats> ().RpcStartStuff ();
					Invoke ("RespawnPlayerTimer", 2.0f);
				}
			}
		}
	}

	public void RemovePlayer(GameObject playerObject , int teamID){
		if(teamID == 1){
//			for(int i = 0; i < team1.Count; i++){
//				if(team1[i] == playerObject){
//					team1.Remove (team1[i]);
//				}
//			}
			team1.Remove(playerObject);
		}
		if(teamID == 2){
			team2.Remove (playerObject);
		}
		otherTeam = !otherTeam;
		players.Remove (playerObject);
	}

	void RespawnPlayerTimer(){
		if(team1.Count > 0){
			//Setting team 1
//			Debug.Log ("Set 1");

			for(int i = 0; i < team1.Count; i++){
//				team1 [i].GetComponent<PlayerStats> ().teamID = 1;
				team1 [i].GetComponent<PlayerStats> ().RpcRespawnPlayer();
			}
		}
		if(team2.Count > 0){

//			Debug.Log ("Set 2");

			//Setting team 2
			for(int i = 0; i < team2.Count; i++){
//				team2 [i].GetComponent<PlayerStats> ().teamID = 2;
				team2 [i].GetComponent<PlayerStats> ().RpcRespawnPlayer();
			}
		}
	}


}

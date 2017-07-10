using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MatchManager : NetworkBehaviour {

	public static MatchManager instance;

	[SyncVar]
	public int team1DeathCount, team2DeathCount;
	public int winKillCount;

	public bool matchEnd;
//	public int maxDeathCount;

	public Text gameStatsText;


	// Use this for initialization
	void Awake () {
		instance = this;
		matchEnd = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(matchEnd){
			return;
		}


		if(GameManager.GM.localPlayerStatsScript.teamID == 1){
			if(GameManager.GM.localPlayerStatsScript == null){
				GameManager.GM.localPlayerStatsScript = GameManager.GM.GetComponent<PlayerStats> ();
			}
			gameStatsText.text = team2DeathCount + " Team Kill";
		}
		else if(GameManager.GM.localPlayerStatsScript.teamID == 2){
			if(GameManager.GM.localPlayerStatsScript == null){
				GameManager.GM.localPlayerStatsScript = GameManager.GM.GetComponent<PlayerStats> ();
			}
			gameStatsText.text = team1DeathCount + " Team Kill";
		}

		if(team1DeathCount >= winKillCount){
			RpcSetGameStatsText ("Team 2 Wins, Match Ended");
			matchEnd = true;
		}
		else if(team2DeathCount >= winKillCount){
			RpcSetGameStatsText ("Team 1 Wins, Match Ended");
			matchEnd = true;
		}

//		if(!isServer){
//			return;
//		}
//
//		if(team1DeathCount >= winKillCount){
//			RpcSetGameStatsText ("Team 2 Wins, Match Ended");
//			matchEnd = true;
//		}
//		else if(team2DeathCount >= winKillCount){
//			RpcSetGameStatsText ("Team 1 Wins, Match Ended");
//			matchEnd = true;
//		}

	}

//	[ClientRpc]
	public void RpcSetGameStatsText(string txt){
		gameStatsText.text = txt;
	}
}

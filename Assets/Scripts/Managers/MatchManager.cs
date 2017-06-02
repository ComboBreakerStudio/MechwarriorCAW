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
		if(!isServer){
			return;
		}

		if(matchEnd){
			return;
		}

		if(team1DeathCount >= winKillCount){
			RpcSetGameStatsText ("Team 2 Wins");
			matchEnd = true;
		}
		else if(team2DeathCount >= winKillCount){
			RpcSetGameStatsText ("Team 1 Wins");
			matchEnd = true;
		}

	}

	[ClientRpc]
	public void RpcSetGameStatsText(string txt){
		gameStatsText.text = txt;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MatchManager : NetworkBehaviour {

	public static MatchManager instance;

	public int[] teamDeathCount;
	public int maxDeathCount;

	public Text gameStatsText;

	// Use this for initialization
	void Awake () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if(!isServer){
			return;
		}
		for(int i = 0; i < teamDeathCount.Length; i++){
			if(teamDeathCount[i] >= maxDeathCount){
				RpcSetGameStatsText( "Team " + i + " Wins!");
			}
		}
	}

	[ClientRpc]
	public void RpcSetGameStatsText(string txt){
		gameStatsText.text = txt;
	}
}

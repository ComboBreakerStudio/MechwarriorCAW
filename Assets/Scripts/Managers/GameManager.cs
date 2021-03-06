﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Networking;

public class GameManager : MonoBehaviour {

	public static GameManager GM;

	public GameObject localPlayer;
	public PlayerStats localPlayerStatsScript;
	public PlayerShoot localPlayerShootScript;

	public GameObject[] respawnPosition_Team1;
	public GameObject[] respawnPosition_Team2;

	public PlayerUI playerUIScript;

	public bool isPlanningPhase;

	public GameObject uiObject,planningPhaseUI;

	public SlotRegionUI[] slotRegionUIScript;


	void Awake()
	{
		if (GM == null) 
		{
			DontDestroyOnLoad (gameObject);
			GM = this;
		} 

		else if (GM != this)
		{
			Destroy (gameObject);
		}
	}

	void Start(){
//		uiObject.SetActive (false);
	}

//	[Command]
//	public void CmdRespawnPlayer (){
//		RpcRespawnPlayer ();
//	}

//	[ClientRpc]
	public void RespawnPlayer(){
//		localPlayerStatsScript = localPlayer.GetComponent<PlayerStats> ();

		localPlayer.GetComponent<PlayerLoadout> ().LoadParts ();
		localPlayerStatsScript.StartStuff ();
		localPlayerShootScript = localPlayer.GetComponent<PlayerShoot> ();
		playerUIScript.playerStatScript = localPlayerStatsScript;

		if(isPlanningPhase){
			uiObject.SetActive (false);
		}
//		Debug.Log ("Player Respawned");


//		do {
		if (localPlayerStatsScript.teamID == 1) {
			localPlayer.transform.position = respawnPosition_Team1 [Random.Range (0, respawnPosition_Team1.Length)].transform.position;
		}
		if (localPlayerStatsScript.teamID == 2) {
			localPlayer.transform.position = respawnPosition_Team2 [Random.Range (0, respawnPosition_Team2.Length)].transform.position;
		}
//		} while (localPlayerStatsScript.teamID != 1 || localPlayerStatsScript.teamID != 2);

		localPlayer.GetComponent<PlayerStats> ().CmdEnablePlayer (true);
		localPlayer.GetComponent<PlayerStats> ().CmdResetStats ();

	}

	public void SetPlanningPhase(bool phase){
		isPlanningPhase = phase;
		uiObject.SetActive (true);
		StartCoroutine ("disableObjects",1f);

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	IEnumerator disableObjects(float t){
		yield return new WaitForSeconds (t);
		planningPhaseUI.SetActive (false);
	}

	#region AI Region


	//1 = High, 2 = Low, 3 = midHigh, 4 = midRight, 5 = midMid, 6 = midLeft
	public void SetAIPosition(int unitType, int spawnPointType){
//		GameObject[] ai;

//		for(int i = 0; i < AIManager.instance.AIUnits.Count; i++){
//			if(AIManager.instance.AIUnits[i].GetComponent<AIStats>().OwnerName == localPlayer.name){
//				aiObject.Add (AIManager.instance.AIUnits[i]);
//			}
//		}

		localPlayerStatsScript.CmdSetUnitPosition (unitType, spawnPointType);
//		localPlayerStatsScript.CmdAddUnit ();
	}

	#endregion
}

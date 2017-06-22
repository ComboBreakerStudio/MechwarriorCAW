using System.Collections;
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
//		playerUIScript.playerStatScript = localPlayerStatsScript;

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
}

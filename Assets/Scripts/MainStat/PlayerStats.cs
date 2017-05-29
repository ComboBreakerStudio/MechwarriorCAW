using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerStats : NetworkBehaviour {
	[SyncVar]
	public int teamID;

	[SyncVar]
	public bool isAlive;

	[SerializeField]
	private TorsoStats torsoStatsScript;

	[SerializeField]
	private LegStats legStats;

	[SerializeField]
	private WeaponSystemStats leftWeaponSystemStats, rightWeaponSystemStats;

	[SyncVar]
	public int torso_Health, leg_Health, leftWeaponSystem_Health, rightWeaponSystem_Health;

	void Start(){
		if (isServer) {
			TeamManager.instance.AddPlayerToList (this.gameObject);
		}
		ResetStats ();
		if(isLocalPlayer){
			GameManager.GM.localPlayer = this.gameObject;
		}

		//TestingPurpose
//		GameManager.GM.RespawnPlayer();
	}

	void Update(){
		//Test
		if(isLocalPlayer){


			//Test
			if(Input.GetKeyDown(KeyCode.R)){
				Debug.Log ("Pressed");
				GameManager.GM.RespawnPlayer ();
			}
		}

		if(!isServer){
			return;
		}
		
		if(torso_Health <= 0){
			isAlive = false;
			CmdEnablePlayer (false);
		}
	}

	//Disable in the Server
	[Command]
	public void CmdEnablePlayer(bool temp){
		RpcEnablePlayer (temp);
	}
	[ClientRpc]
	public void RpcEnablePlayer(bool temp){
		this.gameObject.SetActive (temp);
	}
	//End of Disable

	public void ResetStats(){
		torso_Health += torsoStatsScript.maxHealth;
		leg_Health = legStats.maxHealth;
		leftWeaponSystem_Health = leftWeaponSystemStats.maxHealth;
		rightWeaponSystem_Health = rightWeaponSystemStats.maxHealth;


	}
}

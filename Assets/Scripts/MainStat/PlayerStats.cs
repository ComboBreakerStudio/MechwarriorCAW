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
	private int torso_Health, leg_Health, leftWeaponSystem_Health, rightWeaponSystem_Health;

	void Start(){
	}

	void Update(){
		if(!isServer){
			return;
		}
		
		if(torso_Health <= 0){
			isAlive = true;
			CmdEnablePlayer (false);
		}
	}

	//Disable in the Server
	[Command]
	void CmdEnablePlayer(bool temp){
		RpcEnablePlayer (temp);
	}
	[ClientRpc]
	void RpcEnablePlayer(bool temp){
		this.gameObject.SetActive (temp);
	}
	//End of Disable

	private void ResetStats(){
		torso_Health = torsoStatsScript.maxHealth;
		leg_Health = legStats.maxHealth;
		leftWeaponSystem_Health = leftWeaponSystemStats.maxHealth;
		rightWeaponSystem_Health = rightWeaponSystemStats.maxHealth;


	}

}

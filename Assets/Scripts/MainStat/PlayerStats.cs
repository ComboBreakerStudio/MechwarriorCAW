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
	private LegStats leftLegStats, rightLegStats;

	[SerializeField]
	private WeaponSystemStats leftWeaponSystemStats, rightWeaponSystemStats;

	[SyncVar]
	public int torso_Health, leftLeg_Health, rightLeg_Health , leftWeaponSystem_Health, rightWeaponSystem_Health;

	void Start(){
		if (isServer) {
			TeamManager.instance.AddPlayerToList (this.gameObject);
		}
		CmdResetStats ();
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

		if(leftWeaponSystem_Health <= 0){
			RpcDisableLeftWeaponSystem ();
		}

		if(rightWeaponSystem_Health <= 0){
			RpcDisableRightWeaponSystem ();
		}

		if(leftLeg_Health <= 0){
			RpcDisableLeftLeg ();
		}

		if(rightLeg_Health <= 0){
			RpcDisableRightLeg ();
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

	[Command]
	public void CmdResetStats(){
		torso_Health += torsoStatsScript.maxHealth;
		leftLeg_Health = leftLegStats.maxHealth;
		rightLeg_Health = rightLegStats.maxHealth;
		leftWeaponSystem_Health = leftWeaponSystemStats.maxHealth;
		rightWeaponSystem_Health = rightWeaponSystemStats.maxHealth;
	}

	[Client]
	public void RpcDisableLeftWeaponSystem(){
		leftWeaponSystemStats.gameObject.SetActive (false);
	}

	[Client]
	public void RpcDisableRightWeaponSystem(){
		rightWeaponSystemStats.gameObject.SetActive (false);
	}

	[Client]
	public void RpcDisableLeftLeg(){
		leftLegStats.gameObject.SetActive (false);
	}

	[Client]
	public void RpcDisableRightLeg(){
		rightLegStats.gameObject.SetActive (false);
	}

	// 0 = torso, 1 = leftWeapon, 2 = RightWeapon, 3 = LeftLeg, 4 = RightLeg
	[Command]
	public void CmdApplyDamage(int partsID, int dmg){
		if(partsID == 0){
			torso_Health -= dmg;
		}
		else if(partsID == 1){
			leftWeaponSystem_Health -= dmg;
		}
		else if(partsID == 2){
			rightWeaponSystem_Health -= dmg;
		}
		else if(partsID == 3){
			leftLeg_Health -= dmg;
		}
		else if(partsID == 4){
			rightLeg_Health -= dmg;
		}
	}
}

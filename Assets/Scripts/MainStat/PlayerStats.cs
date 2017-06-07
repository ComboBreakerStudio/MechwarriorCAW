using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerStats : NetworkBehaviour {
	[SyncVar]
	public int teamID;


	public MeshRenderer[] meshRenderer;

	[SyncVar]
	public bool isAlive;

	public GameObject explodeVFX;

	[SerializeField]
	private TorsoStats torsoStatsScript;

	[SerializeField]
	private LegStats leftLegStats, rightLegStats;

	[SerializeField]
	private WeaponSystemStats leftWeaponSystemStats, rightWeaponSystemStats;

	[SyncVar]
	public int torso_Health, leftLeg_Health, rightLeg_Health , leftWeaponSystem_Health, rightWeaponSystem_Health;

	//Test
	public bool setColor;

	void Start(){
		meshRenderer = GetComponentsInChildren<MeshRenderer> ();
		if (isServer) {
			TeamManager.instance.AddPlayerToList (this.gameObject);
		}
		CmdResetStats ();
		if(isLocalPlayer){
//			Debug.Log ("AA");
			GameManager.GM.localPlayer = this.gameObject;
//			GameManager.GM.localPlayerStatsScript = GameManager.GM.localPlayer.GetComponent<PlayerStats>();
			GameManager.GM.localPlayerStatsScript = this;
			GameManager.GM.RespawnPlayer ();

//			RpcRespawnPlayer ();
		}

		if(!setColor){

			Debug.Log ("Set Team Color");
			MeshRenderer[] meshRenderer = GetComponentsInChildren<MeshRenderer> ();
			for(int i = 0; i < meshRenderer.Length; i++){
				if(teamID == 2){
					meshRenderer [i].material.color = Color.blue;
				}
				setColor = true;
			}

		}

		//TestingPurpose
//		GameManager.GM.RespawnPlayer();
	}

	void Update(){
		//Test
//		if(!setColor){

//			Debug.Log ("Set Team Color");
			for(int i = 0; i < meshRenderer.Length; i++){
				if(teamID == 1){
					meshRenderer [i].material.color = Color.yellow;
				}
				if(teamID == 2){
					meshRenderer [i].material.color = Color.blue;
				}
				//					setColor = true;
			}

//		}
		if(!isLocalPlayer){
			return;
		}

		//Test
		if(isLocalPlayer){

			//Eject
			if(Input.GetKeyDown(KeyCode.R)){
				Debug.Log ("Eject");
				GameManager.GM.RespawnPlayer ();
				CmdSetMatchKills ();
			}
		}

//		if(!isServer){
//			return;
//		}

		if(!isAlive){
			return;
		}

		if(torso_Health <= 0){
			isAlive = false;
			CmdEnablePlayer (false);
			if(isLocalPlayer){
				CmdSetMatchKills ();
			}
			CmdResetStats ();
			CmdEnablePlayer (true);
			GameManager.GM.RespawnPlayer ();
		}

		if(leftWeaponSystem_Health <= 0){
			RpcDisableLeftWeaponSystem (true);
		}

		if(rightWeaponSystem_Health <= 0){
			RpcDisableRightWeaponSystem (true);
		}

		if(leftLeg_Health <= 0){
			RpcDisableLeftLeg (true);
		}

		if(rightLeg_Health <= 0){
			RpcDisableRightLeg (true);
		}
	}
	[Command]
	void CmdSetMatchKills(){
		if(teamID == 1){
			MatchManager.instance.team1DeathCount += 1;
		}
		if(teamID == 2){
			MatchManager.instance.team2DeathCount += 2;
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
		torso_Health = torsoStatsScript.maxHealth;
		leftLeg_Health = leftLegStats.maxHealth;
		rightLeg_Health = rightLegStats.maxHealth;
		leftWeaponSystem_Health = leftWeaponSystemStats.maxHealth;
		rightWeaponSystem_Health = rightWeaponSystemStats.maxHealth;
		isAlive = true;

		//Enable Parts
		RpcDisableLeftWeaponSystem(false);
		RpcDisableRightWeaponSystem(false);
		RpcDisableLeftLeg(false);
		RpcDisableRightLeg(false);
	}

	[ClientRpc]
	public void RpcDisableLeftWeaponSystem(bool t){
		leftWeaponSystemStats.gameObject.SetActive (!t);
	}

	[ClientRpc]
	public void RpcDisableRightWeaponSystem(bool t){
		rightWeaponSystemStats.gameObject.SetActive (!t);
	}

	[ClientRpc]
	public void RpcDisableLeftLeg(bool t){
		leftLegStats.gameObject.SetActive (!t);
	}

	[ClientRpc]
	public void RpcDisableRightLeg(bool t){
		rightLegStats.gameObject.SetActive (!t);
	}

	[ClientRpc]
	public void RpcRespawnPlayer(){
		GameManager.GM.RespawnPlayer ();
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

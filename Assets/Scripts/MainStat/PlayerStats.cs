using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerStats : NetworkBehaviour {
	[SyncVar]
	public int teamID;

	public MeshRenderer[] meshRenderer;
//	public Transform[] _childObject;
	public PlayerLoadout playerLoadoutScript;

	[SyncVar]
	public bool isAlive;

	//MechParts
	[SyncVar]
	public bool leftTorsoDown, rightTorsoDown, leftWeaponDown, rightWeaponDown, leftLegDown, rightLegDown;

	public GameObject explodeVFX;

	//Torso
//	[SerializeField]
	public TorsoStats frontTorsoStats, backTorsoStats, leftTorsoStats, rightTorsoStats;

	//Legs
//	[SerializeField]
	public LegStats leftLegStats, rightLegStats;
	public Legs legs;

//	[SerializeField]
	public WeaponSystemStats leftWeaponSystemStats, rightWeaponSystemStats;

	[SyncVar]
	public int frontTorso_Health, backTorso_Health, leftTorso_Health, rightTorso_Health,
				leftLeg_Health, rightLeg_Health , leftWeaponSystem_Health, rightWeaponSystem_Health;

	[SyncVar]
	public float playerMoveSpeed, playerMaxSpeed, decelerationRate;

	[SyncVar]
	public bool canMove;

	//Test
	public bool setColor;
<<<<<<< HEAD
	Transform team1Marker;
	Transform team2Marker;
	public Transform mapMarker;
=======
<<<<<<< .merge_file_a14980
=======
	Transform team1Marker;
	Transform team2Marker;
	public Transform mapMarker;
>>>>>>> .merge_file_a03160
>>>>>>> fa41a64fc2b96f27bf74039ef660f7e2354a4cec

//	public void respawn(int id){
//		GameManager.GM.RespawnPlayer ();
//	}

	void Start(){
		meshRenderer = GetComponentsInChildren<MeshRenderer> ();
<<<<<<< HEAD
		foreach (Transform marker in mapMarker.transform)
			marker.gameObject.SetActive (false);
=======
<<<<<<< .merge_file_a14980
=======
		foreach (Transform marker in mapMarker.transform)
			marker.gameObject.SetActive (false);
>>>>>>> .merge_file_a03160
>>>>>>> fa41a64fc2b96f27bf74039ef660f7e2354a4cec


		//Add player to TeamManager
		if (isServer) {
			TeamManager.instance.AddPlayerToList (this.gameObject);
			StartStuff ();
		}

		//		CmdResetStats ();


		if(isLocalPlayer){
			//			Debug.Log ("AA");
			GameManager.GM.localPlayer = this.gameObject;
			//			GameManager.GM.localPlayerStatsScript = GameManager.GM.localPlayer.GetComponent<PlayerStats>();
			GameManager.GM.localPlayerStatsScript = this;
			GameManager.GM.RespawnPlayer ();


			//			RpcRespawnPlayer ();
		}
		else if(!isLocalPlayer){
			Debug.Log ("Not Local Player");
		}



		if(!setColor){

			Debug.Log ("Set Team Color");
			//MeshRenderer[] meshRenderer = GetComponentsInChildren<MeshRenderer> ();
			for(int i = 0; i < meshRenderer.Length; i++){
				if(teamID == 2){
					meshRenderer [i].material.color = Color.blue;
<<<<<<< HEAD
					mapMarker.Find ("BlueMapMarker").gameObject.SetActive (true);
=======
<<<<<<< .merge_file_a14980
=======
					mapMarker.Find ("BlueMapMarker").gameObject.SetActive (true);
>>>>>>> .merge_file_a03160
>>>>>>> fa41a64fc2b96f27bf74039ef660f7e2354a4cec
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
<<<<<<< HEAD
					mapMarker.Find ("RedMapMarker").gameObject.SetActive (true);
				}
				if(teamID == 2){
					meshRenderer [i].material.color = Color.blue;
					mapMarker.Find ("BlueMapMarker").gameObject.SetActive (true);
=======
<<<<<<< .merge_file_a14980
				}
				if(teamID == 2){
					meshRenderer [i].material.color = Color.blue;
=======
					mapMarker.Find ("RedMapMarker").gameObject.SetActive (true);
				}
				if(teamID == 2){
					meshRenderer [i].material.color = Color.blue;
					mapMarker.Find ("BlueMapMarker").gameObject.SetActive (true);
>>>>>>> .merge_file_a03160
>>>>>>> fa41a64fc2b96f27bf74039ef660f7e2354a4cec
				}
				//					setColor = true;
			}

//		}
//		if(!isLocalPlayer){
//			return;
//		}

		if(!isAlive){
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

		if(!isServer){
			return;
		}
//		if(isServer){
//			Debug.Log (this.gameObject.name);
//		}
//
		//Death
		if(leftLeg_Health <= 0 && rightLeg_Health <= 0){
			canMove = false;
		}

		if(frontTorso_Health <= 0 || backTorso_Health <=0){
			isAlive = false;
			CmdEnablePlayer (false);
//			if(isLocalPlayer){
				CmdSetMatchKills ();
//			}
			CmdResetStats ();
			CmdEnablePlayer (true);
			RpcRespawnPlayer ();
//			if(isLocalPlayer){
//				GameManager.GM.RespawnPlayer ();
//			}
		}

		if(leftLeg_Health <= 0 && rightLeg_Health <= 0){
			isAlive = false;
			CmdEnablePlayer (false);
			//			if(isLocalPlayer){
			CmdSetMatchKills ();
			//			}
			CmdResetStats ();
			CmdEnablePlayer (true);
			RpcRespawnPlayer ();
		}

		if(!isAlive){
			return;
		}
		//End of Death

		if(!leftTorsoDown){
			if(leftTorso_Health <= 0){
				RpcDisableLeftTorso (true);
				RpcDisableLeftWeaponSystem (true);
				leftTorsoDown = true;
			}
		}

		if(!rightTorsoDown){
			if(rightTorso_Health <= 0){
				RpcDisableRightTorso (true);
				RpcDisableRightWeaponSystem (true);
				rightTorsoDown = true;
			}
		}

		if(!leftWeaponDown){
			if(leftWeaponSystem_Health <= 0 || leftTorso_Health <=0){
				RpcDisableLeftWeaponSystem (true);
				leftWeaponDown = true;
			}
		}

		if(!rightWeaponDown){
			if(rightWeaponSystem_Health <= 0 || rightTorso_Health <=0){
				RpcDisableRightWeaponSystem (true);
				rightWeaponDown = true;
			}
		}

		if(!leftLegDown){
			if(leftLeg_Health <= 0){
				RpcDisableLeftLeg (true);
				leftLegDown = true;
				CmdChangeMaxSpeed (playerMaxSpeed/2);
			}
		}

		if(!rightLegDown){

			if(rightLeg_Health <= 0){
				RpcDisableRightLeg (true);
				rightLegDown = true;
				CmdChangeMaxSpeed (playerMaxSpeed/2);
			}
		}
	}

	public void StartStuff(){
		//Set Stats Reference
		//		playerLoadoutScript.LoadParts ();
		playerLoadoutScript.Display();
		bool setLeft = true;
		for(int i = 0; i < playerLoadoutScript.enabledObject.Count; i++){
			if(playerLoadoutScript.enabledObject[i].name == playerLoadoutScript.torsoName){

//				frontTorsoStats = playerLoadoutScript.enabledObject [i].GetComponent<TorsoStats> ();

				TorsoStats[] childTorsoStats = playerLoadoutScript.enabledObject [i].GetComponentsInChildren<TorsoStats> ();

				for(int i2 = 0; i2 < childTorsoStats.Length; i2++){
					if(childTorsoStats[i2].gameObject.name == "FrontTorso"){
//						Debug.Log (childTorsoStats[i2].gameObject.name);
						frontTorsoStats = childTorsoStats [i2];
					}
					else if(childTorsoStats[i2].gameObject.name == "BackTorso"){
						backTorsoStats = childTorsoStats [i2];
					}
					else if(childTorsoStats[i2].gameObject.name == "LeftTorso"){
						leftTorsoStats = childTorsoStats [i2];
					}
					else if(childTorsoStats[i2].gameObject.name == "RightTorso"){
						rightTorsoStats = childTorsoStats [i2];
					}
				}
			}
			else if(playerLoadoutScript.enabledObject[i].name == playerLoadoutScript.leftWeaponSystemName && setLeft){
				leftWeaponSystemStats = playerLoadoutScript.enabledObject [i].GetComponent<WeaponSystemStats> ();
				setLeft = false;
			}
			else if(playerLoadoutScript.enabledObject[i].name == playerLoadoutScript.rightWeaponSystemName && !setLeft){
				rightWeaponSystemStats = playerLoadoutScript.enabledObject [i].GetComponent<WeaponSystemStats> ();
			}
			else if(playerLoadoutScript.enabledObject[i].name == playerLoadoutScript.legName){
				legs = playerLoadoutScript.enabledObject [i].GetComponent<Legs> ();
				LegStats[] childgo = GetComponentsInChildren<LegStats> ();
				for(int i2 = 0; i2 < childgo.Length; i2++){
					if(childgo[i2].gameObject.name == "LeftLeg"){
						leftLegStats = childgo [i2];
					}
					else if(childgo[i2].gameObject.name == "RightLeg"){
						rightLegStats = childgo [i2];
					}
				}
			}
		}


		GetComponent<PlayerShoot> ().SetWeapon();
//		CmdResetStats ();
		if(isServer){
			RpcStartStuff ();
		}
	}

	[ClientRpc]
	public void RpcStartStuff(){
		//Set Stats Reference
		//		playerLoadoutScript.LoadParts ();
		playerLoadoutScript.Display();
		bool setLeft = true;
		for(int i = 0; i < playerLoadoutScript.enabledObject.Count; i++){
			if(playerLoadoutScript.enabledObject[i].name == playerLoadoutScript.torsoName){
//				frontTorsoStats = playerLoadoutScript.enabledObject [i].GetComponent<TorsoStats> ();

				TorsoStats[] childTorsoStats = playerLoadoutScript.enabledObject [i].GetComponentsInChildren<TorsoStats> ();

				for(int i2 = 0; i2 < childTorsoStats.Length; i2++){
					if(childTorsoStats[i2].gameObject.name == "FrontTorso"){
//						Debug.Log (childTorsoStats[i2].gameObject.name);
						frontTorsoStats = childTorsoStats [i2];
					}
					else if(childTorsoStats[i2].gameObject.name == "BackTorso"){
						backTorsoStats = childTorsoStats [i2];
					}
					else if(childTorsoStats[i2].gameObject.name == "LeftTorso"){
						leftTorsoStats = childTorsoStats [i2];
					}
					else if(childTorsoStats[i2].gameObject.name == "RightTorso"){
						rightTorsoStats = childTorsoStats [i2];
					}
				}
			}
			else if(playerLoadoutScript.enabledObject[i].name == playerLoadoutScript.leftWeaponSystemName && setLeft){
				leftWeaponSystemStats = playerLoadoutScript.enabledObject [i].GetComponent<WeaponSystemStats> ();
				setLeft = false;
			}
			else if(playerLoadoutScript.enabledObject[i].name == playerLoadoutScript.rightWeaponSystemName && !setLeft){
				rightWeaponSystemStats = playerLoadoutScript.enabledObject [i].GetComponent<WeaponSystemStats> ();
			}
			else if(playerLoadoutScript.enabledObject[i].name == playerLoadoutScript.legName){
				legs = playerLoadoutScript.enabledObject [i].GetComponent<Legs> ();
				LegStats[] childgo = GetComponentsInChildren<LegStats> ();
				for(int i2 = 0; i2 < childgo.Length; i2++){
					if(childgo[i2].gameObject.name == "LeftLeg"){
						leftLegStats = childgo [i2];
					}
					else if(childgo[i2].gameObject.name == "RightLeg"){
						rightLegStats = childgo [i2];
					}
				}
			}
		}


		GetComponent<PlayerShoot> ().SetWeapon();
		CmdResetStats ();
	}


	void OnDestroy(){
		TeamManager.instance.RemovePlayer(this.gameObject, teamID);
	}

	[Command]
	void CmdChangeMaxSpeed(float maxSpeed){
		playerMaxSpeed = maxSpeed;
	}

	[Command]
	void CmdSetMatchKills(){
		if(teamID == 1){
			MatchManager.instance.team1DeathCount += 1;
		}
		if(teamID == 2){
			MatchManager.instance.team2DeathCount += 1;
		}
		Debug.Log ("Match kills");
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
		frontTorso_Health = frontTorsoStats.maxHealth;
		backTorso_Health = backTorsoStats.maxHealth;
		leftTorso_Health = leftTorsoStats.maxHealth;
		rightTorso_Health = rightTorsoStats.maxHealth;
		leftLeg_Health = leftLegStats.maxHealth;
		rightLeg_Health = rightLegStats.maxHealth;
		leftWeaponSystem_Health = leftWeaponSystemStats.maxHealth;
		rightWeaponSystem_Health = rightWeaponSystemStats.maxHealth;

		//Movement
		playerMoveSpeed = legs.playerMoveSpeed;
		playerMaxSpeed = legs.playerMaxSpeed;
		decelerationRate = legs.decelerationRate;

		isAlive = true;
		canMove = true;

		//MechParts
		leftTorsoDown = false;
		rightTorsoDown = false;
		leftWeaponDown = false;
		rightWeaponDown = false;
		leftLegDown = false;
		rightLegDown = false;

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
	public void RpcDisableLeftTorso(bool t){
		leftTorsoStats.gameObject.SetActive (!t);
	}

	[ClientRpc]
	public void RpcDisableRightTorso(bool t){
		rightTorsoStats.gameObject.SetActive (!t);
	}

	[ClientRpc]
	public void RpcRespawnPlayer(){
		if(GameManager.GM.localPlayer == this.gameObject){
			GameManager.GM.RespawnPlayer ();
		}

	}

	// 0 = torso, 1 = leftWeapon, 2 = RightWeapon, 3 = LeftLeg, 4 = RightLeg
	[Command]
	public void CmdApplyDamage(int partsID, int dmg){
		if(partsID == 0){
			frontTorso_Health -= dmg;
		}
		else if(partsID == 1){
			backTorso_Health -= dmg;
		}
		else if(partsID == 2){
			leftTorso_Health -= dmg;
		}
		else if(partsID == 3){
			rightTorso_Health -= dmg;
		}
		else if(partsID == 4){
			leftWeaponSystem_Health -= dmg;
		}
		else if(partsID == 5){
			rightWeaponSystem_Health -= dmg;
		}
		else if(partsID == 6){
			leftLeg_Health -= dmg;
		}
		else if(partsID == 7){
			rightLeg_Health -= dmg;
		}
	}
}

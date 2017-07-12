using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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

	//Check LocalPlayer
	public bool isLocal;

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

	//UI
	public AmmoUI ammoUIScript;

//	public void respawn(int id){
//		GameManager.GM.RespawnPlayer ();
	//	}

	#region AI region ================================================================================================

	public List<GameObject> aiObject;
	public List<PlanningPhase_DragableUI> aiUI;

	public void AddaiUI(PlanningPhase_DragableUI temp){

		aiUI.Remove (temp);
		aiUI.Add (temp);
	}

	[Command]
	public void CmdSpawnUnits(string ownerName, int unitType){
		Debug.Log ("CmdSpawnUnits TeamID : " + teamID);
		if(teamID == 1){
			PlanningPhaseManager.instance.CmdSpawnAI (ownerName, unitType, GameManager.GM.respawnPosition_Team1[0].transform.position);
		}
		else if(teamID == 2){
			PlanningPhaseManager.instance.CmdSpawnAI (ownerName, unitType, GameManager.GM.respawnPosition_Team1[0].transform.position);
		}

	}

	[Command]
	public void CmdAddUnit(){
		StopCoroutine ("addUnitTimer");
		StartCoroutine ("addUnitTimer", 2f);
//		Debug.Log ("AddUnit");
//		aiObject.Clear ();
//		Debug.Log (AIManager.instance.AIUnits.Count);
//		for(int i = 0; i < AIManager.instance.AIUnits.Count; i++){
//			if(AIManager.instance.AIUnits[i].GetComponent<AIStats>().OwnerName == GameManager.GM.localPlayer.name){
//				aiObject.Add (AIManager.instance.AIUnits[i]);
//				Debug.Log ("Added");
//			}
//		}
	}
	[ClientRpc]
	public void RpcAddUnit(){
		AddUnit ();
	}

	public void AddUnit(){

		Debug.Log ("AddUnit");
		aiObject.Clear ();
		for(int i = 0; i < AIManager.instance.AIUnits.Count; i++){
			if(AIManager.instance.AIUnits[i].GetComponent<AIStats>().OwnerName == this.gameObject.name){
				aiObject.Add (AIManager.instance.AIUnits[i]);
				Debug.Log ("Added");
				AIManager.instance.AIUnits [i].GetComponent<AIStats> ().teamID = teamID;
			}
		}
	}

	IEnumerator addUnitTimer(float t){
		yield return new WaitForSeconds (t);
		AddUnit ();
		RpcAddUnit ();
	}

	[Command]
	public void CmdSetUnitPosition(int unitType, int positionType){
		//1 = High, 2 = Low, 3 = midHigh, 4 = midRight, 5 = midMid, 6 = midLeft

		Debug.Log ("Player Name: " + this.gameObject.name);

		AIStats aiStats;

		for(int i = 0; i < aiObject.Count; i++){
			aiStats = aiObject [i].GetComponent<AIStats> ();
			Debug.Log ("getStats "+ this.gameObject.name);
			if(aiStats.OwnerName == this.gameObject.name){
				Debug.Log ("found AI "+ this.gameObject.name);
				//tank Units
				if(!aiStats.isPlanned){
					Debug.Log ("Not Planned " + this.gameObject.name);
					if(aiStats.unitType == unitType){
						Debug.Log ("positionTYpe : " + positionType);
						if(teamID == 1){
//							aiObject[i].transform.position = GameManager.GM.slotRegionUIScript [positionType - 1].team1Position.transform.position;
							aiStats.NavAgent.enabled = false;
							aiObject[i].transform.position = GameManager.GM.respawnPosition_Team1[0].transform.position;
							aiStats.NavAgent.enabled = true;
//							aiStats.NavAgent.SetDestination (GameManager.GM.slotRegionUIScript [positionType - 1].team1Position.transform.position);
							aiStats.NavAgent.SetDestination (aiUI[i].uiAIStatsScript.destination);
//							aiStats.SetDestination (GameManager.GM.slotRegionUIScript [positionType - 1].team1Position.transform.position);
							aiStats.SetDestination (aiUI[i].uiAIStatsScript.destination);
							Debug.Log ("SetPosition 1" + GameManager.GM.respawnPosition_Team1[0].transform.position);
						}
						else if(teamID == 2){
//							aiObject[i].transform.position = GameManager.GM.slotRegionUIScript [positionType - 1].team2Position.transform.position;
//							aiStats.NavAgent.SetDestination (GameManager.GM.slotRegionUIScript [positionType - 1].team2Position.transform.position);
							aiStats.NavAgent.enabled = false;
							aiObject[i].transform.position = GameManager.GM.respawnPosition_Team2[0].transform.position;
							aiStats.NavAgent.enabled = true;
//							aiStats.NavAgent.SetDestination (GameManager.GM.slotRegionUIScript [positionType - 1].team2Position.transform.position);
							aiStats.NavAgent.SetDestination (aiUI[i].uiAIStatsScript.destination);
//							aiStats.SetDestination (GameManager.GM.slotRegionUIScript [positionType - 1].team2Position.transform.position);
							aiStats.SetDestination (aiUI[i].uiAIStatsScript.destination);
							Debug.Log ("SetPosition 2" + GameManager.GM.respawnPosition_Team2[0].transform.position);
						}
//						aiStats.isPlanned = true;
						break;
					}
				}
			}
		}
	}

	[Command]
	public void CmdSetUnitPosition_UI(string aiName, Vector3 destination){
		for(int i = 0; i < AIManager.instance.AIUnits.Count; i++){
			if(AIManager.instance.AIUnits[i].name == aiName){
				AIStats aiStats = AIManager.instance.AIUnits[i].GetComponent<AIStats>();

				aiStats.NavAgent.enabled = false;

				if(!aiStats.isRespawned){
					if(teamID == 1){
						AIManager.instance.AIUnits[i].transform.position = GameManager.GM.respawnPosition_Team1[0].transform.position;
						aiStats.isRespawned = true;
					}
					else if(teamID == 2){
						AIManager.instance.AIUnits [i].transform.position = GameManager.GM.respawnPosition_Team2 [0].transform.position;
						aiStats.isRespawned = true;
					}
				}

				aiStats.NavAgent.enabled = true;
				aiStats.NavAgent.SetDestination (destination);
				aiStats.SetDestination (destination);
			}
		}
	}

	public void PlanAI(int unitType, string ownerName, PlanningPhase_DragableUI _aiUI){
		for(int i = 0; i < aiUI.Count; i++){

			if(aiUI[i] == _aiUI){
//				Debug.Log ("Hi");
				for(int i2 = 0; i2 < aiObject.Count; i2++){

					AIStats _aiStatsScript = aiObject[i2].GetComponent<AIStats>();
//					Debug.Log (_aiStatsScript.OwnerName + ownerName);

					if(_aiStatsScript.OwnerName == ownerName){
						Debug.Log (_aiStatsScript.OwnerName + ownerName);
						if(_aiStatsScript.unitType == unitType){
							if(!_aiStatsScript.isPlanned){
								_aiStatsScript.isPlanned = true;
								aiUI [i].aiName = _aiStatsScript.gameObject.name;
							}
						}
					}
				}
			}
		}
	}

	//Damage AI
	[Command]
	public void CmdDamageAI(string aiName, int damage){
		Debug.Log (aiName);
		for(int i = 0; i < AIManager.instance.AIUnits.Count; i++){
			if(AIManager.instance.AIUnits[i].name == aiName){
				AIManager.instance.AIUnits [i].GetComponent<AIStats> ().curHealth -= damage;
			}
		}
	}

	public LayerMask aiCommandMask;
	public Camera myCamera;

	[Command]
	public void CmdCommandAI(){
		RaycastHit hit;
		if (Physics.Raycast (myCamera.transform.position, myCamera.transform.forward, out hit, 1000, aiCommandMask)) {
			if(hit.collider.gameObject.CompareTag("Player")){
				//Same Team Member
				if (hit.collider.gameObject.GetComponent<DealDamage> ().playerStats.teamID == teamID) {
					
				}
				//Not same Team
				else {
				
				}
			}
			if(hit.collider.gameObject.CompareTag("AI")){
				//Same Team Member
				if(hit.collider.gameObject.GetComponent<AIStats>().teamID == teamID){
					
				}
				//Not same Team
				else{
					
				}
			}

			if(hit.collider.gameObject.CompareTag("Terrain")){
				Debug.Log ("Terrain");
			}
		}
	}

	#endregion //=======================================================================================================

	void Awake(){

		if(isLocalPlayer){
			Debug.Log ("isLocalAwake");
			isLocal = true;
			GameManager.GM.localPlayerStatsScript = this;
		}

	}

	void Start(){

		meshRenderer = GetComponentsInChildren<MeshRenderer> ();


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
//		if(!isLocalPlayer){
//			return;
//		}

		if(isLocalPlayer){
//			Debug.Log ("isLocal");
//			isLocal = true;
		}

		if(!isAlive){
			return;
		}
		//Test
		if(isLocalPlayer){
//			if(frontTorso_Health > 0 && !this.gameObject.activeSelf){
			if(frontTorso_Health > 0){
				this.gameObject.SetActive (true);
			}
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
		//UI
		ammoUIScript.StartStuffs ();
		if(GameManager.GM.gameObject == this.gameObject){
			GameManager.GM.playerUIScript.StartStuffs ();
		}
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
		//UI
		ammoUIScript.StartStuffs ();
		if(GameManager.GM.gameObject == this.gameObject){
			GameManager.GM.playerUIScript.StartStuffs ();
		}
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
		if(GameManager.GM.isPlanningPhase){
			return;
		}
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
		Debug.Log ("Respawn Player");
		if(GameManager.GM.localPlayer == this.gameObject){
			GameManager.GM.RespawnPlayer ();
//			this.gameObject.SetActive (true);
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

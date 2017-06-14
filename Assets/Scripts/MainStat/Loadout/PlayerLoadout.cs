using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerLoadout : NetworkBehaviour {

	public List<GameObject> torsoParts, leftWeaponSystemParts, rightWeaponSystemParts, legParts, enabledObject;
	public LoadoutPartsName loadoutPartsNameScript;
	[SyncVar]
	public string torsoName, leftWeaponSystemName, rightWeaponSystemName, legName;
	public PlayerStats playerStatsScript;
	public Transform[] playerChild;

	// Use this for initialization
	void Start () {
		playerStatsScript = GetComponent<PlayerStats>();

		playerChild = GetComponentsInChildren<Transform>();

		for(int i = 0; i < playerChild.Length; i++){
			//Set TorsoObjects
			if(playerChild[i].name == "TorsoPivot"){
				TorsoStats[] torsoObjects = playerChild [i].GetComponentsInChildren<TorsoStats> ();
				for(int i2 = 0; i2 < torsoObjects.Length; i2++){
					torsoParts.Add (torsoObjects[i2].gameObject);
				}
			}
			if(playerChild[i].name == "LeftWeaponPivot"){
				WeaponSystemStats[] weaponSystem = playerChild [i].GetComponentsInChildren<WeaponSystemStats> ();
				for(int i2 = 0; i2 < weaponSystem.Length; i2++){
					leftWeaponSystemParts.Add (weaponSystem[i2].gameObject);
				}
			}
			if(playerChild[i].name == "RightWeaponPivot"){
				WeaponSystemStats[] weaponSystem = playerChild [i].GetComponentsInChildren<WeaponSystemStats> ();
				for(int i2 = 0; i2 < weaponSystem.Length; i2++){
					rightWeaponSystemParts.Add (weaponSystem[i2].gameObject);
				}
			}
			if(playerChild[i].name == "LegPivot"){
				Legs[] legChild = playerChild [i].GetComponentsInChildren<Legs> ();
				for(int i2 = 0; i2 < legChild.Length; i2++){
					legParts.Add (legChild[i2].gameObject);
				}
			}
		}

		LoadParts ();

//		playerStatsScript.StartStuff ();
	}

	public void LoadParts(){
		GameManager.GM.localPlayerStatsScript = playerStatsScript;

		loadoutPartsNameScript = GameObject.Find ("loadoutPartsNameScript").GetComponent<LoadoutPartsName>();
		torsoName = loadoutPartsNameScript.torsoName;
		leftWeaponSystemName = loadoutPartsNameScript.leftWeaponSystemName;
		rightWeaponSystemName = loadoutPartsNameScript.rightWeaponSystemName;
		legName = loadoutPartsNameScript.legName;

		if(isLocalPlayer){
			CmdSetPartsName (torsoName, leftWeaponSystemName, rightWeaponSystemName, legName);
		}

//		if(isLocalPlayer){
			Display ();
//		}
	}

	public void Display(){
		EnableParts (torsoName, torsoParts);
		EnableParts (leftWeaponSystemName, leftWeaponSystemParts);
		EnableParts (rightWeaponSystemName, rightWeaponSystemParts);
		EnableParts (legName, legParts);
	}

	[Command]
	public void CmdSetPartsName(string _torsoName, string left, string right, string leg){
//		Debug.Log ("Set");
		torsoName = _torsoName;
		leftWeaponSystemName = left;
		rightWeaponSystemName = right;
		legName = leg;
	}

	void EnableParts(string partName, List<GameObject> mechParts){
//		Debug.Log ("Parts");
		bool canAdd = true;
		for(int i = 0; i < mechParts.Count; i++){
			if (mechParts [i].name != partName) {
				mechParts [i].SetActive (false);
//				Debug.Log (mechParts [i].name);
				//				break;
			} 
			else {
				for(int i2 = 0; i2 < enabledObject.Count; i2++){
					if (mechParts [i] == enabledObject [i2]) {
						canAdd = false;
					}
				}
				if(canAdd){
					enabledObject.Add (mechParts[i]);
				}
			}
		}
	}
}

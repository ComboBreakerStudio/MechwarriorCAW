using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LoadoutManagerV2 : NetworkBehaviour {

	public static LoadoutManagerV2 instance;
//	public LoadoutPartsName loadoutPickerScript;

	public CustomNetworkManager cnm;

//	[SyncVar]
	public string torsoName, leftWeaponSystemName, rightWeaponSystemName, legName;

	public GameObject playerPrefabObject, playerObject,
						torsoObject, leftWeaponObject, rightWeaponObject, legObject;
	public Transform[] playerChild;

	public List<GameObject> torsoParts, leftWeaponSystemParts, rightWeaponSystemParts, legParts;

	// Use this for initialization
	void Start () {

		//Setting Manager Instance
		instance = this;
//		CmdLoadout ();

	}


	[Command]
	public void CmdLoadout(){
		
		//Loadout
		playerObject = Instantiate (playerPrefabObject);
		playerObject.GetComponent<PlayerLoadout> ().LoadParts ();
		playerObject.SetActive (false);
		DontDestroyOnLoad (playerObject);
		cnm.spawnPlayerObject = playerObject;
		NetworkServer.Spawn (playerObject);
	}

	public void SetParts(){
		EnableParts (torsoName, torsoParts, true);
		EnableParts (leftWeaponSystemName, leftWeaponSystemParts, true);
		EnableParts (rightWeaponSystemName, rightWeaponSystemParts, true);
		EnableParts (legName, legParts, true);
	}

	void EnableParts(string partName, List<GameObject> mechParts, bool isEnable){
		for(int i = 0; i < mechParts.Count; i++){
			if(mechParts[i].name != partName){
				mechParts [i].SetActive(false);
				Debug.Log (mechParts[i].name);
//				break;
			}
		}
	}

	[Command]
	public void CmdLoadoutParts(string partName, int partsInfo){
		RpcLoadoutParts(partName, partsInfo);
	}

	[ClientRpc] //1 = torso, 2 = lefthand, 3 = righthand, 4 = legs
	public void RpcLoadoutParts(string partName, int partsInfo){
//		Debug.Log ("YAY");
//		temp = torsoParts;
		//Torso
		if (partsInfo == 1) {

			for(int i = 0; i < torsoParts.Count; i++){
				if(torsoParts[i].name != partName){
					torsoParts [i].SetActive(false);
					Debug.Log (torsoParts[i].name);
					//				break;
				}
			}
		} 
		else if (partsInfo == 2) {
			for(int i = 0; i < leftWeaponSystemParts.Count; i++){
				if (leftWeaponSystemParts [i].name != partName) {
					leftWeaponSystemParts [i].SetActive (false);
					Debug.Log (leftWeaponSystemParts [i].name);
					//				break;
				}
			}
		} 
		else if (partsInfo == 3) {
			for (int i = 0; i < rightWeaponSystemParts.Count; i++) {
				if (rightWeaponSystemParts [i].name != partName) {
					rightWeaponSystemParts [i].SetActive (false);
					Debug.Log (rightWeaponSystemParts [i].name);
					//				break;
				}
			}
		}
		else if(partsInfo == 4){
			for (int i = 0; i < legParts.Count; i++) {
				if (legParts [i].name != partName) {
					legParts [i].SetActive (false);
					Debug.Log (legParts [i].name);
					//				break;
				}
			}
		}
	}
}

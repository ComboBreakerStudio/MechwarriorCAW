using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetworkManager : NetworkManager {

	public static CustomNetworkManager instance;
	public LoadoutManagerV2 myLoadoutManager;
	public LoadoutPartsName loadoutPartsNameScript;

	public GameObject spawnPlayerObject;


	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId){
		//		SetParts ();
		//		Debug.Log("AA");
		//		EnableParts(leftWeaponSystemName, leftWeaponSystemParts, false);
		//		EnableParts(rightWeaponSystemName, rightWeaponSystemParts, false);
//		myLoadoutManager.LoadoutOut();
		myLoadoutManager.CmdLoadout ();
		spawnPlayerObject.SetActive (true);
		NetworkServer.AddPlayerForConnection(conn, spawnPlayerObject, playerControllerId);
	}

	void Start(){
		instance = this;
		Debug.Log ("No");
		DontDestroyOnLoad (myLoadoutManager);
		DontDestroyOnLoad (loadoutPartsNameScript);
	}
}

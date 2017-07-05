using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LoadoutManager : NetworkBehaviour {

	public GameObject torso, leftArm, rightArm, legs;

	public GameObject playerObject, playerPrefabObject;
	public Transform[] childObjects;

	public GameObject testObject;


	public void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	{
		GameObject ga = Instantiate (playerObject);
		Destroy (playerObject);
		playerObject = ga;
		playerObject.SetActive (true);
		NetworkServer.Spawn (playerObject);
		NetworkServer.AddPlayerForConnection(conn, playerObject, playerControllerId);
	}

	// Use this for initialization
	void Start(){
		SetMech();
//		NetworkManager.singleton.playerPrefab = playerObject;
	}



//	public void SetMech(GameObject _torso, GameObject _leftArm, GameObject _rightArm, GameObject _legs){
	public void SetMech(){
//		torso = _torso;
//		leftArm = _leftArm;
//		rightArm = _rightArm;
//		legs = _legs;

		//Set Parts Step Legs -> Torso ->LeftArm -> RightArm

		//Set Leg to Player
		playerObject = Instantiate(playerPrefabObject);

		RpcSetParts (playerObject, legs, "LegPivot");
		RpcSetParts (playerObject, torso, "TorsoPivot");
		RpcSetParts (playerObject, leftArm, "LeftArmPivot");
		RpcSetParts (playerObject, rightArm, "RightArmPivot");

		NetworkManager.singleton.playerPrefab = playerObject;
//		Instantiate (testObject);
		playerObject.SetActive(false);
		DontDestroyOnLoad (playerObject);

	}

	public void RpcSetParts(GameObject PlayerParts, GameObject SettingParts, string PivotName){
		childObjects = PlayerParts.GetComponentsInChildren<Transform> ();

		GameObject SettingPart = Instantiate (SettingParts);
//		NetworkServer.Spawn (SettingPart);

		for(int i = 0; i < childObjects.Length; i++){
			if(childObjects[i].name == PivotName){
				SettingPart.transform.SetParent (childObjects [i]);
//				SettingParts.transform.parent = childObjects[i].gameObject;
				SettingPart.transform.localPosition = Vector3.zero;
			}
		}
	}
}

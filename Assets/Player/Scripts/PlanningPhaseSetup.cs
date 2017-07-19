using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlanningPhaseSetup : NetworkBehaviour {

	public bool inPlanning;

	public Behaviour[] ComponentsToEnable;
	public GameObject[] GameObjectToEnable;
	public GameObject camObject;
	public Rigidbody myRigidBody;

	void Start(){
		if(!isLocalPlayer){
			return;
		}
		for(int i = 0; i < ComponentsToEnable.Length; i++){
			ComponentsToEnable [i].enabled = false;
		}

		for(int i = 0; i < GameObjectToEnable.Length; i++){
			GameObjectToEnable [i].SetActive (false);
		}
		myRigidBody.useGravity = false;
	}

	void Update(){
		if(!isLocalPlayer){
			camObject.SetActive (false);
			return;
		}
		if(!GameManager.GM.isPlanningPhase){
			for(int i = 0; i < ComponentsToEnable.Length; i++){
				ComponentsToEnable [i].enabled = true;
			}

			for(int i = 0; i < GameObjectToEnable.Length; i++){
				GameObjectToEnable [i].SetActive (true);
			}
			myRigidBody.useGravity = true;
			this.enabled = false;
			if(!isLocalPlayer){
			}
		}
	}
}

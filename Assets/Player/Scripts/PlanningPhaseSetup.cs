using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanningPhaseSetup : MonoBehaviour {

	public bool inPlanning;

	public Behaviour[] ComponentsToEnable;
	public GameObject[] GameObjectToEnable;
	public Rigidbody myRigidBody;

	void Start(){
		for(int i = 0; i < ComponentsToEnable.Length; i++){
			ComponentsToEnable [i].enabled = false;
		}

		for(int i = 0; i < GameObjectToEnable.Length; i++){
			GameObjectToEnable [i].SetActive (false);
		}
		myRigidBody.useGravity = false;
	}

	void Update(){
		if(!GameManager.GM.isPlanningPhase){
			for(int i = 0; i < ComponentsToEnable.Length; i++){
				ComponentsToEnable [i].enabled = true;
			}

			for(int i = 0; i < GameObjectToEnable.Length; i++){
				GameObjectToEnable [i].SetActive (true);
			}
			myRigidBody.useGravity = true;
			this.enabled = false;
		}
	}
}

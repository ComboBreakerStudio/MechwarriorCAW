using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AI;

public class AIStats : NetworkBehaviour {
	[SyncVar]
	public int teamID;
	[SyncVar]
	public int curHealth, maxHealth;
	[SyncVar]
	public string AIName;
	[SyncVar]
	public string OwnerName;

	public int unitType;

	public bool isPlanned;

	public NavMeshAgent NavAgent;

	public MonoBehaviour aiBehaviorScript;

	void Start () {
		if(isServer){
			RegisterAI ();
//			Debug.Log ("AIServer " + AIName);
			curHealth = maxHealth;
		}
		NavAgent = GetComponent<NavMeshAgent> ();
	}

	void Update(){
		if(gameObject.name != AIName){
			gameObject.name = AIName;
		}

		if(curHealth <= 0){
//			Destroy (this.gameObject);
			this.gameObject.SetActive(false);
		}
	}
//	[Command]
	void RegisterAI()
	{
		string _ID = transform.gameObject.name + GetComponent<NetworkIdentity> ().netId;
		AIName = _ID;
		transform.gameObject.name = AIName;
		AIManager.instance.AIUnits.Add (this.gameObject);
	}

//	[Command]
	public void CmdSetOwner(string ownerName){
		OwnerName = ownerName;
	}

	public void SetDestination(Vector3 destination){
		NavAgent.SetDestination (destination);
		aiBehaviorScript.SendMessage ("SetAIPoint", destination);
	}

}

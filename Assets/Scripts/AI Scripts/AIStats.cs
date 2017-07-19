using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
<<<<<<< HEAD

public class AIStats : NetworkBehaviour {

=======
using UnityEngine.AI;

public class AIStats : NetworkBehaviour {
    
	[SyncVar]
>>>>>>> e409efdcc9aa6f6b31c31fe273612a241139ccff
	public int teamID;
	[SyncVar]
	public int curHealth, maxHealth;
<<<<<<< HEAD
	public string AIName;

	void Start () {
		RegisterAI ();
	}

	void RegisterAI()
	{
		string _ID = "AI Unit" + GetComponent<NetworkIdentity> ().netId;
		AIName = _ID;
=======
	[SyncVar]
	public string AIName;
	[SyncVar]
	public string OwnerName;

	public int unitType;

	public bool isPlanned, isRespawned;

	public NavMeshAgent NavAgent;

	public MonoBehaviour aiBehaviorScript;

	void Start () {
		if(isServer){
			RegisterAI ();
//			RegisterAI ();
//			Debug.Log ("AIServer " + AIName);
			curHealth = maxHealth;
		}
		AIManager.instance.AIUnits.Add (this.gameObject);
		NavAgent = GetComponent<NavMeshAgent> ();

		NavAgent.enabled = false;
		aiBehaviorScript.enabled = false;
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
	}

//	[Command]
	public void CmdSetOwner(string ownerName){
		OwnerName = ownerName;
	}

	public void SetDestination(Vector3 destination){
		NavAgent.SetDestination (destination);

		aiBehaviorScript.enabled = true;
//		aiBehaviorScript.SendMessage ("SetAIPoint", destination);
>>>>>>> e409efdcc9aa6f6b31c31fe273612a241139ccff
	}

}

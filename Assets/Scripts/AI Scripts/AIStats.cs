using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AIStats : NetworkBehaviour {
	[SyncVar]
	public int teamID;
	[SyncVar]
	public int curHealth, maxHealth;
	public string AIName;

	void Start () {
		RegisterAI ();
		CmdSpawnToServer ();
	}

	void RegisterAI()
	{
		string _ID = "AI Unit" + GetComponent<NetworkIdentity> ().netId;
		AIName = _ID;
	}

	[Command]
	public void CmdSpawnToServer(){
		AIManager.instance.AIUnits.Add (this.gameObject);
	}

	[Command]
	public void CmdSetPosition(Vector3 position){
		transform.position = position;
	}
}

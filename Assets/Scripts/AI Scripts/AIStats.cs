using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AIStats : NetworkBehaviour {

	public int teamID;
	public int curHealth, maxHealth;
	public string AIName;

	void Start () {
		RegisterAI ();
	}

	void RegisterAI()
	{
		string _ID = "AI Unit" + GetComponent<NetworkIdentity> ().netId;
		AIName = _ID;
	}

}

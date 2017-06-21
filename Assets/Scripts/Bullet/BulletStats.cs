using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletStats : NetworkBehaviour {
	[SyncVar]
	public string playerName;
	[SyncVar]
	public int damage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider coll){
		//		Debug.Log (coll.gameObject.name);
		//		CmdDebug();
		DealDamage dm = coll.GetComponent<DealDamage> ();

		if(dm.playerStats.gameObject.name != playerName && dm != null && playerName != ""){
//			Debug.Log (coll.name);
			Destroy (this.gameObject);
			GameManager.GM.localPlayerShootScript.CmdPlayerShot (dm.playerStats.gameObject.name, dm.partsID, damage);
		}
	}
}

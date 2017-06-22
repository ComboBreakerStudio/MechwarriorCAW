using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletStats : NetworkBehaviour {
	[SyncVar]
	public string playerName;
	[SyncVar]
	public int damage;

	public bool canDamage;

	public GameObject explodeFX;

	// Use this for initialization
	void Start () {
//		canDamage = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider coll){
		//		Debug.Log (coll.gameObject.name);
		//		CmdDebug();

		Debug.Log(coll.gameObject.name);
		DealDamage dm = coll.GetComponent<DealDamage> ();

		if(dm != null){
			if(dm.playerStats.gameObject.name != playerName){
				if(playerName != ""){
					//			Debug.Log (coll.name);
					Destroy (this.gameObject);
					if(canDamage){
						GameManager.GM.localPlayerShootScript.CmdPlayerShot (dm.playerStats.gameObject.name, dm.partsID, damage);
					}
					CmdSpawnFX (transform.position);
				}
			}
		}
	}

	void OnCollisionEnter(Collision coll){
		if (coll.gameObject.CompareTag("Terrain")) {
			Destroy (this.gameObject);
			CmdSpawnFX (transform.position);
		}
	}

	[Command]
	public void CmdSpawnFX(Vector3 position){
		GameObject ga = Instantiate (explodeFX);
		ga.transform.position = position;
		NetworkServer.Spawn (ga);
	}
}

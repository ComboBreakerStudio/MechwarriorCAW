using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletForwardMovement : NetworkBehaviour {

	public float moveSpeed;

	void Start(){
	}

	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);
	}

//	[Command]
//	public void CmdDebug(){
//		Debug.Log ("Bulletttt");
//	}
}

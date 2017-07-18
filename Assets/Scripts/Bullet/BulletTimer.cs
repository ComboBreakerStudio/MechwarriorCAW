using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTimer : MonoBehaviour {

	public float timer;
	public BulletStats bulletStatsScript;

	// Use this for initialization
	void Start () {
		Invoke ("DestroySelf",timer);
	}
	
	public void DestroySelf(){
		Destroy (this.gameObject);
		bulletStatsScript.CmdSpawnFX (transform.position);
	}
}

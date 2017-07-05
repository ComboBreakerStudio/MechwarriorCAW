using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMeTimer : MonoBehaviour {

	public float timer;

	// Use this for initialization
	void Start () {
		StartCoroutine ("DestroyTimer", timer);
	}

	IEnumerator DestroyTimer(float t){
		yield return new WaitForSeconds (t);
		Destroy (this.gameObject);
	}
}

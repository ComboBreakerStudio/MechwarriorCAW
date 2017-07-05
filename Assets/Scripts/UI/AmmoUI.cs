using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour {

	[SerializeField]
	private GameObject leftAmmo;

	[SerializeField]
	private GameObject rightAmmo;

	// Use this for initialization
	void Start () {
		leftAmmo.GetComponentInChildren<Text>().text = "9";
		rightAmmo.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

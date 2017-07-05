using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
<<<<<<< .merge_file_a13420

public class AmmoUI : MonoBehaviour {

	[SerializeField]
	private GameObject leftAmmo;

	[SerializeField]
	private GameObject rightAmmo;

	// Use this for initialization
	void Start () {
		leftAmmo.GetComponentInChildren<Text>().text = "9";
		rightAmmo.SetActive (false);
=======
using UnityEngine.Networking;


public class AmmoUI : NetworkBehaviour {
//public class AmmoUI : MonoBehaviour {

	[SerializeField]
	private Text leftAmmo;

	[SerializeField]
	private Text rightAmmo;

	//[SyncVar]
	public WeaponSystemStats leftWeaponAmmo, rightWeaponAmmo;

	public PlayerStats playerStats;

	// Use this for initialization
	void Start () {
		playerStats.StartStuff ();
		Debug.Log ("Weapon system data acquired");
		rightAmmo.gameObject.SetActive (false);
		Debug.Log ("right side disabled");
		leftAmmo.text = playerStats.leftWeaponSystemStats.currentAmmo.ToString ();
		Debug.Log ("ammo data acquired");
>>>>>>> .merge_file_a07776
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

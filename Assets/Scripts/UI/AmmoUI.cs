using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
<<<<<<< HEAD
using UnityEngine.Networking;
=======
<<<<<<< .merge_file_a13420
>>>>>>> fa41a64fc2b96f27bf74039ef660f7e2354a4cec


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
<<<<<<< HEAD
=======
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
>>>>>>> fa41a64fc2b96f27bf74039ef660f7e2354a4cec
		playerStats.StartStuff ();
		Debug.Log ("Weapon system data acquired");
		rightAmmo.gameObject.SetActive (false);
		Debug.Log ("right side disabled");
		leftAmmo.text = playerStats.leftWeaponSystemStats.currentAmmo.ToString ();
		Debug.Log ("ammo data acquired");
<<<<<<< HEAD
=======
>>>>>>> .merge_file_a07776
>>>>>>> fa41a64fc2b96f27bf74039ef660f7e2354a4cec
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

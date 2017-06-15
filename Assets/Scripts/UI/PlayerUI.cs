using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

	[SerializeField]
	private GameObject pauseMenu;

	[SerializeField]
	private GameObject minimap;

	[SerializeField]
	private Image heatBar;

	[SerializeField]
	private GameObject Health;

	bool isLocalPlayer;

	private GameObject HeadHealth;
	private GameObject TorsoHealth;
	private GameObject LeftTorsoHealth;
	private GameObject LeftArmHealth;
	private GameObject LeftLegHealth;
	private GameObject RightTorsoHealth;
	private GameObject RightArmHealth;
	private GameObject RightLegHealth;

	// Use this for initialization
	void Start () {
		PauseMenu.IsOn = false;
		isLocalPlayer = true;
		//heatBar = 
		Debug.Log ("Found Left Ammo?");
		//rightAmmo = GameObject.Find ("RightAmmo").GetComponentInChildren<Text> ();
		//heatBar.fillAmount = GetComponent<WeaponSystemStats> ().currentHeat;
		heatBar.fillAmount = 0;
		foreach (Transform img in Health.transform) {
			img.gameObject.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			TogglePauseMenu ();
		}
	}

	private void TogglePauseMenu(){
//		Debug.Log ("Pause");
		pauseMenu.SetActive (!pauseMenu.activeSelf);
		PauseMenu.IsOn = pauseMenu.activeSelf;
	}

	//[Client]
	void mapDisplay ()
	{
		if (isLocalPlayer == true) {
			minimap.SetActive (true);
		}
	}
}

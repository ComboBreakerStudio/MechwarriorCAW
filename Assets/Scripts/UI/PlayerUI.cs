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

	//public

	public Transform HeadHealth;
	public Transform TorsoHealth;
	public Transform LeftTorsoHealth;
	public Transform LeftArmHealth;
	public Transform LeftLegHealth;
	public Transform RightTorsoHealth;
	public Transform RightArmHealth;
	public Transform RightLegHealth;

	// Use this for initialization
	void Start () {
		PauseMenu.IsOn = false;
		isLocalPlayer = true;
		//heatBar = 
		Debug.Log ("Found Left Ammo?");
		//rightAmmo = GameObject.Find ("RightAmmo").GetComponentInChildren<Text> ();
		//heatBar.fillAmount = GetComponent<WeaponSystemStats> ().currentHeat;
		heatBar.fillAmount = 0;

		foreach (Transform img in HeadHealth.transform) {
			img.gameObject.SetActive (false);
		}
		foreach (Transform img in TorsoHealth.transform) {
			img.gameObject.SetActive (false);
		}
		foreach (Transform img in LeftTorsoHealth.transform) {
			img.gameObject.SetActive (false);
		}
		foreach (Transform img in LeftArmHealth.transform) {
			img.gameObject.SetActive (false);
		}
		foreach (Transform img in LeftLegHealth.transform) {
			img.gameObject.SetActive (false);
		}
		foreach (Transform img in RightTorsoHealth.transform) {
			img.gameObject.SetActive (false);
		}
		foreach (Transform img in RightArmHealth.transform) {
			img.gameObject.SetActive (false);
		}
		foreach (Transform img in RightLegHealth.transform) {
			img.gameObject.SetActive (false);
		}

		HeadHealth.Find ("HeadGreen").gameObject.SetActive (true);
		TorsoHealth.Find ("BodyGreen").gameObject.SetActive (true);
		LeftTorsoHealth.Find ("LeftTorsoGreen").gameObject.SetActive (true);
		LeftArmHealth.Find ("LeftArmGreen").gameObject.SetActive (true);
		LeftLegHealth.Find ("LeftLegGreen").gameObject.SetActive (true);
		RightTorsoHealth.Find ("RightTorsoGreen").gameObject.SetActive (true);
		RightArmHealth.Find ("RightArmGreen").gameObject.SetActive (true);
		RightLegHealth.Find ("RightLegGreen").gameObject.SetActive (true);
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

	void healthDisplay()
	{
		
	}
}

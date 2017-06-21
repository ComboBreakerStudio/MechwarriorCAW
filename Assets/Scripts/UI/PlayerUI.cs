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

	public PlayerStats playerStatScript;

	//ShakeScreen
	public bool canShakeScreen;
	public float shakeScreenTime;
	public float shakePower;

	// Use this for initialization
	void Start () {
		canShakeScreen = true;
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

		if(playerStatScript != null){
			//Front
			if (playerStatScript.frontTorso_Health <= 0) {
				TorsoHealth.SetActive (false);
			} 
			else {
				TorsoHealth.SetActive (true);
			}
			//left torso
			if (playerStatScript.leftTorso_Health <= 0) {
				LeftTorsoHealth.SetActive (false);
			} 
			else {
				LeftTorsoHealth.SetActive (true);
			}
			//Right Torso
			if (playerStatScript.rightTorso_Health <= 0) {
				RightTorsoHealth.SetActive (false);
			} 
			else {
				RightTorsoHealth.SetActive (true);
			}
			//Left Weapon
			if (playerStatScript.leftWeaponSystem_Health <= 0) {
				LeftArmHealth.SetActive (false);
			} 
			else {
				LeftArmHealth.SetActive (true);
			}
			//Right Weapon
			if (playerStatScript.rightWeaponSystem_Health <= 0) {
				RightArmHealth.SetActive (false);
			} 
			else {
				RightArmHealth.SetActive (true);
			}
			//Left Leg
			if (playerStatScript.leftLeg_Health <= 0) {
				LeftLegHealth.SetActive (false);
			} 
			else {
				LeftLegHealth.SetActive (true);
			}
			//Right Leg
			if (playerStatScript.rightTorso_Health <= 0) {
				RightLegHealth.SetActive (false);
			} 
			else {
				RightLegHealth.SetActive (true);
			}
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

	public void shakeScreen(){
		if(canShakeScreen){
			Health.transform.localPosition = new Vector2 (Health.transform.localPosition.x,Health.transform.localPosition.y + shakePower);
			StartCoroutine ("ShakeScreenUITimer", 0.05f);
			canShakeScreen = false;
		}
	}

	IEnumerator ShakeScreenUITimer(float t){
		yield return new WaitForSeconds (t);
		Health.transform.localPosition = new Vector2 (Health.transform.localPosition.x,Health.transform.localPosition.y - shakePower);
		yield return new WaitForSeconds (t);
		Health.transform.localPosition = new Vector2 (Health.transform.localPosition.x,Health.transform.localPosition.y + shakePower);
		yield return new WaitForSeconds (t);
		Health.transform.localPosition = new Vector2 (Health.transform.localPosition.x,Health.transform.localPosition.y - shakePower);
		yield return new WaitForSeconds (t);
		Health.transform.localPosition = new Vector2 (Health.transform.localPosition.x,Health.transform.localPosition.y + shakePower);
		yield return new WaitForSeconds (t);
		Health.transform.localPosition = new Vector2 (Health.transform.localPosition.x,Health.transform.localPosition.y - shakePower);

		yield return new WaitForSeconds (shakeScreenTime);
		canShakeScreen = true;
	}
}

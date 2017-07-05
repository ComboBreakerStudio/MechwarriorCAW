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

	public Transform HeadHealth;
	public Transform TorsoHealth;
	public Transform LeftTorsoHealth;
	public Transform LeftArmHealth;
	public Transform LeftLegHealth;
	public Transform RightTorsoHealth;
	public Transform RightArmHealth;
	public Transform RightLegHealth;

	/*public GameObject HeadHealth;
	public GameObject TorsoHealth;
	public GameObject LeftTorsoHealth;
	public GameObject LeftArmHealth;
	public GameObject LeftLegHealth;
	public GameObject RightTorsoHealth;
	public GameObject RightArmHealth;
	public GameObject RightLegHealth;*/

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
		//minimap.GetComponentInChildren<RawImage>().texture = 
		if(Input.GetKeyDown(KeyCode.Escape)){
			TogglePauseMenu ();
		}

		if(playerStatScript != null){
			//Front
			if (playerStatScript.frontTorso_Health <= 0) {
				TorsoHealth.gameObject.SetActive (false);
			} 
			else {
				TorsoHealth.gameObject.SetActive (true);
			}
			//left torso
			if (playerStatScript.leftTorso_Health <= 0) {
				LeftTorsoHealth.gameObject.SetActive (false);
			} 
			else {
				LeftTorsoHealth.gameObject.SetActive (true);
			}
			//Right Torso
			if (playerStatScript.rightTorso_Health <= 0) {
				RightTorsoHealth.gameObject.SetActive (false);
			} 
			else {
				RightTorsoHealth.gameObject.SetActive (true);
			}
			//Left Weapon
			if (playerStatScript.leftWeaponSystem_Health <= 0) {
				LeftArmHealth.gameObject.SetActive (false);
			} 
			else {
				LeftArmHealth.gameObject.SetActive (true);
			}
			//Right Weapon
			if (playerStatScript.rightWeaponSystem_Health <= 0) {
				RightArmHealth.gameObject.SetActive (false);
			} 
			else {
				RightArmHealth.gameObject.SetActive (true);
			}
			//Left Leg
			if (playerStatScript.leftLeg_Health <= 0) {
				LeftLegHealth.gameObject.SetActive (false);
			} 
			else {
				LeftLegHealth.gameObject.SetActive (true);
			}
			//Right Leg
			if (playerStatScript.rightTorso_Health <= 0) {
				RightLegHealth.gameObject.SetActive (false);
			} 
			else {
				RightLegHealth.gameObject.SetActive (true);
			}
		}
	}

	private void TogglePauseMenu(){
//		Debug.Log ("Pause");
		pauseMenu.SetActive (!pauseMenu.activeSelf);
		PauseMenu.IsOn = pauseMenu.activeSelf;
	}

	//[Client]
	/*void mapDisplay ()
	{
		if (isLocalPlayer == true) {
			minimap.SetActive (true);
		}
	}*/

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
	void healthDisplay()
	{
		
	}
}

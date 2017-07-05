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

	//bool isLocalPlayer;

	public Transform HeadHealth;
	public Transform TorsoHealth;
	public Transform LeftTorsoHealth;
	public Transform LeftArmHealth;
	public Transform LeftLegHealth;
	public Transform RightTorsoHealth;
	public Transform RightArmHealth;
	public Transform RightLegHealth;

	public Text ammoCount;

	public PlayerStats playerStatScript;

	//ShakeScreen
	public bool canShakeScreen;
	public float shakeScreenTime;
	public float shakePower;
	// Use this for initialization
	void Start () {
		canShakeScreen = true;
		PauseMenu.IsOn = false;
		//isLocalPlayer = true;
		//heatBar = 
		//Debug.Log ("Found Left Ammo?");
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
		healthDisplay ();

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
		if(playerStatScript != null){
			//Front
			if (playerStatScript.frontTorso_Health <= 0) {
				TorsoHealth.Find("BodyRed").gameObject.SetActive (false);
			} 
			else if(playerStatScript.frontTorso_Health <= 35) {
				TorsoHealth.Find ("BodyGreen").gameObject.SetActive (false);
				TorsoHealth.Find("BodyOrange").gameObject.SetActive (true);
			}
			else if(playerStatScript.frontTorso_Health <= 20) {
				TorsoHealth.Find ("BodyOrange").gameObject.SetActive (false);
				TorsoHealth.Find("BodyRed").gameObject.SetActive (true);
			}
			//Head
			if (playerStatScript.backTorso_Health <= 0) {
				HeadHealth.Find ("HeadRed").gameObject.SetActive (false);
			} 
			else if(playerStatScript.backTorso_Health <= 15) {
				HeadHealth.Find ("HeadGreen").gameObject.SetActive (false);
				HeadHealth.Find("HeadOrange").gameObject.SetActive (true);
			}
			else if(playerStatScript.backTorso_Health <= 10) {
				HeadHealth.Find ("HeadOrange").gameObject.SetActive (false);
				HeadHealth.Find("HeadRed").gameObject.SetActive (true);
			}
			//left torso
			if (playerStatScript.leftTorso_Health <= 0) {
				LeftTorsoHealth.Find ("LeftTorsoRed").gameObject.SetActive (false);
			} 
			else if (playerStatScript.leftTorso_Health <= 30) {
				LeftTorsoHealth.Find ("LeftTorsoGreen").gameObject.SetActive (false);
				LeftTorsoHealth.Find ("LeftTorsoOrange").gameObject.SetActive (true);
			}
			else if (playerStatScript.leftTorso_Health <= 15) {
				LeftTorsoHealth.Find ("LeftTorsoOrange").gameObject.SetActive (false);
				LeftTorsoHealth.Find ("LeftTorsoRed").gameObject.SetActive (true);
			}
			//Right Torso
			if (playerStatScript.rightTorso_Health <= 0) {
				RightTorsoHealth.Find ("RightTorsoRed").gameObject.SetActive (false);
			} 
			else if (playerStatScript.rightTorso_Health <= 30) {
				RightTorsoHealth.Find ("RightTorsoGreen").gameObject.SetActive (false);
				RightTorsoHealth.Find ("RightTorsoOrange").gameObject.SetActive (true);
			}
			else if (playerStatScript.rightTorso_Health <= 15) {
				RightTorsoHealth.Find ("RightTorsoOrange").gameObject.SetActive (false);
				RightTorsoHealth.Find ("RightTorsoRed").gameObject.SetActive (true);
			}
			//Left Weapon
			if (playerStatScript.leftWeaponSystem_Health <= 0) {
				LeftArmHealth.Find ("LeftArmRed").gameObject.SetActive (false);
			} 
			else if (playerStatScript.leftWeaponSystem_Health <= 15) {
				LeftArmHealth.Find ("LeftArmGreen").gameObject.SetActive (false);
				LeftArmHealth.Find ("LeftArmOrange").gameObject.SetActive (true);
			}
			else if (playerStatScript.leftWeaponSystem_Health <= 10) {
				LeftArmHealth.Find ("LeftArmOrange").gameObject.SetActive (false);
				LeftArmHealth.Find ("LeftArmRed").gameObject.SetActive (true);
			}
			//Right Weapon
			if (playerStatScript.rightWeaponSystem_Health <= 0) {
				RightArmHealth.Find ("RightArmRed").gameObject.SetActive (false);
			} 
			else if (playerStatScript.rightWeaponSystem_Health <= 15) {
				RightArmHealth.Find ("RightArmGreen").gameObject.SetActive (false);
				RightArmHealth.Find ("RightArmOrange").gameObject.SetActive (true);
			}
			else if (playerStatScript.rightWeaponSystem_Health <= 10) {
				RightArmHealth.Find ("RightArmOrange").gameObject.SetActive (false);
				RightArmHealth.Find ("RightArmRed").gameObject.SetActive (true);
			}
			//Left Leg
			if (playerStatScript.leftLeg_Health <= 0) {
				LeftLegHealth.Find ("LeftLegRed").gameObject.SetActive (false);
			} 
			else if (playerStatScript.leftLeg_Health <= 20) {
				LeftLegHealth.Find ("RightArmGreen").gameObject.SetActive (false);
				LeftLegHealth.Find ("LeftLegOrange").gameObject.SetActive (true);
			}
			else if (playerStatScript.leftLeg_Health <= 10) {
				LeftLegHealth.Find ("LeftLegOrange").gameObject.SetActive (false);
				LeftLegHealth.Find ("LeftLegRed").gameObject.SetActive (true);
			}
			//Right Leg
			if (playerStatScript.rightTorso_Health <= 0) {
				RightLegHealth.Find ("RightLegRed").gameObject.SetActive (false);
			} 
			else if (playerStatScript.rightTorso_Health <= 20) {
				RightLegHealth.Find ("RightLegGreen").gameObject.SetActive (false);
				RightLegHealth.Find ("RightLegOrange").gameObject.SetActive (true);
			}
			else if (playerStatScript.rightTorso_Health <= 10) {
				RightLegHealth.Find ("RightLegOrange").gameObject.SetActive (false);
				RightLegHealth.Find ("RightLegRed").gameObject.SetActive (true);
			}
		}
	}
}

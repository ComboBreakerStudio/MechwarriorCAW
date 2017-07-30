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

	public GameObject TorsoHealth;
	public GameObject BackTorsoHealth;
	public GameObject LeftTorsoHealth;
	public GameObject LeftArmHealth;
	public GameObject LeftLegHealth;
	public GameObject RightTorsoHealth;
	public GameObject RightArmHealth;
	public GameObject RightLegHealth;

	public List<GameObject> TorsoUI;
	public List<GameObject> BackTorsoUI;
	public List<GameObject> LeftTorsoUI;
	public List<GameObject> LeftArmUI;
	public List<GameObject> LeftLegUI;
	public List<GameObject> RightTorsoUI;
	public List<GameObject> RightArmUI;
	public List<GameObject> RightLegUI;

	public PlayerStats playerStatScript;

	//ShakeScreen
	//public bool canShakeScreen;
	//public float shakeScreenTime;
	//public float shakePower;

	void Start () {
		//canShakeScreen = true;
		PauseMenu.IsOn = false;
		isLocalPlayer = true;

		//Get Object
		foreach (RectTransform img in TorsoHealth.transform)
			TorsoUI.Add (img.gameObject);

		foreach (RectTransform img in BackTorsoHealth.transform)
			BackTorsoUI.Add (img.gameObject);

		foreach (RectTransform img in LeftTorsoHealth.transform)
			LeftTorsoUI.Add (img.gameObject);

		foreach (RectTransform img in LeftArmHealth.transform)
			LeftArmUI.Add (img.gameObject);

		foreach (RectTransform img in LeftLegHealth.transform)
			LeftLegUI.Add (img.gameObject);

		foreach (RectTransform img in RightTorsoHealth.transform)
			RightTorsoUI.Add (img.gameObject);

		foreach (RectTransform img in RightArmHealth.transform)
			RightArmUI.Add (img.gameObject);

		foreach (RectTransform img in RightLegHealth.transform)
			RightLegUI.Add (img.gameObject);

		//Disable Object
		for (int i = 0; i < BackTorsoUI.Count; i++)
			BackTorsoUI [i].SetActive (false);

		for (int i = 0; i < TorsoUI.Count; i++)
			TorsoUI [i].SetActive (false);

		for (int i = 0; i < LeftTorsoUI.Count; i++)
			LeftTorsoUI [i].SetActive (false);

		for (int i = 0; i < LeftArmUI.Count; i++)
			LeftArmUI [i].SetActive (false);

		for (int i = 0; i < LeftLegUI.Count; i++)
			LeftLegUI [i].SetActive (false);

		for (int i = 0; i < RightTorsoUI.Count; i++)
			RightTorsoUI [i].SetActive (false);

		for (int i = 0; i < RightArmUI.Count; i++)
			RightArmUI [i].SetActive (false);

		for (int i = 0; i < RightLegUI.Count; i++)
			RightLegUI [i].SetActive (false);

		BackTorsoUI [2].SetActive (true);
		TorsoUI [2].SetActive (true);
		LeftTorsoUI [2].SetActive (true);
		LeftArmUI [2].SetActive (true);
		LeftLegUI [2].SetActive (true);
		RightTorsoUI [2].SetActive (true);
		RightArmUI [2].SetActive (true);
		RightLegUI [2].SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			TogglePauseMenu ();
		}
		heatBar.fillAmount = playerStatScript.leftWeaponSystemStats.currentHeat/100;
		healthDisplay ();
	}

	private void TogglePauseMenu(){
		Debug.Log ("Pause");
		pauseMenu.SetActive (!pauseMenu.activeSelf);
		PauseMenu.IsOn = pauseMenu.activeSelf;
	}

	public void StartStuffs(){
		playerStatScript = GameManager.GM.localPlayerStatsScript;
	}

	/*public void shakeScreen(){
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
	}*/



	void healthDisplay()
	{
		if(playerStatScript != null){
			//Front Torso
			if (playerStatScript.frontTorso_Health <= 0) {
				TorsoUI [0].SetActive (false);
			} else if (playerStatScript.frontTorso_Health <= 35) {
				TorsoUI [2].SetActive (false);
				TorsoUI [1].SetActive (true);
			} else if (playerStatScript.frontTorso_Health <= 20) {
				TorsoUI [1].SetActive (false);
				TorsoUI [0].SetActive (true);
			} else if (playerStatScript.frontTorso_Health > 35) {
				TorsoUI [0].SetActive (false);
				TorsoUI [1].SetActive (false);
				TorsoUI [2].SetActive (true);
			}
			//Back Torso
			if (playerStatScript.backTorso_Health <= 0) {
				BackTorsoUI [0].SetActive (false);
			} 
			else if(playerStatScript.backTorso_Health <= 15) {
				BackTorsoUI [2].SetActive (false);
				BackTorsoUI [1].SetActive (true);
			}
			else if(playerStatScript.backTorso_Health <= 10) {
				BackTorsoUI [1].SetActive (false);
				BackTorsoUI [0].SetActive (true);
			} else if (playerStatScript.backTorso_Health > 15) {
				BackTorsoUI [0].SetActive (false);
				BackTorsoUI [1].SetActive (false);
				BackTorsoUI [2].SetActive (true);
			}
			//left torso
			if (playerStatScript.leftTorso_Health <= 0) {
				LeftTorsoUI [0].SetActive (false);
			} 
			else if (playerStatScript.leftTorso_Health <= 30) {
				LeftTorsoUI [2].SetActive (false);
				LeftTorsoUI [1].SetActive (true);
			}
			else if (playerStatScript.leftTorso_Health <= 15) {
				LeftTorsoUI [1].SetActive (false);
				LeftTorsoUI [0].SetActive (true);
			} else if (playerStatScript.leftTorso_Health > 30) {
				LeftTorsoUI [0].SetActive (false);
				LeftTorsoUI [1].SetActive (false);
				LeftTorsoUI [2].SetActive (true);
			}
			//Right Torso
			if (playerStatScript.rightTorso_Health <= 0) {
				RightTorsoUI [0].SetActive (false);
			} 
			else if (playerStatScript.rightTorso_Health <= 30) {
				RightTorsoUI [2].SetActive (false);
				RightTorsoUI [1].SetActive (true);
			}
			else if (playerStatScript.rightTorso_Health <= 15) {
				RightTorsoUI [1].SetActive (false);
				RightTorsoUI [0].SetActive (true);
			} else if (playerStatScript.rightTorso_Health > 30) {
				RightTorsoUI [0].SetActive (false);
				RightTorsoUI [1].SetActive (false);
				RightTorsoUI [2].SetActive (true);
			}
			//Left Weapon
			if (playerStatScript.leftWeaponSystem_Health <= 0) {
				LeftArmUI [0].SetActive (false);
			} 
			else if (playerStatScript.leftWeaponSystem_Health <= 15) {
				LeftArmUI [2].SetActive (false);
				LeftArmUI [1].SetActive (true);
			}
			else if (playerStatScript.leftWeaponSystem_Health <= 10) {
				LeftArmUI [1].SetActive (false);
				LeftArmUI [0].SetActive (true);
			} else if (playerStatScript.leftWeaponSystem_Health > 15) {
				LeftArmUI [0].SetActive (false);
				LeftArmUI [1].SetActive (false);
				LeftArmUI [2].SetActive (true);
			}
			//Right Weapon
			if (playerStatScript.rightWeaponSystem_Health <= 0) {
				RightArmUI [0].SetActive (false);
			} 
			else if (playerStatScript.rightWeaponSystem_Health <= 15) {
				RightArmUI [2].SetActive (false);
				RightArmUI [1].SetActive (true);
			}
			else if (playerStatScript.rightWeaponSystem_Health <= 10) {
				RightArmUI [1].SetActive (false);
				RightArmUI [0].SetActive (true);
			} else if (playerStatScript.rightWeaponSystem_Health > 15) {
				RightArmUI [0].SetActive (false);
				RightArmUI [1].SetActive (false);
				RightArmUI [2].SetActive (true);
			}
			//Left Leg
			if (playerStatScript.leftLeg_Health <= 0) {
				LeftLegUI [0].SetActive (false);
			} 
			else if (playerStatScript.leftLeg_Health <= 20) {
				LeftLegUI [2].SetActive (false);
				LeftLegUI [1].SetActive (true);
			}
			else if (playerStatScript.leftLeg_Health <= 10) {
				LeftLegUI [1].SetActive (false);
				LeftLegUI [0].SetActive (true);
			} else if (playerStatScript.leftLeg_Health > 20) {
				LeftLegUI [0].SetActive (false);
				LeftLegUI [1].SetActive (false);
				LeftLegUI [2].SetActive (true);
			}
			//Right Leg
			if (playerStatScript.rightLeg_Health <= 0) {
				RightLegUI [0].SetActive (false);
			} 
			else if (playerStatScript.rightLeg_Health <= 20) {
				RightLegUI [2].SetActive (false);
				RightLegUI [1].SetActive (true);
			}
			else if (playerStatScript.rightLeg_Health <= 10) {
				RightLegUI [1].SetActive (false);
				RightLegUI [0].SetActive (true);
			} else if (playerStatScript.rightLeg_Health > 20) {
				RightLegUI [0].SetActive (false);
				RightLegUI [1].SetActive (false);
				RightLegUI [2].SetActive (true);
			}
		}
	}

	//Untested code for feedback (UI shaking)
	public float shakeAmount = 30.0f;
	public float shakeTimer = 1.0f;

	public void UIShake()
	{
		if (shakeTimer > 0) {
			Vector2 shakePos = Random.insideUnitCircle * shakeAmount;
			transform.position = new Vector3 (transform.position.x + shakePos.x, transform.position.y + shakePos.y);
			shakeTimer -= Time.deltaTime;
		}
	}

	public void ShakeCamera(float shakePower, float shakeDuration)
	{
		shakeAmount = shakePower;
		shakeTimer = shakeDuration;
	}

	//Untested code for feedback (Hit feedback)
	public RectTransform hitMarker;
	public RectTransform hitFlash;
	bool confirmHit;
	bool beingHit;
	float hitduration = 0.1f;	
	//at void start both rectransform activestate is false
	public void ConfirmHitMarker()
	{
		if (confirmHit) {
			hitMarker.gameObject.SetActive (true);
			hitduration -= Time.deltaTime;
		} else if (beingHit){
			hitFlash.gameObject.SetActive (true);
			hitduration -= Time.deltaTime;
		}
	}
}

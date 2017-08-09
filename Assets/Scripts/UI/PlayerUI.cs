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

	public GameObject FrontTorsoHealth;
	public GameObject BackTorsoHealth;
	public GameObject FrontLeftTorsoHealth;
	public GameObject BackLeftTorsoHealth;
	public GameObject LeftArmHealth;
	public GameObject LeftLegHealth;
	public GameObject FrontRightTorsoHealth;
	public GameObject BackRightTorsoHealth;
	public GameObject RightArmHealth;
	public GameObject RightLegHealth;

	public List<GameObject> FrontTorsoUI;
	public List<GameObject> BackTorsoUI;
	public List<GameObject> FrontLeftTorsoUI;
	public List<GameObject> BackLeftTorsoUI;
	public List<GameObject> LeftArmUI;
	public List<GameObject> LeftLegUI;
	public List<GameObject> FrontRightTorsoUI;
	public List<GameObject> BackRightTorsoUI;
	public List<GameObject> RightArmUI;
	public List<GameObject> RightLegUI;

	public PlayerStats playerStatScript;

	//ShakeScreen
	public bool canShakeScreen;
	public float shakeScreenTime;
	public float shakePower;

	void Start () {
		//canShakeScreen = true;
		PauseMenu.IsOn = false;
		isLocalPlayer = true;

		//Get Object
		foreach (RectTransform img in FrontTorsoHealth.transform)
			FrontTorsoUI.Add (img.gameObject);

		foreach (RectTransform img in BackTorsoHealth.transform)
			BackTorsoUI.Add (img.gameObject);

		foreach (RectTransform img in FrontLeftTorsoHealth.transform)
			FrontLeftTorsoUI.Add (img.gameObject);

		foreach (RectTransform img in BackLeftTorsoHealth.transform)
			BackLeftTorsoUI.Add (img.gameObject);

		foreach (RectTransform img in LeftArmHealth.transform)
			LeftArmUI.Add (img.gameObject);

		foreach (RectTransform img in LeftLegHealth.transform)
			LeftLegUI.Add (img.gameObject);

		foreach (RectTransform img in FrontRightTorsoHealth.transform)
			FrontRightTorsoUI.Add (img.gameObject);

		foreach (RectTransform img in BackRightTorsoHealth.transform)
			BackRightTorsoUI.Add (img.gameObject);

		foreach (RectTransform img in RightArmHealth.transform)
			RightArmUI.Add (img.gameObject);

		foreach (RectTransform img in RightLegHealth.transform)
			RightLegUI.Add (img.gameObject);

		//Disable Object
		for (int i = 0; i < BackTorsoUI.Count; i++)
			BackTorsoUI [i].SetActive (false);

		for (int i = 0; i < FrontTorsoUI.Count; i++)
			FrontTorsoUI [i].SetActive (false);

		for (int i = 0; i < FrontLeftTorsoUI.Count; i++)
			FrontLeftTorsoUI [i].SetActive (false);

		for (int i = 0; i < BackLeftTorsoUI.Count; i++)
			BackLeftTorsoUI [i].SetActive (false);

		for (int i = 0; i < LeftArmUI.Count; i++)
			LeftArmUI [i].SetActive (false);

		for (int i = 0; i < LeftLegUI.Count; i++)
			LeftLegUI [i].SetActive (false);

		for (int i = 0; i < FrontRightTorsoUI.Count; i++)
			FrontRightTorsoUI [i].SetActive (false);

		for (int i = 0; i < BackRightTorsoUI.Count; i++)
			BackRightTorsoUI [i].SetActive (false);

		for (int i = 0; i < RightArmUI.Count; i++)
			RightArmUI [i].SetActive (false);

		for (int i = 0; i < RightLegUI.Count; i++)
			RightLegUI [i].SetActive (false);

		FrontTorsoUI [4].SetActive (true);
		BackTorsoUI [4].SetActive (true);
		FrontLeftTorsoUI [4].SetActive (true);
		BackLeftTorsoUI [4].SetActive (true);
		LeftArmUI [4].SetActive (true);
		LeftLegUI [4].SetActive (true);
		FrontRightTorsoUI [4].SetActive (true);
		BackRightTorsoUI [4].SetActive (true);
		RightArmUI [4].SetActive (true);
		RightLegUI [4].SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			TogglePauseMenu ();
		}

		heatBar.fillAmount = (playerStatScript.leftWeaponSystemStats.currentHeat + playerStatScript.rightWeaponSystemStats.currentHeat)/100;

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
			if (playerStatScript.frontTorso_Health == 70) {
				//resets all
				FrontTorsoUI [0].SetActive (false);
				FrontTorsoUI [1].SetActive (false);
				FrontTorsoUI [2].SetActive (false);
				FrontTorsoUI [3].SetActive (false);
				FrontTorsoUI [4].SetActive (true);
			} else if (playerStatScript.frontTorso_Health <= 53) {
				FrontTorsoUI [4].SetActive (false);
				FrontTorsoUI [3].SetActive (true);
			} else if (playerStatScript.frontTorso_Health <= 35) {
				FrontTorsoUI [3].SetActive (false);
				FrontTorsoUI [2].SetActive (true);
			} else if (playerStatScript.frontTorso_Health <= 18) {
				FrontTorsoUI [2].SetActive (false);
				FrontTorsoUI [1].SetActive (true);
			} else if (playerStatScript.frontTorso_Health <= 0) {
				FrontTorsoUI [1].SetActive (false);
				FrontTorsoUI [0].SetActive (true);
			}
			//Back Torso
			if (playerStatScript.backTorso_Health == 20) {
				//resets all
				BackTorsoUI [0].SetActive (false);
				BackTorsoUI [1].SetActive (false);
				BackTorsoUI [2].SetActive (false);
				BackTorsoUI [3].SetActive (false);
				BackTorsoUI [4].SetActive (true);
			} else if(playerStatScript.backTorso_Health <= 15) {
				BackTorsoUI [4].SetActive (false);
				BackTorsoUI [3].SetActive (true);
			} else if(playerStatScript.backTorso_Health <= 10) {
				BackTorsoUI [3].SetActive (false);
				BackTorsoUI [2].SetActive (true);
			} else if (playerStatScript.backTorso_Health <= 5) {
				BackTorsoUI [2].SetActive (false);
				BackTorsoUI [1].SetActive (true);
			} else if (playerStatScript.backTorso_Health <= 0) {
				BackTorsoUI [1].SetActive (false);
				BackTorsoUI [0].SetActive (true);
			}
			//front/back left torso
			if (playerStatScript.leftTorso_Health == 45) {
				//resets all
				FrontLeftTorsoUI [0].SetActive (false);
				FrontLeftTorsoUI [1].SetActive (false);
				FrontLeftTorsoUI [2].SetActive (false);
				FrontLeftTorsoUI [3].SetActive (false);
				FrontLeftTorsoUI [4].SetActive (true);
				BackLeftTorsoUI [0].SetActive (false);
				BackLeftTorsoUI [1].SetActive (false);
				BackLeftTorsoUI [2].SetActive (false);
				BackLeftTorsoUI [3].SetActive (false);
				BackLeftTorsoUI [4].SetActive (true);
			} else if (playerStatScript.leftTorso_Health <= 34) {
				FrontLeftTorsoUI [4].SetActive (false);
				FrontLeftTorsoUI [3].SetActive (true);
				BackLeftTorsoUI [4].SetActive (false);
				BackLeftTorsoUI [3].SetActive (true);
			} else if (playerStatScript.leftTorso_Health <= 23) {
				FrontLeftTorsoUI [3].SetActive (false);
				FrontLeftTorsoUI [2].SetActive (true);
				BackLeftTorsoUI [3].SetActive (false);
				BackLeftTorsoUI [2].SetActive (true);
			} else if (playerStatScript.leftTorso_Health > 11) {
				FrontLeftTorsoUI [2].SetActive (false);
				FrontLeftTorsoUI [1].SetActive (true);
				BackLeftTorsoUI [2].SetActive (false);
				BackLeftTorsoUI [1].SetActive (true);
			} else if (playerStatScript.leftTorso_Health <= 0) {
				FrontLeftTorsoUI [1].SetActive (false);
				FrontLeftTorsoUI [0].SetActive (true);
				BackLeftTorsoUI [1].SetActive (false);
				BackLeftTorsoUI [0].SetActive (true);
			}
			//Front/Back Right Torso
			if (playerStatScript.rightTorso_Health == 45) {
				//resets all 
				FrontRightTorsoUI [0].SetActive (false);
				FrontRightTorsoUI [1].SetActive (false);
				FrontRightTorsoUI [2].SetActive (false);
				FrontRightTorsoUI [3].SetActive (false);
				FrontRightTorsoUI [4].SetActive (true);
				BackRightTorsoUI [0].SetActive (false);
				BackRightTorsoUI [1].SetActive (false);
				BackRightTorsoUI [2].SetActive (false);
				BackRightTorsoUI [3].SetActive (false);
				BackRightTorsoUI [4].SetActive (true);
			} else if (playerStatScript.rightTorso_Health <= 34) {
				FrontRightTorsoUI [4].SetActive (false);
				FrontRightTorsoUI [3].SetActive (true);
				BackRightTorsoUI [4].SetActive (false);
				BackRightTorsoUI [3].SetActive (true);
			} else if (playerStatScript.rightTorso_Health <= 23) {
				FrontRightTorsoUI [3].SetActive (false);
				FrontRightTorsoUI [2].SetActive (true);
				BackRightTorsoUI [3].SetActive (false);
				BackRightTorsoUI [2].SetActive (true);
			} else if (playerStatScript.rightTorso_Health <= 11) {
				FrontRightTorsoUI [2].SetActive (false);
				FrontRightTorsoUI [1].SetActive (true);
				BackRightTorsoUI [2].SetActive (false);
				BackRightTorsoUI [1].SetActive (true);
			} else if (playerStatScript.rightTorso_Health <= 0) {
				FrontRightTorsoUI [1].SetActive (false);
				FrontRightTorsoUI [0].SetActive (true);
				BackRightTorsoUI [1].SetActive (false);
				BackRightTorsoUI [0].SetActive (true);
			}
			//Left Arm
			if (playerStatScript.leftWeaponSystem_Health  == 25) {
				LeftArmUI [0].SetActive (false);
				LeftArmUI [1].SetActive (false);
				LeftArmUI [2].SetActive (false);
				LeftArmUI [3].SetActive (false);
				LeftArmUI [4].SetActive (true);
			} else if (playerStatScript.leftWeaponSystem_Health <= 15) {
				LeftArmUI [4].SetActive (false);
				LeftArmUI [3].SetActive (true);
			} else if (playerStatScript.leftWeaponSystem_Health <= 10) {
				LeftArmUI [3].SetActive (false);
				LeftArmUI [2].SetActive (true);
			} else if (playerStatScript.leftWeaponSystem_Health <= 15) {
				LeftArmUI [2].SetActive (false);
				LeftArmUI [1].SetActive (true);
			} else if (playerStatScript.leftWeaponSystem_Health <= 0) {
				LeftArmUI [1].SetActive (false);
				LeftArmUI [0].SetActive (true);
			}
			//Right Arm
			if (playerStatScript.rightWeaponSystem_Health  == 25) {
				RightArmUI [0].SetActive (false);
				RightArmUI [1].SetActive (false);
				RightArmUI [2].SetActive (false);
				RightArmUI [3].SetActive (false);
				RightArmUI [4].SetActive (true);
			} else if (playerStatScript.rightWeaponSystem_Health <= 15) {
				RightArmUI [4].SetActive (false);
				RightArmUI [3].SetActive (true);
			} else if (playerStatScript.rightWeaponSystem_Health <= 10) {
				RightArmUI [3].SetActive (false);
				RightArmUI [2].SetActive (true);
			} else if (playerStatScript.rightWeaponSystem_Health <= 15) {
				RightArmUI [2].SetActive (false);
				RightArmUI [1].SetActive (true);
			} else if (playerStatScript.rightWeaponSystem_Health <= 0) {
				RightArmUI [1].SetActive (false);
				RightArmUI [0].SetActive (true);
			}
			//Left Leg
			if (playerStatScript.leftLeg_Health == 40) {
				LeftLegUI [0].SetActive (false);
				LeftLegUI [1].SetActive (false);
				LeftLegUI [2].SetActive (false);
				LeftLegUI [3].SetActive (false);
				LeftLegUI [4].SetActive (false);
			} else if (playerStatScript.leftLeg_Health <= 30) {
				LeftLegUI [4].SetActive (false);
				LeftLegUI [3].SetActive (true);
			} else if (playerStatScript.leftLeg_Health <= 20) {
				LeftLegUI [3].SetActive (false);
				LeftLegUI [2].SetActive (true);
			} else if (playerStatScript.leftLeg_Health <= 10) {
				LeftLegUI [2].SetActive (false);
				LeftLegUI [1].SetActive (true);
			} else if (playerStatScript.leftLeg_Health <= 0) {
				LeftLegUI [1].SetActive (false);
				LeftLegUI [0].SetActive (true);
			}
			//Right Leg
			if (playerStatScript.rightLeg_Health == 40) {
				RightLegUI [0].SetActive (false);
				RightLegUI [1].SetActive (false);
				RightLegUI [2].SetActive (false);
				RightLegUI [3].SetActive (false);
				RightLegUI [4].SetActive (false);
			} else if (playerStatScript.rightLeg_Health <= 30) {
				RightLegUI [4].SetActive (false);
				RightLegUI [3].SetActive (true);
			} else if (playerStatScript.rightLeg_Health <= 20) {
				RightLegUI [3].SetActive (false);
				RightLegUI [2].SetActive (true);
			} else if (playerStatScript.rightLeg_Health <= 10) {
				RightLegUI [2].SetActive (false);
				RightLegUI [1].SetActive (true);
			} else if (playerStatScript.rightLeg_Health <= 0) {
				RightLegUI [1].SetActive (false);
				RightLegUI [0].SetActive (true);
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

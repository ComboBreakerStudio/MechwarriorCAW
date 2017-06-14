using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

	[SerializeField]
	private GameObject pauseMenu;
	private GameObject minimap;
	private Image heatBar;
	private Text leftAmmo;
	private Text rightAmmo;

	bool isLocalPlayer;

	// Use this for initialization
	void Start () {
		PauseMenu.IsOn = false;
		isLocalPlayer = true;
		leftAmmo = GetComponent<Text> ();
		Debug.Log ("Found Left Ammo?");
		//rightAmmo = GameObject.Find ("RightAmmo").GetComponentInChildren<Text> ();
		leftAmmo.text = "8";
		heatBar.fillAmount = GetComponent<WeaponSystemStats> ().currentHeat;
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

	/*[Client]
	void mapDisplay ()
	{
		if(!isLocalPlayer)
		{
			//set minimap something idk
		}
	}*/
}

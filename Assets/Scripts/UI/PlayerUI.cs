using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

	[SerializeField]
	private GameObject pauseMenu;

	[Client]
	GameObject minimap;

	// Use this for initialization
	void Start () {
		PauseMenu.IsOn = false;
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
}

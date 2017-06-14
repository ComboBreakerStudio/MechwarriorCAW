using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour {

	[SerializeField]
	private GameObject pauseMenu;

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

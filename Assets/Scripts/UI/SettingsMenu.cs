using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour {

	Transform menuPanel;
	Transform promptPanel;
	Event keyChangeEvent;
	Text buttonText;
	KeyCode newKeybind;

	bool waitingForKeyChange;	

	// Use this for initialization
	void Start () {
		menuPanel = transform.Find ("SMenu");
		promptPanel = transform.Find ("ChangePrompt");
		promptPanel.gameObject.SetActive (false);
		Debug.Log ("confirmed false");
		//menuPanel.gameObject.SetActive (false);
		//Debug.Log ("confirmed false");
		waitingForKeyChange = false;

		for (int i = 0; i < menuPanel.childCount; i++) {
			if (menuPanel.GetChild (i).name == "ForwardKey") {
				menuPanel.GetChild (i).GetComponentInChildren<Text> ().text = GameManager.GM.forward.ToString ();
			}
			else if (menuPanel.GetChild (i).name == "BackwardKey") {
				menuPanel.GetChild (i).GetComponentInChildren<Text> ().text = GameManager.GM.backward.ToString ();
			}
			else if (menuPanel.GetChild (i).name == "LeftKey") {
				menuPanel.GetChild (i).GetComponentInChildren<Text> ().text = GameManager.GM.left.ToString ();
			}
			else if (menuPanel.GetChild (i).name == "RightKey") {
				menuPanel.GetChild (i).GetComponentInChildren<Text> ().text = GameManager.GM.right.ToString ();
			}
			/*else if (menuPanel.GetChild (i).name == "RangeAttackKey") {
				menuPanel.GetChild (i).GetComponentInChildren<Text> ().text = GameManager.GM.rangeAttack.ToString ();
			}
			else if (menuPanel.GetChild (i).name == "MeleeAttackKey") {
				menuPanel.GetChild (i).GetComponentInChildren<Text> ().text = GameManager.GM.meleeAttack.ToString ();
			}*/
		}
		Debug.Log ("keybind load complete");
	}

	public void menu()
	{
		SceneManager.LoadScene ("MainMenu");
	}
	
	// Update is called once per frame
	void OnGUI () {
		keyChangeEvent = Event.current;

		if (keyChangeEvent.isKey && waitingForKeyChange) {
			newKeybind = keyChangeEvent.keyCode;
			waitingForKeyChange = false;
			promptPanel.gameObject.SetActive (false);
		}
	}

	public void StartAssignment(string keyName)
	{
		if(!waitingForKeyChange)
			StartCoroutine(AssignKey(keyName));
	}

	public void SendText(Text text)
	{
		buttonText = text;
	}

	IEnumerator WaitForKey()
	{
		while(!keyChangeEvent.isKey)
			yield return null;
	}

	public IEnumerator AssignKey(string keyName)
	{
		waitingForKeyChange = true;
		promptPanel.gameObject.SetActive (true);

		yield return WaitForKey();

		switch(keyName)
		{
		case "forward":
			GameManager.GM.forward = newKeybind;
			buttonText.text = GameManager.GM.forward.ToString();
			PlayerPrefs.SetString("forwardKey", GameManager.GM.forward.ToString());
			break;
		case "backward":
			GameManager.GM.backward = newKeybind;
			buttonText.text = GameManager.GM.backward.ToString();
			PlayerPrefs.SetString("backwardKey", GameManager.GM.backward.ToString());
			break;
		case "left":
			GameManager.GM.left = newKeybind;
			buttonText.text = GameManager.GM.left.ToString();
			PlayerPrefs.SetString("leftKey", GameManager.GM.left.ToString());
			break;
		case "right":
			GameManager.GM.right = newKeybind;
			buttonText.text = GameManager.GM.right.ToString();
			PlayerPrefs.SetString("rightKey", GameManager.GM.right.ToString());
			break;
		/*case "rangeAttack":
			GameManager.GM.rangeAttack = newKeybind;
			buttonText.text = GameManager.GM.rangeAttack.ToString();
			PlayerPrefs.SetString("rangeAttackKey", GameManager.GM.rangeAttack.ToString());
			break;
		case "meleeAttack":
			GameManager.GM.meleeAttack = newKeybind;
			buttonText.text = GameManager.GM.meleeAttack.ToString();
			PlayerPrefs.SetString("meleeAttackKey", GameManager.GM.meleeAttack.ToString());
			break;*/
		}
		Debug.Log ("keychange done");
		yield return null;
	}
}
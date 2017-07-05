using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour {

	public Transform menuPanel;
	public Transform promptPanel;
	Event keyChangeEvent;
	Text buttonText;
	KeyCode newKeybind;

	bool waitingForKeyChange;	

	// Use this for initialization
	void Start () {
		//menuPanel = transform.Find ("SMenu");
		//promptPanel = transform.Find ("ChangePrompt");
		promptPanel.gameObject.SetActive (false);
		Debug.Log ("confirmed false");
		//menuPanel.gameObject.SetActive (false);
		//Debug.Log ("confirmed false");
		waitingForKeyChange = false;

		for (int i = 0; i < menuPanel.childCount; i++) {
			if (menuPanel.GetChild (i).name == "ForwardKey") {
				menuPanel.GetChild (i).GetComponentInChildren<Text> ().text = KeybindManager.KBM.Forward.ToString ();
			}
			else if (menuPanel.GetChild (i).name == "BackwardKey") {
				menuPanel.GetChild (i).GetComponentInChildren<Text> ().text = KeybindManager.KBM.Backward.ToString ();
			}
			else if (menuPanel.GetChild (i).name == "LeftKey") {
				menuPanel.GetChild (i).GetComponentInChildren<Text> ().text = KeybindManager.KBM.Left.ToString ();
			}
			else if (menuPanel.GetChild (i).name == "RightKey") {
				menuPanel.GetChild (i).GetComponentInChildren<Text> ().text = KeybindManager.KBM.Right.ToString ();
			}
			else if (menuPanel.GetChild (i).name == "RangeAttackKey") {
				menuPanel.GetChild (i).GetComponentInChildren<Text> ().text = KeybindManager.KBM.rangeAttack.ToString ();
			}
			else if (menuPanel.GetChild (i).name == "MeleeAttackKey") {
				menuPanel.GetChild (i).GetComponentInChildren<Text> ().text = KeybindManager.KBM.meleeAttack.ToString ();
			}
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
			KeybindManager.KBM.Forward = newKeybind;
			buttonText.text = KeybindManager.KBM.Forward.ToString();
			PlayerPrefs.SetString("forwardKey", KeybindManager.KBM.Forward.ToString());
			break;
		case "backward":
			KeybindManager.KBM.Backward = newKeybind;
			buttonText.text = KeybindManager.KBM.Backward.ToString();
			PlayerPrefs.SetString("backwardKey", KeybindManager.KBM.Backward.ToString());
			break;
		case "left":
			KeybindManager.KBM.Left = newKeybind;
			buttonText.text = KeybindManager.KBM.Left.ToString();
			PlayerPrefs.SetString("leftKey", KeybindManager.KBM.Left.ToString());
			break;
		case "right":
			KeybindManager.KBM.Right = newKeybind;
			buttonText.text = KeybindManager.KBM.Right.ToString();
			PlayerPrefs.SetString("rightKey", KeybindManager.KBM.Right.ToString());
			break;
		case "rangeAttack":
			KeybindManager.KBM.rangeAttack = newKeybind;
			buttonText.text = KeybindManager.KBM.rangeAttack.ToString();
			PlayerPrefs.SetString("rangeAttackKey", KeybindManager.KBM.rangeAttack.ToString());
			break;
		case "meleeAttack":
			KeybindManager.KBM.meleeAttack = newKeybind;
			buttonText.text = KeybindManager.KBM.meleeAttack.ToString();
			PlayerPrefs.SetString("meleeAttackKey", KeybindManager.KBM.meleeAttack.ToString());
			break;
		}
		Debug.Log ("keychange done");
		yield return null;
	}
}
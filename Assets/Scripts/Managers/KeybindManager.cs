using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybindManager : MonoBehaviour {

	public static KeybindManager KBM;

	public KeyCode Forward{ get; set;}
	public KeyCode Backward{ get; set;}
	public KeyCode Left{ get; set;}
	public KeyCode Right{ get; set;}
	public KeyCode rangeAttack{ get; set;}
	public KeyCode meleeAttack{ get; set;}
	public KeyCode menuButton;

	void Awake()
	{
		if (KBM == null) 
		{
			DontDestroyOnLoad (gameObject);
			KBM = this;
		} 

		else if (KBM != this)
		{
			Destroy (gameObject);
		}

		Forward = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardKey", "W"));
		Backward = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backwardKey", "S"));
		Left = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "A"));
		Right = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "D"));
		rangeAttack = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rangeAttackKey", "Mouse0"));
		meleeAttack = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("meleeAttackKey", "Mouse1"));
		menuButton = KeyCode.Escape;
	}

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this.gameObject);
		Forward = this.Forward;
		Backward = this.Backward;
		Left = this.Left;
		Right = this.Right;
		rangeAttack = this.rangeAttack;
		meleeAttack = this.meleeAttack;
	}
	
	public void keyChange()
	{
		
	}
}

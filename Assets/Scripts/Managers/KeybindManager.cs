using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybindManager : MonoBehaviour {

	public static KeybindManager KBM;

	public KeyCode Forward;
	public KeyCode Backward;
	public KeyCode Left;
	public KeyCode Right;
	public KeyCode rangeAttack;
	public KeyCode meleeAttack;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this.gameObject);
		Forward = GameManager.GM.forward;
		Backward = GameManager.GM.backward;
		Left = GameManager.GM.left;
		Right = GameManager.GM.right;
		//rangeAttack = GameManager.GM.rangeAttack;
		//meleeAttack = GameManager.GM.meleeAttack;
	}
	
	public void keyChange()
	{
		
	}
}

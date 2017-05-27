using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Transform playerTransform;
	public float playerMoveSpeed = 5.0f;

	// Use this for initialization
	void Start ()
	{
		
	}
	// Update is called once per frame
	void Update () 
	{

		if (Input.GetKey (GameManager.GM.forward))
			playerTransform.position += Vector3.forward * playerMoveSpeed * Time.deltaTime;
		else if(Input.GetKey(GameManager.GM.backward))
			playerTransform.position += Vector3.back * playerMoveSpeed * Time.deltaTime;
		else if(Input.GetKey(GameManager.GM.left))
			playerTransform.position += Vector3.left * playerMoveSpeed * Time.deltaTime;
		else if(Input.GetKey(GameManager.GM.right))
			playerTransform.position += Vector3.right * playerMoveSpeed * Time.deltaTime;

		//Left Weapon Attack
		if(Input.GetKeyDown(KeyCode.Mouse0)){
			
		}

		//Right Weapon Attack
		if(Input.GetKeyDown(KeyCode.Mouse1)){
			
		}
	}
}

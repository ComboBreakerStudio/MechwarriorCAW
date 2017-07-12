using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

	public GameObject leg, body, leftHand, rightHand;
	public Animator legAnim, bodyAnim, leftHandAnim, rightHandAnim;

	// Use this for initialization
	void Start () {
		legAnim = leg.GetComponent<Animator> ();
		bodyAnim = body.GetComponent<Animator> ();
		leftHandAnim = leftHand.GetComponent<Animator> ();
		rightHandAnim = rightHand.GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {

	}
}
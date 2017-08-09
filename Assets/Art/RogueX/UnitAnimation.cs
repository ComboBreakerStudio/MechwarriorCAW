using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimation : MonoBehaviour {

	public GameObject tank;
	public GameObject artillery;
	public GameObject sniper;
	public Animator tankAnim;
	public Animator artilleryAnim;
	public Animator sniperAnim;

	// Use this for initialization
	void Start () {
		tankAnim = tank.GetComponent<Animator> ();
		sniperAnim = sniper.GetComponent<Animator> ();
		artilleryAnim = artillery.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Unit Animations (put into ur behaviour script luqman)
	/*
	unitAnimation.SniperAnim.SetBool ("SniperMove", true);
	unitAnimation.SniperAnim.SetBool ("SniperMove", false);
	unitAnimation.SniperAnim.SetBool ("SniperAim", true);
	unitAnimation.SniperAnim.SetBool ("SniperAim", false);
	unitAnimation.SniperAnim.SetBool ("SniperShoot", true);
	unitAnimation.SniperAnim.SetBool ("SniperShoot", false);

	unitAnimation.SniperAnim.SetBool ("TankMove", true);
	unitAnimation.SniperAnim.SetBool ("TankMove", false);
	unitAnimation.SniperAnim.SetBool ("TankShoot", true);
	unitAnimation.SniperAnim.SetBool ("SniperShoot", false);
	 */
}

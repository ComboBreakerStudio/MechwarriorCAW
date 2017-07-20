using UnityEngine;
using System.Collections;

public class WeaponSystemStats : MonoBehaviour {
	


	//Heat
	[Header ( "Heat System")]
	public float currentHeat, maxHeat,
					heatRegenRate, heatDegenRate,
					coolingTimer;


	public bool canCool, isOverHeat, canShoot;

	//Shoot
	[Header ("Shooting Method")]
	public bool isRaycast;
	public GameObject projectile, raycastVFX, parentObject;
	public Transform gunEnd;

	public float fireRate,
	attackRange;

	public int currentAmmo, maxAmmo;
	public bool needAmmo, isShooting;

	//Stats
	[Header("Stats")]

	public int maxHealth;

	public int weight,
				damage,
				recoilRate,
				AreaOfEffect;

	public bool isMelee;

	public PlayerStats stats;
	public PlayerShoot shoots;
	public PlayerController controller;

	void Start(){
		ResetStats ();
		parentObject = this.gameObject.transform.parent.gameObject;
	}

	void Update(){
		HeatSystem ();
	}


	void HeatSystem (){

		//Overheat Check
		if(currentHeat >= maxHeat){
			isOverHeat = true;
			//disable mech movement
			stats.canMove = false;
			canShoot = false;
			shoots.enabled = false;
			controller.enabled  = false;
		}
		else if(isOverHeat == true && currentHeat <= 70.0f){
			isOverHeat = false;
			stats.canMove = true;
			canShoot = true;
			shoots.enabled = true;
			controller.enabled  = true;
		}
		// End of Overheat Check

		//Heat Cooling System
		if(canCool){
			if(currentHeat >= 0.1f){
				currentHeat -= heatDegenRate * Time.deltaTime;
			}
		}
		//End of Heat Cooling System
	}

	//Stat Reset
	public void ResetStats (){
//		canShoot = true;
		isOverHeat = false;
//		canCool = true;
		currentAmmo = maxAmmo;
	}

	//Attack
	public void Attack(){
		//RayCast
		//if hit object find Tag (Left Weapon / torso / right Weapon), then get respective component from PlayerStats, deal damage accordingly;
	}
}

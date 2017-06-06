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
	public GameObject projectile;

	public float fireRate,
	attackRange;

	public int currentAmmo, maxAmmo;
	public bool needAmmo;

	//Stats
	[Header("Stats")]

	public int maxHealth;

	public int weight,
				damage,
				recoilRate,
				AreaOfEffect;

	void Start(){
		ResetStats ();
	}

	void Update(){
	}

	void HeatSystem (){
		//Overheat Check
		if(currentHeat >= maxHeat){
			isOverHeat = true;
		}
		else if(currentHeat <= 0.0f){
			isOverHeat = false;
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
		canShoot = true;
		isOverHeat = false;
		canCool = true;
		currentAmmo = maxAmmo;
	}

	//Attack
	public void Attack(){
		//RayCast
		//if hit object find Tag (Left Weapon / torso / right Weapon), then get respective component from PlayerStats, deal damage accordingly;
	}
}

using UnityEngine;
using System.Collections;

public class WeaponSystemStats : MonoBehaviour {
	


	//Heat
	[Header ( "Heat System")]
	public float currentHeat, maxHeat,
					heatRegenRate, heatDegenRate,
					coolingTimer;

	public bool canCool, isOverHeat;

	//Shoot
	[Header ("Shooting Method")]
	public bool isRaycast;
	public GameObject projectile;

	public int currentAmmo, maxAmmo;
	public bool needAmmo;

	//Stats
	[Header("Stats")]

	public int maxHealth;

	public int weight,
				damage,
				fireRate,
				recoilRate,
				AreaOfEffect;

	//Attack
	public void Attack(){
		//RayCast
		//if hit object find Tag (Left Weapon / torso / right Weapon), then get respective component from PlayerStats, deal damage accordingly;
	}

	public void ResetStats(){
		
	}
}

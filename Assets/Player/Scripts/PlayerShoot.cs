using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerShoot : NetworkBehaviour {

	private const string PLAYER_TAG = "Player";

	public PlayerStats playerStatsScript;
	public WeaponSystemStats leftWeapon, rightWeapon;
	public Transform leftGunEnd, rightGunEnd;
	public Transform target;
//	public GameObject leftHand;
//	float currentPosition;
//	float recoilPosition;
	private WaitForSeconds shotDuration = new WaitForSeconds (.5f);

	public Transform camObject;

	public ThirdPersonCamera camScript;

	[SerializeField]
	public Camera cam;

	[SerializeField]
	private LayerMask mask;

	// Use this for initialization
	void Start () 
	{
		if (cam == null) {
			Debug.LogError ("PlayerShoot: No camera reference!");
			this.enabled = false;
		}
	}

	public void SetWeapon(){
		//Set weapon
		playerStatsScript = GetComponent<PlayerStats>();
		leftWeapon = playerStatsScript.leftWeaponSystemStats;
		rightWeapon = playerStatsScript.rightWeaponSystemStats;

		if(leftWeapon.gunEnd != null){
			leftGunEnd = leftWeapon.gunEnd;
		}
		if(rightWeapon.gunEnd != null){
			rightGunEnd = rightWeapon.gunEnd;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		//if it's not local player, dont do anything
		if(!isLocalPlayer){
			return;
		}
		//Local player controls below

		
		//Attack
		if (Input.GetButton ("Fire1")) {
			if(leftWeapon.canShoot && leftWeapon.gameObject.activeSelf){
				Debug.Log ("PressedLeft");
				WeaponAttack (leftWeapon, "ResetLeftWeaponAttack");
				//if it's melee
				if(leftWeapon.isMelee){
					camScript.isMelee = true;
				}
			}
		}
		if(Input.GetButton ("Fire2")){
			if(rightWeapon.canShoot && rightWeapon.gameObject.activeSelf){
				Debug.Log ("PressedRight");
				WeaponAttack (rightWeapon, "ResetRightWeaponAttack");
				//if it's melee
				if(rightWeapon.isMelee){
					camScript.isMelee = true;
					Debug.Log ("Melee");
				}
			}
		}
		//Key Up
		if(!Input.GetButton("Fire1") && !Input.GetButton("Fire2")){
			camScript.isMelee = false;
		}
	}

	void WeaponAttack (WeaponSystemStats weapon, string coroutineName){
		if(weapon.canShoot){
			if(weapon.isRaycast){
				RaycastShoot(weapon);
			}
			else if(!weapon.isRaycast){
				if(weapon.needAmmo){
					if(weapon.currentAmmo > 0){
						ProjectileShoot (weapon);
						weapon.currentAmmo -= 1;
					}
				}
			}
			weapon.canShoot = false;
			StartCoroutine(coroutineName,weapon.fireRate);
		}
	}

	IEnumerator ResetLeftWeaponAttack(float t){
		yield return new WaitForSeconds (t);
		leftWeapon.canShoot = true;
	}

	IEnumerator ResetRightWeaponAttack(float t){
		yield return new WaitForSeconds (t);
		rightWeapon.canShoot = true;
	}

//	[Command]
	public void RaycastShoot(WeaponSystemStats weapon){
		Debug.Log ("Shoot");
		RaycastHit _hit;
//		RpcShoot ();

		if (Physics.Raycast (weapon.gunEnd.transform.position, cam.transform.forward, out _hit, weapon.attackRange, mask)) {
			Debug.Log ("Tag " + _hit.collider.gameObject.tag);
//			if (_hit.collider.tag == PLAYER_TAG)
//			{
//				CmdPlayerShot (_hit.collider.name);aa
//			}
//			CmdOnHit(_hit.point, _hit.normal);

			if(_hit.collider.gameObject.tag == "Player"){
				Debug.Log ("MeleePlayer");
				DealDamage dm = _hit.collider.gameObject.GetComponent<DealDamage> ();

				if (dm != null) {
					//				dm.ApplyDamage ((int)weapon.damage);
					Debug.Log ("ApplyDamage");
					CmdPlayerShot (dm.playerStats.gameObject.name, dm.partsID, (int)weapon.damage);
					//				Debug.Log (_hit.collider.gameObject.name + " Dmg : "+ dm.partsID + (int)weapon.damage);
				}
			}
			else if(_hit.collider.CompareTag("AI")){
				playerStatsScript.CmdDamageAI (_hit.collider.name, (int)weapon.damage);
			}

			if (weapon == leftWeapon) {
				CmdOnHit (_hit.point, true);
			} else if (weapon == rightWeapon) {
				CmdOnHit (_hit.point, false);
			}
		}
		else {
			if (weapon == leftWeapon) {

				CmdOnHit (weapon.gunEnd.transform.position, true);
			} else if (weapon == rightWeapon) {
				CmdOnHit (weapon.gunEnd.transform.position, false);
			}
		}
	}

	public void ProjectileShoot(WeaponSystemStats weapon){
//		GameObject ga = Instantiate (weapon.projectile);

//		RaycastHit _hit;
//		if(Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weapon.attackRange, mask)){

			GameObject ga = weapon.projectile;
			ga.transform.position = weapon.gunEnd.transform.position;
//			ga.transform.rotation = camObject.rotation;

			if(weapon == leftWeapon){
				CmdProjectileShoot (ga.transform.position, target.transform.position, true, playerStatsScript.gameObject.name, weapon.damage);
			}
			else if(weapon == rightWeapon){
				CmdProjectileShoot (ga.transform.position, target.transform.position, false, playerStatsScript.gameObject.name, weapon.damage);
			}
//		}
	}

	[Command]
	public void CmdProjectileShoot (Vector3 shootPosition, Vector3 lookPosition, bool isLeft, string playerName, int dmg){
//		RpcProjectileShoot (shootPosition, shootRotation, isLeft);

		if(isLeft){
			GameObject ga = Instantiate (leftWeapon.projectile);
			ga.GetComponent<BulletStats> ().playerName = playerName;
			ga.GetComponent<BulletStats> ().damage = dmg;
			ga.GetComponent<BulletStats> ().canDamage = true;
			ga.transform.position = shootPosition;
//			ga.transform.rotation = shootRotation;
			ga.transform.LookAt(lookPosition);

			NetworkServer.Spawn (ga);
		}
		else if(!isLeft){
			GameObject ga = Instantiate (rightWeapon.projectile);
			ga.GetComponent<BulletStats> ().playerName = playerName;
			ga.GetComponent<BulletStats> ().damage = dmg;
			ga.GetComponent<BulletStats> ().canDamage = true;
			ga.transform.position = shootPosition;
//			ga.transform.rotation = shootRotation;
			ga.transform.LookAt(lookPosition);

			NetworkServer.Spawn (ga);
		}
	}

	[ClientRpc]
	public void RpcProjectileShoot(Vector3 shootPosition, Quaternion shootRotation, bool isLeft){
		if(isLeft){
			GameObject ga = Instantiate (leftWeapon.projectile);
			ga.transform.position = shootPosition;
			ga.transform.rotation = shootRotation;

//			NetworkServer.Spawn (ga);
		}
		else if(!isLeft){
			GameObject ga = Instantiate (rightWeapon.projectile);
			ga.transform.position = shootPosition;
			ga.transform.rotation = shootRotation;

//			NetworkServer.Spawn (ga);
		}
	}


	[Command]
	public void CmdPlayerShot ( string _ID, int partsID, int dmg)
	{
//		Debug.Log (_ID + " has been shot.");

		for(int i = 0; i < TeamManager.instance.players.Count; i++){
			if(TeamManager.instance.players[i].name == _ID){
				TeamManager.instance.players [i].GetComponent<PlayerStats> ().CmdApplyDamage (partsID, dmg);
				Debug.Log ("ID " + _ID + " parts: " + partsID + "Dmg " + dmg);
			}
		}
	}

	[Command]
	void CmdOnHit(Vector3 _pos, bool isLeftWeapon)
	{
		RpcDoHitEffect (_pos, isLeftWeapon);
	}

	[ClientRpc]
	public void RpcDoHitEffect(Vector3 hitPosition, bool isLeftWeapon)
	{
//		muzzleFlash.Play ();
		if (isLeftWeapon) {
			GameObject ga = Instantiate (leftWeapon.raycastVFX, hitPosition, Quaternion.identity);
			NetworkServer.Spawn (ga);
		} 
		else {
			GameObject ga = Instantiate (rightWeapon.raycastVFX, hitPosition, Quaternion.identity);
			NetworkServer.Spawn (ga);
		}
	}
}

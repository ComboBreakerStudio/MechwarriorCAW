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
	private Camera cam;

	[SerializeField]
	private LayerMask mask;

	// Use this for initialization
	void Start () 
	{
		if (cam == null) {
			Debug.LogError ("PlayerShoot: No camera reference!");
			this.enabled = false;
		}
//		currentPosition = leftHand.transform.localPosition.z;
//		recoilPosition = currentPosition - 0.25f;


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
			if(leftWeapon.canShoot){
				Debug.Log ("PressedLeft");
				WeaponAttack (leftWeapon, "ResetLeftWeaponAttack");
				//if it's melee
				if(leftWeapon.isMelee){
					camScript.isMelee = true;
				}
			}
		}
		if(Input.GetButton ("Fire2")){
			if(rightWeapon.canShoot){
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
//		if(leftHand.transform.localPosition.z != currentPosition)
//		{
//			leftHand.transform.localPosition = Vector3.Lerp(leftHand.transform.localPosition, new Vector3(leftHand.transform.localPosition.x, leftHand.transform.localPosition.y, currentPosition), 0.1f);
//		}
	}

	void WeaponAttack (WeaponSystemStats weapon, string coroutineName){
		if(weapon.canShoot){
			if(weapon.isRaycast){
				RaycastShoot(weapon);
			}
			else if(!weapon.isRaycast){
				ProjectileShoot (weapon);
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

//	void Recoil()
//	{
//		leftHand.transform.localPosition = Vector3.Lerp(leftHand.transform.localPosition, new Vector3(leftHand.transform.localPosition.x, leftHand.transform.localPosition.y, recoilPosition), 0.5f);
//	}

//	[Command]
	public void RaycastShoot(WeaponSystemStats weapon){
		Debug.Log ("Shoot");
		RaycastHit _hit;
//		RpcShoot ();

		if (Physics.Raycast (weapon.gunEnd.transform.position, cam.transform.forward, out _hit, weapon.attackRange, mask)) {
			Debug.Log (_hit.collider.name);
//			if (_hit.collider.tag == PLAYER_TAG)
//			{
//				CmdPlayerShot (_hit.collider.name);
//			}
//			CmdOnHit(_hit.point, _hit.normal);

			DealDamage dm = _hit.collider.gameObject.GetComponent<DealDamage> ();

			if (dm != null) {
//				dm.ApplyDamage ((int)weapon.damage);
				Debug.Log ("ApplyDamage");
				CmdPlayerShot (dm.playerStats.gameObject.name, dm.partsID, (int)weapon.damage);
//				Debug.Log (_hit.collider.gameObject.name + " Dmg : "+ dm.partsID + (int)weapon.damage);
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
				CmdOnHit (weapon.gunEnd.transform.position, true);
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
			ga.transform.position = shootPosition;
//			ga.transform.rotation = shootRotation;
			ga.transform.LookAt(lookPosition);

			NetworkServer.Spawn (ga);
		}
		else if(!isLeft){
			GameObject ga = Instantiate (rightWeapon.projectile);
			ga.GetComponent<BulletStats> ().playerName = playerName;
			ga.GetComponent<BulletStats> ().damage = dmg;
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

		Debug.Log ("Spawn FX");
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

	[Command]
	public void CmdDebug(){
		Debug.Log("Hii " + this.gameObject.name);
	}
}

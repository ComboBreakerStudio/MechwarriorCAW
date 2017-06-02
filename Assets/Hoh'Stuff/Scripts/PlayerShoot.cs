using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerShoot : NetworkBehaviour {

	private const string PLAYER_TAG = "Player";

	public WeaponSystemStats leftWeapon, rightWeapon;

	[SerializeField]
	private GameObject cam;

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
			Debug.Log ("Pressed");
			WeaponAttack (leftWeapon, "ResetLeftWeaponAttack");
		}
		if(Input.GetButton ("Fire2")){
			Debug.Log ("Pressed");
			WeaponAttack (rightWeapon, "ResetRightWeaponAttack");
		}
	}

	void WeaponAttack (WeaponSystemStats weapon, string coroutineName){
		if(weapon.canShoot){
			if(leftWeapon.isRaycast){
				RaycastShoot(weapon);
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
		if(Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weapon.attackRange, mask))
		{
			Debug.Log (_hit.collider.name);
//			if (_hit.collider.tag == PLAYER_TAG)
//			{
//				CmdPlayerShot (_hit.collider.name);
//			}

			DealDamage dm = _hit.collider.gameObject.GetComponent<DealDamage>();

			if (dm != null) {
//				dm.ApplyDamage ((int)weapon.damage);
				Debug.Log("ApplyDamage");
				CmdPlayerShot (dm.playerStats.gameObject.name, dm.partsID, (int)weapon.damage);
//				Debug.Log (_hit.collider.gameObject.name + " Dmg : "+ dm.partsID + (int)weapon.damage);
			} 


		}

//		RpcShoot ();
	}

	[Client]
	public void RpcShoot()
	{
		Debug.Log ("Spawn FX");
	}

	[Command]
	void CmdPlayerShot ( string _ID, int partsID, int dmg)
	{
//		Debug.Log (_ID + " has been shot.");

		for(int i = 0; i < TeamManager.instance.players.Count; i++){
			if(TeamManager.instance.players[i].name == _ID){
				TeamManager.instance.players [i].GetComponent<PlayerStats> ().CmdApplyDamage (partsID, dmg);
				Debug.Log ("ID " + _ID + " parts: " + partsID + "Dmg " + dmg);
			}
		}
	}
}

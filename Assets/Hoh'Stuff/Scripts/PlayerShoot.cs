using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

	private const string PLAYER_TAG = "Player";

	public PlayerWeapon weapon;

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

		if (Input.GetButtonDown ("Fire1")) {
			Shoot();
		}
	}

//	[Command]
	public void Shoot(){
//		Debug.Log ("Shoot");
		RaycastHit _hit;
		if(Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weapon.range, mask))
		{
//			Debug.Log (_hit.collider.name);
//			if (_hit.collider.tag == PLAYER_TAG)
//			{
//				CmdPlayerShot (_hit.collider.name);
//			}

			DealDamage dm = _hit.collider.gameObject.GetComponent<DealDamage>();

			if (dm != null) {
//				dm.ApplyDamage ((int)weapon.damage);
				CmdPlayerShot (dm.playerStats.gameObject.name, dm.partsID, (int)weapon.damage);
//				Debug.Log (_hit.collider.gameObject.name + " Dmg : "+ dm.partsID + (int)weapon.damage);
			} 


		}

		RpcShoot ();
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
			}
		}
	}
}

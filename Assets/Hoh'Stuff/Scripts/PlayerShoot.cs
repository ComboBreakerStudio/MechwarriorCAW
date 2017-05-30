using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

	private const string PLAYER_TAG = "Player";

	public PlayerWeapon weapon;

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

//	[Client]
	void Shoot()
	{
		Debug.Log ("Shoot");
		RaycastHit _hit;
		if(Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weapon.range, mask))
		{
			Debug.Log (_hit.collider.name);
			if (_hit.collider.tag == PLAYER_TAG)
			{
				CmdPlayerShot (_hit.collider.name);
			}
		}
	}

	[Command]
	void CmdPlayerShot ( string _ID)
	{
		Debug.Log (_ID + " has been shot.");
	}
}

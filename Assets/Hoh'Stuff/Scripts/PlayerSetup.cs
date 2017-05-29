using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

	[SerializeField]
	Behaviour[] componentsToDisable;

	[SerializeField]
	string remoteLayerName = "RemotePlayer";

	Camera sceneCamera;

	void Start()
	{
		if (isServer) {
			TeamManager.instance.AddPlayerToList (this.gameObject);
		}
		
		// Disable components that should be active on the player that we control
		if (!isLocalPlayer) 
		{
			DisableComponents ();
			AssignRemoteLayer ();
		} 
		else 
		{
			// We are the local player: Disable the scene camera
			sceneCamera = Camera.main;
			if (sceneCamera != null)
			{
				sceneCamera.gameObject.SetActive (false);
			}
		}

		RegisterPlayer ();
	}

	void RegisterPlayer()
	{
		string _ID = "Player" + GetComponent<NetworkIdentity> ().netId;
		transform.name = _ID;
	}

	void AssignRemoteLayer()
	{
		gameObject.layer = LayerMask.NameToLayer (remoteLayerName);
	}

	void DisableComponents()
	{
		for (int i = 0; i < componentsToDisable.Length; i++)
		{
			componentsToDisable [i].enabled = false;
		}
	}

	// When we are destroyed
	void OnDisable()
	{
		// We enable the scene camera
		if (sceneCamera != null) 
		{
			sceneCamera.gameObject.SetActive (true);
		}
	}
		/*
	[SyncVar] // Only sync Float int string etc normal variable - not GameObject
	public int health;//Testing

	[SerializeField]
	private GameObject Bullet;

	// Use this for initialization
	void Start () 
	{
		if(!isLocalPlayer)
		{
			
			return;
		}
		//		playerTransform.parent = transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!isLocalPlayer){
			return;
		}
	}

	[Command]// Only works in Server
	void CmdDebugLog(string log){
		Debug.Log (log);
	}

	//Left Weapon
	void LeftWeapon_Attack(){ 
		//Do a local Raycast to a position

		RpcSpawnBullet (Bullet);
	}

	//Right Weapon
	void RightWeapon_Attack (){
		//Do a local Raycast to a position || enable a collider || do something || Rpc to spawn Bullet
	}

	[Client] // means Calling functions to other client
	void RpcSpawnBullet(GameObject _bulletObject){//Use Rpc Only to spawn Particles/bullets (not for collision detection)
		Debug.Log ("Shoot");
	
		//Below are references

		GameObject go = Instantiate (_bulletObject);
		go.transform.position = transform.position;
		go.transform.rotation = transform.rotation;

		NetworkServer.Spawn (go);//Only can spawn an instantiated game object //Spawn go to other client as a network object
	*/
}

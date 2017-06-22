using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	[SerializeField]
	private float playerMoveSpeed, playerCurrentSpeed, playerMaxSpeed, decelerationRate;
	[SerializeField]
	private float playerRotateSpeed;
	[SerializeField]
	private Rigidbody rb;

	private bool onMenu;

	public PlayerStats playerStatsScript;
	public PlayerUI playerUIScript;

	// Use this for initialization
	void Start () 
	{
		onMenu = false;
		rb = GetComponent<Rigidbody> ();

		playerUIScript = GameObject.Find ("PlayerUI_Canvas").GetComponent<PlayerUI>();

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	// Update is called once per frame
	void Update () 
	{
		if(playerStatsScript.canMove){
			if (Input.GetKey (KeybindManager.KBM.Forward)){
				playerCurrentSpeed += playerMoveSpeed * Time.deltaTime;
				if(playerCurrentSpeed >= playerMaxSpeed){
					playerCurrentSpeed = playerMaxSpeed;
				}
				ShakeScreen ();
			} 
			else if (Input.GetKey (KeybindManager.KBM.Backward)){
				playerCurrentSpeed -= playerMoveSpeed * Time.deltaTime;
				if(playerCurrentSpeed <= -playerMaxSpeed/2){
					playerCurrentSpeed = -playerMaxSpeed/2;
				}
				ShakeScreen ();
			}
			if (Input.GetKey (KeybindManager.KBM.Left)) {
				transform.Rotate (0,-playerRotateSpeed * Time.deltaTime,0);
				ShakeScreen ();
			}
			else if(Input.GetKey(KeybindManager.KBM.Right)){
				transform.Rotate (0,playerRotateSpeed * Time.deltaTime,0);
				ShakeScreen ();
			}
		}

		if(Input.GetKeyDown(KeybindManager.KBM.menuButton)){
			if (onMenu) {
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				onMenu = !onMenu;
			} 
			else {
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				onMenu = !onMenu;
			}
		}

		transform.Translate (Vector3.forward * playerCurrentSpeed * Time.deltaTime);
	}

	void ShakeScreen(){
		playerUIScript.shakeScreen ();
	}
}

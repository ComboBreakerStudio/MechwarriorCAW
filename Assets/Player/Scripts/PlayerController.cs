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
<<<<<<< HEAD
		if(playerStatsScript.canMove){
			//Forward and backward
			if (Input.GetKey (GameManager.GM.forward)) {
				playerCurrentSpeed += playerMoveSpeed * Time.deltaTime;
				if(playerCurrentSpeed >= playerMaxSpeed){
					playerCurrentSpeed = playerMaxSpeed;
				}
				ShakeScreen ();
			} 
			else if (Input.GetKey (GameManager.GM.backward)) {
				playerCurrentSpeed -= playerMoveSpeed * Time.deltaTime;
				if(playerCurrentSpeed <= -playerMaxSpeed/2){
					playerCurrentSpeed = -playerMaxSpeed/2;
				}
				ShakeScreen ();
			}
			else if(!Input.GetKey(GameManager.GM.forward) && !Input.GetKey(GameManager.GM.backward)){

				if(playerCurrentSpeed >= 0f){
					playerCurrentSpeed -= decelerationRate * Time.deltaTime;
					if(playerCurrentSpeed <= 0){
						playerCurrentSpeed = 0;
					}
				}
				else if(playerCurrentSpeed <= 0f){
					playerCurrentSpeed += decelerationRate * Time.deltaTime;
					if(playerCurrentSpeed >= 0){
						playerCurrentSpeed = 0;
					}
				}
			}
			//Rotate
			if (Input.GetKey (GameManager.GM.left)) {
				transform.Rotate (0,-playerRotateSpeed * Time.deltaTime,0);
				ShakeScreen ();
			}
			else if(Input.GetKey(GameManager.GM.right)){
				transform.Rotate (0,playerRotateSpeed * Time.deltaTime,0);
				ShakeScreen ();
			}
		}
=======
		if (Input.GetKey (KeybindManager.KBM.Forward))
			transform.Translate(Vector3.forward * playerMoveSpeed * Time.deltaTime);
		else if (Input.GetKey (KeybindManager.KBM.Backward))
			transform.Translate(Vector3.back * playerMoveSpeed * Time.deltaTime);
		if (Input.GetKey (KeybindManager.KBM.Left)) 
			transform.Rotate (0,-playerRotateSpeed * Time.deltaTime,0);
		else if(Input.GetKey(KeybindManager.KBM.Right))
			transform.Rotate (0,playerRotateSpeed * Time.deltaTime,0);
>>>>>>> DevRichard-UI

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

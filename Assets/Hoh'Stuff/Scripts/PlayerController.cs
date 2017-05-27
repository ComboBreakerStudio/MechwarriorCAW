using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

	[SerializeField]
	private float playerMoveSpeed = 5.0f;

	[SerializeField]
	private float lookSensitivity = 5.0f;

	private PlayerMotor motor;

	// Use this for initialization
	void Start ()
	{
		motor = GetComponent<PlayerMotor> (); 
	}
	// Update is called once per frame
	void Update () 
	{
		//Calculate movement velocity as a 3D vector
		float _xMov = Input.GetAxis ("Horizontal");
		float _zMov = Input.GetAxis ("Vertical");

		Vector3 _moveHorizontal = transform.right * -_xMov;
		Vector3 _moveVertical = transform.forward * -_zMov;

		// Final movement vector
		Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * playerMoveSpeed;

		// Apply movement
		motor.Move (_velocity);

		// Calculate rotation as a 3D vector (turning around)
		float _yRot = Input.GetAxis ("Mouse X");

		Vector3 _rotation = new Vector3 (0f, _yRot, 0f) * lookSensitivity;

		// Apply rotation
		motor.Rotate (_rotation);

		// Calculate camera rotation as a 3D vector (turning around)
		float _xrot = Input.GetAxis ("Mouse Y");

		Vector3 _cameraRotation = new Vector3 (_xrot, 0f, 0f) * lookSensitivity;

		// Apply camera rotation
		motor.RotateCamera (_cameraRotation);

		//Left Weapon Attack
		if(Input.GetKeyDown(KeyCode.Mouse0)){
			
		}

		//Right Weapon Attack
		if(Input.GetKeyDown(KeyCode.Mouse1)){
			
		}
	}
}

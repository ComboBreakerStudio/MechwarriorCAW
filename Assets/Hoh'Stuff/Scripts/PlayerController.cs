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
		float _rotationY = _yRot * lookSensitivity;

		// Apply rotation
		motor.Rotate (_rotationY);

		// Calculate camera rotation as a 3D vector (turning around)
		float _xRot = Input.GetAxis ("Mouse Y");
		float _cameraRotationX = _xRot * lookSensitivity;

		// Apply camera rotation
		motor.RotateCamera (_cameraRotationX);
	}
}

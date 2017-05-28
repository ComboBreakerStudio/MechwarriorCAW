using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

	[SerializeField]
	private Camera cam;

	[SerializeField]
	private Transform upper;

	private Vector3 velocity = Vector3.zero;
	private float cameraRotationX = 0f;
	private float currentCameraRotationX = 0f;
	private float rotationY = 0f;
	private float currentRotationY = 0f;
	private Vector2 pitchMinMax = new Vector2 (-30, 30);
	private Vector2 yawMinMax = new Vector2 (-45, 45);

	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();
	}

	// Gets a movement vector
	public void Move(Vector3 _velocity)
	{
		velocity = _velocity;
	}

	// Gets a rotational vector
	public void Rotate(float _rotationY)
	{
		rotationY = _rotationY;
	}

	// Gets a rotational vector for the camera
	public void RotateCamera(float _cameraRotationX)
	{
		cameraRotationX = _cameraRotationX;
	}

	// Run every physics iteration
	void FixedUpdate()
	{
		PerformMovement ();
		PerformRotation ();
	}

	// Perform movement based on velocity variable
	void PerformMovement()
	{
		if (velocity != Vector3.zero) 
		{
			rb.MovePosition (rb.position + velocity * Time.fixedDeltaTime);
		}
	}

	// Perform rotation
	void PerformRotation()
	{
		currentRotationY = rotationY;
		currentRotationY = Mathf.Clamp (currentRotationY, yawMinMax.x, yawMinMax.y);

		//rb.transform.eulerAngles = new Vector3 (0, currentRotationY, 0);
		rb.MoveRotation (rb.rotation * Quaternion.Euler (0, -currentRotationY, 0));
		if (cam != null) 
		{
			//New rotational calculation
			currentCameraRotationX -= cameraRotationX;
			currentCameraRotationX = Mathf.Clamp (currentCameraRotationX, pitchMinMax.x, pitchMinMax.y);

			cam.transform.eulerAngles = new Vector3 (currentCameraRotationX, 0, 0);
		}
	}
}
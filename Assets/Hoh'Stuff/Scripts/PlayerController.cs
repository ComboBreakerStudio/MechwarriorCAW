using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	[SerializeField]
	private float playerMoveSpeed = 10.0f;
	[SerializeField]
	private float playerRotateSpeed = 10.0f;
	[SerializeField]
	private Rigidbody rb;

	private bool onMenu;

	// Use this for initialization
	void Start () 
	{
		onMenu = false;
		rb = GetComponent<Rigidbody> ();

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey (GameManager.GM.forward))
			transform.Translate(Vector3.forward * playerMoveSpeed * Time.deltaTime);
		else if (Input.GetKey (GameManager.GM.backward))
			transform.Translate(Vector3.back * playerMoveSpeed * Time.deltaTime);
		if (Input.GetKey (GameManager.GM.left)) 
			transform.Rotate (0,-playerRotateSpeed * Time.deltaTime,0);
		else if(Input.GetKey(GameManager.GM.right))
			transform.Rotate (0,playerRotateSpeed * Time.deltaTime,0);

		if(Input.GetKeyDown(GameManager.GM.menuButton)){
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
	}
}

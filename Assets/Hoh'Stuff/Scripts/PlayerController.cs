using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	[SerializeField]
	private float playerMoveSpeed = 10.0f;
	[SerializeField]
	private float playerRotateSpeed = 10.0f;
	[SerializeField]
	private Rigidbody rb;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey (GameManager.GM.forward))
			transform.Translate(Vector3.forward * playerMoveSpeed * Time.deltaTime);
		else if (Input.GetKey (GameManager.GM.backward))
			transform.Translate(Vector3.back * playerMoveSpeed * Time.deltaTime);
		else if (Input.GetKey (GameManager.GM.left)) 
			transform.Rotate (0,-playerRotateSpeed * Time.deltaTime,0);
		else if(Input.GetKey(GameManager.GM.right))
			transform.Rotate (0,playerRotateSpeed * Time.deltaTime,0);
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BulletScript : NetworkBehaviour
{


	void Update () 
    {
        transform.Translate(Vector3.forward * 100.0f * Time.deltaTime);
	}
}

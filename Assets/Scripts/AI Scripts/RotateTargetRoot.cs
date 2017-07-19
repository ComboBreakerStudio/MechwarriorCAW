using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Rotate target root.
/// 
/// This is for the afv to circle around the target.
/// 
/// </summary>


public class RotateTargetRoot : NetworkBehaviour {


    public float speed;

	// Update is called once per frame
	void Update () 
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime);

        float timer = Time.deltaTime;

        if (timer > 4.0f)
        {
            Destroy(this);
        }
	}
}

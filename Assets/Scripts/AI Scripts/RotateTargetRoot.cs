using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RotateTargetRoot : NetworkBehaviour {




    public float speed;
	


    void Start()
    {

    }

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

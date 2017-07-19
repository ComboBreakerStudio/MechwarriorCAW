using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ArtilleryBehavior : NetworkBehaviour {

    
    public Transform target;

    public GameObject bullet;
    public Transform firingPoint;

    void Update()
    {

        firingPoint.LookAt(target.position);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdSpawn();
        }
    }

    [Command]
    void CmdSpawn()
    {
        GameObject go = (GameObject)Instantiate(bullet, firingPoint.position,Quaternion.identity);
        NetworkServer.Spawn(go);
    }

}

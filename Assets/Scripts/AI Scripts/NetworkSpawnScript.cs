using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;


public class NetworkSpawnScript : NetworkBehaviour {

    public GameObject testPrefab;
    public Transform spawnObj;



    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            CmdSpawn();
        }
    }

    [Command]
    void CmdSpawn()
    {
        GameObject go = (GameObject)Instantiate(testPrefab, spawnObj.position,Quaternion.identity);
        NetworkServer.Spawn(go);
        Debug.Log("Key PRESSED");
    }
}

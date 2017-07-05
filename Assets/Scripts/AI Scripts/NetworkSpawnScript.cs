using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;


public class NetworkSpawnScript : NetworkBehaviour {

    public GameObject testPrefab;
    public Transform spawnObj;

	public int teamID;
	public int spawnCount;

	void Start(){
		StartCoroutine ("spawnAiTimer", 1.0f);
	}

    void Update()
    {

//        if (Input.GetKeyDown(KeyCode.A))
//        {
//            CmdSpawn();
//        }
    }

	IEnumerator spawnAiTimer(float t){
		yield return new WaitForSeconds (t);
		for(int i = 0;i < spawnCount; i++){
			CmdSpawn ();
		}
	}

    [Command]
    void CmdSpawn()
    {
        GameObject go = (GameObject)Instantiate(testPrefab, spawnObj.position,Quaternion.identity);
		go.GetComponent<AIStats> ().teamID = teamID;
        NetworkServer.Spawn(go);
        Debug.Log("Key PRESSED");
    }
}

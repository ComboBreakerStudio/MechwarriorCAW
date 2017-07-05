using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlanningPhaseManager : MonoBehaviour {
	
	public List <GameObject> spawnObject;
	public string objectName;
	public Vector3 destinationPosition;
	AIStats aiStats;

//	[Command]
	public void CmdSetPosition(){
		//Find gameobject using objectName
		//then send the destination to the object
	}

	void Start()
	{
//		CmdSpawnAI ();

		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	public void Update(){
//		GetAI ();
	}

	public void localSpawn(){
		for (int i = 0; i < AIManager.instance.playerAIUnits.Count; i++)
		{
			GameObject go = Instantiate (AIManager.instance.playerAIUnits[i]);
			spawnObject.Add (go);
//			NetworkServer.Spawn (go);
		}
	}

	public void GetAI(){
		if (Input.GetMouseButtonDown (0)) { 
			RaycastHit hit; 
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition); 

			if (Physics.Raycast (ray, out hit, 1200.0f)) {
				Debug.Log (hit.transform.gameObject.name);
				aiStats = hit.transform.gameObject.GetComponent<AIStats>();
				if (aiStats != null) {
					objectName = aiStats.AIName;
				} else {
					for (int i = 0; i < spawnObject.Count; i++) {
						if (spawnObject [i].GetComponent<AIStats> ().AIName == objectName) {
							spawnObject [i].GetComponent<AIWalkPoint> ().destinationPosition = hit.point;
							spawnObject [i].GetComponent<AIWalkPoint> ().CmdMove();
						}
					}
				}
			}
		}
	}
			
//	[Command]
	public void CmdSpawnAI()
	{
//		for (int i = 0; i < AIManager.instance.AIUnits.Count; i++)
//		{
//			GameObject go = Instantiate (AIManager.instance.AIUnits[i]);
//			spawnObject.Add (go);
//			NetworkServer.Spawn (go);
//			Debug.Log ("Spawn");
//		}
	}



}

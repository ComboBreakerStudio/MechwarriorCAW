using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AI;

public class AIManager : NetworkBehaviour {

	public static AIManager instance;
	public NavMeshAgent myAgent;

	public List<GameObject> team1_AI, team2_AI, playerAIUnits;

	// Use this for initialization
	void Awake () {
		instance = this;
	}

	public List<GameObject> AIUnits;

	public void AddUnitToList (GameObject go){
		AIUnits.Add (go);
	}

	public void DeletePlayerFromList(GameObject go){
		AIUnits.Remove (go);
	}
}

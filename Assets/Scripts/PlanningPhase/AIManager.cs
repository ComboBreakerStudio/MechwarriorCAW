using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AIManager : NetworkBehaviour {

	public static AIManager instance;

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

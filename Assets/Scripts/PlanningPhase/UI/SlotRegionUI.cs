using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotRegionUI : MonoBehaviour, IDropHandler{

	public Vector3 destinationPosition;
	public Transform team1Position, team2Position;
	public float range;
	public bool isRegion;
	public PlanningPhase_UI_AIStats uiAiStatsScript;
	public bool isSpawned;

	[Header("AI set position")]
	public int spawnPointPosition, unitType;

	public GameObject item{
		get{ 
			if(transform.childCount > 0){
				return transform.GetChild (0).gameObject;
			}
			return null;
		}
	}

	public void OnDrop(PointerEventData eventData){
		PlanningPhase_DragableUI.itemBeingDragged.transform.SetParent (transform);
		uiAiStatsScript = GetComponentInChildren<PlanningPhase_UI_AIStats> ();
		if(isRegion){
			uiAiStatsScript.destination = destinationPosition;
			uiAiStatsScript.spawnPointPosition = spawnPointPosition;
			unitType = uiAiStatsScript.unitType;
		}
	}

	void Update(){
		if(isSpawned){
			this.gameObject.SetActive (false);
			return;
		}
		if(!GameManager.GM.isPlanningPhase && isRegion){
			isSpawned = true;
			uiAiStatsScript.SetTeamID (destinationPosition);
			uiAiStatsScript.spawnPointPosition = spawnPointPosition;
			SetPosition ();
			//here to set position
//			uiAiStatsScript.aiObject.GetComponent<AIStats> ().CmdSetPosition (destinationPosition);
//			Debug.Log ("Update Spawn" + destinationPosition);
			this.enabled = false;
		}

//		if(GameManager.GM.localPlayerStatsScript.teamID == 1 && isRegion){
//			destinationPosition = team1Position.position;
//		}
//		else if(GameManager.GM.localPlayerStatsScript.teamID == 2 && isRegion){
//			destinationPosition = team2Position.position;
//		}
	}

	public void SetPosition(){
//		if(uiAiStatsScript != null){
		Debug.Log ("Slot_SetPosition");
//		uiAiStatsScript.SetPosition ();
//		}
		GameManager.GM.localPlayerStatsScript.CmdSetUnitPosition(unitType, spawnPointPosition);
	}
}

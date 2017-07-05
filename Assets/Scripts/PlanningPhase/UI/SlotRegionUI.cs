using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotRegionUI : MonoBehaviour, IDropHandler{

	public Vector3 destinationPosition;
	public float range;
	public bool isRegion;
	public PlanningPhase_UI_AIStats uiAiStatsScript;
	public bool isSpawned;

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
		}
	}

	void Update(){
		if(isSpawned){
			this.gameObject.SetActive (false);
			return;
		}
		if(!GameManager.GM.isPlanningPhase){
			Debug.Log ("Update Spawn");
			isSpawned = true;
			uiAiStatsScript.SetTeamID ();
			//here to set position
			uiAiStatsScript.aiObject.GetComponent<AIStats> ().CmdSetPosition (destinationPosition);
			this.enabled = false;
		}
	}
}

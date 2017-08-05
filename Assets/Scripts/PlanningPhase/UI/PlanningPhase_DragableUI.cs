using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlanningPhase_DragableUI : MonoBehaviour , IBeginDragHandler, IDragHandler, IEndDragHandler{

	public CanvasGroup canvasGroup;
	public static GameObject itemBeingDragged;
	Vector3 startPosition;
	Transform startParent;

	public PlanningPhase_UI_AIStats uiAIStatsScript;
	public Camera cam;
	public LayerMask myMask;

	public string aiName;// get Ai Name

	public void OnBeginDrag(PointerEventData eventData){
//		itemBeingDragged = gameObject;
		startPosition = transform.position;
//		startParent = transform.parent;
//		GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData){
		transform.position = eventData.position;
	}

	public void OnEndDrag(PointerEventData eventData){
//		itemBeingDragged = null;

//		transform.position = transform.position;

		DestinationRayCast ();

		GameManager.GM.localPlayerStatsScript.AddaiUI (this);

		uiAIStatsScript.SetTeamID ();
//		uiAIStatsScript.spawnPointPosition = spawnPointPosition;
		SetPosition ();
//		if(transform.parent == startParent){
//			transform.position = startPosition;
//		}
//		GetComponent<CanvasGroup> ().blocksRaycasts = true;
	}



	public void DestinationRayCast(){
		Ray ray = cam.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit, 1000f)){
			if (hit.collider.gameObject.CompareTag ("Terrain")) {
				uiAIStatsScript.destination = hit.point;
//				Debug.Log ("Hi");
			}
			else {
				transform.position = startPosition;
				if(GameManager.GM.localPlayerStatsScript.teamID == 1){
					uiAIStatsScript.destination = GameManager.GM.respawnPosition_Team1[0].transform.position;
				}
				if(GameManager.GM.localPlayerStatsScript.teamID == 2){
					uiAIStatsScript.destination = GameManager.GM.respawnPosition_Team2[0].transform.position;
				}
				Debug.Log ("Eh");
			}
			Debug.DrawLine (ray.origin, hit.point);
		}

	}

	public bool isSpawned;

	[Header("AI set position")]
	public int spawnPointPosition;

	void Update(){
		if(isSpawned){
			this.gameObject.SetActive (false);
			return;
		}
		if(!GameManager.GM.isPlanningPhase){
			isSpawned = true;
			uiAIStatsScript.SetTeamID ();
			uiAIStatsScript.spawnPointPosition = spawnPointPosition;
			SetPosition ();
			//here to set position
			//			uiAiStatsScript.aiObject.GetComponent<AIStats> ().CmdSetPosition (destinationPosition);
			//			Debug.Log ("Update Spawn" + destinationPosition);
			this.enabled = false;
		}
	}

	public void SetPosition(){
		//		if(uiAiStatsScript != null){
		Debug.Log ("Slot_SetPosition");
		//		uiAiStatsScript.SetPosition ();
		//		}
//				GameManager.GM.localPlayerStatsScript.CmdSetUnitPosition(uiAIStatsScript.unitType, spawnPointPosition);
//		Debug.Log(uiAIStatsScript.unitType);
		GameManager.GM.localPlayerStatsScript.PlanAI (uiAIStatsScript.unitType, GameManager.GM.localPlayer.name, this);
		GameManager.GM.localPlayerStatsScript.CmdSetUnitPosition_UI(aiName, uiAIStatsScript.destination);
	}

}

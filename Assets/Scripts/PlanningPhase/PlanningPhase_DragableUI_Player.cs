using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlanningPhase_DragableUI_Player : MonoBehaviour , IBeginDragHandler, IDragHandler, IEndDragHandler{

	public Camera cam;

	Vector3 startPosition;

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
	}

	public void DestinationRayCast(){
		Ray ray = cam.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit, 1000f)){
			if (hit.collider.gameObject.CompareTag ("Terrain")) {
				GameManager.GM.planningPhaseSpawn.transform.position = new Vector3 (hit.point.x, hit.point.y + 0.5f, hit.point.z);
			}
			else {
				transform.position = startPosition;
			}
			Debug.DrawLine (ray.origin, hit.point);
		}


	}
}

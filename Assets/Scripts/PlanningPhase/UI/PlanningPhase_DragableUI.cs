using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlanningPhase_DragableUI : MonoBehaviour , IBeginDragHandler, IDragHandler, IEndDragHandler{

	public CanvasGroup canvasGroup;
	public static GameObject itemBeingDragged;
	Vector3 startPosition;
	Transform startParent;

	public void OnBeginDrag(PointerEventData eventData){
		itemBeingDragged = gameObject;
		startPosition = transform.position;
		startParent = transform.parent;
		GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData){
		transform.position = eventData.position;
	}

	public void OnEndDrag(PointerEventData eventData){
		itemBeingDragged = null;

		if(transform.parent == startParent){
			transform.position = startPosition;
		}
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
	}

}

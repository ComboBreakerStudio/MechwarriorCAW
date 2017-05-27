using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class JoinGame : MonoBehaviour {

	private NetworkManager networkManager;

	List<GameObject> roomList = new List<GameObject>();

	[SerializeField]
	private Text statusText;

	[SerializeField]
	private GameObject roomListItemPrefab;

	[SerializeField]
	private Transform roomListParent;

	void Start(){
		networkManager = NetworkManager.singleton;
		if(networkManager.matchMaker == null){
			networkManager.StartMatchMaker ();
		}

		RefreshRoomList ();
	}

	public void RefreshRoomList(){
		ClearRoomList ();
		networkManager.matchMaker.ListMatches (0,20,"",true,0,0,OnMatchList);
		statusText.text = "Loading...";
	}

	public void  OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)﻿{

		statusText.text = "";

		if(!success ||matchList == null){
			statusText.text = "No Matches Found";
			return;
		}

		ClearRoomList ();

		foreach (MatchInfoSnapshot match in matchList) {
			GameObject _roomListItemGO = Instantiate (roomListItemPrefab);
			_roomListItemGO.transform.SetParent (roomListParent);

			RoomListItem _roomListItem = _roomListItemGO.GetComponent<RoomListItem> ();

			if(_roomListItem != null){
				_roomListItem.Setup (match,JoinRoom);
			}

			//Have a component sit on the gameobject
			//that will take care of the name/amount of users.
			// as well as setting up a callback function that will join the game

			roomList.Add (_roomListItemGO);
		}

		//if there is no room
		if(roomList.Count == 0){
			statusText.text = "No Room(s) Found";
		}
	}

	private void ClearRoomList(){
		for(int i = 0; i < roomList.Count; i++){
			Destroy (roomList[i]);
		}

		roomList.Clear ();
	}

	public void JoinRoom(MatchInfoSnapshot _match){
		networkManager.matchMaker.JoinMatch (_match.networkId,"","","",0,0,networkManager.OnMatchJoined);
		ClearRoomList ();
		statusText.text = "Connecting ...";
	}
}

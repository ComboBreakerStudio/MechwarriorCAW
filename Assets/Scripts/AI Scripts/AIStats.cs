using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AI;

public class AIStats : NetworkBehaviour {
    
	[SyncVar]
	public int teamID;
	[SyncVar]
	public int curHealth, maxHealth;
	[SyncVar]
	public string AIName;
	[SyncVar]
	public string OwnerName;

	public int unitType;

	public bool isPlanned, isRespawned;

	public NavMeshAgent NavAgent;

	public MonoBehaviour aiBehaviorScript;

    public bool PlayerCommandToWander;

    public bool FollowingPlayer;

    [SyncVar]
    public Transform targetOwner;


	void Start () {
		if(isServer){
			RegisterAI ();
//			RegisterAI ();
//			Debug.Log ("AIServer " + AIName);
			curHealth = maxHealth;
		}
		AIManager.instance.AIUnits.Add (this.gameObject);
		NavAgent = GetComponent<NavMeshAgent> ();
        targetOwner = GameObject.Find(OwnerName).transform;


		NavAgent.enabled = false;
		aiBehaviorScript.enabled = false;
	}

	void Update(){
		if(gameObject.name != AIName){
			gameObject.name = AIName;
		}

		if(curHealth <= 0){
//			Destroy (this.gameObject);
			this.gameObject.SetActive(false);
		}

        float distToTarget = Vector3.Distance(transform.position, targetOwner.position);



        //I To keep updating the distance, otherwise the agent will just ram to the target it should follow
        if (PlayerCommandToWander == false)
        {
            if (distToTarget < 40f)
            {
                NavAgent.isStopped = true;
            }
            else if (distToTarget > 40f)
            {
                NavAgent.isStopped = false;
            }
        }

        if (FollowingPlayer == true)
        {
            if (distToTarget > 40f)
            {
                NavAgent.SetDestination(targetOwner.position);
            }
            else if (distToTarget < 40f)
            {
                NavAgent.isStopped = true;
            }
        }
        AICommands();
	}
//	[Command]
	void RegisterAI()
	{
		string _ID = transform.gameObject.name + GetComponent<NetworkIdentity> ().netId;
		AIName = _ID;
		transform.gameObject.name = AIName;
	}

//	[Command]
	public void CmdSetOwner(string ownerName){
		OwnerName = ownerName;
	}

	public void SetDestination(Vector3 destination){
		NavAgent.SetDestination (destination);

		aiBehaviorScript.enabled = true;
//		aiBehaviorScript.SendMessage ("SetAIPoint", destination);
	}

    //I AI Commands
    public void AICommands()
    {
        //I Change this keycode if you guys decide to change to something or what should design should be
        if (Input.GetKeyDown(KeyCode.F2))
        {
            //I Command for the wander
            if (PlayerCommandToWander == false)
            {
                PlayerCommandToWander = true;
            }
            else if (PlayerCommandToWander == true)
            {
                PlayerCommandToWander = false;
            }
            if (FollowingPlayer == true)
            {
                FollowingPlayer = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            //I Command for follow me
            if (FollowingPlayer == false)
            {
                FollowingPlayer = true;
            }
            else if (FollowingPlayer == true)
            {
                FollowingPlayer = false;
            }
            if (PlayerCommandToWander == true)
            {
                PlayerCommandToWander = false;
            }
        }

    }

}

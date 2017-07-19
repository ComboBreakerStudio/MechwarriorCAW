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


    float HoldTimer = 1.0f;


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


        //I To keep updating the distance, otherwise the agent will just ram to the target it should follow
        /*
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
        */

        if (FollowingPlayer == true)
        {
            if (Distance() > 40f)
            {
                NavAgent.isStopped = false;
                NavAgent.SetDestination(targetOwner.position);
            }
            else if (Distance() < 40f)
            {
                NavAgent.isStopped = true;
            }
        }
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

    //I Hotkeyed units
    public void CallUnitAFV1()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            HoldTimer -= 0.2f;
            if (HoldTimer <= 0f)
            {
                if (Distance() > 30f)
                {
                    NavAgent.SetDestination(targetOwner.position);
                }
            }
        }
    }

    public void CallUnitAFV2()
    {
        if (Input.GetKey(KeyCode.Alpha2))
        {
            HoldTimer -= 0.2f;
            if (HoldTimer <= 0f)
            {
                if (Distance() > 30f)
                {
                    NavAgent.SetDestination(targetOwner.position);
                }
            }
        }
    }

    public void CallUnitSniper()
    {
        if (Input.GetKey(KeyCode.Alpha3))
        {
            HoldTimer -= 0.2f;
            if(HoldTimer<=0f)
            {
                if (Distance() > 30f)
                {
                    NavAgent.SetDestination(targetOwner.position);
                }
            }
        }
    }

    private float Distance()
    {
        return Vector3.Distance(transform.position, targetOwner.position);
    }

}

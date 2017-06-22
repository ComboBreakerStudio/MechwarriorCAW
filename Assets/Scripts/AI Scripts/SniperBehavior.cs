﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.AI;


public class SniperBehavior : NetworkBehaviour {

    NavMeshAgent agent;

    public List<Transform> visibleTarget = new List<Transform>();

    [Header("AI Sight")]
    public float viewRadius;
    //[Range(0,360)]
    //public float viewAngle;
    public Transform targetposition;
    public Transform AIpoint;
    public LayerMask targetMask;
    public LayerMask wallMask;

    float firingInterval;

    public AIState behaviour = AIState.Wandering;
    public AISetupBehaviour setupbehaviour = AISetupBehaviour.NotSetup;


    [Header("AI wandering Range")]
    public float timer;
    public float wanderRadius;
    public float wanderTimer;



    [Header("AI Shoot")]
    public Transform firingPoint;
    public GameObject bulletPrefab;
    public float minimumRange;


    [Header("Update Time")]
    public float timeInterval;
    public float fireInterval;


    public bool PlayerCommandToWander;


    public float SettingTimer;
    public float setupTimer;

    // Use this for initialization
    public override void OnStartServer()
    {
        //Debug.Log("SERVER STARTED");
        agent = GetComponent<NavMeshAgent>();


        StartCoroutine(AISniper());
    }

    void Update()
    {
        // Determine what to do if there is a target inside the List
        if (visibleTarget.Contains(targetposition))
        {

            if (setupbehaviour == AISetupBehaviour.NotSetup)
            {
                behaviour = AIState.InSight;
            }
            else if (setupbehaviour == AISetupBehaviour.Setup)
            {
                behaviour = AIState.InSight;
            }
        }

        else if (!visibleTarget.Contains(targetposition))
        {
            
            if (PlayerCommandToWander == false)
                behaviour = AIState.Idle;
            else if (PlayerCommandToWander == true)
                behaviour = AIState.Wandering;
        }


        if (behaviour == AIState.Idle)
        {

            if (Input.GetKeyDown(KeyCode.A))
            {
                if (setupbehaviour == AISetupBehaviour.Setup)
                {
                    setupbehaviour = AISetupBehaviour.NotSetup;
                }
                else if (setupbehaviour == AISetupBehaviour.NotSetup)
                {
                    GoToPoint();
                } 
            }
        }

        if (behaviour == AIState.Wandering)
        {
            //Debug.Log("Wandering");
            if (setupbehaviour == AISetupBehaviour.Setup)
            {
               setupbehaviour = AISetupBehaviour.NotSetup;
            }

            else if (setupbehaviour == AISetupBehaviour.NotSetup)
            {
                Wandering();
            }
        }
    }

    void GoToPoint()
    {
      agent.SetDestination(AIpoint.position);
    }

    IEnumerator AISniper()
    {
        while (true)
        {
            FindVisibleTarget();


            yield return new WaitForSeconds(timeInterval);
        }
    }





    //I For random Wandering by using navmesh sphere
    public static Vector3 RandomWandering(Vector3 origin, float dist, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * dist;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    //I Wandering
    void Wandering()
    {
        wanderTimer += Time.deltaTime;

        if (wanderTimer >= timer)
        {
            Vector3 newPos = RandomWandering(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);

            wanderTimer = 0f;
        }
    }


    void Firing()
    {

        Vector3 direction = targetposition.position;
        direction.y = 0;

        transform.LookAt(direction);

        if (Distance() > minimumRange && Distance() < viewRadius)
        {
            fireInterval -= 0.45f;
            if (fireInterval <= 0f)
            {
                //Debug.Log("FIRING");
                //CmdSpawn();
                fireInterval = 2.0f;
            }
        }
        else if (Distance() < minimumRange)
        {
            Vector3 Position = targetposition.transform.position.normalized * -0.5f;
            agent.destination = Position;
        }
    }

    //I Checking the visible target in the list
    void FindVisibleTarget()
    {
        visibleTarget.Clear();
        targetposition = null;

        Collider[] targetInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetInViewRadius.Length; i++)
        {
            targetposition = targetInViewRadius[i].transform;

            Vector3 dirToTarget = (targetposition.position - transform.position).normalized;
            if (Vector3.Angle(transform.position,dirToTarget) < 360)
            {
                float distToTarget = Vector3.Distance(transform.position, targetposition.position);
                if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, wallMask))
                {
                    visibleTarget.Add(targetposition);
                }
            }
        }
    }

    //I Checking Distance
    private float Distance()
    {
        return Vector3.Distance(this.transform.position, targetposition.position);
    }


    [Command]
    void CmdSpawn()
    {
        GameObject go = (GameObject)Instantiate(bulletPrefab, firingPoint.position,Quaternion.identity);
        NetworkServer.Spawn(go);
    }

}
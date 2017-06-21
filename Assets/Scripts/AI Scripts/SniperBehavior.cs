﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.AI;


public class SniperBehavior : NetworkBehaviour {

    NavMeshAgent agent;

    public float rangeDistance;

    public List<Transform> visibleTarget = new List<Transform>();

    [Header("AI Sight")]
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;
    public Transform targetposition;
    public LayerMask targetMask;
    public LayerMask wallMask;

    float firingInterval;

    public AIBehaviour behaviour = AIBehaviour.Wandering;
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




    // Use this for initialization
    public override void OnStartServer()
    {
        //Debug.Log("SERVER STARTED");
        agent = GetComponent<NavMeshAgent>();


        StartCoroutine(AISniper());
    }


    IEnumerator AISniper()
    {
        while (true)
        {
            FindVisibleTarget();

            if (check(targetposition) != null)
            {

                behaviour = AIBehaviour.InSight;

                if (this.setupbehaviour == AISetupBehaviour.NotSetup)
                {

                    //Debug.Log("Unsetting Up");
                    setupbehaviour = AISetupBehaviour.Setup;

                    //Insight(targetposition);
                    if (this.setupbehaviour == AISetupBehaviour.Setup)
                    {
                        Firing();
                    }
                }

                else if (this.setupbehaviour == AISetupBehaviour.Setup)
                {
                    Firing();
                }
            }
            else if (check(targetposition) == null)
            {
                behaviour = AIBehaviour.Wandering;

                if (this.setupbehaviour == AISetupBehaviour.Setup)
                {
                    setupbehaviour = AISetupBehaviour.NotSetup;

                    Wandering();
                }
                else if (this.setupbehaviour == AISetupBehaviour.NotSetup)
                {
                    Wandering();
                }
            }

            yield return new WaitForSeconds(timeInterval);
        }
    }

    //I Checking to change behaviour correctly, dependant on is there a transform inside the targetposition part
    Transform check(Transform target)
    {
        return target;
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

        if (Distance() > minimumRange)
        {
            fireInterval -= 0.45f;
            if (fireInterval <= 0f)
            {
                //Debug.Log("FIRING");
                //CmdSpawn();
                fireInterval = 2.0f;
            }
        }
        else if (Distance() < minimumRange && Distance() > minimumRange)
        {
            Vector3 Position = targetposition.transform.position.normalized * -3f;
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
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float distToTarget = Vector3.Distance(transform.position, targetposition.position);
                if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, wallMask))
                {
                    visibleTarget.Add(targetposition);



                    /*For now I comment this part
                    for (int e  = 0; e < visibleTarget.Count; e++){
                        if(visibleTarget[e].GetComponent<PlayerStats>().teamID == statsScript.teamID){
                            visibleTarget.Remove (visibleTarget [e]);
                        }
                    }*/

                }
            }
        }
    }

    //I To keep track where the object facing towards of sight
    public Vector3 DirecFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;

        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
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

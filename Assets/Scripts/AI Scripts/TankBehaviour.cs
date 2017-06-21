using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.AI;

public class TankBehaviour : NetworkBehaviour {

	public AIStats statsScript;

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


        StartCoroutine(AITankBehaviour());
	}

	/* For Debugging Purpose only
    void Update()
    {
        Debug.Log(Distance());
    }
	*/

    IEnumerator AITankBehaviour()
    {
        while (true)
        {
            FindVisibleTarget();


            //Debug.Log("player " + Distance().ToString() + "Target " + targetposition);


            //I Check the distance and the position of the target to start firing
            if (check(targetposition) != null)
            {
                Firing();
            }
            else if (check(targetposition) == null)
            {
                Wandering();
            }

            yield return new WaitForSeconds(timeInterval);
        }
    }

    //I Checking to change behavior
    Transform check(Transform target)
    {
        return target;
    }

    //I Wandering
    void Wandering()
    {
        behaviour = AIBehaviour.Wandering;

        wanderTimer += Time.deltaTime;
        if (wanderTimer >= timer)
        {
            Vector3 newPos = RandomWandering(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            wanderTimer = 0f;
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


    //I AI Insight
    void Firing()
    {
        behaviour = AIBehaviour.InSight;

        Vector3 direction = targetposition.position;
        direction.y = 0;

        transform.LookAt(direction);

        if (Distance() > minimumRange)
        {
            fireInterval -= 0.45f;
            if (fireInterval <= 0f)
            {
                //Debug.Log("Firing");
                //CmdSpawn();
                fireInterval = 2.0f;
            }
        }
        else if (Distance() < minimumRange && Distance() > minimumRange)
        {
            Vector3 Position = targetposition.transform.position.normalized * -20f;
            agent.destination = Position;
        }
        else
        {
            behaviour = AIBehaviour.Wandering;
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


                    /* For now I comment this part since I don't know what it does but it give me errors
					for (int e  = 0; e < visibleTarget.Count; e++){
						if(visibleTarget[e].GetComponent<PlayerStats>().teamID == statsScript.teamID){
							visibleTarget.Remove (visibleTarget [e]);
						}
					}
                    */

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


    //I I comment this part for now since this is for testing part
    [Command]
    void CmdSpawn()
    {
        GameObject go = (GameObject)Instantiate(bulletPrefab, firingPoint.position,Quaternion.identity);
        NetworkServer.Spawn(go);
    }

}

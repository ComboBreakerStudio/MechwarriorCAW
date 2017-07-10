using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.AI;
using AIEnums;


/// <summary>
/// Sniper behavior.
/// All the code is basically the same as tank, except this part have the setup behaviour, which needs animation
/// to even demonstrate it or test if it's working or not
/// </summary>

public class SniperBehavior : NetworkBehaviour {

    NavMeshAgent agent;

    public List<Transform> visibleTarget = new List<Transform>();

    [Header("AI Sight")]
    public float viewRadius;
    public Transform targetposition;
	public Vector3 AIpoint;
    public LayerMask targetMask;
    public LayerMask wallMask;
    public LayerMask ground;


    float firingInterval;

    public AIState behaviour = AIState.Wandering;
    public AISetupBehaviour setupbehaviour = AISetupBehaviour.NotSetup;

    [Header("Laser")]
    public LineRenderer lineRenderer;

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
        
        if (Input.GetKeyDown(KeyCode.F2))
        {
            if (PlayerCommandToWander == false)
            {
                PlayerCommandToWander = true;
            }
            else if (PlayerCommandToWander == true)
            {
                PlayerCommandToWander = false;
            }
        }

        // Determine what to do if there is a target inside the List
        if (visibleTarget.Contains(targetposition))
        {
            agent.isStopped = true;


            if (setupbehaviour == AISetupBehaviour.NotSetup)
            {
                behaviour = AIState.InSight;
                setupbehaviour = AISetupBehaviour.Setup;
            }
            else if (setupbehaviour == AISetupBehaviour.Setup)
            {
                behaviour = AIState.InSight;
            }
        }

        else if (!visibleTarget.Contains(targetposition))
        {
            if (lineRenderer.enabled)
            {
                lineRenderer.enabled = false;
            }

            agent.isStopped = false;

            if (PlayerCommandToWander == false)
                behaviour = AIState.Idle;
            else if (PlayerCommandToWander == true)
                behaviour = AIState.Wandering;
        }


        if (behaviour == AIState.Idle)
        {
            float distToTarget = Vector3.Distance(transform.position, AIpoint);

            if (distToTarget < 10f)
			{
				agent.isStopped = true;
				behaviour = AIState.Wandering;

            }

            if (Input.GetKeyDown(KeyCode.F1))
            {
                if (setupbehaviour == AISetupBehaviour.Setup)
                {
                    setupbehaviour = AISetupBehaviour.NotSetup;
                }
                else if (setupbehaviour == AISetupBehaviour.NotSetup)
                {
                    if (distToTarget > 10f)
                    {
                        agent.isStopped = false;

                        agent.SetDestination(AIpoint);
                    }
                } 
            }
        }
        else if (behaviour == AIState.Wandering)
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
        else if (behaviour == AIState.InSight)
        {
            Firing();
        }
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
    public static Vector3 RandomWandering(Vector3 origin, float dist, int ground)
    {
        Vector3 randomDirection = Random.insideUnitSphere * dist;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, dist, ground);

        return navHit.position;
    }

    //I Wandering
    void Wandering()
    {
        wanderTimer += Time.deltaTime;

        if (wanderTimer >= timer)
        {
            Vector3 newPos = RandomWandering(transform.position, wanderRadius,ground);
            agent.SetDestination(newPos);

            wanderTimer = 0f;
        }
    }


    void Firing()
    {
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
        }

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
                lineRenderer.SetPosition(0,firingPoint.position);
                lineRenderer.SetPosition(1, targetposition.position);
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

	public void SetAIPoint(Vector3 positon){
		Debug.Log ("AIPoint : " + positon);
		behaviour = AIState.Idle;
		AIpoint = positon;
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.AI;

public class TankBehaviour : NetworkBehaviour {

	public AIStats statsScript;

    NavMeshAgent agent;

    public List<Transform> visibleTarget = new List<Transform>();

    [Header("AI Sight")]
    public float viewRadius;
    public Transform targetposition;
    public Transform AIpoint;
    public LayerMask targetMask;
    public LayerMask wallMask;
    public LayerMask ground;

    float firingInterval;

    public AIState behaviour = AIState.Idle;

    [Header("AI wandering Range")]
    public float timer;
    public float wanderRadius;
    public float wanderTimer;



    [Header("AI Shoot")]
    public Transform tankHead;
    public Transform firingPoint;
    public GameObject bulletPrefab;
    public float minimumRange;




    [Header("Update Time for it to see target")]
    public float timeInterval;
    public float fireInterval;

    public bool PlayerCommandToWander;

	// Use this for initialization
    public override void OnStartServer()
    {
        //Debug.Log("SERVER STARTED");
        agent = GetComponent<NavMeshAgent>();


        StartCoroutine(AITankBehaviour());
	}

    void Update()
    {
        if (visibleTarget.Contains(targetposition))
        {
            Firing();
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
                //Debug.Log("Button Pressed");
                GoToPoint();
                behaviour = AIState.GoToPoint;
            }
        }
        else if (behaviour == AIState.Wandering)
        {
            //Debug.Log("Wandering");
            Wandering();
        }
        else if (behaviour == AIState.GoToPoint)
        {
            //Debug.Log("Go to point");
        }
    }

    IEnumerator AITankBehaviour()
    {
        while (true)
        {
            FindVisibleTarget();



            yield return new WaitForSeconds(timeInterval);
        }
    }

    void GoToPoint()
    {
        float distToTarget = Vector3.Distance(transform.position, AIpoint.position);

        if (distToTarget > 2f)
        {
            agent.SetDestination(AIpoint.position);
        }
    }

    //I Wandering
    void Wandering()
    {
        behaviour = AIState.Wandering;


        wanderTimer += Time.deltaTime;
        if (wanderTimer >= timer)
        {
            Vector3 newPos = RandomWandering(transform.position, wanderRadius, ground);
            agent.SetDestination(newPos);
            wanderTimer = 0f;
        }
    }




	//I For random Wandering by using navmesh sphere
    public static Vector3 RandomWandering(Vector3 origin, float dist, LayerMask ground)
    {
        Vector3 randomDirection = Random.insideUnitSphere * dist;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, dist, ground);

        return navHit.position;
    }


    //I AI Insight
    void Firing()
    {
        behaviour = AIState.InSight;

        Vector3 direction = targetposition.position;
        direction.y = 0;

        tankHead.transform.LookAt(direction);


        if (Distance() > minimumRange && Distance() < viewRadius)
        {
          fireInterval -= 0.45f;
          if (fireInterval <= 0f)
            {
              //Debug.Log("Firing");
              //CmdSpawn();
              fireInterval = 2.0f;
             }
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

            //I Spheric vision, so if there is a wall in between, the AI would not even see the target.
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


    //I I comment this part for now since this is for testing part
    [Command]
    void CmdSpawn()
    {
        GameObject go = (GameObject)Instantiate(bulletPrefab, firingPoint.position,Quaternion.identity);
        NetworkServer.Spawn(go);
    }

}

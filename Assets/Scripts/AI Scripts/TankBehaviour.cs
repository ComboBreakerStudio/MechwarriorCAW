using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.AI;
using AIEnums;



/// <summary>
/// Tank behaviour.
/// If some of the part is not understandable, do specify which part. I'm not going to comment everything and detailed
/// since I already named all the variables correctly. Just inform me if some are weird, too complex or not even correct.
/// For The Sniper Part, they are basically the same as this, except only have setup behaviour part which need an animation
/// to check if the code is correct or need more implementation. And yes, I did check unity asset store to use, but
/// there is none I can use. If you found any, do inform me.
/// </summary>

public class TankBehaviour : NetworkBehaviour {

	public AIStats statsScript;

    public TeamManager team;

    NavMeshAgent agent;


    public List<Transform> visibleTarget = new List<Transform>();

    [Header("AI Sight")]
    public float viewRadius;
    public Transform targetposition;
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
    public float maximumRange;


    [Header("AI Circle Around")]
    public Transform childTarget;
    public Transform TargetRoot;


    [Header("Update Time for it to see target")]
    public float timeInterval;
    public float fireInterval;




    public override void OnStartServer()
    {
        agent = GetComponent<NavMeshAgent>();

        team = GameObject.Find("TeamManager").GetComponent<TeamManager>();

        if (isServer)
        {
            StartCoroutine(AITankBehaviour());

        }
	}

    void Update()
    {
        if(childTarget!=null)
        {
            agent.SetDestination(childTarget.transform.position);
        }



        //I IF there is no target, basically the AI go Idle, then the player can command to wander or move them.
        if (!visibleTarget.Contains(targetposition))
        {

            if (statsScript.PlayerCommandToWander == false)
                behaviour = AIState.Idle;
            else if (statsScript.PlayerCommandToWander == true)
                behaviour = AIState.Wandering;
        }

        //I Check if there is a target visible within the sight radius
        else if (visibleTarget.Contains(targetposition))
        {
            Firing();
        }

        else if (behaviour == AIState.Wandering)
        {
            //Debug.Log("Wandering");
            agent.isStopped = false;
            Wandering();
        }
    }

    IEnumerator AITankBehaviour()
    {
        while (true)
        {
            RpcFindVisibleTarget();



            yield return new WaitForSeconds(timeInterval);
        }
    }

    //I Wandering
    void Wandering()
    {
        behaviour = AIState.Wandering;


        wanderTimer += Time.deltaTime;

        if (wanderTimer >= timer)
        {
            //I Refer to random wandering part if you want to understand this.
            Vector3 newPos = RandomWandering(transform.position, wanderRadius, ground);
            agent.SetDestination(newPos);
            wanderTimer = 0f;
        }
    }




	//I For random Wandering by using navmesh sphere

    public static Vector3 RandomWandering(Vector3 origin, float dist, LayerMask ground)
    {
        //I Making sure the area is within the wander Radius variable only.
        Vector3 randomDirection = Random.insideUnitSphere * dist;

        randomDirection += origin;

        NavMeshHit navHit;

        //I Basically if there is a path for navmesh to move, then it will move in that position only.
        NavMesh.SamplePosition(randomDirection, out navHit, dist, ground);

        return navHit.position;
    }


    //I AI Insight
    void Firing()
    {
        behaviour = AIState.InSight;

        Vector3 direction = targetposition.position;
        //direction.y = 0;

        tankHead.transform.LookAt(direction);


        if (Distance() > minimumRange && Distance() < viewRadius)
        {
          fireInterval -= 0.45f;
          
          if (fireInterval <= 0f)
            {
                //I This part I comment first, since I don't know either you want to keep spawning bullet or using object pooling

              //Debug.Log("Firing");
              CmdSpawn();
              fireInterval = 2.0f;
             }
        }
    }

    //I Checking the visible target in the list
    [ClientRpc]
    void RpcFindVisibleTarget()
    {
        visibleTarget.Clear();

        Collider[] targetInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetInViewRadius.Length; i++)
        {
            targetposition = targetInViewRadius[i].transform;


            //I Spheric vision, so if there is a wall in between, the AI would not even see the target.
            //I The code is basically from sebastian lague, except I don't make it cone of vision style.
            Vector3 dirToTarget = (targetposition.position - transform.position).normalized;
            if (Vector3.Angle(transform.position,dirToTarget) < 360)
            {
                float distToTarget = Vector3.Distance(transform.position, targetposition.position);
                if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, wallMask))
                {
                    //I For AI detection if the target is the same team or not.
                    //I This is where it got bugged, when I test it alone, it work fine but when client joined, it goes bugged
                    if (statsScript.teamID == 1)
                    {
                        for (int j = 0; j < team.team2.Count; j++)
                        {
                            if (team.team2[j])
                            {
                                visibleTarget.Add(targetposition);
                            }

                            if (GameObject.FindWithTag("Target") == null)
                            {

                                CreateTargetRoot();

                            }
                        }
                    }
                    else if (statsScript.teamID == 2)
                    {
                        for (int j = 0; j < team.team1.Count; j++)
                        {
                            if (team.team1[j])
                            {
                                visibleTarget.Add(targetposition);
                            }

                            if (GameObject.FindWithTag("Target") == null)
                            {

                                CreateTargetRoot();

                            }
                        }
                    }

                }
            }
            else if(Vector3.Angle (transform.position, dirToTarget)>360)
            {
                childTarget = null;
            }
        }
    }

    //I Checking Distance
    private float Distance()
    {
          return Vector3.Distance(this.transform.position, targetposition.position);
    }

    void CreateTargetRoot()
    {
        Transform childObject = Instantiate(TargetRoot);
        childObject.transform.SetParent(targetposition,false);
        //childObject.transform.parent = targetposition;
        childTarget = GameObject.FindWithTag("Target").transform;
        //TargetRoot.position =  Vector3.zero;

    }


    //I I comment this part for now since this is for testing part
    [Command]
    void CmdSpawn()
    {
        GameObject go = (GameObject)Instantiate(bulletPrefab, firingPoint.position,tankHead.transform.rotation);
        NetworkServer.Spawn(go);
    }

}

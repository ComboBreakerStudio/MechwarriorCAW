using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class TankBehaviour : NetworkBehaviour {

    UnityEngine.AI.NavMeshAgent agent;

    public float rangeDistance;
    public Transform[] points;

    public List<Transform> visibleTarget = new List<Transform>();

    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;
    public Transform targetposition;
    public LayerMask targetMask;
    public LayerMask wallMask;

    float firingInterval;

    public AIBehaviour behaviour = AIBehaviour.Wandering;




    public Transform firingPoint;
    public GameObject bulletPrefab;

    private int destPoint = 0;

    float fireInterval = 2.0f;


	// Use this for initialization
    public override void OnStartServer()
    {
        //Debug.Log("SERVER STARTED");
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        StartCoroutine(AITankBehaviour());
	}

    void Update()
    {
        Debug.Log(Distance());
    }

    IEnumerator AITankBehaviour()
    {
        while (true)
        {
            


            //Debug.Log("player " + Distance().ToString() + "Target " + targetposition);
            //I Check the distance and the position of the target to start firing
            if (behaviour == AIBehaviour.InSight)
            {
                agent.Stop();
                if (Distance() > 40f && visibleTarget.Contains(targetposition))
                {
                    fireInterval -= 0.45f;
                    if (fireInterval <= 0f)
                    {
                        CmdSpawn();
                        fireInterval = 2.0f;
                    }
                }
                else if (Distance() < 40f && Distance() > 0f)
                {
                    Vector3 Position = targetposition.transform.position.normalized * -3f;
                    agent.destination = Position;
                }
                else
                {
                    behaviour = AIBehaviour.Wandering;
                }
            }
            else if (behaviour == AIBehaviour.Wandering)
            {
                agent.Resume();
                Wandering();
            }
            
            FindVisibleTarget();

            Wandering();

            yield return new WaitForSeconds(0.25f);
        }
    }



    void Wandering()
    {
        if (points.Length == 0)
            return;

        agent.destination = points[destPoint].position;

        destPoint = (destPoint + 1) % points.Length;
    }

    //I Checking the visible target in the list
    void FindVisibleTarget()
    {
        visibleTarget.Clear();

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

                    if (visibleTarget.Contains(targetposition))
                    {
                        behaviour = AIBehaviour.InSight;
                        //transform.rotation = Quaternion.LookRotation(targetposition.transform.position);

                    }
                    else if(!visibleTarget.Contains(targetposition))
                    {
                        targetposition = null;
                        behaviour = AIBehaviour.Wandering;
                    }
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

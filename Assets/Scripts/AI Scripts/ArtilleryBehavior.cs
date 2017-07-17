using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Artillery behavior.
/// 
/// Need the radar sight to do the artillery behavior. For now, this is just for testing purposes on
/// making it work as it was intended.
/// </summary>


public class ArtilleryBehavior : NetworkBehaviour {

    public Transform target;
    public GameObject bullet;

    [Header("Cannon Header")]
    public Transform firingPoint;
    public Transform cannonHead;


    [Header("Firing Header")]
    public float minimumRange;
    public float maximumRange;
    public float firingDelay;

    float fireInterval;

    void Start()
    {
        StartCoroutine(Main());
    }

    IEnumerator Main()
    {
        while (true)
        {
          Firing();


          yield return new WaitForSeconds(0.15f);
           
        }

    }



    void Firing()
    {

        Vector3 direction = target.position;
        direction.y = 0f;

        cannonHead.transform.LookAt(direction);


        //I For the artillery to move back.
        if (Distance() < minimumRange)
        {
            //I For now I leave it empty since it is dependant on position of the target. Which is kind from the player view
            //I and I'm not sure what to do yet.

        }

        //I For the artillery to fire if the target more than minimum range and less than maximum Range
        else if (Distance() > minimumRange && Distance() < maximumRange)
        {
            fireInterval -= 0.25f;

            if (fireInterval <= 0f)
            {
                CmdSpawn();
                fireInterval = firingDelay;
            }
        }

    }

    private float Distance()
    {
        return Vector3.Distance(transform.position, target.position);
    }


    [Command]
    void CmdSpawn()
    {
        GameObject go = (GameObject)Instantiate(bullet, firingPoint.position,Quaternion.identity);
        NetworkServer.Spawn(go);
    }
}

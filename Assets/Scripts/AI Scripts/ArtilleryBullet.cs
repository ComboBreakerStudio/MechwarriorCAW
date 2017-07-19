using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Artillery bullet.
/// 
/// This is for the artillery bullet to make it parabola movement.
/// 
/// </summary>

public class ArtilleryBullet : NetworkBehaviour {


    public ArtilleryBehavior artillery;

    public Transform targetPosition;


    public float fireAngle;
    public float bulletSpeed;

    [Header("Explosions")]
    public float explosionRadius;
    public LayerMask targetLayer;


    void Awake()
    {
        artillery = GameObject.FindObjectOfType<ArtilleryBehavior>() as ArtilleryBehavior;
    }


	void Start ()
    {
        targetPosition = artillery.target;

        StartCoroutine(Launch());
	}

    //I This is where the parabola function happening.

    IEnumerator Launch()
    {
        yield return new WaitForSeconds(0.15f);

        transform.position = transform.position + new Vector3(0, 0.0f, 0);

        Vector3 pos = artillery.firingPoint.position;
        Vector3 target = targetPosition.position;

        // Calculate Distance to target

        transform.LookAt(target);

        //Calculate the velocity needed to fire the object to the target
        float projectile_velocity = Distance() / (Mathf.Sin(2*fireAngle*Mathf.Deg2Rad)/ bulletSpeed);

        //float Vi = Mathf.Sqrt(dist * -Physics.gravity.y / (Mathf.Sin(Mathf.Deg2Rad * fireAngle * 2)));

        float Vy, Vz;

        //Vy = Vi * Mathf.Sin(Mathf.Deg2Rad * fireAngle);
        //Vz = Vi * Mathf.Cos(Mathf.Deg2Rad * _bullseye.position.z);

        Vz = Mathf.Sqrt(projectile_velocity) * Mathf.Cos(fireAngle * Mathf.Deg2Rad);
        Vy = Mathf.Sqrt(projectile_velocity) * Mathf.Sin(fireAngle * Mathf.Deg2Rad);

        //Calculate flight Time
        float flightDuration = Distance() / Vz;

        transform.rotation = Quaternion.LookRotation(target - transform.position);

        //Vector3 localVelocity = new Vector3(0f, Vy, Vz);

        //Vector3 globalVelocity = artillery.firingPoint.transform.TransformDirection(localVelocity);

        //Vector3 globalVelocity = transform.TransformVector(localVelocity);

        //GetComponent<Rigidbody>().velocity = globalVelocity*10f;

        float elapse_time = 0f;

        //Debug.Log(flightDuration);

        while (elapse_time < flightDuration)
        {
            transform.Translate(0, (Vy - (bulletSpeed * elapse_time)) * Time.deltaTime, Vz * Time.deltaTime);

            elapse_time += Time.deltaTime;

            if (elapse_time >= flightDuration)
            {
                ExplosionDamage();

                Destroy(gameObject);
            }

            yield return null;
        }
    }

    private float Distance()
    {
        return Vector3.Distance(transform.position, targetPosition.position);
    }

    void ExplosionDamage()
    {
        Collider[] targetInExplosion = Physics.OverlapSphere(transform.position, explosionRadius, targetLayer);


        foreach (Collider h in targetInExplosion)
        {
            //I Put the Player Stats here to apply damage. I'm not putting it since I don't know which script determine the player mechs health.


        }
    }

}

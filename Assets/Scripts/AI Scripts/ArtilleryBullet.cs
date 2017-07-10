using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ArtilleryBullet : NetworkBehaviour {


    public ArtilleryBehavior artillery;

    public Transform _bullseye;

    public float _targetRange;
    public float fireAngle;

    public float gravity;

    public LayerMask damageLayer;

	// Use this for initialization
    void Awake()
    {
        artillery = GameObject.FindObjectOfType<ArtilleryBehavior>() as ArtilleryBehavior;
    }


	void Start ()
    {
        

        _bullseye = artillery.target;

        StartCoroutine(SimulateProjectile());
	}
	
    void Update()
    {
        //Launch();
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Hit");

        if (col.collider.gameObject.layer == damageLayer)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Hit");

        if (col.gameObject.layer == damageLayer)
        {

            Destroy(this.gameObject);
        }
    }


    private void Launch()
    {
        transform.position = transform.position + new Vector3(0, 0.0f, 0);

        Vector3 pos = artillery.firingPoint.position;
        Vector3 target = _bullseye.position;

        // Calculate Distance to target
        float dist = Vector3.Distance(pos, target);

        transform.LookAt(target);

        //Calculate the velocity needed to fire the object to the target
        float projectile_velocity = dist / (Mathf.Sin(2*fireAngle*Mathf.Deg2Rad)/ gravity);

        //float Vi = Mathf.Sqrt(dist * -Physics.gravity.y / (Mathf.Sin(Mathf.Deg2Rad * fireAngle * 2)));

        float Vy, Vz;

        //Vy = Vi * Mathf.Sin(Mathf.Deg2Rad * fireAngle);
        //Vz = Vi * Mathf.Cos(Mathf.Deg2Rad * _bullseye.position.z);

        Vz = Mathf.Sqrt(projectile_velocity) * Mathf.Cos(fireAngle * Mathf.Deg2Rad);
        Vy = Mathf.Sqrt(projectile_velocity) * Mathf.Sin(fireAngle * Mathf.Deg2Rad);

        //Calculate flight Time
        float flightDuration = dist / Vz;

        transform.rotation = Quaternion.LookRotation(target - transform.position);

        //Vector3 localVelocity = new Vector3(0f, Vy, Vz);

        //Vector3 globalVelocity = artillery.firingPoint.transform.TransformDirection(localVelocity);

        //Vector3 globalVelocity = transform.TransformVector(localVelocity);

        //GetComponent<Rigidbody>().velocity = globalVelocity*10f;

        //float elapse_time = 0f;

    }

    IEnumerator SimulateProjectile()
    {
        yield return new WaitForSeconds(0.15f);

        transform.position = transform.position + new Vector3(0, 0.0f, 0);

        Vector3 pos = artillery.firingPoint.position;
        Vector3 target = _bullseye.position;

        // Calculate Distance to target
        float dist = Vector3.Distance(pos, target);

        transform.LookAt(target);

        //Calculate the velocity needed to fire the object to the target
        float projectile_velocity = dist / (Mathf.Sin(2*fireAngle*Mathf.Deg2Rad)/ gravity);

        //float Vi = Mathf.Sqrt(dist * -Physics.gravity.y / (Mathf.Sin(Mathf.Deg2Rad * fireAngle * 2)));

        float Vy, Vz;

        //Vy = Vi * Mathf.Sin(Mathf.Deg2Rad * fireAngle);
        //Vz = Vi * Mathf.Cos(Mathf.Deg2Rad * _bullseye.position.z);

        Vz = Mathf.Sqrt(projectile_velocity) * Mathf.Cos(fireAngle * Mathf.Deg2Rad);
        Vy = Mathf.Sqrt(projectile_velocity) * Mathf.Sin(fireAngle * Mathf.Deg2Rad);

        //Calculate flight Time
        float flightDuration = dist / Vz;

        transform.rotation = Quaternion.LookRotation(target - transform.position);

        //Vector3 localVelocity = new Vector3(0f, Vy, Vz);

        //Vector3 globalVelocity = artillery.firingPoint.transform.TransformDirection(localVelocity);

        //Vector3 globalVelocity = transform.TransformVector(localVelocity);

        //GetComponent<Rigidbody>().velocity = globalVelocity*10f;

        float elapse_time = 0f;

        while (elapse_time < flightDuration)
        {
            transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vz * Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }
    }
}

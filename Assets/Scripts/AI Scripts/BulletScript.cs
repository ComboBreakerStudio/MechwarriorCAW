using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BulletScript : NetworkBehaviour
{

    public float timer;

    public LayerMask damageLayer;

    // Use this for initialization
    void Start () {
        StartCoroutine ("DestroyTimer", timer);
    }

    IEnumerator DestroyTimer(float t){
        yield return new WaitForSeconds (t);
        Destroy (this.gameObject);
    }

	void Update () 
    {
        transform.Translate(Vector3.forward * 5.0f * Time.deltaTime);
	}

    void onTriggerEnter(Collider hit)
    {
        if (hit.gameObject.layer == damageLayer)
        {
            Debug.Log("HIT");
            Destroy(hit.gameObject);
            Destroy(gameObject);
        }
    }

    void onTriggerEnter(Collision hit)
    {
        if (hit.gameObject.layer == damageLayer)
        {
            Debug.Log("HIT");
            Destroy(hit.gameObject);
            Destroy(gameObject);
        }
    }
}

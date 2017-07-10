using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BulletScript : NetworkBehaviour
{

    public float timer;

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
        transform.Translate(Vector3.forward * 100.0f * Time.deltaTime);



	}
}

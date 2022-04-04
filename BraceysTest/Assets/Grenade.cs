using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public float radius = 10f;
    public float force = 700f;
    float countdown;

    bool hasExploded = false;

    public GameObject explosionEffect;
    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0f && !hasExploded)
        {
            Explode();
        }
    }
    void Explode()
    {
        hasExploded = true;
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] coliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider nearbyObject in coliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }
        Destroy(gameObject);
    }
}

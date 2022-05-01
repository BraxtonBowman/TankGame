using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 40f;
    private float xRange = 30;
    private float zRange = 20;
    private Rigidbody projectileRb;
    public ParticleSystem impactParticle;

    // Start is called before the first frame update
    void Start()
    {
        projectileRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        projectileRb.AddForce(projectileRb.transform.forward * speed);
        destroyOutOfBounds();
    }

    private void destroyOutOfBounds()
    {
        if (transform.position.z < -zRange || transform.position.z > zRange)
        {
            Destroy(gameObject);
;        }
        else if (transform.position.x < -xRange || transform.position.x > xRange)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") == true) {
            Destroy(other.gameObject);
            impactParticle.Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] ParticleSystem explosion;
    [SerializeField] float blastRadius = 10f;
    [SerializeField] float explosionForce = 10f;
    [SerializeField] float Damage = 400f;

    // Start is called before the first frame update
    public void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        ExplosionDamageRadius();
    }

    void ExplosionDamageRadius()
    {
        Instantiate(explosion, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Debug.Log(nearbyObject.gameObject.name);

            TheHealth enemy = nearbyObject.GetComponent<TheHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(Damage);
            }


        }
    }
}

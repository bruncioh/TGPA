using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] float damage = 20f;
    [SerializeField] float damageOverTime = 2f;
    [SerializeField] float DOTtimer = 4f;
    [SerializeField] float currentTime;
    [SerializeField] bool isOnFire;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        if (collision.transform.tag == "Player")//or tag
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<TheHealth>().TakeDamage(damage);
            isOnFire = true;
        }

    }
}

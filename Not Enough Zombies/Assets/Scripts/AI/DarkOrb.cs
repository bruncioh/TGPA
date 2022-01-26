using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkOrb : MonoBehaviour
{
    [SerializeField] float damage = 50f;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        if (collision.transform.tag == "Player")//or tag
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<TheHealth>().TakeDamage(damage);
        }

    }
}

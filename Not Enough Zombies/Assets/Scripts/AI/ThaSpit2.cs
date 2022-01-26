using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThaSpit2 : MonoBehaviour
{
    [SerializeField] float damage = 10f;

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

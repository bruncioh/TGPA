using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkHand : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] Transform Enemy;


    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        if (collision.transform.tag == "Player")//or tag
        {
            Destroy(gameObject);
            Rigidbody pull = Player.GetComponent<Rigidbody>();
            pull.AddForce(Enemy.position - Player.position * 10, ForceMode.Acceleration);
        }

    }
}

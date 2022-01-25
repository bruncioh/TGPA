using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{

    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.position);
        transform.LookAt(player);
    }
}

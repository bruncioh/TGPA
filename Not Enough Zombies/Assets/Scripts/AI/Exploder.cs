using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Exploder : MonoBehaviour
{
    public Transform player;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] ParticleSystem explosion;


    public enum TheStates { chasing, attack }

    public float distanceThreshold = 10000f;
    public float attackDistance = 1f;
    public float damage = 70f;
    public float blastRadius = 10f;


    public TheStates aiState = TheStates.chasing;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(ChasePlayer());
    }

    private void Update()
    {
        ChasePlayer();
        
    }

    // Update is called once per frame

    IEnumerator ChasePlayer()
    {
        while (true)
        {
            switch (aiState)
            {
                case TheStates.chasing:
                    float dist = Vector3.Distance(player.position, transform.position);
                    agent.speed = 8;
                    if (dist < attackDistance)
                    {
                        aiState = TheStates.attack;
                        ExplodePlease();
                        Destroy(gameObject);
                    }
                    agent.SetDestination(player.position);
                    break;
                case TheStates.attack:
                    agent.SetDestination(transform.position);
                    dist = Vector3.Distance(player.position, transform.position);
                    if (dist > attackDistance)
                    {
                        aiState = TheStates.chasing;
                    }
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(1f);
        }

    }

    public void ExplodePlease()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);

        agent.SetDestination(transform.position);

        Collider[] collider = Physics.OverlapSphere(transform.position, blastRadius);
        foreach (Collider Object in collider)
        {
            TheHealth playerHealth = Object.GetComponent<TheHealth>();

            if(playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}

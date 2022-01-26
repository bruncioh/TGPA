using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicAI : BaseAIInherits
{
    [SerializeField] public float damage = 10f;
    [SerializeField] public float attackDistance = 0.5f;
    [SerializeField] public float distanceThreshold;

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

                    }
                    agent.SetDestination(player.position);
                    break;
                case TheStates.attack:
                    agent.SetDestination(transform.position);
                    dist = Vector3.Distance(player.position, transform.position);
                    if(dist > attackDistance)
                    {
                        aiState = TheStates.chasing;
                    }
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(0f);
        }

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "Player")//or tag
        {
            collision.gameObject.GetComponent<TheHealth>().TakeDamage(damage);
        }
    }

}

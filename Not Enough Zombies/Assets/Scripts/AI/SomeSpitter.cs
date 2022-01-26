using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SomeSpitter :BaseRangedEnemyInherits
{
    public float distanceThreshold;
    public float attackDistance = 20;
    [SerializeField] float attackTime = 5f;
    [SerializeField] bool isAttacking;
    [SerializeField] float projectileSpeed = 32f;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        inFireRange = Physics.CheckSphere(transform.position, attackDistance, player);
        if (!inFireRange) ChasePlayer();
        if (inFireRange) HeDoBeSpitting();
    }

    // Update is called once per frame

    void ChasePlayer()
    {
        agent.SetDestination(Player.position);
    }

    void HeDoBeSpitting()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(Player);

        

        if (!isAttacking)
        {
            isAttacking = true;
            Invoke(nameof(ResetSpit), attackTime);
            GameObject spit = Instantiate(Projectile, FirePoint.position, Quaternion.identity);
            spit.GetComponent<Rigidbody>().AddForce(FirePoint.up + FirePoint.forward * 32f, ForceMode.Impulse);
        }
    }

    void ResetSpit()
    {
        isAttacking = false;
    }


}

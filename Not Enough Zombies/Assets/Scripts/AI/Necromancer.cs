using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Necromancer : BaseWizardInherits
{
    [SerializeField] float ManaRegen = 0.75f;
    [SerializeField] float attackDistance = 20f;
    [SerializeField] float GrabDistance = 40f;
    [SerializeField] float PushBackDistance = 3f;
    [SerializeField] float PushBackRange = 10f;
    [SerializeField] bool isInGrabRange;
    [SerializeField] public float ManaCost;
    [SerializeField] public float MaxMana = 100f;
    [SerializeField] public float currentMana;


    // Start is called before the first frame update
    void Start()
    {
        currentMana = 10;
        manaRegenActive = true;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        inFireRange = Physics.CheckSphere(transform.position, attackDistance, player);
        isInGrabRange = Physics.CheckSphere(transform.position, GrabDistance, player);
        isInMeleeRange = Physics.CheckSphere(transform.position, PushBackRange, player);

        if (!inFireRange && !isCastingSpell && !isInGrabRange) ChasePlayer();

        if(inFireRange && currentMana >= 10 && !CoolDownBeginSpell1 && !isCastingSpell)
        {
            StartCoroutine(FireDarkOrb());
            StartCoroutine(CoolDownSpell1(2));
            isCastingSpell = true;
        }

        if(isInGrabRange && currentMana >= 30 && !CoolDownBeginSpell2 && !isCastingSpell && !inFireRange)
        {
            StartCoroutine(FireDarkHand());
            StartCoroutine(CoolDownSpell2(10));
            isCastingSpell = true;
        }

       if(!inFireRange && !isInGrabRange && currentMana >= 50 && !CoolDownBeginSpell4 && !isCastingSpell)
        {
            StartCoroutine(SummonThings());
            StartCoroutine(CoolDownSpell4(20));
            isCastingSpell = true;
        }

       if(isInMeleeRange && !isCastingSpell && currentMana >= 5 && !CoolDownBeginSpell3)
        {
            StartCoroutine(PushBackPlayer());
            StartCoroutine(CoolDownSpell3(5));
            isCastingSpell = true;
        }


        if (manaRegenActive == true)
        {
            currentMana += ManaRegen * Time.deltaTime;
            if(currentMana >= MaxMana)
            {
                manaRegenActive = false;
            }
        }
    }

    IEnumerator FireDarkOrb()
    {

        if(CoolDownBeginSpell1 == false)
        {
            agent.SetDestination(transform.position);

            transform.LookAt(Player);


                ManaCost = 10f;
                currentMana -= ManaCost;
                yield return new WaitForSeconds(2);
                GameObject Orb = Instantiate(SpellOne, FirePoint.position, Quaternion.identity);
                Orb.GetComponent<Rigidbody>().AddForce(FirePoint.up + FirePoint.forward * 24f, ForceMode.Impulse);
                isCastingSpell = false;

        }

    }

    IEnumerator FireDarkHand()
    {
        agent.SetDestination(transform.position);


        if(CoolDownBeginSpell2 == false)
        {
            transform.LookAt(Player);
            ManaCost = 30;
            currentMana -= ManaCost;
            yield return new WaitForSeconds(2);
            GameObject Hand = Instantiate(SpellTwo, FirePoint.position, Quaternion.identity);
            Hand.GetComponent<Rigidbody>().AddForce(FirePoint.forward * 24f, ForceMode.Impulse);
        }
        isCastingSpell = false;
    }

    IEnumerator PushBackPlayer()
    {
        agent.SetDestination(transform.position);

        if(CoolDownBeginSpell3 == false)
        {
            ManaCost = 5;
            currentMana -= ManaCost;
            yield return new WaitForSeconds(1);

            Collider[] collider = Physics.OverlapSphere(transform.position, PushBackRange);
            foreach (Collider Object in collider)
            {
                Rigidbody Push = Player.GetComponent<Rigidbody>();
                Push.AddExplosionForce(PushBackDistance, transform.position, PushBackRange, PushBackDistance, ForceMode.Impulse);
            }
            isCastingSpell = false;
        }
    }

    IEnumerator SummonThings()
    {
        if(CoolDownBeginSpell4 == false)
        {
            agent.SetDestination(transform.position);


                ManaCost = 50;
                currentMana -= ManaCost;

                yield return new WaitForSeconds(5);

        
                for(int i = 0; i < 10; i++)
                {
                  Instantiate(SpellFour, SummonPoint.position, Quaternion.identity);
                }
                isCastingSpell = false;
            

        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PushBackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, GrabDistance);
    }



    void ChasePlayer()
    {
        agent.SetDestination(Player.position);
    }

    IEnumerator CoolDownSpell1(float CoolDownTimer)
    {
        CoolDownBeginSpell1 = true;
        yield return new WaitForSeconds(CoolDownTimer);
        CoolDownBeginSpell1 = false;
    }
    IEnumerator CoolDownSpell2(float CoolDownTimer)
    {
        CoolDownBeginSpell2 = true;
        yield return new WaitForSeconds(CoolDownTimer);
        CoolDownBeginSpell2 = false;
    }

    IEnumerator CoolDownSpell3(float CoolDownTimer)
    {
        CoolDownBeginSpell3 = true;
        yield return new WaitForSeconds(CoolDownTimer);
        CoolDownBeginSpell3 = false;
    }

    IEnumerator CoolDownSpell4(float CoolDownTimer)
    {
        CoolDownBeginSpell4 = true;
        yield return new WaitForSeconds(CoolDownTimer);
        CoolDownBeginSpell4 = false;
    }
}


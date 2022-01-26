using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FireWizard : BaseWizardInherits
{
    [SerializeField] float ManaRegen = 0.75f;
    [SerializeField] float attackDistance = 20f;
    [SerializeField] float ManaCost;
    [SerializeField] float MaxMana = 100f;
    [SerializeField] float currentMana;
    [SerializeField] float PushBackDistance = 3f;
    [SerializeField] float PushBackRange = 10f;

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

        if (!inFireRange && !isCastingSpell) ChasePlayer();

        if (inFireRange && currentMana >= 10 && !CoolDownBeginSpell1 && !isCastingSpell)
        {
            StartCoroutine(FireBall());
            StartCoroutine(CoolDownSpell1(5));
            isCastingSpell = true;
        }

        if (isInMeleeRange && !isCastingSpell && currentMana >= 5 && !CoolDownBeginSpell3)
        {
            StartCoroutine(PushBackPlayer());
            StartCoroutine(CoolDownSpell3(5));
            isCastingSpell = true;
        }

        if (!inFireRange && !isCastingSpell && currentMana >= 60 && !CoolDownBeginSpell4)
        {
            StartCoroutine(DropMeteor());
            StartCoroutine(CoolDownSpell4(20));
            isCastingSpell = true;
        }


        if (manaRegenActive == true)
        {
            currentMana += ManaRegen * Time.deltaTime;
            if (currentMana >= MaxMana)
            {
                manaRegenActive = false;
            }
        }
    }

    IEnumerator FireBall()
    {

        if (CoolDownBeginSpell1 == false)
        {
            agent.SetDestination(transform.position);

            transform.LookAt(Player);


            ManaCost = 10f;
            currentMana -= ManaCost;
            yield return new WaitForSeconds(2);
            GameObject Ball = Instantiate(SpellOne, FirePoint.position, Quaternion.identity);
            Ball.GetComponent<Rigidbody>().AddForce(FirePoint.up + FirePoint.forward * 24f, ForceMode.Impulse);
            isCastingSpell = false;

        }

    }

    IEnumerator FireBeam()
    {
        agent.SetDestination(transform.position);


        if (CoolDownBeginSpell2 == false)
        {
            transform.LookAt(Player);
            ManaCost = 30;
            currentMana -= ManaCost;
            yield return new WaitForSeconds(2);
            GameObject Beam = Instantiate(SpellTwo, FirePoint.position, Quaternion.identity);
            Beam.GetComponent<Rigidbody>().AddForce(FirePoint.forward * 24f, ForceMode.Impulse);
        }
        isCastingSpell = false;
    }


    IEnumerator PushBackPlayer()
    {
        agent.SetDestination(transform.position);

        if (CoolDownBeginSpell3 == false)
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

    IEnumerator DropMeteor()
    {
        agent.SetDestination(transform.position);

        if(CoolDownBeginSpell4 == false)
        {
            transform.LookAt(transform);
            ManaCost = 50;
            currentMana -= ManaCost;
            yield return new WaitForSeconds(2);

            GameObject Rock = Instantiate(SpellFour, DropPoint.position, Quaternion.identity);
            Rock.GetComponent<Rigidbody>().AddForce(DropPoint.forward * 24, ForceMode.Impulse);
        }
        isCastingSpell = false;
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

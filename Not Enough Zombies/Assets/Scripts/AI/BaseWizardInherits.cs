using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseWizardInherits : MonoBehaviour
{
    public Transform Player;
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public LayerMask player;
    [SerializeField] public Transform FirePoint;
    [SerializeField] public Transform DropPoint;

    [SerializeField] public Transform SummonPoint;
    [SerializeField] public GameObject SpellOne;
    [SerializeField] public GameObject SpellTwo;
    [SerializeField] public GameObject SpellThree;
    [SerializeField] public GameObject SpellFour;

    [SerializeField] public bool inFireRange;
    [SerializeField] public bool isCastingSpell;
    [SerializeField] public bool CoolDownBeginSpell1;
    [SerializeField] public bool CoolDownBeginSpell2;
    [SerializeField] public bool CoolDownBeginSpell3;
    [SerializeField] public bool CoolDownBeginSpell4;
    [SerializeField] public bool manaRegenActive;
    [SerializeField] public bool isInMeleeRange;
}

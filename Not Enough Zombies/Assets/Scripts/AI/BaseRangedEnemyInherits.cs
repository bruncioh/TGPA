using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseRangedEnemyInherits : MonoBehaviour
{
    public Transform Player;
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public LayerMask player;
    [SerializeField] public GameObject Projectile;
    [SerializeField] public Transform FirePoint;
    [SerializeField] public bool inFireRange;
}

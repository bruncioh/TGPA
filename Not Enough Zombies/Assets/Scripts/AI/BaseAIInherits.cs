using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseAIInherits : MonoBehaviour
{
    public Transform player;
    [SerializeField] public NavMeshAgent agent;

    public enum TheStates { chasing, attack }

    public TheStates aiState = TheStates.chasing;

}

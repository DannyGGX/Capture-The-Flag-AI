using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Opponent : MonoBehaviour
{
    private NavMeshAgent agent;
    private StateMachine stateMachine;
    
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        stateMachine = new StateMachine();
    }
    

    void Update()
    {
        stateMachine.Update();
    }

    void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

}

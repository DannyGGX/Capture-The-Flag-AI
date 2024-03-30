using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Vision))]
[RequireComponent(typeof(Rigidbody))]
public class Opponent : Unit
{
    private NavMeshAgent agent;
    private StateMachine stateMachine;
    private Rigidbody _rigidbody;
    [SerializeField] private Vision playerDetector;
    
    public Transform PlayerTransform;
    public Transform RedBaseTransform;
    public Transform BlueBaseTransform;
    
    [SerializeField] float rescueFlagDistance = 5f;
    
    
    private void Awake()
    {
        BaseAwake();
        agent = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponent<Rigidbody>();
        
        stateMachine = new StateMachine();
        
        //Declare states
        var chasePlayerState = new ChasePlayerState(agent, this);
        var attackPlayerState = new OpponentLanceBuildUpState(agent, this);
        var getPowerUpState = new GetPowerUpState(agent, this);
        var rescueBlueFlagState = new RescueBlueFlagState(agent, this);
        var fetchRedFlagState = new FetchRedFlagState(agent, this);
        var carryRedFlagState = new CarryRedFlagState(agent, this);
        
        // Define transitions
        AnyTransition(chasePlayerState, new FuncCondition(playerDetector.CanDetectPlayer));
        SpecificTransition(chasePlayerState, attackPlayerState, new FuncCondition(playerDetector.CanAttackPlayer));
        AnyTransition(rescueBlueFlagState, new FuncCondition(DetermineIfRescueBlueFlag));
        AnyTransition(carryRedFlagState, new FuncCondition(DetermineIfCarryRedFlag)); // move to red base
        AnyTransition(fetchRedFlagState, new FuncCondition(() => true)); // move to blue base
        
        // Set initial state
        stateMachine.SetInitialState(fetchRedFlagState);
    }
    
    private void SpecificTransition(IState from, IState to, ICondition condition)
    {
        stateMachine.AddSpecificTransition(from, to, condition);
    }
    private void AnyTransition(IState to, ICondition condition)
    {
        stateMachine.AddAnyTransition(to, condition);
    }
    
    private bool DetermineIfRescueBlueFlag()
    {
        return Vector3.Distance(transform.position, FlagsTracker.Instance.BlueFlagCurrentPos.position) < rescueFlagDistance;
    }
    private bool DetermineIfCarryRedFlag()
    {
        return FlagsTracker.Instance.RedFlag.IsCarried;
    }
    // private bool DetermineIfFetchRedFlag()
    // {
    //     
    // }

    void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }
    
    void Update()
    {
        stateMachine.Update();
    }

    private void OnCollisionEnter(Collision other)
    {
        BaseOnEnterCollision(other);
    }

    protected override void Die()
    {
        base.Die();
        agent.enabled = false;
        _rigidbody.useGravity = false;
        enabled = false; // so that the state machine FixedUpdate doesn't run and call the NavMesh SetDestination
    }

    protected override void Respawn()
    {
        base.Respawn();
        _rigidbody.useGravity = true;
        agent.enabled = true;
        enabled = true;
    }
}

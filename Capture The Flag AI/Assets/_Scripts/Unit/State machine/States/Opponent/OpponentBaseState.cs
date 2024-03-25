using UnityEngine;
using UnityEngine.AI;

public abstract class OpponentBaseState : BaseState
{
    protected Opponent opponent { get; }
    protected NavMeshAgent navMeshAgent { get; }
    
    /// <summary>
    /// NavMeshAgent target
    /// </summary>
    protected Transform Target { get; set; }

    protected OpponentBaseState(NavMeshAgent navMeshAgent, Opponent opponent)
    {
        this.navMeshAgent = navMeshAgent;
        this.opponent = opponent;
    }
}

public class ChasePlayerState : OpponentBaseState
{
    public ChasePlayerState(NavMeshAgent navMeshAgent, Opponent opponent) : base(navMeshAgent, opponent)
    {
    }

    public override void OnEnter()
    {
        Debug.Log("ChasePlayerState");
        Target = opponent.PlayerTransform;
    }

    public override void FixedUpdate()
    {
        navMeshAgent.SetDestination(Target.position);
    }
}

public class AttackPlayerState : OpponentBaseState
{
    public AttackPlayerState(NavMeshAgent navMeshAgent, Opponent opponent) : base(navMeshAgent, opponent)
    {
    }
    
    public override void OnEnter()
    {
        Debug.Log("AttackPlayerState");
        Target = opponent.PlayerTransform;
    }
    
    public override void FixedUpdate()
    {
        navMeshAgent.SetDestination(Target.position);
        opponent.Attack();
    }
}

public class GetPowerUpState : OpponentBaseState
{
    public GetPowerUpState(NavMeshAgent navMeshAgent, Opponent opponent) : base(navMeshAgent, opponent)
    {
    }
    
    // pass in transform position from vision script
    
}

public class FetchRedFlagState : OpponentBaseState
{
    public FetchRedFlagState(NavMeshAgent navMeshAgent, Opponent opponent) : base(navMeshAgent, opponent)
    {
    }

    public override void OnEnter()
    {
        Debug.Log("FetchRedFlagState");
        Target = FlagsTracker.Instance.RedFlagCurrentPos;
    }

    public override void FixedUpdate()
    {
        navMeshAgent.SetDestination(Target.position);
    }
}

public class RescueBlueFlagState : OpponentBaseState
{
    public RescueBlueFlagState(NavMeshAgent navMeshAgent, Opponent opponent) : base(navMeshAgent, opponent)
    {
    }
    
    public override void OnEnter()
    {
        Debug.Log("RescueBlueFlagState");
        Target = FlagsTracker.Instance.BlueFlagCurrentPos;
    }
    public override void FixedUpdate()
    {
        navMeshAgent.SetDestination(Target.position);
    }
}

public class CarryRedFlagState : OpponentBaseState
{
    public CarryRedFlagState(NavMeshAgent navMeshAgent, Opponent opponent) : base(navMeshAgent, opponent)
    {
    }
    
    public override void OnEnter()
    {
        Debug.Log("CarryRedFlagState");
        Target = opponent.RedBaseTransform;
        navMeshAgent.SetDestination(Target.position);
    }
}


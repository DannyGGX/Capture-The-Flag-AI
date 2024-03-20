using UnityEngine;
using UnityEngine.AI;

public abstract class OpponentBaseState : BaseState
{
    protected NavMeshAgent NavMeshAgent { get; set; }
    
    /// <summary>
    /// NavMeshAgent target
    /// </summary>
    public Transform Target { get; set; }

    protected OpponentBaseState(NavMeshAgent navMeshAgent)
    {
        NavMeshAgent = navMeshAgent;
    }
}

public class ChasePlayerState : OpponentBaseState
{
    public ChasePlayerState(NavMeshAgent navMeshAgent) : base(navMeshAgent)
    {
    }
    
    public override void FixedUpdate()
    {
        NavMeshAgent.SetDestination(Target.position);
    }
}

public class AttackPlayerState : OpponentBaseState
{
    public AttackPlayerState(NavMeshAgent navMeshAgent) : base(navMeshAgent)
    {
    }
    
    public override void FixedUpdate()
    {
        NavMeshAgent.SetDestination(Target.position);
    }
}

public class GetPowerUpState : OpponentBaseState
{
    public GetPowerUpState(NavMeshAgent navMeshAgent) : base(navMeshAgent)
    {
    }
}

public class RetrieveOwnFlagState : OpponentBaseState
{
    public RetrieveOwnFlagState(NavMeshAgent navMeshAgent) : base(navMeshAgent)
    {
    }
}

public class RescueEnemyFlagState : OpponentBaseState
{
    public RescueEnemyFlagState(NavMeshAgent navMeshAgent) : base(navMeshAgent)
    {
    }
}


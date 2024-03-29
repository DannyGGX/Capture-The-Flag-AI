using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Lance : MonoBehaviour
{
    protected int currentLanceCount;
    [HideInInspector] public float currentBuildUpAmount = 0;
    [HideInInspector] public float currentCooldownTime = 0;
    [HideInInspector] public float currentAttackDuration = 0;
    [SerializeField] protected LanceStatsSO stats;
    [SerializeField] protected Unit unit;
    [SerializeField] protected MeshRenderer meshRenderer;
    private StateMachine stateMachine;
    public LanceState CurrentLanceState { get; set; } // To indicate to other classes what the current state is
    public bool BuildUpInput { get; set; } = false;

    private void Awake()
    {
        stateMachine = new StateMachine();
        
        // Declare states
        var buildUpState = new LanceBuildUpState(this, stats, meshRenderer);
        var attackState = new LanceAttackState(this, stats, meshRenderer);
        var cooldownState = new LanceCooldownState(this, stats, meshRenderer);
        var readyState = new LanceReadyState(this, stats, meshRenderer);
        
        // Define transitions
        SpecificTransition(readyState, buildUpState, new FuncCondition(() => BuildUpInput));
        SpecificTransition(buildUpState, readyState, new FuncCondition(() => BuildUpInput == false));
        SpecificTransition(buildUpState, attackState, new FuncCondition(DetermineIfAttack));
        SpecificTransition(attackState, cooldownState, new FuncCondition(DetermineIfCooldown));
        SpecificTransition(cooldownState, readyState, new FuncCondition(DetermineIfReady));
        
        // Set initial state
        stateMachine.SetInitialState(readyState);

        currentLanceCount = stats.InitialLanceCount;
        
    }
    private void SpecificTransition(IState from, IState to, ICondition condition)
    {
        stateMachine.AddSpecificTransition(from, to, condition);
    }
    
    protected bool DetermineIfAttack()
    {
        return currentBuildUpAmount >= stats.MaxBuildUpAmount;
    }
    protected bool DetermineIfCooldown()
    {
        return currentAttackDuration >= stats.AttackDuration;
    }
    protected bool DetermineIfReady()
    {
        return currentCooldownTime > 0;
    }


    public void PickUpPowerUp()
    {
        currentLanceCount++;
    }

    

    public void ChangeProtection(bool isProtected)
    {
        unit.IsProtected = isProtected;
    }
    
    private void Update()
    {
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }
}

public enum LanceState
{
    BuildUp,
    Attack,
    Cooldown,
    Ready
}

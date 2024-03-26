using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class LanceBaseState : BaseState
{
    protected Lance lance { get; }
    protected LanceStatsSO stats { get; }
    protected MeshRenderer meshRenderer { get; }

    public LanceBaseState(Lance lance, LanceStatsSO stats, MeshRenderer meshRenderer)
    {
        this.lance = lance;
        this.stats = stats;
        this.meshRenderer = meshRenderer;
    }
}

public class LanceReadyState : LanceBaseState
{
    public LanceReadyState(Lance lance, LanceStatsSO stats, MeshRenderer meshRenderer) : base(lance, stats, meshRenderer)
    {
        
    }

    public override void OnEnter()
    {
        lance.CurrentLanceState = LanceState.Ready;
    }

    public override void Update()
    {
        if (lance.currentBuildUpAmount > 0)
        {
            lance.currentBuildUpAmount -= Time.deltaTime * stats.BuildUpDecreaseRate;
            meshRenderer.material.color = Color.Lerp(stats.RegularColor, stats.BuildUpColor, lance.currentBuildUpAmount / stats.MaxBuildUpAmount);
        }
        else
        {
            lance.currentBuildUpAmount = 0;
            lance.ChangeProtection(false);
            meshRenderer.material.color = stats.RegularColor;
        }
    }
}

public class LanceBuildUpState : LanceBaseState
{
    public LanceBuildUpState(Lance lance, LanceStatsSO stats, MeshRenderer meshRenderer) : base(lance, stats, meshRenderer)
    {
        
    }
    public override void OnEnter()
    {
        lance.CurrentLanceState = LanceState.BuildUp;
        lance.ChangeProtection(true);
    }

    public override void Update()
    {
        // if (lance.currentBuildUpAmount < stats.MaxBuildUpAmount) // This is always going to be true in this state
        // {
        // }
        lance.currentBuildUpAmount += Time.deltaTime * stats.BuildUpIncreaseRate;
        meshRenderer.material.color = Color.Lerp(stats.RegularColor, stats.BuildUpColor, lance.currentBuildUpAmount / stats.MaxBuildUpAmount);
    }
}

public class LanceAttackState : LanceBaseState
{
    public LanceAttackState(Lance lance, LanceStatsSO stats, MeshRenderer meshRenderer) : base(lance, stats, meshRenderer)
    {
        
    }
    public override void OnEnter()
    {
        lance.CurrentLanceState = LanceState.Attack;
        meshRenderer.material.color = stats.BuildUpColor;
    }
    public override void Update()
    {
        lance.currentAttackDuration += Time.deltaTime;
    }

    public override void OnExit()
    {
        lance.currentAttackDuration = 0;
    }
}

public class LanceCooldownState : LanceBaseState
{
    public LanceCooldownState(Lance lance, LanceStatsSO stats, MeshRenderer meshRenderer) : base(lance, stats, meshRenderer)
    {
        
    }
    public override void OnEnter()
    {
        lance.CurrentLanceState = LanceState.Cooldown;
        meshRenderer.material.color = stats.CooldownColor;
    }
    
    public override void Update()
    {
        lance.currentCooldownTime -= Time.deltaTime;
    }
    public override void OnExit()
    {
        lance.currentCooldownTime = stats.CooldownTime;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(FlagBearer))]
[RequireComponent(typeof(Lance))]
[RequireComponent(typeof(Collider))]
public abstract class Unit : MonoBehaviour
{
    [field: SerializeField] public TeamEnum Team { get; set; }
    [SerializeField] private Spawner spawner;
    [SerializeField] protected float respawnTime = 3f;
    [Space]
    [SerializeField] protected Lance lance;

    [HideInInspector] public bool IsProtected = false;
    [Space]
    MeshRenderer meshRenderer;

    private Collider _collider;
    [SerializeField] protected Material normalMaterial;
    [SerializeField] protected Material deathMaterial;
    public FlagBearer flagBearer;

    protected void BaseAwake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
        spawner.SpawnAtRandomPoint(this);
    }

    protected void BaseOnEnterCollision(Collision other)
    {
        if (other.collider.TryGetComponent<Unit>(out var otherUnit))
        {
            // if (lance.CurrentLanceState == LanceState.Attack)
            // {
            //     otherUnit.TakeDamage();
            // }
            otherUnit.TakeDamage();
            TakeDamage(); // adding this in because only the player ends up dying 
        }

        // if (other.collider.TryGetComponent<Base>(out var flagBase))
        // {
        //     if (flagBase.Team == Team && flagBearer.IsHoldingFlag)
        //     {
        //         this.Log("Flag Captured");
        //         //flagBearer.DropFlag(); doesn't need to do this
        //         GameManager.Instance.FlagCaptured(Team);
        //     }
        // }
    }

    public void TakeDamage()
    {
        if (!IsProtected)
        {
            Die();
        }
    }
    
    protected virtual void Die()
    {
        if (flagBearer.IsHoldingFlag)
        {
            flagBearer.DropFlag();
        }
        meshRenderer.material = deathMaterial;
        _collider.enabled = false;
        Invoke(nameof(Respawn), respawnTime);
    }

    protected virtual void Respawn()
    {
        spawner.SpawnAtRandomPoint(this);
        meshRenderer.material = normalMaterial;
        _collider.enabled = true;
        IsProtected = false;
    }
    
    public virtual void Attack()
    {
        
    }
    
    public virtual bool CanAttack()
    {
        return true;
    }
    
    public void PickUpPowerUp()
    {
        lance.PickUpPowerUp();
    }
    
    public virtual void PickUpFlag()
    {
        flagBearer.PickUpOwnFlag();
    }
    
    
}

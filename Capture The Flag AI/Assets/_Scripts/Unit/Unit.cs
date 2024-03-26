using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(FlagBearer))]
[RequireComponent(typeof(Lance))]
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
    [SerializeField] protected Material normalMaterial;
    [SerializeField] protected Material deathMaterial;
    [SerializeField] protected FlagBearer flagBearer;

    protected void BaseAwake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        spawner?.SpawnAtRandomPoint(this);
    }

    protected void BaseOnEnterCollision(Collision other)
    {
        if (other.collider.TryGetComponent<Unit>(out var unit))
        {
            if (lance.CurrentLanceState == LanceState.Attack)
            {
                unit.TakeDamage();
            }
        }

        if (other.collider.TryGetComponent<Base>(out var flagBase))
        {
            if (flagBase.Team != Team && flagBearer.IsHoldingFlag)
            {
                //flagBearer.DropFlag(); doesn't need to do this
                GameManager.Instance.FlagCaptured(Team);
            }
        }
    }

    private void TakeDamage()
    {
        if (!IsProtected)
        {
            Die();
        }
    }
    
    protected virtual void Die()
    {
        flagBearer.DropFlag();
        meshRenderer.material = deathMaterial;
        Invoke(nameof(Respawn), respawnTime);
    }

    protected virtual void Respawn()
    {
        spawner.SpawnAtRandomPoint(this);
        meshRenderer.material = normalMaterial;
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

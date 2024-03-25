using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public abstract class Unit : MonoBehaviour
{
    [field: SerializeField] public TeamEnum Team { get; set; }
    [SerializeField] private Spawner spawner;
    [SerializeField] protected float respawnTime = 3f;
    [Space]
    [SerializeField] protected Lance lance;
    [Space]
    protected MeshRenderer meshRenderer;
    [SerializeField] protected Material normalMaterial;
    [SerializeField] protected Material deathMaterial;
    [SerializeField] protected FlagBearer flagBearer;

    protected void BaseAwake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        spawner?.SpawnAtRandomPoint(this);
    }

    protected virtual void Die()
    {
        Respawn();
        flagBearer.DropFlag();
        meshRenderer.material = deathMaterial;
        Invoke(nameof(Respawn), respawnTime);
    }

    protected virtual void Respawn()
    {
        spawner.SpawnAtRandomPoint(this);
        meshRenderer.material = normalMaterial;
    }


    public void PickUpPowerUp()
    {
        lance.PickUpPowerUp();
    }
    
    public virtual void PickUpFlag()
    {
        flagBearer.PickUpOwnFlag();
    }

    protected void BaseCollision(Collision other)
    {
        
    }

    public virtual void Attack()
    {
        
    }
    
    public virtual bool CanAttack()
    {
        return true;
    }
}

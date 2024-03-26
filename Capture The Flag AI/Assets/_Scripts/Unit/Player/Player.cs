using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    [SerializeField] private RespawnCountDownUI respawnCountDownUI;
    [SerializeField] private FlagInteractUI flagInteractUI;
    private Collider _collider;

    private void Awake()
    {
        BaseAwake();
        _collider = GetComponent<Collider>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //BaseOnEnterCollision(hit);
        if (hit.collider.TryGetComponent<Unit>(out var unit))
        {
            if (lance.CurrentLanceState == LanceState.Attack)
            {
                unit.TakeDamage();
            }
        }

        if (hit.collider.TryGetComponent<Base>(out var flagBase))
        {
            if (flagBase.Team == Team && flagBearer.IsHoldingFlag)
            {
                this.Log("Flag Captured");
                //flagBearer.DropFlag(); doesn't need to do this
                GameManager.Instance.FlagCaptured(Team);
            }
        }
        // end of base collision
        if (hit.gameObject.CompareTag("PlayerFlag"))
        {
            flagInteractUI.ShowPickUpFlagPrompt();
        }
        else if (hit.gameObject.CompareTag("OpponentFlag"))
        {
            flagInteractUI.ShowReturnFlagPrompt();
        }
    }

    

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("PlayerFlag"))
        {
            flagInteractUI.Hide();
        }
        else if (other.gameObject.CompareTag("OpponentFlag"))
        {
            flagInteractUI.Hide();
        }
    }
    

    protected override void Die()
    {
        _collider.enabled = false;
        respawnCountDownUI.RespawnCountDown((int)respawnTime);
        base.Die();
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerMovement))]
public class Player : Unit
{
    [SerializeField] private RespawnCountDownUI respawnCountDownUI;
    [SerializeField] private FlagInteractUI flagInteractUI;

    private CharacterController _characterController;
    PlayerMovement playerMovement;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        playerMovement = GetComponent<PlayerMovement>();
        BaseAwake();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //BaseOnEnterCollision(hit);
        if (hit.collider.TryGetComponent<Unit>(out var otherUnit))
        {
            // if (lance.CurrentLanceState == LanceState.Attack)
            // {
            //     unit.TakeDamage();
            // }
            otherUnit.TakeDamage();
        }

        if (hit.collider.TryGetComponent<Base>(out var flagBase))
        {
            if (flagBase.Team == Team && flagBearer.IsHoldingFlag)
            {
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
        respawnCountDownUI.RespawnCountDown((int)respawnTime);
        _characterController.enabled = false;
        playerMovement.enabled = false;
        base.Die();
    }

    protected override void Respawn()
    {
        base.Respawn();
        playerMovement.enabled = true;
        _characterController.enabled = true;
    }
}

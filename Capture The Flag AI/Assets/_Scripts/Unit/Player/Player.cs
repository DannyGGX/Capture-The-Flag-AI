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

    private void OnCollisionEnter(Collision other)
    {
        BaseOnEnterCollision(other);
        if (other.gameObject.CompareTag("PlayerFlag"))
        {
            flagInteractUI.ShowPickUpFlagPrompt();
        }
        else if (other.gameObject.CompareTag("OpponentFlag"))
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

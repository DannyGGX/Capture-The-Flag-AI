using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    [SerializeField] private RespawnCountDownUI respawnCountDownUI;
    
    
    protected override void Die()
    {
        respawnCountDownUI.RespawnCountDown((int)respawnTime);
        base.Die();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("OpponentFlag"))
        {
            
        }
        else if (other.gameObject.CompareTag("PlayerFlag"))
        {
            
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("OpponentFlag"))
        {
            
        }
        else if (other.gameObject.CompareTag("PlayerFlag"))
        {
            
        }
    }
}

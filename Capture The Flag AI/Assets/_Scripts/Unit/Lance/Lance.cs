using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Lance : MonoBehaviour
{
    private int lanceCount;
    [SerializeField] private LanceUI lanceUI;
    public float currentBuildUpAmount = 0;
    public float maxBuildUpAmount = 1; // in seconds
    

    public void PickUpPowerUp()
    {
        lanceCount++;
        lanceUI.SetLanceCount(lanceCount);
    }

    public enum AttackState
    {
        BuildUp,
        Dashing,
        Cooldown,
        Ready
    }
    
    public void IncreaseBuildUpAmount(float amount)
    {
        currentBuildUpAmount += amount;
        if (currentBuildUpAmount > maxBuildUpAmount)
        {
            currentBuildUpAmount = maxBuildUpAmount;
        }
    }

    private void ChangeMeshColor(MeshRenderer meshRenderer)
    {
        
    }
    
    private void TurnInvincible()
    {
        
    }
    
    private void TurnVulnerable()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Lance Stats SO", menuName = "Scriptable Object/Lance Stats")]
public class LanceStatsSO : ScriptableObject
{
    [Header("Build Up")]
    public float MaxBuildUpAmount = 100;
    public float BuildUpIncreaseRate = 100;
    public float BuildUpDecreaseRate = 50;
    [Space]
    [Header("Attack")]
    public float MovementSpeedMultiplier = 2;
    public float TurnSpeedMultiplier = 0;
    public float AttackRange = 10;
    public float AttackDuration = 1f;
    [Space]
    [Header("Cooldown")]
    public float CooldownTime = 3;
    [Space]
    public Color RegularColor;
    [Tooltip("Color during attack state")] public Color BuildUpColor;
    public Color CooldownColor;
    [Space] 
    public int InitialLanceCount = 5;

}

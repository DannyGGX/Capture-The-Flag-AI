using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Lance Stats SO", menuName = "Scriptable Object/Lance Stats")]
public class LanceStatsSO : ScriptableObject
{
    public float MovementSpeedMultiplier = 2;
    public float CooldownTime = 1.5f;
    public float turnSpeedMultiplier = 0.1f;
    public float velocity;
}

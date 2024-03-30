using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AI Agent Movement Stats", menuName = "Scriptable Object/AI Agent Movement Stats")]
public class AiAgentMovementStatsSO : ScriptableObject
{
    public float Speed = 6;
    public float AngularSpeed = 160;
    public float Acceleration = 8;
    public float PlayerDetectionSpeed = 8;
    public float AttackSpeed = 10;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatsSO", menuName = "Scriptable Object/Player Stats")]
public class PlayerStatsSO : ScriptableObject
{
    [Header("Horizontal Movement")]
    public float WalkSpeed = 10;
    public float SprintSpeed = 15;
    public float InputSmoothTime = 0.05f;
    [Space]
    [Header("Jump")] 
    public float JumpHeight = 15;
    public float CoyoteTime = 0.01f;
    public float GravityMagnitude = 7;
    [Space] 
    public float MouseSensitivity;
}

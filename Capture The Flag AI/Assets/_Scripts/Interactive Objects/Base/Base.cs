using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public TeamEnum Team;

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.TryGetComponent<Unit>(out var unit))
        {
            if (unit.Team == Team && unit.flagBearer.IsHoldingFlag)
            {
                GameManager.Instance.FlagCaptured(Team);
            }
        }
    }
}

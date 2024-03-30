using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    private System.Random _random;

    public void SpawnAtRandomPoint(Unit unit)
    {
        _random = new Random();
        this.Log(unit.ToString());
        unit.transform.position = spawnPoints[_random.Next(0, spawnPoints.Length)].position;
    }
}

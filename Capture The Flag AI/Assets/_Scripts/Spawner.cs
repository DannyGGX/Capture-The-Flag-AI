using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnDelay = 1f;
    private System.Random _random;

    private void Awake()
    {
        _random = new Random();
    }

    public void SpawnAtRandomPoint(Unit unit)
    {
        unit.transform.position = spawnPoints[_random.Next(0, spawnPoints.Length)].position;
    }
}

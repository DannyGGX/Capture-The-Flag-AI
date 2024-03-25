using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(MeshRenderer))]
public class Flag : MonoBehaviour
{
    [SerializeField] private TeamEnum team;
    private Vector3 spawnPoint;
    private MeshRenderer _meshRenderer;
    private Collider _collider;

    [HideInInspector] public bool IsCarried = false;

    private Unit unit;

    private void Awake()
    {
        spawnPoint = transform.position;
        _meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        unit = other.gameObject.GetComponent<Unit>();
        if (unit == null) return;
        Debug.Log("flag trigger entered");

        if (unit.Team == team)
        {
            Debug.Log("Pick up flag");
            PickUpOwnColourFlag();
        }
        else
        {
            ReturnFlagToSpawner();
        }
        FlagsTracker.Instance.FlagIsPickedUp(team);
    }

    private void PickUpOwnColourFlag()
    {
        unit.PickUpFlag();
        unit = null;
        IsCarried = true;

        _meshRenderer.enabled = false;
        _collider.enabled = false;
    }
    
    private void ReturnFlagToSpawner()
    {
        transform.position = spawnPoint;
    }

    public void DropFlag(Vector3 dropPosition)
    {
        _meshRenderer.enabled = true;
        _collider.enabled = true;
        transform.position = dropPosition;
        IsCarried = false;
        FlagsTracker.Instance.FlagIsDropped(team, transform);
    }
}

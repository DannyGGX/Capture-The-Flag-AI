using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagBearer : MonoBehaviour
{
    [SerializeField] private GameObject heldFlag;
    [HideInInspector] public bool IsHoldingFlag = false;
    [SerializeField] private Flag ownFlag;
    
    private void Awake()
    {
        heldFlag.SetActive(false);
    }

    public void DropFlag()
    {
        heldFlag.SetActive(false);
        IsHoldingFlag = false;
        ownFlag.DropFlag(transform.position);
    }
    public void PickUpOwnFlag()
    {
        heldFlag.SetActive(true);
        IsHoldingFlag = true;
    }
}

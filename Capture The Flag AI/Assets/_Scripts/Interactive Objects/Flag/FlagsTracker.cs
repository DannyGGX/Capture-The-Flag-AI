using System;
using UnityEngine;

public class FlagsTracker : MonoBehaviour
{
    public static FlagsTracker Instance { get; private set; }
    
    // track flags positions when they aren't being carried
    public Flag BlueFlag;
    public Flag RedFlag;

    // track flag positions when they are carried
    [SerializeField] private Transform BlueFlagBearer;
    [SerializeField] private Transform RedFlagBearer;

    // These are the actual flag positions that this script is tracking
    public Transform BlueFlagCurrentPos;
    public Transform RedFlagCurrentPos;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    
    public void FlagIsDropped(TeamEnum whichFlag, Transform flagPos)
    {
        if (whichFlag == TeamEnum.Blue)
        {
            BlueFlagCurrentPos = flagPos;
        }
        else
        {
            RedFlagCurrentPos = flagPos;
        }
    }

    public void FlagIsPickedUp(TeamEnum whichFlag)
    {
        if (whichFlag == TeamEnum.Blue)
        {
            BlueFlagCurrentPos = BlueFlagBearer;
        }
        else
        {
            RedFlagCurrentPos = RedFlagBearer;
        }
    }

    public bool IsBlueFlagAtRedBase()
    {
        return BlueFlag.AtSpawnPoint;
    }
}
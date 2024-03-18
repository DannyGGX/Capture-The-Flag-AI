using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private float xMouseInput;
    private float yMouseInput;

    public float horizontalRotation { get; private set; }
    public float verticalRotation { get; private set; }
    
    private Camera mainCamera;

    [SerializeField] private PlayerStatsSO playerStats;
    [SerializeField] private float upDownRange = 90;
    
    private void Awake()
    {
        mainCamera = Camera.main; // Cache camera so that the main camera only needs to be found once.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        SetMouseInput();
    }

    private void SetMouseInput()
    {
        xMouseInput = Input.GetAxisRaw("Mouse X");
        yMouseInput = Input.GetAxisRaw("Mouse Y");
    }

    private void FixedUpdate()
    {
        ApplyHorizontalRotation();
        //ApplyVerticalRotation();
    }
    private void ApplyHorizontalRotation()
    {
        
        horizontalRotation = xMouseInput * playerStats.MouseSensitivity;
        transform.Rotate(0, horizontalRotation, 0);
    }
    private void ApplyVerticalRotation()
    {
        verticalRotation -= yMouseInput * playerStats.MouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }
    
    
}

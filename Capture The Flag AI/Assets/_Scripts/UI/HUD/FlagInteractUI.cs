using System;
using TMPro;
using UnityEngine;

public class FlagInteractUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void Awake()
    {
        Hide();
    }

    public void ShowPickUpFlagPrompt()
    {
        text.text = "[E] Pick up flag";
        
    }
    public void ShowReturnFlagPrompt()
    {
        text.text = "[E] Return flag";
    }
    
    public void Hide()
    {
        text.text = "";
    }
}
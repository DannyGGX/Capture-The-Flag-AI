using TMPro;
using UnityEngine;

public class FlagInteractUI
{
    [SerializeField] private TextMeshProUGUI text;

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
using UnityEngine;
using TMPro;

public class LanceUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void SetLanceCount(int count)
    {
        text.text = count.ToString();
    }
}
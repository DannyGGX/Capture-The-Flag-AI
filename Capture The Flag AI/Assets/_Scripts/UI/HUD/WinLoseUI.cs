using TMPro;
using UnityEngine;

public class WinLoseUI : MonoBehaviour
{
    [SerializeField, Tooltip("Includes restart and main menu buttons and text")] private GameObject winLoseUI;
    [SerializeField] private TextMeshProUGUI winLoseText;
    [SerializeField] private string winText = "You win!";
    [SerializeField] private string loseText = "Opponent wins!";
    private void Awake()
    {
        winLoseUI.SetActive(false);
    }
    public void WinLose(bool win)
    {
        winLoseUI.SetActive(true);
        winLoseText.text = win ? winText : loseText;
    }
    
    public void Restart()
    {
        winLoseUI.SetActive(false);
        SceneManagerScript.Instance.ReloadScene();
        GameManager.Instance.EndMatch();
    }
    public void GoToMainMenu()
    {
        winLoseUI.SetActive(false);
        SceneManagerScript.Instance.LoadScene(Scenes.MainMenu);
        GameManager.Instance.EndMatch();
    }
}
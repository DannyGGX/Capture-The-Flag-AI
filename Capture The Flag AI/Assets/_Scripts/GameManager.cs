using UnityEngine;

// Used for ending the round and ending the match
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }
    
    [SerializeField] private WinLoseUI winLoseUI;

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
    
    public void FlagCaptured(TeamEnum team)
    {
        Score.Instance.AddScore(team);
    }

    public void PlayerWinGame()
    {
        winLoseUI.WinLose(true);
    }

    public void PlayerLoseGame()
    {
        winLoseUI.WinLose(false);
    }

    public void EndMatch()
    {
        Score.Instance.ResetScore();
        
    }
}
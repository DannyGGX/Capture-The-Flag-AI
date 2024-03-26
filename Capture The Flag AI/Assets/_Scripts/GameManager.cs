using UnityEngine;

// Used for ending the round and ending the match
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }
    
    [SerializeField] private WinLoseUI winLoseUI;
    private bool _winLoseGame = false;

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
        _winLoseGame = false;
    }
    
    public void FlagCaptured(TeamEnum team)
    {
        Score.Instance.AddScore(team);
        if (_winLoseGame == false)
        {
            winLoseUI.RextRound(team);
            Invoke(nameof(NextRound), 2);
        }
    }

    private void NextRound()
    {
        SceneManagerScript.Instance.ReloadScene();
    }

    

    public void PlayerWinGame()
    {
        winLoseUI.WinLose(true);
        _winLoseGame = true;
    }

    public void PlayerLoseGame()
    {
        winLoseUI.WinLose(false);
        _winLoseGame = true;
    }

    public void EndMatch()
    {
        Score.Instance.ResetScore();
        
    }
}
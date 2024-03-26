using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private int maxScore = 5;
    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private TextMeshProUGUI opponentScoreText;
    [SerializeField] private ScoreSaveSO scoreSaveSO;
    private bool scoreAdded = false;

    public static Score Instance { get; private set; }
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

        scoreAdded = false;
        playerScoreText.text = $"Blue score: {scoreSaveSO.playerScore}";
        opponentScoreText.text = $"Red score: {scoreSaveSO.opponentScore}";
    }
    public void AddScore(TeamEnum team)
    {
        if (scoreAdded)
        {
            return;
        }
        if (team == TeamEnum.Blue)
        {
            scoreSaveSO.playerScore++;
            playerScoreText.text = $"Blue score: {scoreSaveSO.playerScore}";

            if (scoreSaveSO.playerScore == maxScore)
            {
                GameManager.Instance.PlayerWinGame();
            }
        }
        else
        {
            scoreSaveSO.opponentScore++;
            opponentScoreText.text = $"Red score: {scoreSaveSO.opponentScore}";
            if (scoreSaveSO.opponentScore == maxScore)
            {
                GameManager.Instance.PlayerLoseGame();
            }
        }

        scoreAdded = true;
    }
    
    public void ResetScore()
    {
        scoreSaveSO.ResetScore();
        playerScoreText.text = $"Blue score: {scoreSaveSO.playerScore}";
        opponentScoreText.text = $"Red score: {scoreSaveSO.opponentScore}";
    }
}

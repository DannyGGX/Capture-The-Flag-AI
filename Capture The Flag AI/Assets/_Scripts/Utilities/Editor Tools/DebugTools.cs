using System.Linq;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
public static class DebugTools
{
    [MenuItem("Debug/Reset Score")]
    public static void ResetScore()
    {
        if (Application.isPlaying)
        {
            Score.Instance.ResetScore();
        }
        else
        {
            var scoreSaveSO = Resources.FindObjectsOfTypeAll<ScoreSaveSO>().FirstOrDefault(s => s.name == "Score Save");
            scoreSaveSO.ResetScore();
        }
    }
    
    [MenuItem("Debug/Win Game")]
    public static void WinGame()
    {
        if (Application.isPlaying)
        {
            GameManager.Instance.PlayerWinGame();
        }
    }

    [MenuItem("Debug/Lose Game")]
    public static void LoseGame()
    {
        if (Application.isPlaying)
        {
            GameManager.Instance.PlayerLoseGame();
        }
    }
    
    [MenuItem("Debug/Speed Up")]
    public static void SpeedUp()
    {
        if (Application.isPlaying)
        {
            Time.timeScale = 3;
        }
    }
    
    [MenuItem("Debug/Speed Normal")]
    public static void SpeedNormal()
    {
        if (Application.isPlaying)
        {
            Time.timeScale = 1;
        }
    }
}

#endif
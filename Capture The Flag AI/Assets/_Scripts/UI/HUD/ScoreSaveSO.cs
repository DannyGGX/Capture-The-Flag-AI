using System;
using UnityEngine;


[CreateAssetMenu(fileName = "Score Save SO", menuName = "Scriptable Objects/ScoreSave")]
public class ScoreSaveSO : ScriptableObject
{
    public int playerScore;
    public int opponentScore;

    public void ResetScore()
    {
        playerScore = 0;
        opponentScore = 0;
    }
}
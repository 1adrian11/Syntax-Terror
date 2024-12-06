using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    private const string HighScoreKey = "HighScore"; // A PlayerPrefs kulcsa
    public static UnityEvent<int> OnNewHighScore = new UnityEvent<int>();

    // Betölti az aktuális legmagasabb pontszámot
    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt(HighScoreKey, 0); // Ha nincs mentett pont, alapérték: 0
    }

    // Elmenti az új pontszámot, ha az magasabb, mint az aktuális high score
    public static void SaveScore(int score)
    {
        int currentHighScore = GetHighScore();
        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, score);
            PlayerPrefs.Save(); // Az adatok mentése

            
            OnNewHighScore.Invoke(score);
            
        }
    }
}

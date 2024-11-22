using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameoverHighScore : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI highScoreText; // TMP szövegmező
    [SerializeField] private TMP_FontAsset electronicHighwayFont; // A betűtípus asset

    // void OnEnable()
    // {
    //     int highScore = HighScoreManager.GetHighScore(); // Legmagasabb pontszám lekérése
    //     highScoreText.text = $"Legmagasabb pontszám: {highScore}"; // Szöveg beállítása
    // }

    void OnEnable() {
    int highScore = HighScoreManager.GetHighScore();
    highScoreText.text = $"Highest Score: {highScore}";

        if (electronicHighwayFont != null)
        {
            highScoreText.font = electronicHighwayFont; // Betűtípus beállítása
        }
        highScoreText.alignment = TextAlignmentOptions.Center; // Középre igazítás

    
    HighScoreManager.OnNewHighScore.AddListener(UpdateHighScoreText);
}

    void OnDisable() {
        
        HighScoreManager.OnNewHighScore.RemoveListener(UpdateHighScoreText);
    }

    public void UpdateHighScoreText(int highScore) {
        highScoreText.text = $"Highest Score: {highScore}";
    }
}

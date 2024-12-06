using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro használata

public class HighScoreUIManager : MonoBehaviour
{
    public GameObject highScorePanel; // A panel, amit meg akarunk jeleníteni
    public TextMeshProUGUI highScoreText; // A TextMeshPro UI szöveg

    private void Start()
    {
        // Kezdetben elrejtjük az ablakot
        highScorePanel.SetActive(false);
    }

    // Az ablak megjelenítése és pontszám kiírása
    public void ShowHighScore()
    {
        int highScore = HighScoreManager.GetHighScore();
        highScoreText.text = $"Your highest score: {highScore}";
        highScorePanel.SetActive(true);
    }

    // Az ablak bezárása
    public void CloseHighScore()
    {
        highScorePanel.SetActive(false);
    }
}

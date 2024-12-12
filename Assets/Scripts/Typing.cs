//using System.Collections;
//using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Typing : MonoBehaviour
{
    public TMP_Text textComponent;
    public string text;
    public float typingSpeed = 0.1f;
    private int currentChar = 0;
    private float timer = 0f;
    private int charCount = 0;

    private void Start()
    {
        textComponent.text = "";
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > typingSpeed && currentChar < text.Length)
        {
            // Add the next character to the text component
            textComponent.text += text[currentChar];
            currentChar++;
            charCount++;
            timer = 0f;
        }

        // Check if 200 characters have been written
        if (charCount >= 1000)
        {
            // Wait for the next space character to reset
            if (currentChar < text.Length && text[currentChar] == ' ')
            {
                textComponent.text = "";
                charCount = 0;
            }
        }

        // Reload scene on Space key press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }
    }
}
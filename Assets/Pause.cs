using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject UI_Canvas;

    public static bool isPaused;

    // Update is called once per frame

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame(){
        PausePanel.SetActive(true);
        UI_Canvas.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame(){
        PausePanel.SetActive(false);
        UI_Canvas.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void RTM(){
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

    public void Quit(){
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
     public GameObject PausePanel;

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
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame(){
        PausePanel.SetActive(false);
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

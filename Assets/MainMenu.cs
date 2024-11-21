using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public void PlayGame(){
        SceneManager.LoadSceneAsync(2);
    }

    public void Options(){
        
    }

    public void QuitGame(){
        Application.Quit();
    }
}

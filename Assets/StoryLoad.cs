using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryLoad : MonoBehaviour
{
    public void LoadStory(){
        SceneManager.LoadScene("Intro");
    }
}

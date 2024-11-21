using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Typing : MonoBehaviour
{
    public TMP_Text textComponent;
    public string text;
    public float typingSpeed = 0.1f;
    int currentChar = 0;
    float timer = 0f;

    private void Start(){
        textComponent.text = "";
    }

    private void Update(){
        timer += Time.deltaTime;
        if(timer > typingSpeed){
            textComponent.text += text[currentChar];
            currentChar++;
            timer = 0f;
        } 

        if(Input.GetKeyDown(KeyCode.Space)){
            SceneManager.LoadScene(1);
        }
    }
}

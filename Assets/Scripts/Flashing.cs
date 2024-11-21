using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveGameMessage : MonoBehaviour
{
    public float speed;
    public TMP_Text text;

    private Color textColor;

    // Start is called before the first frame update
    void Start()
    {
        textColor = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        textColor.a = (Mathf.Sin(Time.time * speed) + 1.0f) / 2.0f;
        text.color = textColor;         
    }
}
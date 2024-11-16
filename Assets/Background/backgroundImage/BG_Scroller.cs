using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BG_Scroller : MonoBehaviour
{
    [SerializeField] private RawImage Picture;
    [SerializeField] private float xx, yy; //merre menjen a bg

    void Update()
    {
        Picture.uvRect = new Rect(Picture.uvRect.position + new Vector2(xx,yy) * Time.deltaTime, Picture.uvRect.size);
    }
}

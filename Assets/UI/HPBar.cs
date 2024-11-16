using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [field: SerializeField]
    public Image HpImage {get; private set;}

    [field: SerializeField]
    public RectTransform Bar {get; private set;}

    
    public void Start(){
        HpImage.gameObject.SetActive(false);
    }

    public void GiveLives(int db){
        foreach (Transform hp in Bar)
        {
            Destroy(hp.gameObject);
        }
        //Debug.Log($"{db} hp maradt");
        for (int i = 0; i < db; i++)
        {
            Image HP = Instantiate<Image>(HpImage, Bar);
            HP.gameObject.SetActive(true);
        }
    }
}

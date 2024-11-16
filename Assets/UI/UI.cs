using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.Unity.VisualStudio.Editor;


[RequireComponent(typeof(ShipSpawner))]
public class UI : MonoBehaviour
{
    [field: SerializeField]
    private TextMeshProUGUI points {get; set;}

    /*private ShipSpawner Controller;

    public void Start(){
        Controller = GetComponent<ShipSpawner>();
    }*/

    public void ScoreRefresh(int pont){
        points.text = pont.ToString().PadLeft(13, '0');
    }
}

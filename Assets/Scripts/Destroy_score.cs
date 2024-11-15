using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Runtime.InteropServices;

/*[System.Serializable]
public class Destroyer : UnityEvent<ShootControll>{}*/
public class Destroy_score : MonoBehaviour
{ 
    [field: SerializeField]
    public ShipSpawner Controller {get; set;}

    [field: SerializeField]
    public int Score {get; private set;}
    
    [field: SerializeField]
    private UnityEvent<ShootControll, Destroy_score> Triggered {get; set;} //találatkor mi történjen
    
    
    private void OnTriggerEnter2D(Collider2D a) {    // lövedékkel ütközés
        ShootControll Bullet = a.GetComponent<ShootControll>();
        if(Bullet != null) {  //ha ez egy bullet 
            if(Triggered.GetPersistentEventCount() == 0
            
            ) { // self destroy
                ObjectRemove(Bullet);
            }
            else {
                Triggered.Invoke(Bullet, this);
            }
        }
    }
    
    /*
    private void OnTriggerEnter2D(Collider2D a) {    // lövedékkel ütközés
        ShootControll Bullet = a.GetComponent<ShootControll>();
        if(Bullet != null) {  //ha ez egy bullet 
            ObjectRemove(Bullet);
        }
    }*/
    
    /*public void ObjectRemove (ShootControll bululet){ //objekt remove
        ShipSpawner.ScoreManager(Score);
        Destroy(bululet.gameObject);
        Destroy(this.gameObject);
    }*/
    public void ObjectRemove(ShootControll bululet)
    {
        if (Controller != null)
        {
            Controller.ScoreManager(Score); // Itt használjuk a Controller példányt
        }
        Destroy(bululet.gameObject);
        Destroy(this.gameObject);
    }
}

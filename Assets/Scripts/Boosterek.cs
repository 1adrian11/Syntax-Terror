using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boosterek : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player_Movement p = other.GetComponent<Player_Movement>();
        if(p == null) {
            return;
        }
        IfCollect(p);
    }
    
    public virtual void IfCollect (Player_Movement pl){
        Destroy(this.gameObject);
    }
}

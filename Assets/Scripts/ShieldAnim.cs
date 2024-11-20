using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ShieldAnim : MonoBehaviour
{
    
    public void Visible (Player_Movement pl){
        GetComponent<SpriteRenderer>().enabled = pl.Shieldpow > 0;
    }
}

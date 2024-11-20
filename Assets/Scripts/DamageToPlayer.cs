using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToPlayer : MonoBehaviour
{
    public virtual void Hit(Player_Movement pla){
        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDammageToPlayer : MonoBehaviour
{
    public BossMovement TakeDamage;

    public virtual void Hit(Player_Movement pla){
        if(TakeDamage.BossHp > 0) {
            TakeDamage.BossHp--;
        } else {
            Destroy(this.gameObject);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Boosterek
{

    public override void IfCollect (Player_Movement pl){
        pl.Shieldpow ++;
        base.IfCollect(pl);
    }
}

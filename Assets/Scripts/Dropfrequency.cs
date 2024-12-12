
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropfrequency : MonoBehaviour
{

    [field: SerializeField]
    public List<Boosterek> Boosters {get; private set;}

    [field: SerializeField]
    public float DropChanse {get; private set;} //hány % eséllyel droppol 1=100%

    public void Drop(Destroy_score Destroyable){
        float Hz = Random.Range(0, 100);
        if(Hz > DropChanse) {
            return;
        }
        int i = Random.Range(0,Boosters.Count);
        Boosterek Boost = Boosters[i];
        Instantiate(Boost, Destroyable.transform.position, Quaternion.identity); //hova droppolja
    }

}

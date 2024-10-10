using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [field: SerializeField]
    public Player_Movement Player {get; private set;}  //hajo pusztulasakor amit spawnol
    [field: SerializeField]
    public Transform Spawnpoint {get; private set;}
    [field: SerializeField]
    public float SpawnDelay {get; private set;}   //mennyi ido a respawnhoz
    [field: SerializeField]
    public float WhereSpawn {get; private set;} = -1;
    void Start()
    {
        Spawnplayer();
    }

    void Update()
    {
        if(WhereSpawn > 0 && Time.time > WhereSpawn) {
            Spawnplayer();
        }
    }

    public void DestroyMark (Player_Movement NeedDestroy){
        Destroy(NeedDestroy.gameObject);
        WhereSpawn = Time.time + SpawnDelay;
    }

    private void Spawnplayer (){
        Player_Movement pl = Player_Movement.Spawn(Player, this);
        pl.transform.position = Spawnpoint.position;
        WhereSpawn = -1;
    }
}

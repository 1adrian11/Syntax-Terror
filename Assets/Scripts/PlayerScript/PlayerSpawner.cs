using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{

    public Player_Movement Player;  //hajo pusztulasakor amit spawnol
    public Transform Spawnpoint;
    public float SpawnDelay;  //mennyi ido a respawnhoz
    public float WhereSpawn = -1;
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
        Player_Movement pl = Instantiate(Player);
        pl.PlayerSpawner = this;
        pl.transform.position = Spawnpoint.position;
        WhereSpawn = -1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawner : MonoBehaviour
{
    [field: SerializeField]
    public Player_Movement Player {get; private set;}  //hajo pusztulasakor amit spawnol
    /*
    ehelyett mehet     
    public List<Player_Movement> PlayerShips {get; private set;} = new ();
    hogy több ship közül lehessen választani
    */
    [field: SerializeField]
    public Enemy_Spawner EnemySpawner {get; private set;}

    [field: SerializeField]
    public Transform E_SpawnPoint {get; private set;}

    [field: SerializeField]
    public List<Transform> Borders {get; private set;}

    [field: SerializeField]
    public List<Enemy_Movement_1> PossibleEnemies {get; private set;}

    [field: SerializeField]
    public Transform Spawnpoint {get; private set;}
    [field: SerializeField]
    public float SpawnDelay {get; private set;}   //mennyi ido a respawnhoz
    [field: SerializeField]
    public float WhereSpawn {get; private set;} = -1;
    void Start()
    {
        Spawnplayer();
        List<Transform> borders = new() {Borders[0],Borders[1],Borders[2]};

        Enemy_Interface ship = new E_ship(PossibleEnemies[0], borders, E_SpawnPoint.position);

        List<Enemy_Interface> enemies = new () {ship, ship, ship};

        EnemySpawner.NextEnemyInRow(enemies);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public Transform E_SpawnPoint1 {get; private set;}

    [field: SerializeField]
    public Transform E_SpawnPoint2 {get; private set;}

    [field: SerializeField]
    public Transform E_SpawnPoint3 {get; private set;}

    [field: SerializeField]
    public List<Transform> Borders {get; private set;}

    [field: SerializeField]
    public List<Enemy_Movement_1> PossibleEnemies {get; private set;}

    [field: SerializeField]
    public Transform Spawnpoint {get; private set;}

    [field: SerializeField]
    public float PlayerSpawnDelay {get; private set;}   //mennyi ido a respawnhoz

    [field: SerializeField]
    public float WhenSpawn {get; private set;} = -1;

    //[field: SerializeField] //static, mert Destroy_score nem éri el anélkül
    public /*static*/ int Points {
        get => Pontok;
        private set{
            Pontok = value;
            scorechanger.Invoke(Pontok);
        }
    } //highscore számláló

    private static int Pontok = 0;
    //public UnityEngine.Events.UnityEvent<int> scorechanger;
    public /*static*/ UnityEngine.Events.UnityEvent<int> scorechanger = new UnityEngine.Events.UnityEvent<int>();

    void Start()
    {
        Spawnplayer();
        List<Transform> borders = new() {Borders[0],Borders[1],Borders[2]};

        Enemy_Interface ship = new E_ship(PossibleEnemies[0], borders, E_SpawnPoint1.position);

        List<Enemy_Interface> enemies = new () {ship, ship, ship};

        EnemySpawner.NextEnemyInRow(enemies);
    }

    void Update()
    {
        if(WhenSpawn > 0 && Time.time > WhenSpawn) {
            Spawnplayer();
        }
        Pontok = Points;
    }

    public /*static*/ void ScoreManager (int allscore){  //static, mert Destroy_score nem éri el anélkül
        Points += allscore; //adja mindig hozzá a megszerzett pontot
    }

    public void DestroyMark (Player_Movement NeedDestroy){
        Destroy(NeedDestroy.gameObject);
        WhenSpawn = Time.time + PlayerSpawnDelay; // az eltelt időtől számolva hány másodpercel később spawnol
    }

    private void Spawnplayer (){
        Player_Movement pl = Player_Movement.Spawn(Player, this);
        pl.transform.position = Spawnpoint.position;
        WhenSpawn = -1;  // ne spawnoljon állandóan 
    }
}
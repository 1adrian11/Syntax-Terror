using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShipSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> playerPrefabs;
    private string ShipNumber;
    private int HPLeft = 3;

    public int BasicHPLeft
    {
        get => HPLeft;
        private set
        {
            HPLeft = value;
            HpChange.Invoke(HPLeft);
        }
    }

    [field: SerializeField]
    public Player_Movement Player { get; private set; }

 
    /* ehelyett mehet 
    public List<Player_Movement> PlayerShips {get; private set;} = new ();
    hogy több ship közül lehessen választani
    */

    [SerializeField] private Canvas GameOverCanvas;

    [field: SerializeField]
    public Enemy_Spawner EnemySpawner { get; private set; }

    [field: SerializeField]
    public Transform E_SpawnPoint1 { get; private set; }

    [field: SerializeField]
    public Transform E_SpawnPoint2 { get; private set; }

    [field: SerializeField]
    public Transform E_SpawnPoint3 { get; private set; }

    [field: SerializeField]
    public List<Transform> Borders { get; private set; }

    [field: SerializeField]
    public List<Enemy_Movement_1> PossibleEnemies { get; private set; }

    [field: SerializeField]
    public Transform Spawnpoint { get; private set; }

    [field: SerializeField]
    public float PlayerSpawnDelay { get; private set; }

    [field: SerializeField]
    public float WhenSpawn { get; private set; } = -1;

    public int Points
    {
        get => Pontok;
        private set
        {
            Pontok = value;
            scorechanger.Invoke(Pontok);
        }
    }

    private static int Pontok = 0;
    public UnityEngine.Events.UnityEvent<int> HpChange;
    public UnityEngine.Events.UnityEvent<int> scorechanger = new UnityEngine.Events.UnityEvent<int>();

    void Start()
    {
        GameOverCanvas.gameObject.SetActive(false);
        ShipNumber = ButtonHighlighter.Instance?.GetSelectedButtonName() ?? "1"; // Alapértelmezett hajó beállítása
        Spawnplayer();
        List<Transform> borders = new() { Borders[0], Borders[1], Borders[2] };

        Enemy_Interface ship = new E_ship(PossibleEnemies[0], borders, E_SpawnPoint1.position);

        List<Enemy_Interface> enemies = new() { ship, ship, ship };

        EnemySpawner.NextEnemyInRow(enemies);
    }

    void Update()
    {
        if (WhenSpawn > 0 && Time.time > WhenSpawn)
        {
            Spawnplayer();
        }
        Pontok = Points;
    }

    public void ScoreManager(int allscore)
    {
        Points += allscore;
        scorechanger.Invoke(Points);
    }

    public void DestroyMark(Player_Movement NeedDestroy)
    {
        Destroy(NeedDestroy.gameObject);
        WhenSpawn = Time.time + PlayerSpawnDelay;
        BasicHPLeft--;
    }

    private void Spawnplayer()
    {
        if (BasicHPLeft > 0)
        {
            Player_Movement pl = Player_Movement.Spawn(Player, this);
            pl.transform.position = Spawnpoint.position;
            WhenSpawn = -1;
        }
        else
        {
            GameOverCanvas.gameObject.SetActive(true);
            HighScoreManager.SaveScore(Points);
        }
    }

     /*void UpdatePlayer()
        {
            switch (ShipNumber)
            {
             case "1":
                    Player = Instantiate(playerPrefabs[0]).GetComponent<Player_Movement>();
                    break;
                case "2":
                    Player = Instantiate(playerPrefabs[1]).GetComponent<Player_Movement>();
                    break;
                case "3":
                    Player = Instantiate(playerPrefabs[2]).GetComponent<Player_Movement>();
                    break;
                case "4":
                    Player = Instantiate(playerPrefabs[3]).GetComponent<Player_Movement>();
                    break;
                case "5":
                    Player = Instantiate(playerPrefabs[4]).GetComponent<Player_Movement>();
                    break;
                case "6":
                    Player = Instantiate(playerPrefabs[5]).GetComponent<Player_Movement>();
                    break;
                case "7":
                    Player = Instantiate(playerPrefabs[6]).GetComponent<Player_Movement>();
                    break;
                case "8":
                    Player = Instantiate(playerPrefabs[7]).GetComponent<Player_Movement>();
                    break;
                case "9":
                    Player = Instantiate(playerPrefabs[8]).GetComponent<Player_Movement>();
                    break;
                case "10":
                    Player = Instantiate(playerPrefabs[9]).GetComponent<Player_Movement>();
                    break;
                case "11":
                    Player = Instantiate(playerPrefabs[10]).GetComponent<Player_Movement>();
                    break;
                case "12":
                    Player = Instantiate(playerPrefabs[11]).GetComponent<Player_Movement>();
                    break;
                default:
                    Debug.LogError("Invalid ship number selected!");
                    break;
            }
        }*/
}
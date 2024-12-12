using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShipSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> playerPrefabs;
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
    public Player_Movement Player { get; set; }

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
    public Transform Spawnpoint { get; set; }

    [field: SerializeField]
    public float PlayerSpawnDelay { get; set; }

    [field: SerializeField]
    public float WhenSpawn { get; private set; } = -1;

    [field: SerializeField]
    public float EnemySpawnInterval { get; set; } /*= 5f;*/ // időköz az ellenségek között

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

    private Coroutine enemySpawnCoroutine;

    void Start()
    {
        GameOverCanvas.gameObject.SetActive(false);
        Spawnplayer();
        StartEnemySpawning();

        List<Transform> borders = new() { Borders[0], Borders[1], Borders[2], Borders[3], Borders[4], Borders[5]};
        Enemy_Interface ship = new E_ship(PossibleEnemies[0], borders, E_SpawnPoint1.position);
        List<Enemy_Interface> enemies = new() { ship, ship, ship };

        EnemySpawner.NextEnemyInRow(enemies); // Az eredeti hármas generálás megtartása
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

    public void Spawnplayer()
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
            StopEnemySpawning();
        }
    }

    private void StartEnemySpawning()
    {
        enemySpawnCoroutine = StartCoroutine(SpawnEnemies());
    }

    private void StopEnemySpawning()
    {
        if (enemySpawnCoroutine != null)
        {
            StopCoroutine(enemySpawnCoroutine);
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(EnemySpawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        // Random válasszon egy ellenséget és egy spawn pontot
        Enemy_Movement_1 enemy = PossibleEnemies[Random.Range(0, PossibleEnemies.Count)];
        Transform spawnPoint = GetRandomSpawnPoint();

        Enemy_Interface newEnemy = new E_ship(enemy, Borders, spawnPoint.position);
        EnemySpawner.NextEnemyInRow(new List<Enemy_Interface> { newEnemy });
    }

    private Transform GetRandomSpawnPoint()
    {
        Transform[] spawnPoints = { E_SpawnPoint1, E_SpawnPoint2, E_SpawnPoint3 };
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{

    [field: SerializeField]
    public float SpawnSpeed {get; private set;}
    [field: SerializeField]
    public float Last {get; private set;} = 0;
    [field: SerializeField]
    public List<Enemy_Interface> Enemies {get; private set;} = new ();
    [field: SerializeField]
    public ShipSpawner Controll {get;  set;}


    void Start()
    {
        this.InvokeRepeating(nameof(NextSpawn), SpawnSpeed, SpawnSpeed);
    }

    private void NextSpawn(){
        if(Enemies.Count > 0) {
            Enemy_Interface ship = Enemies[0];
            Enemies.RemoveAt(0);      
            Enemy_Movement_1.SpawnEnemy(ship, Controll);
        }
    }

    public void NextEnemyInRow(List<Enemy_Interface> ships){
        foreach (Enemy_Interface en in ships){
            Enemies.Add(en);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_ship : Enemy_Interface
{
    public Enemy_Movement_1 Prefab {get;}
    public List<Transform> Borders {get;}
    public Vector2 SpawnPoint {get;}

    public E_ship(Enemy_Movement_1 prefab, List<Transform> borders, Vector2 spawnpoint){
        Prefab = prefab;
        Borders = borders;
        SpawnPoint = spawnpoint;
    }

}
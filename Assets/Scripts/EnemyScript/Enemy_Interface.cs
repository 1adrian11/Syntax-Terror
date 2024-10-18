using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Enemy_Interface
{
    public Enemy_Movement_1 Prefab {get;}
    public List<Transform> Borders {get;}
    public Vector2 SpawnPoint {get;}
}
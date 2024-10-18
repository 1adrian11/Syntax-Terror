using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    [field: SerializeField]
    public Vector2 speed {get; private set;}

    void Update()
    {
        BulletMove();
    }
    private void BulletMove(){
        float x_coord, y_coord;
        x_coord = transform.position.x + (speed.x * Time.deltaTime);
        y_coord = transform.position.y + (speed.y * Time.deltaTime);
        transform.position = new Vector2(x_coord, y_coord);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootControll : MonoBehaviour
{
    [field: SerializeField]
    private float SpeedMagnitude = 10f;  // Maximális sebesség

    private Vector2 speed;

    void Update()
    {
        BulletMove();
    }
    
    private void OnTriggerEnter2D(Collider2D other) {    // Lövedékkel ütközés
        ShootControll Bullet = other.GetComponent<ShootControll>();
        Debug.Log($"{this}.OnTriggerEnter2D({other})");
    }

    private void BulletMove(){ 
        float x_coord, y_coord;
        x_coord = transform.position.x + (speed.x * Time.deltaTime);
        y_coord = transform.position.y + (speed.y * Time.deltaTime);
        transform.position = new Vector2(x_coord, y_coord);
    }

    public void SetSpeed(Vector2 newSpeed){
        speed = newSpeed;
    }

    public float GetSpeedMagnitude(){
        return SpeedMagnitude;
    }
}
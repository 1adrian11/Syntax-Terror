using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootControll : MonoBehaviour
{
    [field: SerializeField]
    private float speedMagnitude = 10f;  // Maximális sebesség nagysága, amit az irány alapján számolunk

    private Vector2 speed;

    void Update()
    {
        LaserMove();
    }
    
    private void OnTriggerEnter2D(Collider2D other) {    // Lövedékkel ütközés
        ShootControll Bullet = other.GetComponent<ShootControll>();
        Debug.Log($"{this}.OnTriggerEnter2D({other})");
    }

    private void LaserMove(){
        float new_x = transform.position.x + (speed.x * Time.deltaTime);
        float new_y = transform.position.y + (speed.y * Time.deltaTime);
        transform.position = new Vector2(new_x, new_y);
    }

    public void SetSpeed(Vector2 newSpeed){
        speed = newSpeed;
    }

    public float GetSpeedMagnitude(){
        return speedMagnitude;
    }
}
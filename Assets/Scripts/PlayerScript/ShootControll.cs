using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootControll : MonoBehaviour
{
    [field: SerializeField]
    public Vector2 speed {get; private set;}


    void Update()
    {
        LaserMove ();
    }
    
    private void OnTriggerEnter2D(Collider2D other) {    //lovedekkel utkozes
        ShootControll Bullet = other.GetComponent<ShootControll>();
        Debug.Log($"{this}.OnTriggerEnter2D({other}");
    }

    private void LaserMove (){
        float new_x = transform.position.x + (speed.x*Time.deltaTime);
        float new_y = transform.position.y + (speed.y*Time.deltaTime);
        transform.position = new Vector2(new_x, new_y);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

[RequireComponent(typeof(Destroy_score))]
public class Enemy_Movement_1 : MonoBehaviour
{
    public Vector2 optimize; //smooth movehoz
    
    // ÁLLANDÓ SPEED MOVEMENT
    public Vector2 Velocity {
        get {
            Vector2 ship = MovementBorder[BetweenBorders].position;
            optimize = ship - (Vector2)this.transform.position;
            optimize.Normalize();
            return optimize;
        }
    }

    // FOLYAMATOSAN LASSULO MOVEMENT:
    /*public Vector2 Velocity {
        get {
            Vector2 ship = MovementBorder[BetweenBorders].position;
            return ship - (Vector2)this.transform.position;
        }
    }*/
    [field: SerializeField]
    public float ShootFrequency {get; private set;} //hány másodpercenként lő

    [field: SerializeField]
    public float IfFire {get; private set;} = 0;

    [field: SerializeField]
    public List<Transform> MovementBorder {get; private set;} //ami között mozoghat az enemy hajo

    [field: SerializeField]
    public float Speed {get; private set;}

    [field: SerializeField]
    public int BetweenBorders {get; private set;} = 0;

    [field: SerializeField]
    public GameObject Bullet {get; private set;}

    [field: SerializeField]
    public Transform BulletSpawn {get; private set;}

    public static Enemy_Movement_1 SpawnEnemy(Enemy_Interface ship, ShipSpawner Controll){
        Enemy_Movement_1 NextEnemy = Instantiate(ship.Prefab);
        //NextEnemy.GetComponent<Destroy_score>().Controller = Controll;
        NextEnemy.MovementBorder = ship.Borders;
        NextEnemy.transform.position = ship.SpawnPoint;
        /*NextEnemy.MovementBorder = ship.Borders;
        NextEnemy.transform.position = ship.SpawnPoint;*/
        return NextEnemy;
    }


    void Start(){
        IfFire = Time.time;
    }

    void Update(){
        Fire();
        Move();
        BorderAssist();
    }

    private void BorderAssist(){
        Vector2 enemy = MovementBorder[BetweenBorders].position;  //hova probal menni
        float tav = Vector2.Distance(enemy,transform.position);
        //ha ott van menjen a masikhoz
        if(tav < 0.5) {
            BetweenBorders = (BetweenBorders + 1) % MovementBorder.Count; //0 es 1 között (2 pont) megy folyamatosan
        }
    }

    private void Move(){
        // Mozgatás
        float new_x = transform.position.x + (Velocity.x * Speed * Time.deltaTime);
        float new_y = transform.position.y + (Velocity.y * Speed * Time.deltaTime);
        transform.position = new Vector2(new_x, new_y);
    }

    private void Fire(){
        if(Time.time > (IfFire + ShootFrequency)) {
            GameObject bull = Instantiate(Bullet);
            bull.transform.position = BulletSpawn.position;  // a hajo elott jojjon letre a bullet
            IfFire = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {    // lövedékkel ütközés
        ShootControll Bullet = other.GetComponent<ShootControll>();
        if(Bullet != null) {
            IfHit(Bullet);
        }
    }

    private void IfHit(ShootControll bullet) {
        Destroy(this.gameObject);
        Destroy(bullet.gameObject);
    }
}
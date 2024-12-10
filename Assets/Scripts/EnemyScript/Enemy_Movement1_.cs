using System.Collections;
using System.Collections.Generic;
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

    [field: SerializeField]
    public float ShootFrequency {get; private set;} //hány másodpercenként lő

    [field: SerializeField]
    public float IfFire {get; private set;} = 0;

    [field: SerializeField]
    public List<Transform> MovementBorder {get; set;} //ami között mozoghat az enemy hajo

    [field: SerializeField]
    public float Speed {get; set;}

    [field: SerializeField]
    public int BetweenBorders {get; set;} = 0;

    [field: SerializeField]
    public GameObject Bullet {get; private set;}

    [field: SerializeField]
    public Transform BulletSpawn {get; private set;}

    private bool isWaiting = false; // Állapot a várakozáshoz
    private float waitTimer = 0f; // Időzítő a várakozáshoz
    private float waitDuration = 0f; // Várakozási idő

    public static Enemy_Movement_1 SpawnEnemy(Enemy_Interface ship, ShipSpawner Controller){
        Enemy_Movement_1 NextEnemy = Instantiate(ship.Prefab);
        NextEnemy.GetComponent<Destroy_score>().Controller = Controller;
        NextEnemy.MovementBorder = ship.Borders;
        NextEnemy.transform.position = ship.SpawnPoint;
        
        // Véletlenszerű border választás
        NextEnemy.BetweenBorders = Random.Range(0, ship.Borders.Count); // Véletlen border választása
        return NextEnemy;
    }

    void Start(){
        IfFire = Time.time;
        SetRandomWaitTime(); // Állítsuk be az első várakozási időt
    }

    void Update(){
        Fire();
        Move();
        BorderAssist();
    }

    private void BorderAssist(){
        Vector2 enemy = MovementBorder[BetweenBorders].position;  //hova próbál menni
        float tav = Vector2.Distance(enemy,transform.position);
        
        if (tav < 0.5f && !isWaiting) {
            // Ha elérte a border-t, és nem az első border
            if (BetweenBorders > 0) {
                isWaiting = true;
                waitTimer = Time.time; // Kezdjük a várakozást
                SetRandomWaitTime(); // Állítsuk be a következő várakozási időt
            } else {
                // Ha az első bordernél van, folytassa a mozgást
                BetweenBorders = (BetweenBorders + 1) % MovementBorder.Count;
            }
        }

        // Ha várakozik, ellenőrizzük, hogy letelt-e a várakozási idő
        if (isWaiting && Time.time >= waitTimer + waitDuration) {
            // Várakozás után haladjon a következő borderhez
            BetweenBorders = Random.Range(0, MovementBorder.Count); // Véletlenszerű border választás
            isWaiting = false; // Leállítjuk a várakozást
        }
    }

    public void SetRandomWaitTime(){
        // Véletlenszerű várakozási idő 2 és 5 másodperc között
        waitDuration = Random.Range(2f, 5f);
    }

    private void Move(){
        // Mozgatás
        if (!isWaiting) {
            float new_x = transform.position.x + (Velocity.x * Speed * Time.deltaTime);
            float new_y = transform.position.y + (Velocity.y * Speed * Time.deltaTime);
            transform.position = new Vector2(new_x, new_y);
        }
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
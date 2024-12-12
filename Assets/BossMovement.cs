using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [field: SerializeField]
    public Transform BulletSpawn1 {get; private set;}
    [field: SerializeField]
    public Transform BulletSpawn2 {get; private set;}
    [field: SerializeField]
    public GameObject Bullet {get; private set;}
    [field: SerializeField]
    public float ShootFrequency {get; private set;} //hány másodpercenként lő
    [field: SerializeField]
    public float IfFire {get; private set;} = 0;
    public Transform midPoint; // Az első célpont
    public Transform leftPoint; // A bal oldali célpont
    public Transform rightPoint; // A jobb oldali célpont
    public float speedToMidPoint = 2f; // Sebesség az első célponthoz
    public float speedBetweenPoints = 1f; // Sebesség a bal és jobb pont között
    public float delayBeforeStart = 2f; // Késleltetés az első mozgás előtt (másodpercben)

    private Transform currentTarget; // Az aktuális célpont
    private bool movingToMidPoint = true; // Az objektum az első célponthoz mozog
    private float delayTimer; // Késleltetési időzítő
    public int BossHp;

    void Start()
    {
        transform.position = new Vector2(0, 8); // Kezdő pozíció beállítása
        currentTarget = midPoint; // Kezdésként az első célpont
        delayTimer = delayBeforeStart; // Késleltetési időzítő beállítása
    }

    void Update()
    {
        if (delayTimer > 0)
        {
            delayTimer -= Time.deltaTime; // Késleltetési időzítő csökkentése
        }
        else
        {
            MoveObject();
            Fire();
        }
    }

    void MoveObject()
    {
        if (movingToMidPoint)
        {
            MoveTowardsTarget(midPoint, speedToMidPoint);

            if (Vector2.Distance(transform.position, midPoint.position) < 0.1f)
            {
                movingToMidPoint = false;
                currentTarget = leftPoint;
            }
        }
        else
        {
            MoveTowardsTarget(currentTarget, speedBetweenPoints);

            if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
            {
                currentTarget = currentTarget == leftPoint ? rightPoint : leftPoint;
            }
        }
    }

    void MoveTowardsTarget(Transform target, float speed)
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
    /*private void Fire(){
        if(Time.time > (IfFire + ShootFrequency)) {
            GameObject bull = Instantiate(Bullet);
            bull.transform.position = BulletSpawn.position;  // a hajo elott jojjon letre a bullet
            IfFire = Time.time;
        }
    }*/
    private void Fire()
    {
        if (Time.time > (IfFire + ShootFrequency))
        {
            GameObject bull1 = Instantiate(Bullet);
            bull1.transform.position = BulletSpawn1.position; // Az első lövedék pozíciója

            GameObject bull2 = Instantiate(Bullet);
            bull2.transform.position = BulletSpawn2.position; // A második lövedék pozíciója

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
        if(BossHp > 0) {
            BossHp--;
        } else {
            Destroy(this.gameObject);
        }
        Destroy(bullet.gameObject);
    }
}